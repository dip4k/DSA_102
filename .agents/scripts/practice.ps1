[CmdletBinding()]
param (
    [ValidateSet("dsa", "lld", "all")]
    [string]$Type = "all",

    [ValidateSet("test", "status", "list")]
    [string]$Action = "test",

    [ValidateSet("csharp", "python", "both")]
    [string]$Lang = "both",

    [string]$Filter = ""
)

$RepoRoot = Split-Path -Parent (Split-Path -Parent $PSScriptRoot)
if (-not (Test-Path "$RepoRoot\Coding_Practice")) {
    $RepoRoot = (Get-Location).Path
}

Write-Host "==========================================================" -ForegroundColor Cyan
Write-Host " 🚀 Antigravity Practice Runner: $($Type.ToUpper()) | Action: $Action" -ForegroundColor Cyan
Write-Host "==========================================================" -ForegroundColor Cyan

if ($Action -eq "status" -or $Action -eq "list") {
    Write-Host "`n📁 Available Practice Projects:" -ForegroundColor Yellow
    Write-Host "  [DSA] C# Project: Coding_Practice\Senior_Practice\csharp\SeniorPractice.Tests.csproj"
    Write-Host "  [DSA] Python Pkg: Coding_Practice\Senior_Practice\python"
    Write-Host "  [LLD] C# Project: Coding_Practice\LLD_Practice\csharp\LLDPractice.Tests.csproj"
    Write-Host "  [LLD] Python Pkg: Coding_Practice\LLD_Practice\python"

    Write-Host "`n📊 Recent Practice Log Entries:" -ForegroundColor Yellow
    $logPath = "$RepoRoot\learning_tracking\Practice_Log.md"
    if (Test-Path $logPath) {
        Get-Content $logPath | Select-Object -Last 10
    }
    exit 0
}

# --- TEST ACTION ---
if ($Action -eq "test") {
    $csharpDsaProj = "$RepoRoot\Coding_Practice\Senior_Practice\csharp\SeniorPractice.Tests.csproj"
    $pythonDsaDir = "$RepoRoot\Coding_Practice\Senior_Practice\python"
    $csharpLldProj = "$RepoRoot\Coding_Practice\LLD_Practice\csharp\LLDPractice.Tests.csproj"
    $pythonLldDir = "$RepoRoot\Coding_Practice\LLD_Practice\python"

    # DSA C#
    if (($Type -eq "dsa" -or $Type -eq "all") -and ($Lang -eq "csharp" -or $Lang -eq "both")) {
        Write-Host "`n⚡ Running DSA C# Tests (dotnet test)..." -ForegroundColor Green
        $filterArg = if ($Filter) { "--filter ""FullyQualifiedName~$Filter""" } else { "" }
        if ($filterArg) {
            dotnet test $csharpDsaProj --filter "FullyQualifiedName~$Filter"
        } else {
            dotnet test $csharpDsaProj
        }
    }

    # DSA Python
    if (($Type -eq "dsa" -or $Type -eq "all") -and ($Lang -eq "python" -or $Lang -eq "both")) {
        Write-Host "`n⚡ Running DSA Python Tests (py -m unittest)..." -ForegroundColor Green
        if ($Filter) {
            py -m unittest discover -s $pythonDsaDir -p "*$Filter*.py"
        } else {
            py -m unittest discover -s $pythonDsaDir
        }
    }

    # LLD C#
    if (($Type -eq "lld" -or $Type -eq "all") -and ($Lang -eq "csharp" -or $Lang -eq "both")) {
        Write-Host "`n⚡ Running LLD C# Tests (dotnet test)..." -ForegroundColor Magenta
        if ($Filter) {
            dotnet test $csharpLldProj --filter "FullyQualifiedName~$Filter"
        } else {
            dotnet test $csharpLldProj
        }
    }

    # LLD Python
    if (($Type -eq "lld" -or $Type -eq "all") -and ($Lang -eq "python" -or $Lang -eq "both")) {
        Write-Host "`n⚡ Running LLD Python Tests (py -m unittest)..." -ForegroundColor Magenta
        if ($Filter) {
            py -m unittest discover -s $pythonLldDir -p "*$Filter*.py"
        } else {
            py -m unittest discover -s $pythonLldDir -p "test_*.py"
        }
    }

    Write-Host "`n✅ All requested test runs completed!" -ForegroundColor Cyan
}
