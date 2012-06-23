[System.Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms") | Out-Null

Function Get-FileName()
{
	$OpenFileDialog = New-Object System.Windows.Forms.OpenFileDialog
	$OpenFileDialog.title = "Select package to install on " + $Device.Metadata.ProductName
	$OpenFileDialog.filter = "Android Package (*.apk)| *.apk"
	$OpenFileDialog.ShowDialog() | Out-Null
	$OpenFileDialog.filename
}

# *** Entry Point to Script ***

$FileName = Get-FileName;
$Device.InstallPackage($FileName)