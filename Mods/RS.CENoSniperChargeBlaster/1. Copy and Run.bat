:: ========= Variables =========

SET mod_name=CENoSniperChargeBlaster
SET target_directory=D:\Games\Steam\steamapps\common\RimWorld\Mods\%mod_name%

:: ========= Copy ==========
 
rd "%target_directory%" /s /q
mkdir "%target_directory%"

:: About
mkdir "%target_directory%\About"
xcopy "About\*.*" "%target_directory%\About" /e

:: Patches
mkdir "%target_directory%\Patches"
xcopy "Patches\*.*" "%target_directory%\Patches" /e

:: LoadFolders.xml
copy "LoadFolders.xml" "%target_directory%\LoadFolders.xml"

:: ========= Run ==========

start steam://rungameid/294100
