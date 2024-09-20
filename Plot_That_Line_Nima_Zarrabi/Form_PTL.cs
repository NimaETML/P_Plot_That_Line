namespace Plot_That_Line_Nima_Zarrabi
{
    using Microsoft.VisualBasic.FileIO;
    using ScottPlot;
    using ScottPlot.Colormaps;
    using ScottPlot.PlotStyles;
    using ScottPlot.Plottables;
    using System.Collections.Generic;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public partial class Form_PTL : Form
    {
        public Form_PTL()
        {
            InitializeComponent();

           
            List<string> paths =
            [
                @"C:\Users\pu78ifh\Desktop\P_Plot_That_Line\Data\Filecoin.csv",
                //@"C:\Users\pu78ifh\Desktop\P_Plot_That_Line\Data\Bitcoin_Gold.csv",
                @"C:\Users\pu78ifh\Desktop\P_Plot_That_Line\Data\Fantom.csv",
            ];
            TextFieldParser parsedcsv;


            // data list initialization 
            List<string> dates = new List<string>();
            List<double> opens = new List<double>();
            List<double> highs = new List<double>();
            List<double> lows = new List<double>();
            List<double> closes = new List<double>();
            List<double> volumes = new List<double>();
            List<string> currencies = new List<string>();
            List<DateTime> datetime = new List<DateTime>();

            // data initialization 
            string[] fields;
            string date;
            string open;
            string high;
            string low;
            string close;
            string volume;
            string currency;


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
                    date = fields[0];
                    open = fields[1];
                    high = fields[2];
                    low = fields[3];
                    close = fields[4];
                    volume = fields[5];
                    currency = fields[6];


                    // Add to the lists

                    datetime.Add(DateTime.Parse(date));
                    dates.Add(date);
                    opens.Add(double.Parse(open)); // Convert open to double
                    highs.Add(double.Parse(high)); // Convert high to double
                    lows.Add(double.Parse(low)); // Convert low to double
                    closes.Add(double.Parse(close)); // Convert close to double
                    volumes.Add(double.Parse(volume)); // Convert volume to double
                    currencies.Add(currency);

                }
                // use LINQ and DateTime.ToOADate() to convert DateTime[] to double[]
                //double[] doubledatetime = datetime.Select(x => x.ToOADate()).ToArray();

                // Now plot the data
                formsPlot1.Plot.Add.ScatterLine(datetime, closes);

                formsPlot1.Plot.YLabel("Price per unit (in " + currencies[0] + ")");
                // tell the plot to display dates on the bottom axis
                formsPlot1.Plot.Axes.DateTimeTicksBottom();

                formsPlot1.Refresh();

                // OUT OF RANGE PROBLEM, NEED TO MAKE CLASSES
                datetime.Clear();
                dates.Clear();
                opens.Clear();
                highs.Clear();
                lows.Clear();
                volumes.Clear();
                currencies.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
