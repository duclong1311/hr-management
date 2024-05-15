using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using HRM.Domain.Models;
using Microsoft.Data.SqlClient;

namespace HRM.UI.Views
{
    /// <summary>
    /// Interaction logic for ListContractView.xaml
    /// </summary>
    public partial class ListContractView : UserControl
    {
        public ListContractView()
        {
            InitializeComponent();

            //Dispatcher.BeginInvoke(new Action(LoadNhanVienComboBox));
        }
        private void LoadNhanVienComboBox()
        {
            try
            {
                string connectionString = "server=DESKTOP-1FQO5UE\\SQLEXPRESS;database=HRMDb;trusted_connection=true;TrustServerCertificate=True;";
                string query = "SELECT MaNhanVien, HoTen FROM dbo.NhanSus";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    ObservableCollection<NhanSu> nhanVienList = new ObservableCollection<NhanSu>();

                    while (reader.Read())
                    {
                        string maNhanVien = reader["MaNhanVien"].ToString();
                        string tenNhanVien = reader["HoTen"].ToString();

                        NhanSu NhanSu = new NhanSu
                        {
                            MaNhanVien = maNhanVien,
                            HoTen = tenNhanVien
                        };

                        nhanVienList.Add(NhanSu);
                    }
                    
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
