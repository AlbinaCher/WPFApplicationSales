using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace WpfAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        SqlConnection con;// it is connection adapter
        SqlCommand cmd; //it is the query proceening adapter
        private void connection_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=LAPTOP-QDM6EGQ3;Initial Catalog=model;Integrated Security=True";
            con = new SqlConnection(connectionString);
            con.Open();
            MessageBox.Show("Connection Established");
            con.Close();

        }

        private void insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //1 Open connection
                con.Open();

                //2 generation of the Query
                string Query = "insert into Product values(@prod_name,@prod_Id,@amount,@price)";

                //3 create the command for Database
                cmd = new SqlCommand(Query, con);

                //4 assign values to the query
                cmd.Parameters.AddWithValue("@prod_name", prod_name.Text);
                cmd.Parameters.AddWithValue("@prod_Id", int.Parse(prod_Id.Text));
                cmd.Parameters.AddWithValue("@amount", float.Parse(amount.Text)+"{0:F2}");
                cmd.Parameters.AddWithValue("@price", float.Parse(price.Text) + "{0:F2}");

                //5 execute the Command/Query
                cmd.ExecuteNonQuery();

                //6 Successful Message
                MessageBox.Show("Insertion is Successfull");
                con.Close();

            }catch (SqlException ex) {
                MessageBox.Show(ex.Message);

            }
        }

        private void show_Click(object sender, RoutedEventArgs e)
        {
            try
            {//1 open connection
                con.Open();
                //sele ct query
                string Query = "select * from Product";
                //Create the command to execute
                cmd = new SqlCommand(Query, con);
                //Prepare the data for dataGrid
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //Update datagrid ItemSource
                dbGrid.ItemsSource = dt.AsDataView();
                //6 Bind the data in the wpf frontend
                DataContext = da;
                con.Close();

            }catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                string query = "UPDATE Product ";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error" + ex);
            }

            con.Close();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand del = new SqlCommand("DELETE fron[Product] WHERE name = (@prod_name,@prod_Id,@amount,@price)");
                del.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error" + ex);
            }

        }
    }
}
