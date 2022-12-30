---
permalink: /ManualInstallation.html
title: ManualInstallation
description: the flat-pack Kiea instructions, written in Kerbalese, unusally present
# layout: bare
tags: installation,directions,page,kerbal,ksp,zer0Kerbal,zedK
---

<!-- ManualInstallation.md v1.1.1.0
SimpleLogistics! (SLOG)
created: 01 Oct 2019
updated: 27 Mar 2022 -->

<!-- based upon work by Lisias -->

# SimpleLogistics! (SLOG)

A SimpleCo add-on for sharing resources between vessels within physics range for Kerbal Space Program.

## Installation Instructions

### Using CurseForge/OverWolf app or CKAN

You should be all good! (check for latest version on CurseForge)

### If Downloaded from CurseForge/OverWolf manual download

To install, place the GameData folder inside your Kerbal Space Program folder:

* **REMOVE ANY OLD VERSIONS OF THE PRODUCT BEFORE INSTALLING**, including any other fork:
  * )optional( back up `<KSP_ROOT>/GameData/SimpleLogistics/Plugins/PluginData/SimpleLogistics` and elsewhere you have saved SimpleLogistics
  * Delete `<KSP_ROOT>/GameData/SimpleLogistics`
* Extract the package's `SimpleLogistics/` folder into your KSP's as follows:
  * `<PACKAGE>/SimpleLogistics` --> `<KSP_ROOT>/GameData/SimpleLogistics`
    * Overwrite any preexisting file.
    * )optional( restore from backup to `<KSP_ROOT>/GameData/SimpleLogistics/Plugins/PluginData/SimpleLogistics`

### If Downloaded from SpaceDock / GitHub / other

To install, place the GameData folder inside your Kerbal Space Program folder:

* **REMOVE ANY OLD VERSIONS OF THE PRODUCT BEFORE INSTALLING**, including any other fork:
  * )optional( back up `<KSP_ROOT>/GameData/SimpleLogistics/Plugins/PluginData/SimpleLogistics` and elsewhere you have saved SimpleLogistics
  * Delete `<KSP_ROOT>/GameData/SimpleLogistics`
* Extract the package's `GameData/` folder into your KSP's as follows:
  * `<PACKAGE>/GameData/SimpleLogistics` --> `<KSP_ROOT>/GameData`
    * Overwrite any preexisting file.
    * )optional( restore from backup to `<KSP_ROOT>/GameData/SimpleLogistics/Plugins/PluginData/SimpleLogistics`

## The following file layout must be present after installation

```
<KSP_ROOT>
  [GameData]
    [SimpleLogistics]
      [Compatibility]
        ...
      [Localization]
        ...
      [Plugins]
        ...
        [Logs]
          ...
        [Plugin Data]
          ...
          [SimpleLogistics]
            config.xml
        [Textures]
          ...
      2.0.5.0.htm
      changelog.md
      GPLv3.txt
      SimpleLogistics.version
      readme.htm
    ...
  KSP.log
  ...
```

### Dependencies

* [Module Manager /L][mm]
* [ToolbarController][thread:tbc]

[thread:mm]:  https://github.com/net-lisias-ksp/ModuleManager "Module Manager"
[thread:tbc]: https://forum.kerbalspaceprogram.com/index.php?/topic/169509-* "ToolbarController"
