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

        private void button1_Click(object sender, EventArgs e)
        {
            //if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
                build_Graph();
            //else
            //{
            //    MessageBox.Show("Заполни!!!!!!!!!!!!");
            //}
        }
        private void build_Graph()
        {
            //Очистка предыдущих коллекций
            cartesianChart1.Series.Clear();
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisY.Clear();

            SeriesCollection series = new SeriesCollection();//Коллекция линий
            LineSeries ln = new LineSeries();//Линия
            //ln.ChartPoints//Посмотреть
            ln.Title = "";
            int[,] val = new int[2, 5] { { 0, 1 ,0,1,0 }, {-5, -4, -3,-2,-1} };//Массив значений {-5, -4, -3,-2,-1}-Ox { 0, 1 ,0,1,0 }-Oy
            ChartValues<int> Values = new ChartValues<int>();//Коллекция значений по Oy
            //Переписали из массива значения по Oy
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < val.GetLength(1); j++)
                {
                    
                    Values.Add(val[i,j]);
                }

            }
            ln.Values = Values;//Добавление значений на линию
            series.Add(ln);//Добавление линии в коллекцию линий
            cartesianChart1.Series = series;//Добавление колеекции на график
            //Определение максимума и минимума по Ox
            double min = val[1, 0];
            double max = val[1, 0];
            for (int i = 1; i < 2; i++)
            {
                for (int j = 0; j < val.GetLength(1); j++)
                {
                    if (min > val[i, j])
                    {
                        min = val[i, j];
                    }
                    if (max < val[i, j])
                    {
                        max = val[i, j];
                    }
                }
            }
            //Определили точки по оси Ox
            List<double> values = new List<double>();
            double step = (max - min) / (val.GetLength(1) - 1);
            for (int i = 0; i < val.GetLength(1); i++)
            {
               
                values.Add(min);
                min += step;
            }
            //Записали в лист стринг для создания оси
            List<string> l = new List<string>();
            for (int i = 0; i <values.Count; i++)
            {
                l.Add(values[i].ToString());
            }
            //Ось Ox
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "X",//подпись
                Labels = l,//значения на оси
                Separator = new Separator
                {
                    StrokeThickness = 1,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                }
            });
            //Ось Oy
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Y",
                Labels = new[] { "-1", "0", "1"},
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
