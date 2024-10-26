using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement
{
    // --------------------------------------------------------------------------------------------------
    // The class representing a bank account
    public class BankAccount
    {
        // --------------------------------------------------------------------------------------------------

        public int Id { get; set; }
        public string AccountHolder { get; private set; }
        public string AccountType { get; private set; }
        public decimal Balance { get; private set; }

        // --------------------------------------------------------------------------------------------------

        public BankAccount(string accountHolder, string accountType, decimal initialBalance = 0)
        {
            AccountHolder = accountHolder;
            AccountType = accountType;
            Balance = initialBalance;
            Id = DatabaseHelper.AddAccount(accountHolder, accountType, initialBalance);
        }

        // --------------------------------------------------------------------------------------------------

        public void Deposit(decimal amount)
        {
            Balance += amount;
            DatabaseHelper.AddTransaction(Id, "Deposit", amount);
            DatabaseHelper.UpdateBalance(Id, Balance);
        }

        // --------------------------------------------------------------------------------------------------

        public bool Withdraw(decimal amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                DatabaseHelper.AddTransaction(Id, "Withdrawal", amount);
                DatabaseHelper.UpdateBalance(Id, Balance);
                return true;
            }
            return false;
        }

        // --------------------------------------------------------------------------------------------------
    }
}
