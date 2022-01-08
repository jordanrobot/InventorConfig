## Inventor Config

This is a simple config tool for Autodesk Inventor to standardize Inventor options across users' computers. 

It works by editing a select subset of Inventor Application Options from a JSON configuration file. The thought is that the administrator can create a json configuration file and deploy the configuration to the users. This will ensuring their Inventor configs match the company standards.  You can also use it to back-up and quickly change some settings and command shortcuts.

There are two versions, the gui tool (inventor-config-gui.exe), and a command-line tool (inventor-config.exe).

![GUI Screenshot](https://github.com/jordanrobot/InventorConfig/blob/master/images/gui-screenshot.png)

## How to Run

1. Download the binary from the Releases page, or download and build from source.
2. Modify a default.json [configuration] file to suit your needs.


Command-line tool:

3. Double-click the inventor-config.exe file to run in place.  A default.json [configuration] file must be in the same directory as the inventor-config executable. Or
4. Run in a command line with ```./inventor-config.exe``` with the various options shown below, or
5. Copy the inventor-config.exe file into a directory in your PATH.  Then run from a command line with the command ```inventor-config```.

GUI tool:

6. Open the application, select a file, and click "Apply", or
7. click on "Save New Config", select a new file name and location.

## Detailed Command-Line Usage

The basic usage will let you load a default json configuration file into Inventor.  The default configuration file needs to be named ```default.json```; this file should be located in current working directory (if using the command line).  To load the configuration using the command line, type the following command at a command line prompt (powershell, cmd, etc).

    inventor-config
 
You can also double-click the inventor-config.exe (if a ```default.json``` file is present in the same directory).

To load a user-supplied JSON configuration file into Inventor (say the config file is called ```default.json```), issue the command as shown:

    inventor-config -p your-config.json

To create a JSON configuration file from Inventor's current options, issue the following command:

    inventor-config -o your-config.json

To verify a JSON configuration file's syntax is formatted correctly, issue the following command:

    inventor-config -p your-config.json --test

### Command Line Options

<dl>
  <dt>--path (-p)</dt>
  <dd>Specify a filename of a configuration json file to load into Inventor.  May be a relative or absolute path.</dd>
  
  <dt>--test</dt>
  <dd>Test json configuration file for valid format - Inventor settings will not be modified.</dd>
  
  <dt>--output (-o)</dt>
  <dd>Specify a destination filename; writes a json file of the existing Inventor application configuration.  May be a relative or absolute path.</dd>
  
  <dt>--help</dt>
  <dd>Display this help screen.</dd>
  
  <dt>--version</dt>
  <dd>Display version information.</dd>

## Json Configuration Spec

- This tool uses a JSON file to specify the configuration to load.
- Empty value strings will be ignored when the config is loaded; that particular setting will not be touched in Inventor.
- You can create a JSON configuration from an existing Inventor install using the --output option.
- You can create multiple JSON config files, each with a different configuration.
- Paths should use either of the following directory separators: ```/``` or ```\\```.  (Using the ```\``` character will throw an error as it is a JSON escape character.)
- The following variables may be used in paths:
  - %PUBLICDOCUMENTS%
  - %USERPROFILE%
  - %RELEASE% (As in "Inventor %RELEASE%" = "Inventor 2021")
  - %LANGUAGE%
  - %USERNAME%
  - %INVUSERNAME% (Inventor Options User Name)

### Example Config

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
     ],
     "ProjectFiles": [
        "%PUBLICDOCUMENTS%\\Autodesk\\Inventor 2021\\Default.ipj"
     ],
    "ControlShortcuts": {
        "AppApplicationOptionsCmd": "OP",
        "AppBottomViewCmd": "BBB",
        "AppCustomizeCmd": "CUI",
        "AppFileCloseCmd": "CLOSE"
    }
    }
```

### JSON Configuration Key/Values

|Option|Value Type|Description|
|------|-------|-------|
|ConfigName|string|A user name for the configuration.  This is not used by Inventor at all; it is provided for the people editing the configuration.|
|UserName|string|The User Name used by Inventor.|
|DefaultVBAProjectFileFullFilename|string|The default VBA project location.  Must be a valid file location.|
|TemplatesPath|string|Default template folder.|
|DesignDataPath|string|Default design data folder.|
|PresetsPath|string|User preset folder.|
|SymbolLibraryPath|string|Symbol library folder.|
|SheetMetalPunchesRootPath|string|Sheet metal punches iFeatures folder.|
|iFeatureOptionsRootPath|string|iFeatures folder.|
|CCAccess|string|Content Center access type. Valid values are "desktop" or "vault".|
|CCLibraryPath|string|Content Center libraries path.|
|CCCustomFamilyAsStandard|boolean|Controls if Content Center parts are placed as custom parts or standard parts.|
|CCRefreshOutOfDateStandardParts|boolean|Controls if Content Center parts are automatically refreshed when placed.|
|SectionAllParts|boolean|Controls if all parts are sectioned in drawing section views.|
|DefaultDrawingFileType|string|Controls the default drawing file format.  Valid values are "idw" or "dwg".|
|CleanExternalRuleDirectories|boolean|If this is set to true, the existing ExternalRuleDirectories will be deleted before applying the ones specified below.|
|ExternalRuleDirectories|array|Directories to search for iLogic rules.|
|ProjectFiles|array|Project files to load into Inventor's list of projects.|
|ControlShortcuts|Dictionary<string, string>|Inventor command keyboard shortcuts.|

### Build

InventorConfig (command-line) dependencies:

* Autodesk Inventor installation (2021 tested)
* Newtonsoft.Json (Nuget)
* Microsoft.CSharp (Nuget)
* CommandLineParser (Nuget)

Nuke build dependencies:

* GitVersion.CommandLine (Nuget)
* Nuke.Common (Nuget)

To build, enter the following command in powershell:

* `nuke compile`

The inventor-config.exe will be placed in the `artifacts/` directory