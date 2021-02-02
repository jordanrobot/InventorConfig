## Config Loader

This is a simple config tool for Autodesk Inventor to standardize Inventor options across users' computers. 

It works by editing a select subset of Inventor Application Options from a json configuration file. The thought is that the administrator can configure a json file and deploy it with this tool to the users; ensuring their Inventor configs match the company standards.

## How to Use

Load a default json configuration file into Inventor (file located in current working directory):

    config-loader

Load a json configuration file into Inventor:

    config-loader --path your-config.json

Write a json configuration file from Inventor (NOT YET IMPLEMENTED):

    config-loader --output your-config.json

Verify a json configuration file is formatted correctly:

    config-loader --path your-config.json --test

### Command Line Options

<dl>
  <dt>--path</dt>
  <dd>Specify a filename of a configuration json file to load into Inventor.  May be a relative or absolute path.</dd>
  
  <dt>--test</dt>
  <dd>Test json configuration file for valid format - Inventor settings will not be modified.</dd>
  
  <dt>--output</dt>
  <dd>FUTURE FEATURE: Specify a destination filename; writes a json file of the existing Inventor application configuration.  May be a relative or absolute path.</dd>
  
  <dt>--help</dt>
  <dd>Display this help screen.</dd>
  
  <dt>--version</dt>
  <dd>Display version information.</dd>

## Json Configuration File

- This tool uses a json file to specify the configuration to load.
- Empty value strings will be ignored when the config is loaded; that particular setting will not be touched in Inventor.
- Simple to edit and push to users machines.
- You can create a json configuration from an existing Inventor install.
- You can create multiple json config files with different configurations.
- The current json configuration spec is as follows:

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

Where:

- CCAccess = ```Desktop``` or ```Vault```
- DefaultDrawingFileType = ```idw``` or ```dwg```
