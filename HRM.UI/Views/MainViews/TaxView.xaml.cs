using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts.Wpf;
using LiveCharts;

namespace HRM.UI.Views
{
    /// <summary>
    /// Interaction logic for TaxView.xaml
    /// </summary>
    public partial class TaxView : UserControl
    {
        public TaxView()
        {
            InitializeComponent();

            // Tạo dữ liệu cho biểu đồ
            MyChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "2024",
                    Values = new ChartValues<double> { 10, 50, 39, 50 }
                }
            };

            MyChart.AxisX.Add(new Axis
            {
                Title = "Tháng",
                Labels = new[] { "Jan", "Feb", "Mar", "Apr" }
            });

            MyChart.AxisY.Add(new Axis
            {
                Title = "Doanh thu",
                LabelFormatter = value => value.ToString("C")
            });
        }
    }
}
