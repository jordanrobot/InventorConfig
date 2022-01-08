<a name="2.2.0"></a>
## [v2.2.0](https://github.com/jordanrobot/InventorConfig/releases/tag/v2.2.0) (2022-01-07)

### Added

* Add icon.
* GUI application (inventor-config-gui.exe)
* Add ControlShortcuts to specify custom command shortcuts within inventor
* Config keywords are no longer required.  I.e. you can include as many or as little config keywords and the command will work all the same; it will ignore missing keywords.
* Empty array values are now ignored in configs.
* Command Line: --path and --option switches now have short switches (-p and -o, respectively).

### Fixed

- Added conventional commits keywords to gitversion configuration.
- Improved user feedback in inventor-config-gui
- Cleaned up nuget packages.

<a name="2.1.0"></a>
## [2.1.0](https://github.com/jordanrobot/config-loader/compare/v2.0.0...2.1.0) (2021-02-13)

### Chore

* updated social image for rename

### Docs

* updated ProjectFiles in samples
* Updated changelog

### Feat

* add ProjectFiles
* Added %USERNAME% and %INVUSERNAME% parsing


<a name="v2.0.0"></a>
## [v2.0.0](https://github.com/jordanrobot/config-loader/compare/v1.1.0...v2.0.0) (2021-02-08)

### Docs

* removed reference to --output as Future Feat
* updated readme, json spec
* Added changelog

### BREAKING CHANGE


The console application was renamed to inventor-config


<a name="v1.1.0"></a>
## [v1.1.0](https://github.com/jordanrobot/config-loader/compare/v1.0.0...v1.1.0) (2021-02-06)

### Chore

* reordered "CleanExternalRuleDirectories" in Configuation object.

### Docs

* added git-chglog configuration
* added --output to readme.md

### Feat

* Added --output option

### Refactor

* Configuration.Apply renamed to Configuration.LoadConfigurationIntoInventor
* improved variable and method names in ConfigEngine


<a name="v1.0.0"></a>
## v1.0.0 (2021-02-04)

### Chore

* removed references to --output option until it is released.
* reorged src directory

### Docs

* Created sample configs.

### Feat

* added better error handling for loading string options into Inventor
* Started to add SetRuleDirectories --> broken cannot load ilogic.Core

### Feature

* Active instance of Inventor is now used if available

### Fix

* Config was not updating in Inventor. Fixed with Action<T> calls.

### Refactor

* embedded types

