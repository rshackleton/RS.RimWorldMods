:: ========= Variables =========

SET mod_name=RS.SanguophageRebalanced
SET target_directory=D:\Games\Steam\steamapps\common\RimWorld\Mods\%mod_name%

:: ========= Copy ==========
 
rd "%target_directory%" /s /q
mkdir "%target_directory%"

:: About
mkdir "%target_directory%\About"
xcopy "About\*.*" "%target_directory%\About" /e

:: 1.4 - Patches
mkdir "%target_directory%\1.4\Patches"
xcopy "1.4\Patches\*.*" "%target_directory%\1.4\Patches" /e
