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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_PTL));
            ScottlLinePlot = new ScottPlot.WinForms.FormsPlot();
            PlotLinesCheckBoxList = new CheckedListBox();
            SuspendLayout();
            // 
            // ScottlLinePlot
            // 
            ScottlLinePlot.DisplayScale = 1F;
            ScottlLinePlot.Location = new Point(12, 12);
            ScottlLinePlot.Name = "ScottlLinePlot";
            ScottlLinePlot.Size = new Size(571, 411);
            ScottlLinePlot.TabIndex = 0;
            // 
            // PlotLinesCheckBoxList
            // 
            PlotLinesCheckBoxList.AllowDrop = true;
            PlotLinesCheckBoxList.BackColor = SystemColors.Control;
            PlotLinesCheckBoxList.BorderStyle = BorderStyle.None;
            PlotLinesCheckBoxList.CheckOnClick = true;
            PlotLinesCheckBoxList.Font = new Font("Segoe UI", 11F);
            PlotLinesCheckBoxList.FormattingEnabled = true;
            PlotLinesCheckBoxList.Location = new Point(589, 34);
            PlotLinesCheckBoxList.Name = "PlotLinesCheckBoxList";
            PlotLinesCheckBoxList.Size = new Size(199, 396);
            PlotLinesCheckBoxList.TabIndex = 1;
            PlotLinesCheckBoxList.UseTabStops = false;
            PlotLinesCheckBoxList.ItemCheck += PlotLinesCheckBoxList_ItemCheck;
            PlotLinesCheckBoxList.SelectedIndexChanged += PlotLinesCheckBoxList_SelectedIndexChanged;
            // 
            // Form_PTL
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(PlotLinesCheckBoxList);
            Controls.Add(ScottlLinePlot);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form_PTL";
            Text = "Crypto Visualizer";
            Load += ScottlLinePlot_Load;
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot ScottlLinePlot;
        private CheckedListBox PlotLinesCheckBoxList;
    }
}
