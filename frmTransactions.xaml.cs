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
    /// Interaction logic for frmTransactions.xaml
    /// </summary>
    public partial class frmTransactions : Window
    {

        SqlConnection connection;
        SqlCommand command;

        Account account;
        List<Account> accounts = new List<Account>();


        public frmTransactions()
        {
            InitializeComponent();
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            FillAccountsList();
        }

        public void FillAccountsList()
        {
            //Create the SELECT query
            string selectAccount = "SELECT client_code, clientaccount_id, accounttype_description FROM ClientsAccounts ORDER BY client_code";

            try
            {
                command = new SqlCommand(selectAccount, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    account = new Account();
                    account.AccountId = reader["clientaccount_id"].ToString();
                    account.AccountDescription = reader["accounttype_description"].ToString();


                    //Add to list
                    accounts.Add(account);
                }
                TransactionsAccountsList.DataContext = accounts.ToList();

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

        private void TransactionsAccountsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //If no account is selected
            if (TransactionsAccountsList.SelectedIndex == -1)
            {
                return;
            }

            Account aAccount = accounts[TransactionsAccountsList.SelectedIndex];
            string selectAccount = "SELECT transaction_date, client_code, accounttype_description, clientaccount_id, transactiontype_description, clientaccount_id_transferto, transaction_amount  FROM TransactionsHistory WHERE clientaccount_id = '" + aAccount.AccountId + "'";

            command = new SqlCommand(selectAccount, connection);
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
