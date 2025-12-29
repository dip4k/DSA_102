# DSA Master Curriculum v8.0 - Folder Structure Setup Script
# Version: 8.0
# Generated: December 29, 2025
# Purpose: Create complete folder structure

Write-Host "🚀 Creating DSA Master Curriculum v8.0 Folder Structure..." -ForegroundColor Cyan
Write-Host "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" -ForegroundColor Gray

# Create main directory
Write-Host "📁 Creating main directory: DSA_Master_Curriculum_v8..." -ForegroundColor Yellow
New-Item -ItemType Directory -Path "DSA_Master_Curriculum_v8" -Force | Out-Null

# Create metadata subdirectories
Write-Host "📁 Creating metadata directories..." -ForegroundColor Yellow

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
    Write-Host "  ✅ Created: $dir" -ForegroundColor Green
}

# Create regular weeks (1-3, 4, 5, 6-12, 14-16)
Write-Host "📁 Creating regular week folders (1-16)..." -ForegroundColor Yellow
for ($week = 1; $week -le 16; $week++) {
    $weekFolder = "DSA_Master_Curriculum_v8\WEEKS\Week_$week"
    New-Item -ItemType Directory -Path "$weekFolder\Instructional_Files" -Force | Out-Null
    New-Item -ItemType Directory -Path "$weekFolder\Support_Files" -Force | Out-Null
    Write-Host "  ✅ Created: Week_$week (Instructional & Support)" -ForegroundColor Green
}

# Create special weeks (4.5, 5.5, 13)
Write-Host "📁 Creating special tier weeks (4.5, 5.5, 13)..." -ForegroundColor Yellow
$specialWeeks = @("4.5", "5.5", "13")
foreach ($week in $specialWeeks) {
    $weekFolder = "DSA_Master_Curriculum_v8\WEEKS\Week_$week"
    New-Item -ItemType Directory -Path "$weekFolder\Instructional_Files" -Force | Out-Null
    New-Item -ItemType Directory -Path "$weekFolder\Support_Files" -Force | Out-Null
    Write-Host "  ✅ Created: Week_$week (Instructional & Support)" -ForegroundColor Green
}

Write-Host "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" -ForegroundColor Gray
Write-Host "✅ COMPLETE!" -ForegroundColor Green
Write-Host "📊 Summary:" -ForegroundColor Cyan
Write-Host "  • 7 metadata directories created" -ForegroundColor White
Write-Host "  • 16 week folders created" -ForegroundColor White
Write-Host "  • 2 subfolders per week (Instructional + Support)" -ForegroundColor White
Write-Host "  • Special tier weeks included (4.5, 5.5, 13)" -ForegroundColor White
Write-Host "  • Total: 50+ folders ready" -ForegroundColor White
Write-Host "" -ForegroundColor White
Write-Host "🎯 Next Steps:" -ForegroundColor Cyan
Write-Host "  1. Download all metadata files" -ForegroundColor White
Write-Host "  2. Place files in corresponding folders" -ForegroundColor White
Write-Host "  3. Start generating weekly content" -ForegroundColor White
