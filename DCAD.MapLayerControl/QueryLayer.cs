#region CLASS DOCUMENTATION
/*
 * Project:            DCAD.MapLayerControl (DCADGIS Library)
 * Class:              QueryLayer.cs
 * Author:             Craig Browne
 * Date Created:       07/07/2021
 * Target Application: ArcGIS Pro 2.6 +
 * Revisions:          mm/dd/yyyy -programmer-: Summary of modifications to code or user interface.
 * **************************************************************************************************
 * 
 * Purpose: Builds basic DCAD map layer information that can be used for a DCAD Query tool.
 * 
 * **************************************************************************************************
 */
#endregion

namespace MapLayerControl
{
    public class QueryLayer
    {
        public QueryLayer()
        {

        }

        public QueryLayer(string querySelection)
        {
            QuerySelection = querySelection;
        }

        // the input Query Selection which can come from a query type selected by the user.
        public string QuerySelection { get; set; }
        // A unique id for a map layer in the table of contents.
        public int LayerId { get; set; }

        // The layer name, as it appears in the table of contents.
        public string LayerName { get; set; }
        
        // The map layer's field name that should be queried on.
        public string QueryClause { get; set; }

        // The .sde database that the query should be applied to.
        public string SourceGDB { get; set; }

        // The rows to highlight, if using alternative row colorization in the XAML for a listbox.
        public int QueryRowsReturned { get; set; }
    }
}
