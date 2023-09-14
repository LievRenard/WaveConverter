# WaveConverter
<img src="https://img.shields.io/badge/Windows-0078D4?style=for-the-badge&logo=windows&logoColor=white">
<img src="https://img.shields.io/badge/.Net-512BD4?style=for-the-badge&logo=dotnet&logoColor=white">
<img src="https://img.shields.io/badge/CSharp-239120?style=for-the-badge&logo=csharp&logoColor=white">

A program of optical value conversion among light wavelength, frequency, angular frequency, angular wavenumber and photon energy.

## Installation

1. Download **WaveConverterInstaller.ps1** in release.
2. Move the installer file to where you want.
3. Open windows terminal, or powershell at the desired folder.
4. Execute **WaveConverterInstaller.ps1** in terminal.

## Usage

`conv <value> <unit>`

\<value\> - The value you want to convert.

\<unit\> - The unit you want to convert. "nm" for wavelength, "Hz" for frequency, "rad/s" for angular frequency, "rad/cm" for angular wavenumber, and "eV" for photon energy. Capital and small letters should be distinguished.

**There can be blank spaces between \<value\> and \<unit\>, or no blank space.*

### Example

`conv 2e15rad/s`

It will be convert angular frequency of 2x10^15rad/s to other optical variables.
