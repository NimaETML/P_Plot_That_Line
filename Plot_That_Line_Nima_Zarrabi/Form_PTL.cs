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
            List<float> opens = new List<float>();
            List<float> highs = new List<float>();
            List<float> lows = new List<float>();
            List<float> closes = new List<float>();
            List<float> volumes = new List<float>();
            List<string> currencies = new List<string>();
            List<DateTime> datetime = new List<DateTime>();

            // data initialization 
            string[] fields;

            if (Directory.Exists(sourceDirectory))
            {
                string[] paths = Directory.GetFiles(sourceDirectory);

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

                    if (currencies.Distinct().Skip(1).Any())
                    {
                        cryptos.Add(new Crypto(cryptoIdCount, "blp", datetime, opens, highs, lows, closes, volumes, currencies[0]));

                        // Now plot the data
                        formsPlot1.Plot.Add.ScatterLine(datetime, closes);

                        formsPlot1.Plot.YLabel("Price per unit (in " + currencies[0] + ")");
                        // tell the plot to display dates on the bottom axis
                        formsPlot1.Plot.Axes.DateTimeTicksBottom();

                        formsPlot1.Refresh();

                        // OUT OF RANGE PROBLEM, NEED TO MAKE CLASSES
                    }
                    else
                    {
                        MessageBox.Show("Votre CSV est pourri, y'a plusieurs valeurs different pour le type de monnaie, on peut pas faire un PlotLine avec ces données, c'est pas possible d'être aussi nul!");
                    }
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
