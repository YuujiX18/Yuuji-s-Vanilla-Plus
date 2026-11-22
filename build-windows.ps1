$ErrorActionPreference = 'Stop'
Set-Location $PSScriptRoot
$version = '9.8.2'
$target = Join-Path $PSScriptRoot ".gradle-dist\gradle-$version"
$gradle = Join-Path $target 'bin\gradle.bat'

if (-not (Get-Command java -ErrorAction SilentlyContinue)) {
  throw 'Java not found. Install JDK 25 and put it on PATH.'
}
$javaVersion = (& java -version 2>&1 | Out-String)
if ($javaVersion -notmatch 'version "25') {
  throw "Yuuji Minecraft 26.3 requires JDK 25. Current: $javaVersion"
}
if (-not (Test-Path $gradle)) {
  Write-Host "Downloading official Gradle $version for the first build..."
  $cacheDir = Join-Path $PSScriptRoot '.gradle-dist'
  New-Item -ItemType Directory -Force -Path $cacheDir | Out-Null
  $zip = Join-Path $cacheDir "gradle-$version-bin.zip"
  Invoke-WebRequest -Uri "https://services.gradle.org/distributions/gradle-$version-bin.zip" -OutFile $zip
  Expand-Archive -LiteralPath $zip -DestinationPath $cacheDir -Force
  Remove-Item $zip
}
& $gradle --no-daemon build
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
$projectVersion = (Get-Content (Join-Path $PSScriptRoot 'gradle.properties') | Where-Object { $_ -match '^version=' }) -replace '^version=', ''
Write-Host "Build completed. Check build\libs\yuuji-$projectVersion.jar"































