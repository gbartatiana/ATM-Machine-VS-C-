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

namespace BankATM
{
    /// <summary>
    /// Interaction logic for clientWithdrawal.xaml
    /// </summary>
    public partial class clientWithdrawal : Window
    {

        bool open = false;

        SqlConnection connection;
        CurrentUser user;
        SqlCommand command;
        Account account;
        List<Account> accounts = new List<Account>();


        internal clientWithdrawal(CurrentUser current)
        {
            InitializeComponent();
            user = current;
            //Display the current user in the window's title
            Title += " - " + user.FullName;
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            FillAccountsList();
            open = true;
        }

        public void FillAccountsList()
        {

            //Create the SELECT query
            string selectAccounts = "SELECT clientaccount_id, accounttype_description FROM ClientsAccounts WHERE client_code='" + user.UserId + "'  AND accounttype_description LIKE 'Savings' OR client_code='" + user.UserId + "' AND accounttype_description LIKE 'Checking'  ORDER BY accounttype_code";
            command = new SqlCommand(selectAccounts, connection);
            try
            {
               
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())

                { //Create a new account
                    account = new Account();
                    account.AccountId = reader["clientaccount_id"].ToString();
                    account.AccountDescription = reader["accounttype_description"].ToString();


                    //Add to list
                    accounts.Add(account);
                }
                wdAccountsList.DataContext = accounts.ToList();
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

        private void wdAccountsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //If no account is selected
            if (wdAccountsList.SelectedIndex == -1)
            {
                return;
            }

            Account aAccount = accounts[wdAccountsList.SelectedIndex];
            string selectAccount = "SELECT account_balance FROM ClientsAccounts WHERE clientaccount_id = '" + aAccount.AccountId + "'";
            command = new SqlCommand(selectAccount, connection);


            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    wdAccountBalance.Text = reader["account_balance"].ToString().Trim();

                }

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

        public bool ValidateInput()
        {
            bool OK = true;
            if (wdAmount.Text.Trim() == string.Empty ||
             wdAccountsList.SelectedIndex == -1)

            {
                OK = false;
            }
            return OK;
        }

        private void btnWdSave_Click(object sender, RoutedEventArgs e)
        {
            bool OK = ValidateInput();
            if (OK)
            {

                decimal amount;
                decimal balance;
                decimal bankBalance;
                string test = wdAccountBalance.Text;

                Bank b = new Bank();
                string checkAtmBalance = "SELECT bank_balance FROM Bank WHERE bank_code = '1'";
                command = new SqlCommand(checkAtmBalance, connection);


                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        b.BankBalance = reader["bank_balance"].ToString();

                        if (decimal.TryParse(b.BankBalance, out bankBalance) && decimal.TryParse(wdAmount.Text, out amount))
                        {

                            if (amount > bankBalance)
                            {
                                MessageBox.Show("Withdrawal NOT Confirmed. Funds not available in ATM. ", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                                return;
                            }

                            else
                            {

                                try
                                {
                                   
                                    Account a = accounts[wdAccountsList.SelectedIndex];
                                    if (decimal.TryParse(test, out balance) && decimal.TryParse(wdAmount.Text, out amount))
                                    {

                                        string withdrawalMoney = "UPDATE ClientsAccounts SET account_balance = account_balance - '" + wdAmount.Text + "'  WHERE clientaccount_id = '" + a.AccountId + "'";
                                        command = new SqlCommand(withdrawalMoney, connection);


                                        try
                                        {
                                            connection.Close();
                                            connection.Open();

                                            if (amount > 1000)
                                            {
                                                MessageBox.Show("Withdrawal NOT Confirmed. Maximum Amount is C$1000,00. ", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                                                return;
                                            }

                                            else if (amount > balance)
                                            {

                                                MessageBox.Show("Withdrawal NOT Confirmed. Funds not available in account.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                                                return;
                                            }


                                            else
                                            {
                                                int line = command.ExecuteNonQuery();
                                                if (line != 0)
                                                {

                                                    MessageBox.Show("Withdrawal Confirmed", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                                                    this.Close();


                                                    string feedHistory = $"INSERT INTO TransactionsHistory (transaction_date, client_code, accounttype_description, clientaccount_id, transactiontype_description, transaction_amount) " +
                                                    $"Values('{DateTime.Now}','{user.UserId}', '{account.AccountDescription}','{ account.AccountId}', '{"Withdrawal"}', '{wdAmount.Text}')";
                                                    command = new SqlCommand(feedHistory, connection);


                                                    try
                                                    {
                                                        connection.Close();
                                                        connection.Open();
                                                        SqlDataReader reader3 = command.ExecuteReader();

                                                    }

                                                    catch (Exception ex)
                                                    {
                                                        MessageBox.Show(ex.Message);
                                                    }

                                                    finally
                                                    {
                                                        connection.Close();
                                                    }


                                                    string alterAtmBalance = "UPDATE Bank SET bank_balance = bank_balance - '" + wdAmount.Text + "'  WHERE bank_code = '1'";
                                                    command = new SqlCommand(alterAtmBalance, connection);

                                                    try
                                                    {
                                                        connection.Open();
                                                        SqlDataReader reader3 = command.ExecuteReader();

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

            else
            {
                MessageBox.Show("Missing information in a field.", "Warning!", MessageBoxButton.OK,
                MessageBoxImage.Exclamation);
            }


            
        }
  

        private void btnWdCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit? ", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();


            }
            else
            {
                MessageBox.Show("Cancelling exit.", "Cancel",
                MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
