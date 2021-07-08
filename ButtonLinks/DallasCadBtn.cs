#region CLASS DOCUMENTATION
/*
 * Project:              DCAD-Tool-Tabs
 * Class:                DallasCadBtn.cs
 * Author:               Craig Browne
 * Date Created:         09/20/2020
 * Application Platform: ArcGIS Pro 2.6 +
 * Revisions:            mm/dd/yyyy -programmer-: Summary of modifications to code or user interface.
 * **************************************************************************************************
 * 
 * Purpose: Button link to Dallas Central Appraisal District's main website.
 * 
 * **************************************************************************************************
 */
#endregion

using ArcGIS.Desktop.Framework.Contracts;

namespace DCADGISTools.ButtonLinks
{
    internal class DallasCadBtn : Button
    {
        #region Declared Variables
        internal static string cadPath = @"http://dallascad.org/AcctDetail.aspx?ID=";
        internal static string dpmPath = @"https://maps.dcad.org/prd/dpm/?parcelid=";
        #endregion
        protected override void OnClick()
        {
            string dallascadPath = @"http://www.dallascad.org";
            System.Diagnostics.Process.Start(dallascadPath);
        }
    }
}
