# ZergRush exploit for temporary root access
# Works with most Android Froyo (2.2)/Gingerbread (2.3) devices
#
# Exploit developed by Revolutionary Team
# For more information see: http://forum.xda-developers.com/showthread.php?t=1296916
# To donate visit: https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=NQRTG2YL4FPXY

# Create a new directory
Write-Progress -PercentComplete 0 -Status "Creating temporary folder..."
$Device.RunADBCommandOrThrow("shell cd /data/local && mkdir tmp")
$Device.RunADBCommandOrThrow("shell cd /data/local/tmp/ && rm *")

# Push the exploit
Write-Progress -PercentComplete 10 -Status "Uploading exploit..."
$Device.PushFile("zergRush", "/data/local/tmp")
$Device.ChangeMode(777, "/data/local/tmp/zergRush");

# Run the exploit
Write-Progress -PercentComplete 20 -Status "Running exploit..."
$Device.RunADBCommandOrThrow("shell ./data/local/tmp/zergRush")

# Wait for the device to reconnect with ADB as root
Write-Progress -PercentComplete 90 -Status "Waiting for device to reconnect..."
$Device.WaitForDevice()

Write-Progress -Completed