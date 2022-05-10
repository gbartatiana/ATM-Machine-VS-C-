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
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace BankATM
{
    /// <summary>
    /// Interaction logic for clientTransactions.xaml
    /// </summary>
    public partial class clientTransactions : Window
    {

        SqlConnection connection;
        SqlCommand command;
        CurrentUser user;


        internal clientTransactions(CurrentUser current)
        {
            InitializeComponent();
            user = current;
            //Display the current user in the window's title
            Title += " - " + user.FullName;
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            string showTransactions = "SELECT transaction_date, accounttype_description, clientaccount_id, transactiontype_description, clientaccount_id_transferto, transaction_amount  FROM TransactionsHistory WHERE client_code = '" + user.UserId + "' ORDER BY transaction_date";

            command = new SqlCommand(showTransactions, connection);
            connection.Open();

            try
            {

                DataTable dt = new DataTable();
                SqlDataAdapter a = new SqlDataAdapter(command);
                a.Fill(dt);
                grdTransactions.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

    

