# PowerShell Script: Create Week 01-19 Folder Structure (v13.0)
# Run this from the repo root
# Updated: January 26, 2026
# Version: 13.0
# Note: Using underscore separators for folder names


# Mapping of folder names -> human-readable week titles (v13.0)
$weekFolders = [ordered]@{
    "week_01_foundations_i_computational_fundamentals"                              = "Week 1: Computational Fundamentals, Peak Finding & Asymptotics"
    "week_02_foundations_ii_linear_data_structures"                                 = "Week 2: Linear Data Structures & Binary Search"
    "week_03_foundations_iii_sorting_and_hashing"                                   = "Week 3: Sorting, Heaps & Hashing"
    "week_04_core_problem_solving_patterns_i"                                       = "Week 4: Core Problem-Solving Patterns I"
    "week_05_tier_1_critical_patterns"                                              = "Week 5: Tier 1 Critical Patterns ⭐"
    "week_06_string_manipulation_patterns"                                          = "Week 6: String Manipulation Patterns"
    "week_07_trees_and_balanced_search_trees"                                               = "Week 7: Trees & Balanced Search Trees"
    "week_08_graph_fundamentals"                                                    = "Week 8: Graph Fundamentals"
    "week_09_graph_algorithms_i"                                                    = "Week 9: Graph Algorithms I - Shortest Paths & MST"
    "week_10_dynamic_programming_i_fundamentals"                                    = "Week 10: Dynamic Programming I - Fundamentals"
    "week_11_dp_ii_advanced"                                                        = "Week 11: Dynamic Programming II - Advanced"
    "week_12_greedy_and_paradigms"                                                  = "Week 12: Greedy Algorithms & Exchange Arguments"
    "week_13_backtracking_and_advanced_graphs"                                      = "Week 13: Backtracking & Branch & Bound"
    "week_14_matrix_backtracking_bits"                                              = "Week 14: Matrix Problems, Bitmasks & Number Theory"
    "week_15_advanced_strings_flow"                                                 = "Week 15: Advanced Strings & Network Flow"
    "week_16_advanced_data_structures"                                              = "Week 16: Advanced Data Structures"
    "week_17_advanced_graphs_hld_fft"                                               = "Week 17: Advanced Graphs, HLD & FFT"
    "week_18_probabilistic_ds_systems"                                              = "Week 18: Probabilistic DS & System Design"
    "week_19_mock_interviews_mastery"                                               = "Week 19: Mock Interviews & Final Review"
}


# Base path (current directory)
$BasePath = Get-Location


Write-Host ""
Write-Host "╔═══════════════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║                                                                   ║" -ForegroundColor Cyan
Write-Host "║       📁 Creating Week 01-19 Folder Structure (v13.0)            ║" -ForegroundColor Cyan
Write-Host "║                                                                   ║" -ForegroundColor Cyan
Write-Host "╚═══════════════════════════════════════════════════════════════════╝" -ForegroundColor Cyan
Write-Host ""
Write-Host "📍 Base Path: $BasePath" -ForegroundColor Yellow
Write-Host "🔖 Version: 13.0" -ForegroundColor Yellow
Write-Host "📅 Date: $(Get-Date -Format 'MMMM dd, yyyy')" -ForegroundColor Yellow
Write-Host "🔤 Naming: Underscore Separators (week_XX_format)" -ForegroundColor Yellow
Write-Host ""
Write-Host "────────────────────────────────────────────────────────────────────" -ForegroundColor Gray
Write-Host ""


# Create each week folder with v13 and support files subfolders
$createdCount = 0
$skippedCount = 0
$weekCounter = 1


foreach ($folderName in $weekFolders.Keys) {
    $fullPath = Join-Path $BasePath $folderName
    $weekTitle = $weekFolders[$folderName]
    
    if (-not (Test-Path $fullPath)) {
        # Create main week folder
        New-Item -ItemType Directory -Path $fullPath -Force | Out-Null
        
        # Create subfolders for v13 structure
        New-Item -ItemType Directory -Path (Join-Path $fullPath 'v13_instructional') -Force | Out-Null
        New-Item -ItemType Directory -Path (Join-Path $fullPath 'v13_support_files') -Force | Out-Null
        New-Item -ItemType Directory -Path (Join-Path $fullPath 'v13_assets') -Force | Out-Null
        
        Write-Host "✅ Week $($weekCounter.ToString().PadLeft(2, '0')): $weekTitle" -ForegroundColor Green
        Write-Host "   📁 $folderName" -ForegroundColor Gray
        Write-Host "   ├── 📂 v13_instructional/" -ForegroundColor DarkGray
        Write-Host "   ├── 📂 v13_support_files/" -ForegroundColor DarkGray
        Write-Host "   └── 📂 v13_assets/" -ForegroundColor DarkGray
        Write-Host ""
        
        $createdCount++
    } else {
        Write-Host "⏭️  Week $($weekCounter.ToString().PadLeft(2, '0')): $weekTitle (exists)" -ForegroundColor Cyan
        Write-Host "   📁 $folderName" -ForegroundColor Gray
        Write-Host ""
        
        $skippedCount++
    }
    
    $weekCounter++
}


Write-Host "────────────────────────────────────────────────────────────────────" -ForegroundColor Gray
Write-Host ""
Write-Host "╔═══════════════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║                         ✅ SUMMARY                               ║" -ForegroundColor Cyan
Write-Host "╚═══════════════════════════════════════════════════════════════════╝" -ForegroundColor Cyan
Write-Host ""
Write-Host "📊 Folder Creation Statistics:" -ForegroundColor Yellow
Write-Host "   ✅ Created:  $createdCount folder(s)" -ForegroundColor Green
Write-Host "   ⏭️  Skipped:  $skippedCount folder(s) (already exist)" -ForegroundColor Cyan
Write-Host "   📁 Total:    $($createdCount + $skippedCount) weeks" -ForegroundColor Yellow
Write-Host ""
Write-Host "📋 Structure per Week:" -ForegroundColor Yellow
Write-Host "   ├── v13_instructional/       (Daily instructional content)" -ForegroundColor Gray
Write-Host "   ├── v13_support_files/       (Guidelines, roadmaps, etc.)" -ForegroundColor Gray
Write-Host "   └── v13_assets/              (Diagrams, visuals, resources)" -ForegroundColor Gray
Write-Host ""
Write-Host "🎯 Phase Distribution:" -ForegroundColor Yellow
Write-Host "   🟦 Phase A (Weeks 01-03):    Foundations & Computational Thinking" -ForegroundColor Gray
Write-Host "   🟩 Phase B (Weeks 04-06):    Core Patterns & String Manipulation" -ForegroundColor Gray
Write-Host "   🟨 Phase C (Weeks 07-11):    Trees, Graphs & Dynamic Programming" -ForegroundColor Gray
Write-Host "   🟧 Phase D (Weeks 12-13):    Algorithm Paradigms" -ForegroundColor Gray
Write-Host "   🟪 Phase E (Weeks 14-15):    Integration & Extensions" -ForegroundColor Gray
Write-Host "   🟫 Phase F (Weeks 16-18):    Advanced Deep Dives (Optional)" -ForegroundColor Gray
Write-Host "   🔴 Phase G (Week 19):        Mock Interviews & Final Review" -ForegroundColor Gray
Write-Host ""
Write-Host "────────────────────────────────────────────────────────────────────" -ForegroundColor Gray
Write-Host ""
Write-Host "✨ Folder structure created successfully!" -ForegroundColor Green
Write-Host ""
Write-Host "🚀 Next Steps:" -ForegroundColor Yellow
Write-Host "   1. Navigate to individual week folders" -ForegroundColor Gray
Write-Host "   2. Add instructional content to v13_instructional/" -ForegroundColor Gray
Write-Host "   3. Add support files to v13_support_files/" -ForegroundColor Gray
Write-Host "   4. Add assets to v13_assets/" -ForegroundColor Gray
Write-Host "   5. Follow v13.0 curriculum standards" -ForegroundColor Gray
Write-Host ""
Write-Host "📚 Resources:" -ForegroundColor Yellow
Write-Host "   • COMPLETE_SYLLABUS_v13.md - Full curriculum details" -ForegroundColor Gray
Write-Host "   • README_v13.md - Main project README" -ForegroundColor Gray
Write-Host "   • Week guidelines - For each week's strategy" -ForegroundColor Gray
Write-Host ""
Write-Host "════════════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "Script completed successfully! ✨" -ForegroundColor Green
Write-Host "════════════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""
