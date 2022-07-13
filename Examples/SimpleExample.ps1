Import-Module ".\pssynth.psd1" 

$a = New-Wave -note A3 -length 2 
$c = New-Wave -note C4 -length 1
$r = New-Wave -note A4 -waveForm Rest -length 1 
$g = New-Wave -note G4 -length 2

$sample = New-Sample $a,$r,$g,$c,$c,$r -loop
$sample | Play-Sample 

# How to stop playing: 
$sample | Stop-sample