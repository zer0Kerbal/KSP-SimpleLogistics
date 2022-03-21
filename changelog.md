<!-- 
changelog.md v1.1.1.1
modname
created: 01 Aug 2016
updated: 05 Mar 2022

——————————————————————————————————————————————————
——— changelog ————————————————————————————————————
——————————————————————————————————————————————————
-->

# Changelog  
  
| modName    | SimpleLogistics! (SLOG!)                                         |
| ---------- | ---------------------------------------------------------------- |
| license    | GPLv3                                                            |
| author     | Real-Gecko, mwerle and zer0Kerbal                                |
| forum      | (https://forum.kerbalspaceprogram.com/index.php?/topic/191045-*) |
| github     | (https://github.com/zer0Kerbal/zer0Kerbal/SimpleLogistics)       |
| curseforge | (https://www.curseforge.com/kerbal/ksp-mods/SimpleLogistics)     |
| spacedock  | (https://spacedock.info/mod/1079)                                |
| ckan       | SimpleLogistics                                                  |

## Version 2.0.6.0 - for KSP 1.12.3 [20-Mar-2022]

* #9 - PT-BR translation. - contributed by Lisias
* #16 - Merge LGG's change  - contributed by linuxgurugamer
* #17 - Localization - contributed by zer0Kerbal
* #19 - Lgg dev branch - contributed by linuxgurugamer
* #21 - 2.0.4.0 - contributed by zer0Kerbal
* #22 - correction in comments - contributed by zer0Kerbal
* #24 - Version 2.0.5.0 - Routine Maintenance - contributed by zer0Kerbal
* #26 - update new branch before working on it - contributed by zer0Kerbal
* #28 - [ImgBot] Optimize images - contributed by imgbot[bot]
* #35 - upstream refresh from master - contributed by zer0Kerbal
* #7 - Localization
* #14 - add game settings/difficulty setting (options):
* #18 - ability to set in part.cfg

## Version 2.0.5.0 - Routine Maintenance

* 11 Aug 2021
* Released for Kerbal Space Program 1.9.1

### Patch Linting and Maintenance

### Updated

* Corrections
	* `:AFTER[SquadExpansion,SimpleLogistics]` -> `:AFTER[SquadExpansion]`
	* removed [ModuleSolarPanel] from [DeployedSoloarPanel]
	* adjusted [LogisticsModule] in [DeployedRTG] to now include IsActive = True
	* corrected isActive to IsActive
* Add
	* [ModuleGenerator] to  [DeployedSoloarPanel]
	* [IsActive = False] to [CommandLogistics.cfg] patches
* Remove
	* duplicate patch [Lights.cfg] from [KAS-PortPylonPwr.cfg]

### Backstage

* Updated
	* backend automation
	* Readme.md
	* version.md template
	* removed [assets/] folder and cleaned up the [Localization/] folder

### Status

* Working:
	* Vessel <Situations> limitations
	* Vessel <Control> limitations
* Not Working:
	* range limitations

* closes #24 - Version 2.0.5.0 - Routine Maintenance - contributed by zer0Kerbal

## Version 2.0.4.0 - New Situations and New Versions

* 05 Apr 2020
* compile for KSP 1.9.1
* Special Thank you to @LinuxGuruGamer for all his assistance and patience.

* CODE
	* remove old GUI code
	* updated PAW to use grouping, included .dll version in label
	* [IsActive = True] *should* now be working from within part.module

* UI
	* removed old pastel skin
	* added option in game.settings to switch between Unity and KSP
	* thank you to @LinuxGuruGamer

* Features, Feature, and MORE Features
	* Working:
	* Vessel <Situations> limitations
	* Vessel <Control> limitations
	* Not Working:
	* range limitations

### Status

* closes #19 - Lgg dev branch - contributed by linuxgurugamer
* closes #21 - 2.0.4.0 - contributed by zer0Kerbal
* closes #22 - correction in comments - contributed by zer0Kerbal
* closes #7 - Localization
* closes #14 - add game settings/difficulty setting (options):
* closes #18 - ability to set in part.cfg


## Version 2.0.3.2 - Deployable Serenity

* thank you to forum users @Buzz light fear, @Cavscout74 for the idea!
* for those who have the DLC Breaking Ground installed there is a new feature for two Serenity parts
* the DeployedRTG and the DeployedSolarPanel can now be plugged into a SimpleLogistic's network

### Mini-NUK-PD Radioisotope Thermoelectric Generator  (DeployedRTG)

	*     all the normal rules for RTG apply
	*     produces .50 EC/s
	*     produces heat like stock RTG
	*     has a battery of 25 EC units

### OX-Stat-PD Photovoltaic Panel (DeployedSolarPanel)

	* produces .15 (max) EC/s
	* has a battery of 25 EC units
	* needs sunlight
	* all the normal rules for solar apply
	* range of inclusion in SimpleLogistic Network - currently ~2.4km, can always use relays
	* after deploying, must manually 'plug-in' to the SimpleLogistics network.
	* existing rtg/solarpanels might not work - might have to pick them up (using RMB/PAW) and redeploying them again
	* there is a limitation, SimpleLogistics currently just averages out resources over the network. Future plans include being able to set a vessel to 'pump', 'store', 'suck' (priorities).

### Add SimpleLogistics! to KAS PrtPylonPwer (so anything attached can draw power)

	* Thank you to leatherneck6017 for this idea.
	* add KAS-PortPylonPwr.cfg
	* experimental: IsActive = True
	* FUTURE: need to expand to anything that has cck-lights, including deployable-lights and tracking lights.
	* range of inclusion in SimpleLogistic Network - currently ~2.4km, can always use relays
	* after deploying, must manually 'plug-in' to the SimpleLogistics network.
	* existing rtg/solarpanels might not work - might have to pick them up (using RMB/PAW) and redeploying them again
	* there is a limitation, SimpleLogistics currently just averages out resources over the network. Future plans include being able to set a vessel to 'pump', 'store', 'suck' (priorities).

### Code and Code Related

	* added Version.tt and AssemblyVersion.tt automation
	* moved AssemblyVersion.tt/.cs into Properties/
	* updated to v2 of InstallChecker.cs
	* moved Textures/ -> Plugins/Textures/

### Deployment and Backend

	* update changelog to include new Kerbal Changelog features
	* updated _deploy and _buildRelease
	* automated Readme.md -> Readme.htm 
	* Readme.htm now included in releae
	* CONTRIBUTING.md now included in repository
	* updated .version to be avc compliant
	* added avc github checker and badge
	* updated .gitattributes .gitignore
	* added json's
	* updated / modernized .csproj
	* updated Readme.md
	* updated Releases.layout.md

## Version 2.0.3.1 - the LGG update - 연결 되었습니까? Collegato?

* Toolbarcontroller and Clickthroughblocker are now dependencies
* Now supports Toolbar (Blizzy's) (optional)

### Updated

	* toolbars and icons
	* Restored all PNG textures because the code was loading files with the png name (thank you linuxgurugamer)
	* updated palette.cs from png -> dds (thank you linuxgurugamer)
	* Restored all PNG textures because the the GUI was pink. SOMEBODY went to town a little hard....
	* updated palette.cs from dds -> png

### Code

* Added 
	* ClickThroughBlocker code (thank you linuxgurugamer)
	* ToolbarController code (thank you linuxgurugamer)
	* started adding in game settings page. Currently it does nothing. Bill is wondering why.
* Commented out
	* all stock toolbar code (thank you linuxgurugamer)
	* all Blizzy toolbar code (thank you linuxgurugamer)
	* entire ToolbarWrapper.cs file (thank you linuxgurugamer)

### Localization

* 물류를 사용하려면 상륙해야합니다
* Added localization for Korean (ko.cfg) (translate.google.com)
* Deve essere atterrato per utilizzare la logistica
* Added localization for Italian (it-it.cfg) (translate.google.com)
* SimpleLogistics is a polyglot! Twelve languages (Ch-De-En-Es-Fr-It-Jp-Ko-Ru-Pt-Sw-No)

### Status

* closes #4 - Added Toolbarcontroller and Clickthroughblocker - contributed by linuxgurugamer
* closes #5 - 2.0.3.1 - contributed by zer0Kerbal
* closes #6 - Create es-es.cfg - contributed by Fitiales
* closes #8 - Create ru.txt - contributed by zer0Kerbal
* closes #9 - PT-BR translation. - contributed by Lisias
* closes #16 - Merge LGG's change  - contributed by linuxgurugamer
* closes #17 - Localization - contributed by zer0Kerbal

## Version 2.0.3.0.4 - Подключен? - Conectado? - Ansluten? - Tilkoblet?

### Localization

* Аппарат должен быть приземлён чтобы использовать Logistics
* Added localization for Russian (ru.cfg) (thank you @malanok1)
* Logística disponível apenas quando em pouso
* Added localization for Brazilian Portuguese (pt-br.cfg) (thank you @lisias)
* Måste landas för att använda logistik
* Added localization for Swedish (sw-sw.cfg) (translate.google.com)
* Må landes for å bruke logistikk
* Added localization for Norwegian (no-no.cfg) (translate.google.com)
* SimpleLogistics now speaks ten languages (Ch-De-En-Es-Fr-Jp-Ru-Pt-Sw-No)

## Version 2.0.3.0.3 - Verbunden? - Branché? - プラグインしましたか？

### Localization

* Netzwerk für Logistics
* Added localization for German (de-de.cfg) (thank you @malanok1)
* Réseau logistique
* Added localization for French (fr-fr.cfg) (translate.google.com)
* 物流ネットワーク
* Added localization for Japanese (jp.cfg) (translate.google.com)
* SimpleLogistics now speaks six languages (Ch-De-En-Es-Fr-Jp)

## Version 2.0.3.0.2 - ¿Conectado?

### Localization

* Red de logística simple
* Added localization for Spanish (es-es.cfg) (thank you @Fitiales)
* SimpleLogistics now speaks three languages (En-Ch-Es)

## Version 2.0.3.0.1 - So that's where the extra nut goes!

* 01 Feb 2020

### Code

* corrected localization code for vessel name (thank you linuxgurugamer)
* Added a Localizer.Format to the GUI where the vessel name was being shown (thank you linuxgurugamer)
* fixed missing textures for buttons 

## Version 2.0.3.0 - Now with more grease

* 16 Jan 2020

### Code

* updated to KSP 1.8.1
* updated to .NET 4.8
* updated to Unity 2019.2.2f1

### Compatibility

* update patch to include ":NEEDS"

### Social Media

* added to SpaceDock, updated CKAN (thank you linuxgurugamer)

### Localization

* Chinese localization by tinygrox. Thank you.
* Localization code provided by tinygrox. Thank you.

### Status

* closes #2 - 2.0.2.4 - contributed by zer0Kerbal
* closes #3 - 2.0.3.0 - contributed by zer0Kerbal

## Version 2.0.2.4 - Restocked

### Updated

* [CommandLogistics.cfg] patch file

### Behind the scenes

* updated to automated build process (deploy.bat/buildRelease.bat/AssemblyVersion.tt)
* renamed source code repository from /SimpleLogistics/ to /Source/

## Version 2.0.2.3 - Toolbar update

* 23 Sep 2019

### Code

* clean, update pass
* reformatted changelog into KerbalChangeLog formating

### Updated

* ToolbarWrapper.cs

### Added

* InstallChecker.cs
* AssemblyVersion.tt

### Status

* closes #1 - v.2.0.2.2 - contributed by zer0Kerbal

## Version 2.0.2.2 - KSP 1.7.3

* 23 Sep 2019
* Recompile for KSP 1.7.x

* Non-Code License updated to CC BY-NC-SA 4.0 from GNU(GPL v3)
* moved Changelog into separate file
* zer0Kerbal
* added .zip .rar and .7z (and various other things) to .gitignore

### Status

* closes #1 - v.2.0.2.2 - contributed by zer0Kerbal

## Version 2.0.2.1 - recompile by forum user @Miracle Magician

* Recompile for KSP 1.7.2

## Version 2.0.2.0 - recompile by forum user @Usgiyi

* Recompile for KSP 1.4.5

## Version 2.0.2

* Oct 17, 2017
* Real-Gecko

* Recompile for KSP 1.3.1

## Version 2.0.1

* Nov 24, 2016
* Real-Gecko

* Fixed MM patch
* GNU GENERAL PUBLIC LICENSE : Version 3, 29 June 2007

## 2.0.0

* Nov 24, 2016
* Real-Gecko
 
* Complete overhaul.

## Version 1.0.0

* Initial release
* Nov 20, 2016
* created by Real-Gecko

<!-- CC BY-NC-ND 3.0 Unported zer0Kerbal -->
