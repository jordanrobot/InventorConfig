## Config Loader

This is a simple config tool for Autodesk Inventor to standardize Inventor options across users' computers. 

It works by editing a select subset of Inventor Application Options from a JSON configuration file. The thought is that the administrator can configure a json file and deploy it with this tool to the users; ensuring their Inventor configs match the company standards.

## How to Run

1. Download the binary from the Releases page, or download and build from source.
2. Modify a config-loader.json file, and place it in the same directory.

There are three options after this:

3. Double-click the config-loader.exe file to run in place,
4. Run in a command line with ```./config-loader.exe``` with the various options shown below, or
5. Copy the config-loader.exe file into a directory in your PATH.  Then run from a command line with the command ```config-loader```.

## Detailed Usage

The basic usage will let you load a default json configuration file into Inventor.  The default configuration file needs to be named ```config-loader.json```; this file should be located in current working directory (if using the command line).  To load the configuration using the command line, type the following command at a command line prompt (powershell, cmd, etc).

    config-loader
 
You can also double-click the config-loader.exe (if a ```config-loadeer.json``` file is present in the same directory).

To load a user-supplied JSON configuration file into Inventor (say the config file is called ```your-config.json```), issue the command as shown:

    config-loader --path your-config.json

Write a JSON configuration file from Inventor (NOT YET IMPLEMENTED):

    config-loader --output your-config.json

To verify a JSON configuration file's syntax is formatted correctly, issue the following command:

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

- This tool uses a JSON file to specify the configuration to load.
- Empty value strings will be ignored when the config is loaded; that particular setting will not be touched in Inventor.
- Simple to edit and push to users machines.
- You can create a JSON configuration from an existing Inventor install.
- You can create multiple JSON config files with different configurations.
- The current JSON configuration spec is as follows:

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
    "CleanExternalRuleDirectories" : false,
    "ExternalRuleDirectories" : [
        "directory",
        "directory2"
     ]
    }
```

Where:

- CCAccess = ```Desktop``` or ```Vault```
- DefaultDrawingFileType = ```idw``` or ```dwg```
- CleanExternalRuleDirectories = ```true``` or ```false```
  - Will remove all existing ExternalRuleDirectories before adding those you list
- Paths should use either of the following directory seperators: ```/``` or ```\\```.  Using the ```\``` character will throw an error as it is a JSON escape character.
