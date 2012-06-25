# ZergRush exploit for temporary root access
# Works with most Android Froyo (2.2)/Gingerbread (2.3) devices
#
# Exploit developed by Revolutionary Team
# For more information see: http://forum.xda-developers.com/showthread.php?t=1296916
# To donate visit: https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=NQRTG2YL4FPXY

# Create a new directory
$Device.RunADBCommandOrThrow("shell cd /data/local && mkdir tmp")
$Device.RunADBCommandOrThrow("shell cd /data/local/tmp/ && rm *")

# Push the exploit
$Device.PushFile("zergRush", "/data/local/tmp")
$Device.ChangeMode(777, "/data/local/tmp/zergRush");

# Run the exploit
$Device.RunADBCommandOrThrow("shell ./data/local/tmp/zergRush")
$Device.WaitForDevice()