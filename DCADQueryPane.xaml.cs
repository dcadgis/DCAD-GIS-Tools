using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Core.Data;
using MapLayerControl;
using DCAD.MapUtilities;

namespace DCADGISTools
{
    /// <summary>
    /// Interaction logic for DCADQueryPaneView.xaml
    /// </summary>
    public partial class DCADQueryPaneView : UserControl
    {
        #region Initialize Command
        public DCADQueryPaneView()
        {
            InitializeComponent();
        }
        #endregion

        #region DECLARED VARIABLES
        internal static int qId = 0;
        internal static string qLayer = "";
        internal static string qClause = "";
        internal static string qGDB = "";
        internal static string userInput = "";
        internal static string queryInput;
        #endregion 

        #region COMBOBOX Dropdown controls
        private void QueryLayerInput_DropDownOpened(object sender, EventArgs e)
        {
            // Clear the items in the drop down list.
            QueryLayerInput.Items.Clear();

            // Get the count of the items in drop down list (should now be 0)
            var validLayerCount = QueryLayerInput.Items.Count;

            // Get the query types available for the drop down list
            Dictionary<string, string> queryType = QueryType.GetQueryType();

            if (validLayerCount <= 0)
            {
                foreach (KeyValuePair<string, string> i in queryType)
                {
                    // Confirm that the layer needed for the query is in TOC
                    var validTOCLayer = MapManagement.ConfirmTOCLayer(i.Value);

                    if (validTOCLayer == true)
                    {
                        // Add the query type to the drop down box.
                        QueryLayerInput.Items.Add(i.Key);
                    }
                }
            }


        }

        private void QueryLayerInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (QueryLayerInput.SelectedIndex >= 0)
            {
                resultBox.Items.Clear();

                var querySelection = QueryLayerInput.SelectedItem.ToString();

                var query = QueryLayerRepository.RetrieveQueryLayer(querySelection);

                qId = query.LayerId;
                qLayer = query.LayerName;
                qClause = query.QueryClause;
                qGDB = query.SourceGDB;
                resultBox.AlternationCount = query.QueryRowsReturned;

                MapManagement.SelectTOCLayer(qLayer);

                QueryLayerInput.Focus();
            }
        }

        private void QueryLayerInput_Unloaded(object sender, RoutedEventArgs e)
        {
            QueryLayerInput.Items.Clear();
        }

        #endregion

        #region TEXTBOX user input controls
        private void userTextInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                queryBtn_Click(sender, e);
            }
        }

        private void userTextInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            userTextInput.Clear();
            userInput = "";
            resultBox.Items.Clear();
        }

        private void userTextInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            //userTextInput.Focus();
            userInput = userTextInput.Text.ToString();
        }

        private void userTextInput_Unloaded(object sender, RoutedEventArgs e)
        {
            userTextInput.Text = "Please put your query here.";
        }

        #endregion

        #region BUTTON query control
        private void queryBtn_Click(object sender, RoutedEventArgs e)
        {
            resultBox.Items.Clear();

            var isUserInputValid = ValidateUserInput(userInput);

            try
            {
                if (!isUserInputValid)
                {
                    MessageBox.Show("Please input a query to search for in the text box.",
                                                                 "No Query Input!", System.Windows.MessageBoxButton.OK,
                                                                 System.Windows.MessageBoxImage.Warning);
                }
                else
                {
                    QueryUserInput(userInput, qId);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }

        }
        #endregion

        #region LISTBOX results control
        private async void resultBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedLayer = (FeatureLayer)MapView.Active.GetSelectedLayers().OfType<FeatureLayer>().FirstOrDefault();

                if (resultBox.SelectedIndex >= 0)
                {
                    queryInput = resultBox.SelectedItem.ToString();
                }

                string wClause = string.Format("{0}='{1}'", qClause, queryInput);

                await QueuedTask.Run(() =>
                {
                    FeatureLayer fLyr = selectedLayer as FeatureLayer;
                    QueryFilter qf = new QueryFilter { WhereClause = wClause };
                    fLyr.Select(qf);
                    MapView.Active.ZoomToSelected();
                });
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }
        #endregion

        #region METHOD: ValidateUserInput()- validate text box is not empty
        // Confirms and controls textbox input values
        private bool ValidateUserInput(string userInput)
        {
            bool validInput = false;

            userInput = userTextInput.Text.Trim();

            string outMsg = "Please put your query here.";

            if (string.IsNullOrEmpty(userInput) || string.IsNullOrWhiteSpace(userInput) || userInput == outMsg)
            {
                userTextInput.Text = outMsg;
            }
            else
            {
                validInput = true;
            }

            return validInput;
        }
        #endregion

        #region METHOD: QueryUserInput()- performs query for user input on layer selected in combobox.
        private async void QueryUserInput(string userInput, int fType)
        {
            try
            {
                if (userInput == "")
                {
                    MessageBox.Show("No input query specified! Please add an input query and try again.");
                    return;
                }
                else if (fType == 0)
                {
                    MessageBox.Show("No layer specified! Please select a layer to query on and try again.");
                    return;
                }
                else
                {
                    var results = await QueuedTask.Run(() => QueryUtilities.GetSearchResultsArray(userInput, qId, qGDB));

                    results.TrimToSize();

                    foreach (string i in results)
                    {
                        resultBox.Items.Add(i);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }


        }

        #endregion
    }
}
