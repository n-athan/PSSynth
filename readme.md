# About
This is a project to practice making a Powershell module in C#. I'm trying to make a synth, perhaps one that can be used in Live coding/algorave.

# Get Started
Clone or download the repository and load the module from the root folder: 

```powershell
Import-Module ".\pssynth.psd1"
```
## Basic beat
```powershell
$g4 = New-Wave -note G4 -length 2
$rest = New-Wave -WaveForm Rest -length 1 -Frequency 0
$a2 = New-Wave -note A2 -length 2
$sample = New-Sample -Waves $g4,$rest,$a2 -Loop
Play-Sample $sample

Start-Sleep -Seconds 10

Stop-Sample $sample
```
See [Examples](.\Example) for more.  


# Referenced Docs
- tutorial: https://docs.microsoft.com/en-us/powershell/scripting/dev-cross-plat/writing-portable-modules?view=powershell-7.2 
- How to use VS Code debugger https://docs.microsoft.com/en-us/powershell/scripting/dev-cross-plat/vscode/using-vscode-for-debugging-compiled-cmdlets?view=powershell-7.2
- possible NuGet lib: https://github.com/ClementCaton/ALGOSUP_2022_Project_3_A 
- random example cmdlet in C# https://github.com/microsoft/powerbi-powershell/blob/master/src/Modules/Workspaces/Commands.Workspaces/GetPowerBIWorkspace.cs 
- Powershell nuget config http://blog.tintoy.io/2017/02/building-modules-for-powershell-core/ 
- http://soundfile.sapp.org/doc/WaveFormat/
- https://docs.microsoft.com/nl-nl/archive/blogs/dawate/intro-to-audio-programming-part-4-algorithms-for-different-sound-waves-in-c
- https://www.youtube.com/watch?v=fp1Snqq9ovw&ab_channel=G223Productions
- frequencies of notes: https://pages.mtu.edu/~suits/notefreqs.html
- on calculating frequencies: https://www.simplifyingtheory.com/math-in-music/ 

# Next steps
- aanpasbare samplerate, bits per sample, tempo and tuning. 
- beat
- meedere waves mixen (andere parameterset en extra functie met samples als input ipv waves).
- check the triangles, == saw?

# nice to have
- custom wave form
- midi input
- record to file (Wav and midi?)
- amplitude mask
- modulation 
- Effects on whole sample
- handige overgang naar ander sample (niet zelf hoeven timen)
- https://github.com/naudio/NAudio