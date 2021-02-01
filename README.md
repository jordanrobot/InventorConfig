## Config Loader

This is a simple config tool for Autodesk Inventor to standardize Inventor options across users' computers. 

It works by editing a select subset of Inventor Application Options from a json configuration file. The thought is that the administrator can configure a json file and deploy it with this tool to the users; ensuring their Inventor configs match the company standards.

### Console application

The console application will have the following options (not all implemented yet):

    --default : use the default "config-loader.json" in the application's current directory
    --dry : dry run of the options?  Would verify that the configuration is valid without changing any inventor options. TBD
    --help : displays a help screen
    --output : create a new json config file based on the current Inventor options
    --path : specifiy the json configuration file, may be absolute or relative to the current working directory
    --version : shows the version of the application

### Json configuration

- This tool uses a json file to specify the configuration to load.
- Empty value strings will be ignored when the config is loaded; that particular setting will not be touched in Inventor.
- Simple for anyone to configure.
- You can create a json configuration from an existing Inventor install.
- You can make multiple json config files with different configurations.
- The current json configuration is as follows:

```json
{   "ConfigName" : "Default Config",
    "UserName" : "",
    "DefaultVBAProjectFileFullFilename" : "%PUBLICDOCUMENTS%/Autodesk/Inventor %RELEASE%/Macros/Default.ivb",
    "TemplatesPath" : "%PUBLICDOCUMENTS%/Autodesk/Inventor %RELEASE%/Templates/%LANGUAGE%/",
    "DesignDataPath" : "%PUBLICDOCUMENTS%/Autodesk/Inventor %RELEASE%/Design Data/",
    "PresetsPath" : "%USERPROFILE%/AppData/Roaming/Autodesk/Inventor %RELEASE%/Presets/",
    "SymbolLibraryPath" : "Z:/Inventor/Symbol Library/",
    "SheetMetalPunchesRootPath" : "Z:/Inventor/Punches/",
    "iFeatureOptionsRootPath" : "%PUBLICDOCUMENTS%/Autodesk/Inventor %RELEASE%/Catalog/",
    "CCAccess" : "Desktop",
    "CCLibraryPath" : "Z:/Inventor/Content Center Libraries/2021/",
    "CCCustomFamilyAsStandard" : true,
    "CCRefreshOutOfDateStandardParts" : true,
    "SectionAllParts" : true,
    "DefaultDrawingFileType" : "idw",
    "ExternalRuleDirectories" : [
        "directory",
        "directory2"
     ]
    }
```
