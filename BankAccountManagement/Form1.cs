using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace BankAccountManagement
{
    public partial class Form1 : Form
    {
        // --------------------------------------------------------------------------------------------------

        private Dictionary<string, BankAccount> accounts = new Dictionary<string, BankAccount>();

        // --------------------------------------------------------------------------------------------------

        public Form1()
        {
            InitializeComponent();
            LoadAccounts();     // Load accounts on startup
        }

        // --------------------------------------------------------------------------------------------------

        private void LoadAccounts()
        {
            List<BankAccount> accounts = DatabaseHelper.GetAccounts();
            dgvAccounts.DataSource = accounts;
        }

        // --------------------------------------------------------------------------------------------------

        private void dgvAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int accountId = (int)dgvAccounts.Rows[e.RowIndex].Cells["Id"].Value;
                LoadTransactions(accountId);
            }
        }

        // --------------------------------------------------------------------------------------------------

        private void LoadTransactions(int accountId)
        {
            List<Transaction> transactions = DatabaseHelper.GetTransactionsForAccount(accountId);
            dgvTransactions.DataSource = transactions;
        }

        // --------------------------------------------------------------------------------------------------

        // Event Handler for "Create Account" button
        private void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            string holderName = txtAccountHolder.Text;
            string accountType = cboAccountType.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(holderName) || accountType == null)
            {
                MessageBox.Show("Please provide all account details.");
                return;
            }

            BankAccount account = new BankAccount(holderName, accountType);
            accounts.Add(holderName, account);

            MessageBox.Show($"Account created for {holderName} with {accountType} account.");
        }

        // --------------------------------------------------------------------------------------------------

        // Event Handler for "Deposit" button
        private void BtnDeposit_Click(object sender, EventArgs e)
        {
            string holderName = txtAccountHolder.Text;
            if (!accounts.ContainsKey(holderName))
            {
                MessageBox.Show("Account not found.");
                return;
            }

            BankAccount account = accounts[holderName];
            decimal amount = PromptForAmount();
            if (amount > 0)
            {
                account.Deposit(amount);
                MessageBox.Show($"Deposited {amount:C} to {holderName}'s account.");
            }
        }

        // --------------------------------------------------------------------------------------------------

        // Event handler for "Withdraw" button
        private void BtnWithdraw_Click(object sender, EventArgs e)
        {
            string holderName = txtAccountHolder.Text;
            if (!accounts.ContainsKey(holderName))
            {
                MessageBox.Show("Account not found.");
                return;
            }

            BankAccount account = accounts[holderName];
            decimal amount = PromptForAmount();
            if (amount > 0 && account.Withdraw(amount))
            {
                MessageBox.Show($"Withdraw {amount:C} from {holderName}'s account.");
            }
            else
            {
                MessageBox.Show("Insufficient funds.");
            }
        }

        // --------------------------------------------------------------------------------------------------

        // Event handler for "Check Balance" button
        private void BtnCheckBalance_Click(object sender, EventArgs e)
        {
            string holderName = txtAccountHolder.Text;
            if (!accounts.ContainsKey(holderName))
            {
                MessageBox.Show("Account not found.");
                return;
            }

            BankAccount account = accounts[holderName];
            txtBalance.Text = account.Balance.ToString("C");
        }

        // --------------------------------------------------------------------------------------------------

        private decimal PromptForAmount()
        {
            decimal amount;
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter Amount", "Transaction", "0");

            if (decimal.TryParse(input, out amount))
            {
                return amount;
            }
            MessageBox.Show("Invalid amount.");
            return amount;
        }

        // --------------------------------------------------------------------------------------------------

        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            string holderName = txtAccountHolder.Text;
            if (!accounts.ContainsKey(holderName))
            {
                MessageBox.Show("Account not found.");
                return;
            }

            var account = accounts[holderName];
            var report = DatabaseHelper.GetTransactionsForAccount(account.Id);

            string reportText = $"Transaction Report for {holderName}\n";
            reportText += "Date\t\tType\tAmount\n";

            foreach (var transaction in report)
            {
                reportText += $"{transaction.Date}\t{transaction.Type}\t{transaction.Amount:C}\n";
            }

            MessageBox.Show(reportText, "Transaction Report");
        }
        // --------------------------------------------------------------------------------------------------

    }
}