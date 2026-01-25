# PowerShell Script: Create Week 01–19 Folder Structure (v12.1)
# Run this from the repo root

# Mapping of folder names -> human-readable week titles
$weekFolders = [ordered]@{
    "week_01_foundations_i_computational_fundamentals"                              = "Week 1: Foundations I — Computational Fundamentals"
    "week_02_foundations_ii_linear_data_structures_and_binary_search"               = "Week 2: Foundations II — Linear Data Structures & Binary Search"
    "week_03_foundations_iii_sorting_heaps_and_hashing"                             = "Week 3: Foundations III — Sorting, Heaps & Hashing"
    "week_04_core_problem_solving_patterns_i"                                       = "Week 4: Core Problem-Solving Patterns I"
    "week_05_tier_1_critical_patterns"                                              = "Week 5: Tier 1 Critical Patterns"
    "week_06_string_manipulation_patterns"                                          = "Week 6: String Manipulation Patterns"
    "week_07_trees_and_balanced_search_trees"                                       = "Week 7: Trees & Balanced Search Trees"
    "week_08_graph_fundamentals_representations_bfs_dfs_and_topological_sort"       = "Week 8: Graph Fundamentals — Representations, BFS, DFS & Topological Sort"
    "week_09_graph_algorithms_i_shortest_paths_mst_and_union_find"                  = "Week 9: Graph Algorithms I — Shortest Paths, MST & Union-Find"
    "week_10_dynamic_programming_i_fundamentals"                                    = "Week 10: Dynamic Programming I — Fundamentals"
    "week_11_dynamic_programming_ii_trees_dags_and_advanced_patterns"               = "Week 11: Dynamic Programming II — Trees, DAGs & Advanced Patterns"
    "week_12_greedy_algorithms_and_exchange_arguments"                              = "Week 12: Greedy Algorithms & Exchange Arguments"
    "week_13_advanced_graphs_and_formal_amortized_analysis"                         = "Week 13: Advanced Graphs & Formal Amortized Analysis"
    "week_14_matrix_problems_backtracking_and_bit_tricks"                           = "Week 14: Matrix Problems, Backtracking & Bit Tricks"
    "week_15_advanced_strings_and_network_flow"                                     = "Week 15: Advanced Strings & Network Flow"
    "week_16_segment_trees_bit_and_geometry"                                        = "Week 16: Segment Trees, BIT & Geometry"
    "week_17_advanced_graphs_hld_and_fft"                                           = "Week 17: Advanced Graphs, HLD & FFT"
    "week_18_probabilistic_data_structures_and_algorithmic_system_design"           = "Week 18: Probabilistic Data Structures & Algorithmic System Design"
    "week_19_mock_interviews_and_final_mastery"                                     = "Week 19: Mock Interviews & Final Mastery"
}

# Base path (current directory)
$BasePath = Get-Location

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Creating Week 01–19 Folder Structure" -ForegroundColor Cyan
Write-Host "Base Path: $BasePath" -ForegroundColor Yellow
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Create each week folder with v12 and support files subfolders
$createdCount = 0
$skippedCount = 0

foreach ($folderName in $weekFolders.Keys) {
    $fullPath = Join-Path $BasePath $folderName
    
    if (-not (Test-Path $fullPath)) {
        # Create main week folder
        New-Item -ItemType Directory -Path $fullPath -Force | Out-Null
        
        # Create subfolders
        New-Item -ItemType Directory -Path (Join-Path $fullPath 'v12') -Force | Out-Null
        New-Item -ItemType Directory -Path (Join-Path $fullPath 'support files') -Force | Out-Null
        
        Write-Host "✅ Created: $folderName" -ForegroundColor Green
        Write-Host "   └── v12/" -ForegroundColor Gray
        Write-Host "   └── support files/" -ForegroundColor Gray
        
        $createdCount++
    } else {
        Write-Host "⏭️  Skipped: $folderName (already exists)" -ForegroundColor Cyan
        $skippedCount++
    }
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "✅ Summary:" -ForegroundColor Green
Write-Host "   Created: $createdCount folders" -ForegroundColor Green
Write-Host "   Skipped: $skippedCount folders (already exist)" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
