Write-Output "Setting alias in Powershell profile..."
if (!(Test-Path $profile)) {
    Write-Output "Powershell profile not found. Making new profile..."
    New-Item -Path $profile -ItemType "file" -Force
    Write-Output "Powershell profile created."
}
$command = "Set-Alias conv " + (Get-Location).Path + "\WaveConverter\WaveConverter.exe"
Add-Content -Path $profile -Value $command
Write-Output "Setting end. Please restart terminal."