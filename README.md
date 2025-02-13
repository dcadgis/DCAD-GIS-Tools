# DCAD-GIS-Tools
Creates an user specific tab in the ArcGIS Pro ribbon with basic ESRI tools, as well as, an in-house DCAD Query Pane with DCAD Query tools to search on DCAD specific feature attributes.  Tools to be used with the initial launch of the GIS_Map_Viewer_1.0.

# PURPOSE:
There a number of ArcGIS Pro's *out of the box* functions and tools that the user uses more frequently and that are nested in different Ribbon Tabs that the user has to remember and revisit in order to perform their workflow.  This script consolidates ArcGIS Pro's tools and contains DCAD specific tools into one tab to assist the user with their workflow.

## STATUS: 
In Production

## PROGRAMMING:
Language(s)- C# 

SDK- ArcGIS Pro SDK (.NET)

SDK Version- 2.6

## DEPLOYMENT:
Platform- ArcGIS Pro

Application-  Address Data Management

User- GIS Specialists

### How to Deploy to a Local Machine

1. Download the repo as a zip file from github (ver 2.6) DCAD-GIS-Tools located at: https://github.com/dcadgis/DCAD-GIS-Tools
1. Unzip the archive to a location on the local C:\
1. Open the DCAD.MapLayerControl solution and build the solution.
1. Open the DCAD.MapUtilities solution and build the solution.
1. Open the DCADGISTools solution and build the solution.
1. Go find the file named **DCADGISTools.esriAddinX** file and place it in the ```\\dcad.org\GIS\Source\AddinsPro directory```
1. This file will only be about 63 KB and that is ok, it will still work.

## FUNCTIONALITY
The basic ArcGIS Pro functions and tools are no different in the GIS Tools Tab, as they are in their original tab locations in the Ribbon.  The initial launch does have one new DCAD specific tool called the DCAD Query Pane.

### DCAD Query Pane

![DCADGISTools](./Images/locateIcon.png)

This tool opens the DCAD Query Pane dockable window with a combo box, a user text box input and a result, list box for the user to query the DCAD feature classes in the active map.


## DOCUMENTATION:
For user support see DCAD wiki- [ArcGISPro_MapViewer](http://dcadwiki.dcad.org/dcadwiki/ArcGISPro-GIS_Tools_Tab?highlight=%28%5CbEndUsers-ArcGISPro%5Cb%29)

----
## Revision History

|*Date*|*Rev*|*Description*|*Author*|
|------|-----|-------------|--------|
|07/29/21|1.0 |Initial Release |Craig Browne |
|02/13/25|1.1 |How to Deploy |John W. Fell |
