#region CLASS DOCUMENTATION
/*
 * Project:              DCAD-Tool-Tabs
 * Class:                GISwikiBtn.cs
 * Author:               Craig Browne
 * Date Created:         09/20/2020
 * Application Platform: ArcGIS Pro 2.6 +
 * Revisions:            mm/dd/yyyy -programmer-: Summary of modifications to code or user interface.
 * **************************************************************************************************
 * 
 * Purpose: Button link to Dallas Central Appraisal District's wiki page.
 * 
 * **************************************************************************************************
 */
#endregion

using ArcGIS.Desktop.Framework.Contracts;

namespace DCADGISTools.ButtonLinks
{
    internal class DCADwikiBtn : Button
    {
        protected override void OnClick()
        {
            string wikiPath = @"http://dcadwiki.dcad.org/dcadwiki/CategoryEndUsers";
            System.Diagnostics.Process.Start(wikiPath);
        }
    }
}
