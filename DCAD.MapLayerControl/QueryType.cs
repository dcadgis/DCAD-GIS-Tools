#region CLASS DOCUMENTATION
/*
 * Project:            DCAD.MapLayerControl (DCADGIS Library)
 * Class:              QueryTypes.cs
 * Author:             Craig Browne
 * Date Created:       07/07/2021
 * Target Application: ArcGIS Pro 2.6 +
 * Dependency:         QueryLayerRepository.cs
 * Revisions:          mm/dd/yyyy -programmer-: Summary of modifications to code or user interface.
 * **************************************************************************************************
 * 
 * Purpose: Creates a dictionary of the query types that the user can query for and the map layers that the
 * query will be applied on.  The dictionary is used to help populate a query combo box.  A query type and
 * map layer can be added for expanding what the user can query for (make sure to update QueryLayerRepository.cs
 * with the properties needed, for any new query types, as well).
 * 
 * **************************************************************************************************
 */
#endregion

using System.Collections.Generic;


namespace MapLayerControl
{
    public class QueryType
    {
        /// <summary>
        /// Dictionary with two strings (1 for key & 1 for value). The key is the query Type for the drop down box, 
        /// while the value is the TOC layer needed for the query.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetQueryType()
        {
            Dictionary<string, string> queryType = new Dictionary<string, string>();

            queryType.Add("Account Number", "PARCEL_VIEW");
            queryType.Add("Owner Name", "PARCEL_VIEW");
            queryType.Add("Legal Line 1", "PARCEL_VIEW");
            queryType.Add("Dallas City Block", "Block");
            queryType.Add("Subd/Condo Name", "SimultaneousConveyance");
            queryType.Add("Subd/Condo Document Id", "SimultaneousConveyance");
            queryType.Add("Fabric Tax Parcel", "Parcels");

            return queryType;
        }
    }
}
