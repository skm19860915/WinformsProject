makecert -r -pe -n "CN=MarketingAssistant" -ss CA -sr CurrentUser -a sha256 -cy authority -sky signature -sv MA19.pvk MA19.cer

makecert -pe -n "CN=MarketingAssistant" -a sha256 -cy end -sky signature -ic MA19.cer -iv MA19.pvk -sv MA19.pvk MA19.cer


issuer and signing password = "ma2019"

MA19.pfx 


x-com assembly signing may be required to avoid multi-process running.. not sure - fixing the tray icon not found may have been what fixed that.



What's this?? Marketing Assistant.appref-ms

Apparently a ClickOnce app needs to be started from the correct filetype in order to actually run with admin perms... So we're including 
file in the application's deployment directory.

Later, we reference it here: Computer\HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run
		reg.SetValue("Marketing Assistant", Application.ExecutablePath.ToString().Substring(0, Application.ExecutablePath.ToString().IndexOf(".exe")) + ".appref-ms");
****THIS FILE WILL NEED TO BE UPDATED TO THE NEW KEYTOKEN (found in the .manifest file) IF THE KEY CHANGES
	optionally, you could just grab it off the desktop of a fresh install.
		Otherwise? ClickOnce breaks, so does AutoUpdate, so does Admin perms and our ability to run Chrome/ChromeDriver


Another FYI - USERS NEED TO HAVE CHROME INSTALLED FOR THIS APP TO WORK!