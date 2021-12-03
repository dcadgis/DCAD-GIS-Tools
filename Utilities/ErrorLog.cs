#region CLASS DOCUMENTATION
/*
 * Project:            DCAD.MapUtilities (DCADGIS Library)
 * Class:              ErrorLog.cs
 * Author:             Craig Browne
 * Date Created:       07/07/2021
 * Target Application: ArcGIS Pro 2.6 +
 * Dependency:         None
 * Revisions:          mm/dd/yyyy -programmer-: Summary of modifications to code or user interface.
 * **************************************************************************************************
 * 
 * Purpose: An error log that populates a user's desktop machine's Event Log with any errors that may
 * occur from using the DCAD GIS application.
 * 
 * **************************************************************************************************
 */
#endregion

using System;
using System.Diagnostics;
using System.Text;

namespace DCADGISTools.Utilities
{
    public class ErrorLog
    {
        /// <summary>
        /// Uses StringBuilder and Exception properties to populate the user's event log viewer.
        /// If multiple exceptions exist, appends them to the log viewer event log.
        /// </summary>
        /// <param name="exception"></param>
        public static void Log(Exception exception)
        {
            // Use StringBuilder to build the log's message
            StringBuilder sbExMsg = new StringBuilder();

            // Use Exception's properties to get the Exception Name, 
            // the Exception message & the StackTrace (line #) of the issue.
            sbExMsg.Append("Application Feature: Find Account Pane \n \n");
            sbExMsg.Append("Exception Type \n");
            sbExMsg.Append("\n");
            sbExMsg.Append(exception.GetType().Name);
            sbExMsg.Append("\n \n");
            sbExMsg.Append("Message \n");
            sbExMsg.Append(exception.Message + "\n \n");
            sbExMsg.Append("Stack Trace \n");
            sbExMsg.Append(exception.StackTrace + "\n");

            // Create a variable to hold previous Exception if more than one exists.
            Exception innerException = exception.InnerException;

            // Append log with info from each Exception found.
            while (innerException != null)
            {
                sbExMsg.Append("Exception Type \n");
                sbExMsg.Append("\n");
                sbExMsg.Append(innerException.GetType().Name);
                sbExMsg.Append("\n \n");
                sbExMsg.Append("Message \n");
                sbExMsg.Append(innerException.Message + "\n \n");
                sbExMsg.Append("Stack Trace \n");
                sbExMsg.Append(innerException.StackTrace + "\n");
            }

            // Check to ensure that source exists.
            if (EventLog.SourceExists("Application"))
            {
                EventLog log = new EventLog("Application");
                log.Source = "Application";
                log.WriteEntry(sbExMsg.ToString(), EventLogEntryType.Error);
            }
        }
    }
}
