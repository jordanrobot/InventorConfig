Original:
$(ILMergeConsolePath) /target:exe /allowDup /out:$(OutDir)inventor-config.exe $(OutDir)InventorConfigConsole.exe $(OutDir)InventorConfig.dll $(OutDir)CommandLine.dll $(OutDir)Newtonsoft.Json.dll

Console WORKING!
$(SolutionDir)packages\ILRepack.2.0.18\tools\ILRepack.exe /target:exe /out:$(OutDir)inventor-config.exe $(OutDir)inventor-config.exe $(OutDir)InventorConfig.dll $(OutDir)CommandLine.dll $(OutDir)Newtonsoft.Json.dll

GUI WIP - still shows console window!!!:
$(SolutionDir)packages\ILRepack.2.0.18\tools\ILRepack.exe /target:exe /out:$(OutDir)inventor-config-gui.exe $(OutDir)inventor-config-gui.exe $(OutDir)InventorConfig.dll $(OutDir)Newtonsoft.Json.dll

C:\Users\mjordan\_Sync\dev\github\InventorConfig\packages\ILRepack.2.0.18\tools\ILRepack.exe /target:exe /out:inventor-config-gui.exe inventor-config-gui.exe InventorConfig.dll Newtonsoft.Json.dll