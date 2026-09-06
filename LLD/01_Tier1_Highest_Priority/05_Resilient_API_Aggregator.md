# 05. Resilient API Aggregator (External Integrations)

## 📌 Context
Senior engineers are constantly building "BFFs" (Backend For Frontend) or orchestrators that call multiple external microservices. This question tests your ability to handle partial failures, timeout management, `Task.WhenAll`, and resilience patterns using Polly.

---

## 1. The Scenario
You need to build a `GetDashboardSummaryAsync(userId)` endpoint that fetches data from three separate microservices:
1. `UserService` (Fast, highly reliable)
2. `OrderService` (Medium speed)
3. `RecommendationService` (Slow, frequently times out)

**Goal:** Return a response within 1.5 seconds maximum. If `RecommendationService` fails or times out, return the data you *do* have, rather than failing the whole request.

---

## 2. Setting up HttpClientFactory and Polly (`Program.cs`)
Never manually instantiate `new HttpClient()`. Use the factory to manage connection pooling, and attach Polly policies.

```csharp
// Define a resilient policy: 3 retries, exponential backoff, plus a circuit breaker
var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

var circuitBreakerPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

// Register clients
builder.Services.AddHttpClient("UserService", c => c.BaseAddress = new Uri("https://user-api/"))
    .AddPolicyHandler(retryPolicy);

builder.Services.AddHttpClient("OrderService", c => c.BaseAddress = new Uri("https://order-api/"))
    .AddPolicyHandler(retryPolicy)
    .AddPolicyHandler(circuitBreakerPolicy);

// Recommendation Service has a strict timeout policy!
builder.Services.AddHttpClient("RecommendationService", c => 
    {
        c.BaseAddress = new Uri("https://rec-api/");
        c.Timeout = TimeSpan.FromMilliseconds(1000); // 1 second absolute timeout
    })
    .AddPolicyHandler(retryPolicy);
```

---

## 3. The Aggregation Service
Run tasks concurrently. Handle individual exceptions gracefully.

```csharp
public class DashboardAggregatorService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<DashboardAggregatorService> _logger;

    public DashboardAggregatorService(IHttpClientFactory httpClientFactory, ILogger<DashboardAggregatorService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync(string userId, CancellationToken ct)
    {
        // 1. Create global timeout cancellation token (1.5s max total)
        using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        timeoutCts.CancelAfter(TimeSpan.FromMilliseconds(1500));

        // 2. Start all tasks CONCURRENTLY
        var userTask = FetchUserAsync(userId, timeoutCts.Token);
        var ordersTask = FetchOrdersAsync(userId, timeoutCts.Token);
        var recsTask = FetchRecommendationsAsync(userId, timeoutCts.Token);

        // 3. Await all without throwing immediately on the first failure
        var allTasks = new Task[] { userTask, ordersTask, recsTask };
        try
        {
            await Task.WhenAll(allTasks);
        }
        catch (Exception ex)
        {
            // Task.WhenAll only throws the *first* exception. We log and ignore it 
            // because we will inspect the individual tasks manually next.
            _logger.LogWarning("One or more dashboard tasks failed: {Message}", ex.Message);
        }

        // 4. Safely extract results (checking for IsCompletedSuccessfully)
        return new DashboardSummaryDto
        {
            User = userTask.IsCompletedSuccessfully ? userTask.Result : null,
            RecentOrders = ordersTask.IsCompletedSuccessfully ? ordersTask.Result : new List<OrderDto>(),
            Recommendations = recsTask.IsCompletedSuccessfully ? recsTask.Result : new List<RecommendationDto>()
        };
    }

    private async Task<UserDto> FetchUserAsync(string userId, CancellationToken ct)
    {
        var client = _httpClientFactory.CreateClient("UserService");
        return await client.GetFromJsonAsync<UserDto>($"/users/{userId}", ct);
    }

    private async Task<List<OrderDto>> FetchOrdersAsync(string userId, CancellationToken ct)
    {
        var client = _httpClientFactory.CreateClient("OrderService");
        return await client.GetFromJsonAsync<List<OrderDto>>($"/orders?userId={userId}", ct);
    }

    private async Task<List<RecommendationDto>> FetchRecommendationsAsync(string userId, CancellationToken ct)
    {
        var client = _httpClientFactory.CreateClient("RecommendationService");
        return await client.GetFromJsonAsync<List<RecommendationDto>>($"/recommendations?userId={userId}", ct);
    }
}
```
---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"Why use `Task.WhenAll` instead of `Task.WaitAll`?"*
**You:** "`Task.WaitAll` blocks the calling thread synchronously until all tasks finish, which leads to thread-pool starvation in ASP.NET Core under high load. `Task.WhenAll` is asynchronous and returns a Task that we can `await`, freeing up the worker thread to serve other HTTP requests while we wait for the external APIs."

**Interviewer:** *"Why use a Circuit Breaker for the Order Service?"*
**You:** "If the Order Service goes completely down, standard retries will just hammer it with traffic, making it harder for it to recover, and wasting our own system's resources waiting for timeouts. A Circuit Breaker detects the failure threshold (e.g., 5 consecutive errors) and 'opens' the circuit, immediately failing fast for the next 30 seconds without even trying the network call, giving the Order Service time to recover."

**Interviewer:** *"Why do we need `IHttpClientFactory`? Why not just `new HttpClient()`?"*
**You:** "Instantiating `new HttpClient()` per request causes **Socket Exhaustion** because the OS doesn't release sockets immediately after disposal (TIME_WAIT state). Conversely, using a single static `HttpClient` forever ignores DNS changes. `IHttpClientFactory` solves both by pooling and recycling the underlying `HttpMessageHandler` objects every 2 minutes."
