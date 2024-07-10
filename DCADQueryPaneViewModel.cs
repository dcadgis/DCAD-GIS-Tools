using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using System.Windows.Input;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DCADGISTools
{
    internal class DCADQueryPaneViewModel : DockPane
    {
        private const string _dockPaneID = "DCADGISTools_DCADQueryPane";

        protected DCADQueryPaneViewModel() { }

        /// <summary>
        /// Show the DockPane.
        /// </summary>
        internal static void Show()
        {
#pragma warning disable CA1416 // Validate platform compatibility
            DockPane pane = FrameworkApplication.DockPaneManager.Find(_dockPaneID);
#pragma warning restore CA1416 // Validate platform compatibility
            if (pane == null)
                return;

#pragma warning disable CA1416 // Validate platform compatibility
            pane.Activate();
#pragma warning restore CA1416 // Validate platform compatibility
        }

        #region esri ClearSelectionTool button binding.
#pragma warning disable CA1416 // Validate platform compatibility
        public ICommand ClearSelectionTool { get { return FrameworkApplication.GetPlugInWrapper("esri_mapping_clearSelectionButton") as ICommand; } }
#pragma warning restore CA1416 // Validate platform compatibility
        #endregion

        /// <summary>
        /// Text shown near the top of the DockPane.
        /// </summary>
        private string _heading = "DCAD Query Tool";
        public string Heading
        {
            get { return _heading; }
            set
            {
#pragma warning disable CA1416 // Validate platform compatibility
                SetProperty(ref _heading, value, () => Heading);
#pragma warning restore CA1416 // Validate platform compatibility
            }
        }
    }

    /// <summary>
    /// Button implementation to show the DockPane.
    /// </summary>
    internal class DCADQueryPane_ShowButton : Button
    {
        protected override void OnClick()
        {
            DCADQueryPaneViewModel.Show();
        }
    }
}
