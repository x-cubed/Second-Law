[System.Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms") | Out-Null

Function Get-FileName()
{
	$OpenFileDialog = New-Object System.Windows.Forms.OpenFileDialog
	$OpenFileDialog.title = "Select backup file to be restored onto " + $Device.Metadata.ProductName
	$OpenFileDialog.filter = "Android Backup (*.ab)| *.ab"
	$OpenFileDialog.ShowDialog() | Out-Null
	$OpenFileDialog.filename
}

# *** Entry Point to Script ***

$Activity = "Restoring backup to device..."

Write-Progress -Activity $Activity -PercentComplete  0 -Status "Prompting for backup file..."
$FileName = Get-FileName

Write-Progress -Activity $Activity -PercentComplete  5 -Status "Restoring from backup... (follow the instructions on the screen of the device)"
$Device.Restore($FileName)

Write-Progress -Activity $Activity -Complete -Status "Restore complete"