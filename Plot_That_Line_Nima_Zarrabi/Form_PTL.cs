namespace Plot_That_Line_Nima_Zarrabi
{
    public partial class Form_PTL : Form
    {
        public Form_PTL()
        {
            InitializeComponent();

            double[] dataX = { 1, 2, 3, 4, 5 };
            double[] dataY = { 1, 4, 9, 16, 25 };

            formsPlot1.Plot.Add.Scatter(dataX, dataY);
            formsPlot1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
