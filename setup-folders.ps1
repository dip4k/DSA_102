# DSA Master Curriculum v8.0 - AUTOMATIC FOLDER SETUP
# Generated: Dec 30, 2025 | Execute: .\setup-folders.ps1

Write-Host "🚀 DSA Master Curriculum v8.0 - Creating Folder Structure..." -ForegroundColor Cyan
Write-Host "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" -ForegroundColor Gray

# 1. Create main directory
New-Item -ItemType Directory -Path "DSA_Master_Curriculum_v8" -Force | Out-Null
Write-Host "📁 Main: DSA_Master_Curriculum_v8" -ForegroundColor Green

# 2. Create 7 metadata directories
$metadataDirs = @(
    "DSA_Master_Curriculum_v8\01_ROOT_METADATA",
    "DSA_Master_Curriculum_v8\02_TEMPLATES_STANDARDS", 
    "DSA_Master_Curriculum_v8\03_AI_SYSTEM",
    "DSA_Master_Curriculum_v8\04_CORE_CURRICULUM",
    "DSA_Master_Curriculum_v8\05_SUPPORTING_DOCS",
    "DSA_Master_Curriculum_v8\06_GUIDES_SUMMARIES",
    "DSA_Master_Curriculum_v8\07_BATCH_GENERATION"
)

foreach ($dir in $metadataDirs) {
    New-Item -ItemType Directory -Path $dir -Force | Out-Null
    Write-Host "  📂 $($dir.Split('\')[-1])" -ForegroundColor Yellow
}

# 3. Create WEEKS folder structure (16 weeks + 3 special)
New-Item -ItemType Directory -Path "DSA_Master_Curriculum_v8\WEEKS" -Force | Out-Null

# Regular weeks 1-16
for ($week = 1; $week -le 16; $week++) {
    $weekPath = "DSA_Master_Curriculum_v8\WEEKS\Week_$week"
    New-Item -ItemType Directory -Path "$weekPath\Instructional_Files" -Force | Out-Null
    New-Item -ItemType Directory -Path "$weekPath\Support_Files" -Force | Out-Null
    Write-Host "  📚 Week_$week (Instructional + Support)" -ForegroundColor Cyan
}

# Special Tier weeks (4.5, 5.5, 13)
$specialWeeks = @("4.5", "5.5", "13")
foreach ($week in $specialWeeks) {
    $weekPath = "DSA_Master_Curriculum_v8\WEEKS\Week_$week"
    New-Item -ItemType Directory -Path "$weekPath\Instructional_Files" -Force | Out-Null
    New-Item -ItemType Directory -Path "$weekPath\Support_Files" -Force | Out-Null
    Write-Host "  ⭐ Week_$week (Tier $(if($week -eq '4.5'){'1'}elseif($week -eq '5.5'){'2'}else{'3'}))" -ForegroundColor Magenta
}

# Summary
$totalFolders = (Get-ChildItem -Path "DSA_Master_Curriculum_v8" -Recurse -Directory).Count
Write-Host "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" -ForegroundColor Gray
Write-Host "✅ COMPLETE! Created $totalFolders folders" -ForegroundColor Green
Write-Host "📁 Root: DSA_Master_Curriculum_v8/" -ForegroundColor Cyan
Write-Host "📊 7 Metadata + 19 Weeks + 38 Subfolders = $totalFolders total" -ForegroundColor White
Write-Host "🚀 Next: Download 8 essential files → Organize → Generate Week 1" -ForegroundColor Yellow
Write-Host "📖 See STEP_BY_STEP_GUIDE.md for complete instructions" -ForegroundColor Cyan

# Open folder in Explorer
Start-Process explorer.exe "DSA_Master_Curriculum_v8"