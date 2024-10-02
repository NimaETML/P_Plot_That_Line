namespace Plot_That_Line_Nima_Zarrabi
{
    using Microsoft.VisualBasic.FileIO;
    using ScottPlot;
    using ScottPlot.Colormaps;
    using ScottPlot.PlotStyles;
    using ScottPlot.Plottables;
    using System.Collections.Generic;
    using System.Linq;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public partial class Form_PTL : Form
    {
        public Form_PTL()
        {
            InitializeComponent();

            string sourceDirectory = (@"..\..\..\..\data");

            TextFieldParser parsedcsv;

            int cryptoIdCount = 0;

            List<Crypto> cryptos = new List<Crypto>();

            // data list initialization 
            //List<string> dates = new List<string>();
            List<float> opens = [];
            List<float> highs = [];
            List<float> lows = [];
            List<float> closes = [];
            List<float> volumes = [];
            List<string> currencies = [];
            List<DateTime> datetime = [];

            // data initialization 
            string[] fields;

            if (Directory.Exists(sourceDirectory))
            {
                string[] paths = Directory.GetFiles(sourceDirectory, "*.csv");

                if (paths.Length == 0)
                {
                    MessageBox.Show("Veillez vous assurez que le dossier \"data\" contiens au moins un fichier .csv", "No valid imput file Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                foreach (string csv_path in paths)
                {
                    parsedcsv = new TextFieldParser(csv_path);
                    // Set CSV parser
                    parsedcsv.CommentTokens = new string[] { "#" };
                    parsedcsv.SetDelimiters(new string[] { "," });
                    parsedcsv.HasFieldsEnclosedInQuotes = true;

                    // Skip the row with the column names
                    parsedcsv.ReadLine();

                    while (!parsedcsv.EndOfData)
                    {
                        // Read current line fields, pointer moves to the next line.
                        fields = parsedcsv.ReadFields();

                        // Add to the lists

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
                        cryptos.Add(new Crypto(cryptoIdCount, "blp", datetime, opens, highs, lows, closes, volumes, currencies[0]));

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
                        MessageBox.Show("Votre CSV est pourri, y'a plusieurs valeurs different pour le type de monnaie, on peut pas faire un PlotLine avec ces données, c'est pas possible d'être aussi nul!");
                        break;
                        Application.Exit();
                    }
                }
                foreach (Crypto currentCrypto in cryptos)
                {
                    // Now plot the data
                    formsPlot1.Plot.Add.ScatterLine(currentCrypto.Date, currentCrypto.Close);

                    formsPlot1.Plot.YLabel("Price per unit (in " + currentCrypto.Currency + ")");
                    // tell the plot to display dates on the bottom axis
                    formsPlot1.Plot.Axes.DateTimeTicksBottom();

                    formsPlot1.Refresh();
                }
            }
            else
            {
                MessageBox.Show("{0} is not a valid directory.");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
