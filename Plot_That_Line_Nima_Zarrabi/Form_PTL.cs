/*
 * Nima Zarrabi
 * ETML
 * Plot That Line
 * Ver-1.0
 * 03/11/2024
 */

namespace Plot_That_Line_Nima_Zarrabi
{
    using Microsoft.VisualBasic.FileIO;
    using System.Windows.Forms;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Reflection;
    using ScottPlot;
    using ScottPlot.Colormaps;
    using ScottPlot.PlotStyles;
    using ScottPlot.Plottables;
    using System.Collections.Generic;
    using System.Linq;
    using static System.Runtime.InteropServices.JavaScript.JSType;
    using System.Security.Cryptography;
    using ScottPlot.DataSources;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
    using static System.Windows.Forms.CheckedListBox;

    public partial class Form_PTL : Form
    {
        /*
        Constants and variables initialization
        */

        const string DEFAULTSOURCEDIR = (@"..\..\..\..\data\data_set_high_cost");

        bool initilized = false;

        TextFieldParser parsedcsv;

        List<Crypto> cryptos = new List<Crypto>();

        int cryptoIdCount = 0;

        // Data list initialization 
        List<DateTime> datetime = [];
        List<float> opens = [];
        List<float> highs = [];
        List<float> lows = [];
        List<float> closes = [];
        List<float> volumes = [];
        List<string> currencies = [];

        // Table of strings where the CSV data will be stored, contains 7 strings for the 7 fields the CSV must contains
        string[] fields = new string[6];

        private List<Scatter> scatterList = [];

        public Form_PTL()
        {
            InitializeComponent();

            // Check if given path is valid
            CheckIfValidPath(DEFAULTSOURCEDIR);

            // get all CSVs paths
            string[] paths = Directory.GetFiles(DEFAULTSOURCEDIR, "*.csv");

            // Check if given path has CSVs in it
            CheckIfCSV(paths, DEFAULTSOURCEDIR);

            // Add CSV data to the Crypto class
            paths.ToList().ForEach(csv_path => AddCSVDataToCrypto(csv_path));

            // if all currencies are the same across CSVs
            CheckCurrencyConsistency(cryptos);

            // Generate ScottPlot plot
            GenerateScottPlot(ScottlLinePlot.Plot, cryptos);

            // program now knows it has been initilized and so checking boxes will remove/add scatterlines
            initilized = true;
        }
        
        private void GenerateScottPlot(Plot plot, List<Crypto> cryptos)
        {
            // Plott Y axis label
            // cryptos[0].Currency is used since every cryptos have the same curreny, but the cryptos list will always have at least its [0] position filled
            plot.YLabel("Price per unit (" + cryptos[0].Currency + ")");

            // Plott displays dates on the bottom axis
            plot.Axes.DateTimeTicksBottom();

            cryptos.ForEach(crypto => GenerateScatterLine(plot, crypto));

            plot.ShowLegend(Alignment.UpperRight);
            ScottlLinePlot.Refresh();
        }

        private void GenerateScatterLine(Plot plot, Crypto crypto)
        {
            // Add new ScatterLine to the plot and to scatterList, then add current crypto name to it's LegendText
            scatterList.Add(plot.Add.ScatterLine(crypto.Date, crypto.Close));
            scatterList.Last().LegendText = crypto.Name;

            // Add every crypto to the PlotLinesCheckBoxList and check them all by default
            PlotLinesCheckBoxList.Items.Add(crypto.Name);

            PlotLinesCheckBoxList.SetItemChecked(PlotLinesCheckBoxList.Items.Count - 1, true);
        }

        private void PlotLinesCheckBoxList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // If PlotLinesCheckBoxList element checked, element is added to the ScottlLinePlot, only run once the program has been initiilized
            if (e.NewValue == CheckState.Checked && initilized)
            {
                ScottlLinePlot.Plot.Add.Plottable(scatterList.Where(scatter => scatter.LegendText == PlotLinesCheckBoxList.Items[e.Index].ToString()).First());
                ScottlLinePlot.Refresh();
            }
            // If PlotLinesCheckBoxList element unchecked, element is removed from the ScottlLinePlot, only run once the program has been initiilized
            else if (!(e.NewValue == CheckState.Checked) && initilized)
            {
                ScottlLinePlot.Plot.Remove(scatterList.Where(scatter => scatter.LegendText == PlotLinesCheckBoxList.Items[e.Index].ToString()).First());
                ScottlLinePlot.Refresh();
            }
        }

        private void CheckCurrencyConsistency(List<Crypto> cryptos)
        {
            // Check if all cryptos in the list have the same value as currency
            if (cryptos.Select(c => c.Currency).Distinct().Skip(1).Any())
            {
                MessageBox.Show("Vos CSV séléctionné n'on pas tous le même type de monnaie, et ne peuvent alors pas être comparés.", "Different currency in different files Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void CheckIfValidPath(string dir)
        {
            if (!Directory.Exists(dir))
            {
                MessageBox.Show("Le répértoire séléctionné \"" + dir + "\" n'est pas un répéroire valid.", "invalid directory Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }
        
        private void CheckIfCSV(string[] paths, string dir)
        {
            if (paths.Length == 0)
            {
                MessageBox.Show("Veillez vous assurez que le dossier " + dir + " contient au moins un fichier .csv", "No valid imput file Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void AddCSVDataToCrypto(string csv_path)
        {
            // Setup CSV parser
            parsedcsv = new TextFieldParser(csv_path);
            parsedcsv.CommentTokens = new string[] { "#" };
            parsedcsv.SetDelimiters(new string[] { "," });
            parsedcsv.HasFieldsEnclosedInQuotes = true;

            // Skip the row with the column names
            parsedcsv.ReadLine();

            while (!parsedcsv.EndOfData)
            {
                // Read current line fields, pointer moves to the next line.
                fields = parsedcsv.ReadFields();

                // Add CSV data to the lists
                datetime.Add(DateTime.Parse(fields[0]));
                opens.Add(float.Parse(fields[1])); // Convert open to float
                highs.Add(float.Parse(fields[2])); // Convert high to float
                lows.Add(float.Parse(fields[3])); // Convert low to float
                closes.Add(float.Parse(fields[4])); // Convert close to float
                volumes.Add(float.Parse(fields[5])); // Convert volume to float
                currencies.Add(fields[6]);
            }
            if (!currencies.Distinct().Skip(1).Any())
            {

                cryptos.Add(new Crypto(cryptoIdCount, Path.GetFileNameWithoutExtension(csv_path), datetime, opens, highs, lows, closes, volumes, currencies[0]));

                // Clear lists after use
                datetime.Clear();
                opens.Clear();
                highs.Clear();
                lows.Clear();
                closes.Clear();
                volumes.Clear();
                currencies.Clear();
            }
            else
            {
                // Message if CSV contains different values for the currency
                MessageBox.Show("Le CSV (" + Path.GetFileName(csv_path) + ") ne contient pas des valeurs adéquates au programme, veillez vous assurer que le champ correspondant au type de monnai soit consistant", "Different values in currency field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            // Add to CryptoIdCount so next crypto Id will have incremented by 1
            cryptoIdCount++;
        }

        // Required function to run the Application
        private void ScottlLinePlot_Load(object sender, EventArgs e)
        {

        }

        private void PlotLinesCheckBoxList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
