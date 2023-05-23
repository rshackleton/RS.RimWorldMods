:: ========= Variables =========

SET mod_name=RS.ResearchPreReqTweaks
SET target_directory=D:\Games\Steam\steamapps\common\RimWorld\Mods\%mod_name%

:: ========= Copy ==========

rd "%target_directory%" /s /q
mkdir "%target_directory%"

:: About
mkdir "%target_directory%\About"
xcopy "About\*.*" "%target_directory%\About" /e

:: 1.4
mkdir "%target_directory%\1.4"
xcopy "1.4\*.*" "%target_directory%\1.4" /e

:: 1.4 - Assemblies
:: mkdir "%target_directory%\1.4\Assemblies"
:: copy "1.4\Assemblies\RS.ResearchPreReqTweaks.dll" "%target_directory%\1.4\Assemblies\RS.ResearchPreReqTweaks.dll"
:: copy "1.4\Assemblies\RS.ResearchPreReqTweaks.pdb" "%target_directory%\1.4\Assemblies\RS.ResearchPreReqTweaks.pdb"
