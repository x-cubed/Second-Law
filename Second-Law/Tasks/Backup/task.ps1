[System.Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms") | Out-Null

Function Get-FileName()
{
	$SaveFileDialog = New-Object System.Windows.Forms.SaveFileDialog
	$SaveFileDialog.title = "Select backup destination for " + $Device.Metadata.ProductName
	$SaveFileDialog.filter = "Android Backup (*.ab)| *.ab"
	$SaveFileDialog.ShowDialog() | Out-Null
	$SaveFileDialog.filename
}

# *** Entry Point to Script ***

$Activity = "Backing up device..."

Write-Progress -Activity $Activity -PercentComplete  0 -Status "Prompting for backup destination..."
$FileName = Get-FileName;

Write-Progress -Activity $Activity -PercentComplete  5 -Status "Starting backup... (follow the instructions on the screen of the device)"
$Device.Backup($FileName);

Write-Progress -Activity $Activity -Complete -Status "Backup complete"