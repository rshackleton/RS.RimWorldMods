:: ========= Variables =========

SET mod_name=RS.DynamicPersonaWeapons
SET target_directory=D:\Games\Steam\steamapps\common\RimWorld\Mods\%mod_name%

:: ========= Copy ==========

rd "%target_directory%" /s /q
mkdir "%target_directory%"

:: About
mkdir "%target_directory%\About"
xcopy "About\*.*" "%target_directory%\About" /e

:: 1.4
:: mkdir "%target_directory%\1.4"
:: xcopy "1.4\*.*" "%target_directory%\1.4" /e

:: 1.4 - Assemblies
mkdir "%target_directory%\1.4\Assemblies"
copy "1.4\Assemblies\DeepCloner.dll" "%target_directory%\1.4\Assemblies\DeepCloner.dll"
copy "1.4\Assemblies\RS.DynamicPersonaWeapons.dll" "%target_directory%\1.4\Assemblies\RS.DynamicPersonaWeapons.dll"
copy "1.4\Assemblies\RS.DynamicPersonaWeapons.pdb" "%target_directory%\1.4\Assemblies\RS.DynamicPersonaWeapons.pdb"
