<# 
.SYNOPSIS
  Creates v10 DSA curriculum folder structure with flat week directories and a core_curriculum folder.

.PARAMETER BasePath
  Root directory where the structure should be created. Defaults to current directory.
#>

param(
    [string]$BasePath = (Get-Location).Path
)

# Ensure base path exists
if (-not (Test-Path $BasePath)) {
    throw "Base path '$BasePath' does not exist."
}

# Mapping of folder names -> human-readable week titles
$weekFolders = [ordered]@{
    "week_01_foundations_i_computational_fundamentals" = "Week 1: Foundations I — Computational Fundamentals"
    "week_02_foundations_ii_linear_data_structures"    = "Week 2: Foundations II — Linear Data Structures"
    "week_03_foundations_iii_sorting_and_hashing"       = "Week 3: Foundations III — Sorting & Hashing"
    "week_04_core_problem_solving_patterns_i"          = "Week 4: Core Problem-Solving Patterns I"
    "week_05_tier_1_critical_patterns"                 = "Week 5: Tier 1 Critical Patterns"
    "week_06_tier_1_5_string_manipulation_patterns"    = "Week 6: Tier 1.5 String Manipulation Patterns"
    "week_07_trees_and_heaps"                          = "Week 7: Trees & Heaps"
    "week_08_tier_2_strategic_patterns_and_transformations" = "Week 8: Tier 2 Strategic Patterns & Transformations"
    "week_09_graphs_i_foundations"                     = "Week 9: Graphs I — Foundations"
    "week_10_graphs_ii_advanced"                       = "Week 10: Graphs II — Advanced"
    "week_11_specialized_data_structures"              = "Week 11: Specialized Data Structures"
    "week_12_strings_and_math_mastery"                 = "Week 12: Strings & Math Mastery"
    "week_13_greedy_and_backtracking"                  = "Week 13: Greedy & Backtracking"
    "week_14_dynamic_programming_mastery"              = "Week 14: Dynamic Programming Mastery"
    "week_15_interview_pattern_integration"            = "Week 15: Interview Pattern Integration"
    "week_16_tier_3_advanced_extensions"               = "Week 16: Tier 3: Advanced Extensions"
    "week_17_advanced_mastery_deep_dives_part_1"       = "Week 17: Advanced Mastery — Deep Dives Part 1"
    "week_18_advanced_mastery_deep_dives_part_2"       = "Week 18: Advanced Mastery — Deep Dives Part 2"
    "week_19_mock_interviews_and_final_mastery"        = "Week 19: Mock Interviews & Final Mastery"
}

# Create each week folder (flat — no nested structure)
foreach ($folderName in $weekFolders.Keys) {
    $fullPath = Join-Path $BasePath $folderName
    if (-not (Test-Path $fullPath)) {
        New-Item -ItemType Directory -Path $fullPath | Out-Null
        Write-Host "Created $folderName"
    } else {
        Write-Host "Exists  $folderName"
    }
}

# Create core_curriculum folder and copy master config files if present
$coreDir = Join-Path $BasePath "core_curriculum"
if (-not (Test-Path $coreDir)) {
    New-Item -ItemType Directory -Path $coreDir | Out-Null
    Write-Host "Created core_curriculum"
} else {
    Write-Host "Exists  core_curriculum"
}

$masterFiles = @(
    "COMPLETE_SYLLABUS_v10_FINAL.md",
    "TEMPLATE_v10.md",
    "SYSTEM_CONFIG_v10_FINAL.md",
    "MASTER_PROMPT_v10_FINAL.md",
    "WEEKLY_BATCH_GENERATION_PROMPT_v10.md",
    "EMOJI_ICON_GUIDE_v8.md"
)

foreach ($file in $masterFiles) {
    $source = Join-Path $BasePath $file
    if (Test-Path $source) {
        Copy-Item -Path $source -Destination $coreDir -Force
        Write-Host "Copied $file to core_curriculum"
    } else {
        Write-Warning "File '$file' not found at base path. Skipped."
    }
}

Write-Host "Folder setup complete."