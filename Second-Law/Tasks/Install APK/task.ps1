[System.Reflection.Assembly]::LoadWithPartialName("System.windows.forms") | Out-Null

Function Get-FileName()
{
    $OpenFileDialog = New-Object System.Windows.Forms.OpenFileDialog
    $OpenFileDialog.filter = "Android Package (*.apk)| *.apk"
    $OpenFileDialog.ShowDialog() | Out-Null
    $OpenFileDialog.filename
}

# *** Entry Point to Script ***

Get-FileName