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
    /// Interaction logic for adminMainScreen.xaml
    /// </summary>
    public partial class adminMainScreen : Window
    {
        bool open = false;
        SqlConnection connection;
        CurrentUser user;
        SqlCommand command;
        Account account;
        List<Account> accounts = new List<Account>();
       

        internal adminMainScreen(CurrentUser current)
        {
            {
                InitializeComponent();
                user = current;
                //Display the current user in the window's title
                Title += " - " + user.FullName;
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
               
                FillAccountsList();
                open = true;
            }
        }

       

        public void FillAccountsList()
        {

            //Create the SELECT query
            string selectAccounts = "SELECT client_code, clientaccount_id, accounttype_description FROM ClientsAccounts ORDER BY client_code";

            try
            {
                command = new SqlCommand(selectAccounts, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())

                { 
                    //Create a new account
                    account = new Account();
                    account.AccountId = reader["clientaccount_id"].ToString();
                    account.AccountDescription = reader["accounttype_description"].ToString();


                    //Add to list
                    accounts.Add(account);
                }
                AdminMainAccountsList.DataContext = accounts.ToList();
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


        private void refreshBalance()
        {
            if (AdminMainAccountsList.SelectedIndex == -1)
            {
                return;
            }

            Account aAccount = accounts[AdminMainAccountsList.SelectedIndex];
            string refreshBalance = "SELECT account_balance FROM ClientsAccounts WHERE clientaccount_id = '" + aAccount.AccountId + "'";

            command = new SqlCommand(refreshBalance, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    AdminMainViewBalance.Text = "C$" + reader["account_balance"].ToString();
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

        private void AdminMainAccountsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //If no account is selected
            if (AdminMainAccountsList.SelectedIndex == -1)
            {
                return;
            }

            refreshBalance();

            Account aAccount = accounts[AdminMainAccountsList.SelectedIndex];
            string selectAccount = "SELECT * FROM ClientsAccounts FULL OUTER JOIN Clients ON ClientsAccounts.client_code = Clients.client_code WHERE clientaccount_id = '" + aAccount.AccountId + "'";
            command = new SqlCommand(selectAccount, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    AdminMainFullName.Text = reader["client_fullname"].ToString();
                    AdminMainClientCode.Text = reader["client_code"].ToString();
                    AdminMainTelephone.Text = reader["client_phone"].ToString();
                    AdminMainEmail.Text = reader["client_email"].ToString();

                    switch (reader["client_situation"].ToString())
                    {
                        case "B":
                            rbBlocked.IsChecked = true;
                            break;

                        case "U":
                            rbUnblocked.IsChecked = true;
                            break;
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

        private void btnAdminAddClient_Click(object sender, RoutedEventArgs e)
        {
            {
                //Create new form instance
                frmNewClient addClient = new frmNewClient();
                //Display the form
                addClient.ShowDialog();
                //Refresh accounts list
                accounts.Clear();
                FillAccountsList();

            }
        }

        private void btnAdminAddAccount_Click(object sender, RoutedEventArgs e)
        {
            //Create new form instance
            frmNewAccount addAccount = new frmNewAccount();
            //Display the form
            addAccount.ShowDialog();
            //Refresh account list
            accounts.Clear();
            FillAccountsList();
        }


        private void btnMainExit_Click_1(object sender, RoutedEventArgs e)
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
 

        public void blockCLient()
        {
            string blockClient = "UPDATE Clients SET client_situation = 'B' WHERE client_code = '" + AdminMainClientCode.Text.Trim() + "'";

            command = new SqlCommand(blockClient, connection);
            connection.Open();
            try
            {

                if (MessageBox.Show("Are you sure you want to block client access? ", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int line = command.ExecuteNonQuery();
                    MessageBox.Show("Client is now blocked.", "Confirmation",
                       MessageBoxButton.OK, MessageBoxImage.Information);
                }

               else
               {
                    rbUnblocked.IsChecked = true;
                    MessageBox.Show("Action canceled.", "Cancel",
                        MessageBoxButton.OK, MessageBoxImage.Information);
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


        public void unblockCLient()
        {
            string unblockClient = "UPDATE Clients SET client_situation = 'U' WHERE client_code = '" + AdminMainClientCode.Text.Trim() + "'";

            command = new SqlCommand(unblockClient, connection);
            connection.Open();
            try
            {

                if (MessageBox.Show("Are you sure you want to unblock client access? ", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int line = command.ExecuteNonQuery();
                    MessageBox.Show("Client is now unblocked.", "Confirmation",
                       MessageBoxButton.OK, MessageBoxImage.Information);
                }

                else
                {
                    rbBlocked.IsChecked = true;
                    MessageBox.Show("Action canceled.", "Cancel",
                        MessageBoxButton.OK, MessageBoxImage.Information);
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


        private void rbUnblocked_Click(object sender, RoutedEventArgs e)
        {
            unblockCLient();
        }

        private void rbBlocked_Click(object sender, RoutedEventArgs e)
        {
            blockCLient();
        }

        private void btnAdminMainIncreaseLimit_Click(object sender, RoutedEventArgs e)
        {

            string increaseLineOfCredit = "UPDATE ClientsAccounts SET account_balance = account_balance + (account_balance*0.05) WHERE accounttype_code = 'AL'";

            command = new SqlCommand(increaseLineOfCredit, connection);
            connection.Open();

            try
            {
            
                if (MessageBox.Show("Increase 5% to all line of credit accounts? ", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int line = command.ExecuteNonQuery();
                    MessageBox.Show("Increased successfully.", "Confirmation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Transaction canceled.", "Cancel",
                       MessageBoxButton.OK, MessageBoxImage.Information);
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


            refreshBalance();

        }

        private void btnAdminMainAddInterest_Click(object sender, RoutedEventArgs e)
        {

            string addInterest = "UPDATE ClientsAccounts SET account_balance = account_balance + (account_balance*0.01) WHERE accounttype_code = 'AS'";

            command = new SqlCommand(addInterest, connection);
            connection.Open();

            try
            {
                if (MessageBox.Show("Add 1% interest to all savings accounts? ", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int line = command.ExecuteNonQuery();
                    MessageBox.Show("Interests added successfully.", "Confirmation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Transaction canceled.", "Cancel",
                       MessageBoxButton.OK, MessageBoxImage.Information);
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
            refreshBalance();

        }

        private void btnAdminMainTransactions_Click(object sender, RoutedEventArgs e)
        {
            //Create new form instance
            frmTransactions transactionsHistory = new frmTransactions();
            //Display the form
            transactionsHistory.ShowDialog();
        }

        private void btnAdminMainAddMOney_Click(object sender, RoutedEventArgs e)
        {

            //Create new form instance
            DepositPaperMoney fillAtm = new DepositPaperMoney(user);
            //Display the form
            fillAtm.ShowDialog();
        }

        private void btnMainCloseATM_Click(object sender, RoutedEventArgs e)
        {

            string closeATM = "UPDATE Bank SET bank_status = 'C'";

            command = new SqlCommand(closeATM, connection);
            connection.Open();

            try
            {
                if (MessageBox.Show("Are you sure you want to CLOSE THE ATM? ", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int line = command.ExecuteNonQuery();
                    MessageBox.Show("ATM IS NOW CLOSED.", "Confirmation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Action canceled.", "Cancel",
                       MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void btnAdminMainMortgageW_Click(object sender, RoutedEventArgs e)
        {
            //Create new form instance
            adminWithdraw MortgageWithdrawl = new adminWithdraw();
            //Display the form
            MortgageWithdrawl.ShowDialog();
            //Refresh main window balance
            refreshBalance();
        }
    }
}
