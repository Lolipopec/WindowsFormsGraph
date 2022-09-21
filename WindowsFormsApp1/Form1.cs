using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using Brushes = System.Windows.Media.Brushes;
using LiveCharts.Defaults;
using LiveCharts.Wpf.Charts.Base;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void CartesianChart1OnDataClick(object sender, ChartPoint chartPoint)
        {
            MessageBox.Show("You clicked (" + chartPoint.X + "," + chartPoint.Y + ")");
        }

        public double[,] SumSin()
        {
            double[,] val = new double[2, 5] { { 0, 1, 0, 1, 0 }, { -5, -4, -3, -2, -1 } };//Массив значений {-5, -4, -3,-2,-1}-Ox { 0, 1 ,0,1,0 }-Oy
            return val;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            build_Graph(SumSin());
        }
        private void build_Graph(double[,] val)
        {
            //Очистка предыдущих коллекций
            cartesianChart1.Series.Clear();
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisY.Clear();

            SeriesCollection series = new SeriesCollection();//Коллекция линий
            LineSeries ln = new LineSeries();//Линия
         
            ChartValues<ObservablePoint> Values = new ChartValues<ObservablePoint>();//Коллекция значений по Oy
            for (int j = 0; j < val.GetLength(1); j++)
            {
                Values.Add(new ObservablePoint(val[1, j], val[0, j]));
            }
            ln.Values = Values;//Добавление значений на линию
            series.Add(ln);//Добавление линии в коллекцию линий
            cartesianChart1.Series = series;//Добавление колеекции на график
            //Определение максимума и минимума по Ox
            double min = val[1, 0];
            double max = val[1, 0];
            for (int j = 0; j < val.GetLength(1); j++)
            {
                if (min > val[1, j])
                {
                    min = val[1, j];
                }
                if (max < val[1, j])
                {
                    max = val[1, j];
                }
            }
            //Ось Ox
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "X",//подпись
                LabelFormatter = value => value.ToString(""),
                MinValue = min,
                MaxValue = max,
                Separator = new Separator
                {
                    StrokeThickness = 1,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                }
            });
            min = val[0, 0];
            max = val[0, 0];
            for (int j = 0; j < val.GetLength(1); j++)
            {
                if (min > val[0, j])
                {
                    min = val[0, j];
                }
                if (max < val[0, j])
                {
                    max = val[0, j];
                }
            }
            //Ось Oy
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Y",
                MinValue = min,
                MaxValue = max,
                Separator = new Separator
                {
                    StrokeThickness = 1,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                }
            });
            cartesianChart1.LegendLocation = LegendLocation.Bottom;//Где расположить легенду графика


            cartesianChart1.DataClick += CartesianChart1OnDataClick;
        }
    }
}
