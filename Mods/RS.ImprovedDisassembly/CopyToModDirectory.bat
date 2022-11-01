:: ========= Variables =========

SET mod_name=RS.ImprovedDisassembly
SET target_directory=D:\Games\Steam\steamapps\common\RimWorld\Mods\%mod_name%

:: ========= Copy ==========
 
rd "%target_directory%" /s /q
mkdir "%target_directory%"

:: About
mkdir "%target_directory%\About"
xcopy "About\*.*" "%target_directory%\About" /e

:: 1.3
mkdir "%target_directory%\1.3"
xcopy "1.3\*.*" "%target_directory%\1.3" /e

:: 1.4
mkdir "%target_directory%\1.4"
xcopy "1.4\*.*" "%target_directory%\1.4" /e
