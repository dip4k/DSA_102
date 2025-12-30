# $instFolder    = "WEEKS\Week_2\Instructional_Files"
# $supportFolder = "WEEKS\Week_2\Support_Files"

# New-Item -ItemType Directory -Path $instFolder, $supportFolder -Force | Out-Null

# $instructionalFiles = @(
#     "Week_2_Day_1_Arrays_Instructional.md",
#     "Week_2_Day_2_Dynamic_Arrays_Instructional.md",
#     "Week_2_Day_3_Linked_Lists_Instructional.md",
#     "Week_2_Day_4_Stacks_And_Queues_Instructional.md",
#     "Week_2_Day_5_Binary_Search_Instructional.md"
# )

# $supportFiles = @(
#     "Week_2_Guidelines.md",
#     "Week_2_Summary_Key_Concepts.md",
#     "Week_2_Interview_QA_Reference.md",
#     "Week_2_Problem_Solving_Roadmap.md",
#     "Week_2_Daily_Progress_Checklist.md"
# )

# $instructionalFiles | ForEach-Object {
#     New-Item -ItemType File -Path (Join-Path $instFolder $_) -Force | Out-Null
# }

# $supportFiles | ForEach-Object {
#     New-Item -ItemType File -Path (Join-Path $supportFolder $_) -Force | Out-Null
# }

# --- week 3

# Week 3 folders
$instFolder    = "WEEKS\Week_3\Instructional_Files"
$supportFolder = "WEEKS\Week_3\Support_Files"

# Create directories
New-Item -ItemType Directory -Path $instFolder, $supportFolder -Force | Out-Null

# Instructional files
$instructionalFiles = @(
    "Week_3_Day_1_Elementary_Sorts_Instructional.md",
    "Week_3_Day_2_Merge_Sort_And_Quick_Sort_Instructional.md",
    "Week_3_Day_3_Heap_Sort_And_Variants_Instructional.md",
    "Week_3_Day_4_Hash_Tables_I_Instructional.md",
    "Week_3_Day_5_Hash_Tables_II_Instructional.md"
)

# Support files
$supportFiles = @(
    "Week_3_Guidelines.md",
    "Week_3_Summary_Key_Concepts.md",
    "Week_3_Interview_QA_Reference.md",
    "Week_3_Problem_Solving_Roadmap.md",
    "Week_3_Daily_Progress_Checklist.md"
)

# Create empty instructional files
$instructionalFiles | ForEach-Object {
    New-Item -ItemType File -Path (Join-Path $instFolder $_) -Force | Out-Null
}

# Create empty support files
$supportFiles | ForEach-Object {
    New-Item -ItemType File -Path (Join-Path $supportFolder $_) -Force | Out-Null
}