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

    public partial class Form_PTL : Form
    {
        private List<List<double>> xs;
        private List<List<double>> ys;
        private List<List<double>> eye;
        private List<Scatter> scatterList = [];

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
                    //Error Message if no valid file in selected path
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

                        cryptos.Add(new Crypto(cryptoIdCount, Path.GetFileNameWithoutExtension(csv_path), datetime, opens, highs, lows, closes, volumes, currencies[0]));
                        cryptoIdCount++;

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
                        MessageBox.Show("Votre CSV (" + Path.GetFileName(csv_path) + ") est pourri, y'a plusieurs valeurs different pour le type de monnaie, on peut pas faire un PlotLine avec ces données, c'est pas possible d'être aussi nul!", "Different values in currency field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                        Application.Exit();
                    }
                }


                //List<double> ys = new()
                xs = [[1, 2, 3, 4, 5], [1, 2, 3, 4, 5]];
                ys = [[1, 2, 3, 4, 5], [1, 2, 3, 4, 9]];
                // if all currencies are the same across CSVs
                if (!cryptos.Select(c => c.Currency).Distinct().Skip(1).Any())
                {
                    Plot plotCanva = new Plot();
                    //plotCanva.Remove()
                    Scatter scatter = new Scatter(new ScatterSourceGenericList<List<double>, List<double>>(xs, ys));
                    scatter.Label = "test";
                    scatterList.Add(scatter);
                    Arrow ar = new Arrow()
                    {
                        Base = new Coordinates(1, 2),
                        Tip = new Coordinates(3, 4),
                    };
                    //Scatter scatter = new Scatter(new IScatterSource(xs, ys));
                    //scatter.LineStyle.Color = new Color();
                    //scatter.MarkerStyle.FillColor = new Color();
                    //ScottlLinePlot.Plot.PlottableList.Add(scatter);

                    //ScottlLinePlot.Plot.Add.Plottable(scatter);


                    // Setup plot
                    ScottlLinePlot.Plot.YLabel("Price per unit (in " + cryptos[0].Currency + ")");
                    // tell the plot to display dates on the bottom axis
                    ScottlLinePlot.Plot.Axes.DateTimeTicksBottom();
                    //ScottlLinePlot.Plot.Add.Plot(scatter);
                    ScottlLinePlot.Plot.Add.Plottable(scatter);


                    // PROBLEM HERE  // PROBLEM HERE  // PROBLEM HERE  // PROBLEM HERE  // PROBLEM HERE  // PROBLEM HERE  // PROBLEM HERE  // PROBLEM HERE

                    PlotLinesCheckBoxList.Items.Add(scatterList.Last().Label);
                    ScottlLinePlot.Refresh();


                    //ScottlLinePlot.Plot.PlottableList.Add(Scatter(xs, ys,));
                    /*
                    ScottlLinePlot.Plot.PlottableList.Add(new Arrow()
                    {
                        Base = new Coordinates(1, 2),
                        Tip = new Coordinates(3, 4),
                    });*/
                }
                else
                {
                    MessageBox.Show("Vos CSV séléctionné n'on pas tous le même type de monnaie, et ne peuvent alors pas être comparés.", "Different currency in different files Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }

            }
            else
            {
                MessageBox.Show("Le répértoire séléctionné \"{0}\" n'est pas un répéroire valid.", "invalid directory Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ScottlLinePlot_Load(object sender, EventArgs e)
        {

        }

        private void PlotLinesCheckBoxList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        private void GenerateScottPlot(Plot plot, List<Crypto> cryptos)
        {
            // Setup plot
            ScottlLinePlot.Plot.YLabel("Price per unit (in " + cryptos[0].Currency + ")");
            // tell the plot to display dates on the bottom axis
            ScottlLinePlot.Plot.Axes.DateTimeTicksBottom();
            foreach (Crypto currentCrypto in cryptos)
            {
                // Now plot the data
                ScottlLinePlot.Plot.Add.ScatterLine(currentCrypto.Date, currentCrypto.Close);

                PlotLinesCheckBoxList.Items.Add(currentCrypto.Name);

                // Automatically check new checkboxes as they get made
                PlotLinesCheckBoxList.SetItemChecked(PlotLinesCheckBoxList.Items.Count - 1, true);
            }
            ScottlLinePlot.Refresh();
        }


        private void PlotLinesCheckBoxList_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (e.NewValue == CheckState.Checked)
            {
                ScottlLinePlot.Plot.Remove(ScottlLinePlot.Plot.PlottableList[0]);
                ScottlLinePlot.Refresh();
                MessageBox.Show(PlotLinesCheckBoxList.Items[e.Index] + "  IS CHECKED");
            }
            else
            {
                // code fait en Linq, non fonctionnel car ScottlLinePlot.Plot.Add.Plottable n'accepte pas un list.Where en paramètre car il le consider comme un array et il demande un seul objet
                // METTRE .first !!!
                // !!!
                // !!!
                //ScottlLinePlot.Plot.Add.Plottable(scatterList.Where(scatter => scatter.LegendText == (PlotLinesCheckBoxList.Items[e.Index])));

                foreach (Scatter element in scatterList)
                {
                    if (element.LegendText == PlotLinesCheckBoxList.Items[e.Index])
                    {
                        ScottlLinePlot.Plot.Add.Plottable(element);
                        ScottlLinePlot.Refresh();
                    }
                }
                MessageBox.Show(PlotLinesCheckBoxList.Items[e.Index] + "  IS UNCHECKED");
            }

            // MAKE IT SO CHECKING ON/OFF MAKES THE PLOTLINES APPEAR/DISAPEAR
            //if this.
            //ScottlLinePlot.Plot.Remove()
        }
    }
}
