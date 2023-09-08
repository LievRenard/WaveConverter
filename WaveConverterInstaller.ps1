Write-Output "Install WaveConverter..."
if (Test-Path .\WaveConverter\) {
    Remove-Item .\WaveConverter\ -Recurse
}
Invoke-WebRequest -Uri https://github.com/LievRenard/WaveConverter/releases/download/release/WaveConverter.zip -OutFile .\WaveConverter.zip
Expand-Archive .\WaveConverter.zip
Remove-Item .\WaveConverter.zip
if (!(Test-Path $profile)) {
    New-Item -Path $profile -ItemType "file" -Force
}
$command = "Set-Alias conv" + (Get-Location).Path + "\WaveConverter\WaveConverter.exe"
Add-Content -Path $profile -Value $command
Write-Output "Installation end. Please restart terminal."