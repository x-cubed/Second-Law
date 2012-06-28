# Install Superuser
# Superuser is from: http://androidsu.com/superuser/

$Activity = "Installing super user..."

$TempFolder = "/data/local/tmp/"
$BusyBox = "busybox"
$Su = "su"
$SystemFolder = "/system"

$BusyBoxTemp = $TempFolder + $BusyBox
$BusyBoxInstallFolder = $SystemFolder +"/xbin/"
$BusyBoxInstalled = $BusyBoxInstallFolder + $BusyBox
$BusyBoxSu = $BusyBoxInstallFolder + $Su

$SuInstallFolder = $SystemFolder + "/bin/"
$SuInstalled = $SuInstallFolder + $Su

$Owner = "root.shell"

# Create a new directory
Write-Progress -Activity $Activity -PercentComplete  0 -Status "Creating temporary folder..."
$Device.RunShellCommand("cd /data/local && mkdir tmp")
$Device.RunShellCommand("cd " + $TempFolder + " && rm *")

# Install BusyBox
Write-Progress -Activity $Activity -PercentComplete  5 -Status "Uploading busybox..."
$Device.PushFile($BusyBox, $TempFolder)
$Device.ChangeMode(755, $BusyBoxTemp)

# Remount the system partition as writeable
Write-Progress -Activity $Activity -PercentComplete 15 -Status "Remounting system partition as writeable..."
$Device.RunShellCommand($BusyBoxTemp + " mount -o remount,rw " + $SystemFolder)

# Copy BusyBox onto the system partition
Write-Progress -Activity $Activity -PercentComplete 25 -Status "Copying busybox to the system partition..."
$Device.CopyFile($BusyBoxTemp, $BusyBoxInstalled)
$Device.ChangeOwner($Owner, $BusyBoxInstalled)
$Device.ChangeMode(4755, $BusyBoxInstalled)

# Install BusyBox
Write-Progress -Activity $Activity -PercentComplete 45 -Status "Installing busybox..."
$Device.RunShellCommand($BusyBoxInstalled + " --install -s " + $BusyBoxInstallFolder)
$Device.RemoveFile($BusyBoxTemp)

# Install su
Write-Progress -Activity $Activity -PercentComplete 65 -Status "Installing su..."
$Device.PushFile("su", $SuInstallFolder)
$Device.ChangeOwner($Owner, $SuInstalled)
$Device.ChangeMode(6755, $SuInstalled)
$Device.RemoveFile($BusyBoxSu)
$Device.CreateSymbolicLink($SuInstalled, $BusyBoxSu)

# Install Superuser.apk as a system application
Write-Progress -Activity $Activity -PercentComplete 85 -Status "Installing Superuser application..."
$Device.PushFile("Superuser.apk", "/system/app/")

# Clean up temp folder
Write-Progress -Activity $Activity -PercentComplete 95 -Status "Cleaning up..."
$Device.RemoveFile($TempFolder + "*")
$Device.RunShellCommand("rmdir " + $TempFolder)
Write-Progress -Activity $Activity -Status "Done" -Completed