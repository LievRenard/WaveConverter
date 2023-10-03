# WaveConverter
<img src="https://img.shields.io/badge/Windows-0078D4?style=for-the-badge&logo=windows&logoColor=white">
<img src="https://img.shields.io/badge/.Net-512BD4?style=for-the-badge&logo=dotnet&logoColor=white">
<img src="https://img.shields.io/badge/CSharp-239120?style=for-the-badge&logo=csharp&logoColor=white">

A program of optical value conversion among light wavelength, frequency, angular frequency, angular/linear wavenumber and photon energy.

## Installation

1. Download **WaveConverterInstaller.zip** in release.
2. Extract zip file to where you want.
3. Execute **WaveConverterAliasSetting.ps1** in terminal.
4. Restart terminal.

**To Update Program**

Just change old files in the folder that WaveConverter installed, to new files.

## Usage

`conv <value> <unit>`

\<value\> - The value you want to convert. Scientific notation is also allowed.

\<unit\> - The unit you want to convert. Capital and small letters should be distinguished.

**Supported Units**

- Wavelength - nm, um 
- Frequency - Hz, THz
- Angular Frequency - rad/s
- Angular Wavenumber - rad/cm, rad/um
- Linear Wavenumber - cm-1
- Photon Energy - eV

**There can be blank spaces between \<value\> and \<unit\>, or no blank space.*

`conv --help or -h`

Prints help page for WaveConverter.

### Example

`conv 2e15rad/s`

It will be convert angular frequency of 2x10^15rad/s to other optical variables.
