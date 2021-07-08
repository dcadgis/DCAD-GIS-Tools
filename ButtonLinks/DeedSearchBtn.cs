#region CLASS DOCUMENTATION
/*
 * Project:              DCAD-Tool-Tabs
 * Class:                DeedSearchBtn.cs
 * Author:               Craig Browne
 * Date Created:         09/20/2020
 * Application Platform: ArcGIS Pro 2.6 +
 * Revisions:            mm/dd/yyyy -programmer-: Summary of modifications to code or user interface.
 * **************************************************************************************************
 * 
 * Purpose: Button link to Dallas County Clerk's property records search.
 * 
 * **************************************************************************************************
 */
#endregion

using ArcGIS.Desktop.Framework.Contracts;

namespace DCADGISTools.ButtonLinks
{
    internal class DeedSearchBtn : Button
    {
        protected override void OnClick()
        {
            // legacy web page address for Dallas County vendor (kofile).
            //string deedPath = @"https://dallastx.clerksearch.kofile.com/48113/Home/Index/1";

            // Dallas County vendor has changed its web page address 5/6/2021
            string deedPath = @"https://dallas.tx.publicsearch.us/";
            System.Diagnostics.Process.Start(deedPath);
        }
    }
}
