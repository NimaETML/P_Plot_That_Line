namespace Plot_That_Line_Nima_Zarrabi
{
    partial class Form_PTL
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ScottlLinePlot = new ScottPlot.WinForms.FormsPlot();
            PlotLinesCheckBoxList = new CheckedListBox();
            SuspendLayout();
            // 
            // ScottlLinePlot
            // 
            ScottlLinePlot.DisplayScale = 1F;
            ScottlLinePlot.Location = new Point(12, 12);
            ScottlLinePlot.Name = "ScottlLinePlot";
            ScottlLinePlot.Size = new Size(560, 315);
            ScottlLinePlot.TabIndex = 0;
            // 
            // PlotLinesCheckBoxList
            // 
            PlotLinesCheckBoxList.FormattingEnabled = true;
            PlotLinesCheckBoxList.Location = new Point(578, 27);
            PlotLinesCheckBoxList.Name = "PlotLinesCheckBoxList";
            PlotLinesCheckBoxList.Size = new Size(210, 274);
            PlotLinesCheckBoxList.TabIndex = 1;
            PlotLinesCheckBoxList.SelectedIndexChanged += PlotLinesCheckBoxList_SelectedIndexChanged;
            PlotLinesCheckBoxList.ItemCheck += PlotLinesCheckBoxList_ItemCheck;
            // 
            // Form_PTL
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(PlotLinesCheckBoxList);
            Controls.Add(ScottlLinePlot);
            Name = "Form_PTL";
            Text = "ScottlLinePlot";
            Load += ScottlLinePlot_Load;
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot ScottlLinePlot;
        private CheckedListBox PlotLinesCheckBoxList;
    }
}
