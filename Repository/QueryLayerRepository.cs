using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCADGISTools.Repository
{
    public class QueryLayerRepository
    {
        public static QueryLayer RetrieveQueryLayer(string querySelection)
        {
            #region QUERY TYPE VARIABLES
            string acct = "Account Number";
            string ownr = "Owner Name";
            string lgal = "Legal Line 1";
            string blok = "Dallas City Block";
            string subn = "Subd/Condo Name";
            string subd = "Subd/Condo Document Id";
            string ftax = "Fabric Tax Parcel";
            #endregion

            QueryLayer layerParams = new QueryLayer(querySelection);

            if (querySelection == acct)
            {
                // parameters for querying PARCEL_VIEW to return the account number
                layerParams.LayerId = 1;
                layerParams.LayerName = "PARCEL_VIEW";
                layerParams.QueryClause = "Account_Num";
                layerParams.SourceGDB = "GPUB";
                layerParams.QueryRowsReturned = 4;
            }
            else if (querySelection == ownr)
            {
                // parameters for querying PARCEL_VIEW to return the owner name
                layerParams.LayerId = 2;
                layerParams.LayerName = "PARCEL_VIEW";
                layerParams.QueryClause = "Account_Num";
                layerParams.SourceGDB = "GPUB";
                layerParams.QueryRowsReturned = 4;
            }
            else if (querySelection == lgal)
            {
                // parameters for querying PARCEL_VIEW to return the legal line one.
                layerParams.LayerId = 3;
                layerParams.LayerName = "PARCEL_VIEW";
                layerParams.QueryClause = "Account_Num";
                layerParams.SourceGDB = "GPUB";
                layerParams.QueryRowsReturned = 4;
            }
            else if (querySelection == blok)
            {
                // parameters for querying Block to return the Dallas city block.
                layerParams.LayerId = 4;
                layerParams.LayerName = "Block";
                layerParams.QueryClause = "BLKDSGNTR";
                layerParams.SourceGDB = "GPUB";
                layerParams.QueryRowsReturned = 1;
            }
            else if (querySelection == subn)
            {
                // parameters for querying SimultaneousConveyance to return the Subd/Condo Name
                layerParams.LayerId = 5;
                layerParams.LayerName = "SimultaneousConveyance";
                layerParams.QueryClause = "CNVYNAME";
                layerParams.SourceGDB = "GPUB";
                layerParams.QueryRowsReturned = 3;
            }
            else if (querySelection == subd)
            {
                // parameters for querying SimultaneousConveyance to return the Subd/Condo source document ID number
                layerParams.LayerId = 6;
                layerParams.LayerName = "SimultaneousConveyance";
                layerParams.QueryClause = "CNVYNAME";
                layerParams.SourceGDB = "GPUB";
                layerParams.QueryRowsReturned = 3;
            }
            else if (querySelection == ftax)
            {
                // parameters for querying Parcel Fabric to return the GIS Fabric account number (PID)
                layerParams.LayerId = 7;
                layerParams.LayerName = "Parcels";
                layerParams.QueryClause = "Name";
                layerParams.SourceGDB = "GEDT";
                layerParams.QueryRowsReturned = 1;
            }

            return layerParams;
        }
    }

}