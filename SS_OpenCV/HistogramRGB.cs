using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SS_OpenCV
{
    public partial class HistogramRGB : Form
    {
        public HistogramRGB(int[,] matrix)
        {
            InitializeComponent();
            DataPointCollection list1;
            DataPointCollection list2;
            DataPointCollection list3;
            DataPointCollection list4;
            int blue, green, red;
            if (matrix.GetLength(0) < 4)
            {
                blue = 0;
                green = 1;
                red = 2;
            } else
            {
                blue = 1;
                green = 2;
                red = 3;
            }
            

            if (matrix.GetLength(0) > 3)
            {
                list1 = chart1.Series[0].Points;
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    list1.AddXY(i, matrix[0, i]);
                }
                chart1.Series[0].Color = Color.Gray;
                
            }

            list2 = chart1.Series[1].Points;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                list2.AddXY(i, matrix[blue, i]);
            }

            chart1.Series[1].Color = Color.Blue;



            list3 = chart1.Series[2].Points;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                list3.AddXY(i, matrix[green, i]);
            }

            chart1.Series[2].Color = Color.Green;



            list4 = chart1.Series[3].Points;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                list4.AddXY(i, matrix[red, i]);
            }

            chart1.Series[3].Color = Color.Red;
            chart1.ChartAreas[0].AxisX.Maximum = 255;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Title = "Intensidade";
            chart1.ChartAreas[0].AxisY.Title = "Numero Pixeis";
            chart1.ResumeLayout();
        }

            private void HistogramRGB_Load(object sender, EventArgs e)
        {

        }
    }
}
