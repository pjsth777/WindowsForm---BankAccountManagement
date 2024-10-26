using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Transactions;

namespace BankAccountManagement
{
    // --------------------------------------------------------------------------------------------------

    public static class DatabaseHelper
    {
        // --------------------------------------------------------------------------------------------------
     
        private const string ConnectionString = "Data Source=BankAccountManagement.db;Version=3;";

        // --------------------------------------------------------------------------------------------------

        public static void InitializeDatabase()
        {
            // --------------------------------------------------------------------------------------------------

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string createAccountsTable = @"
                    CREATE TABLE IF NOT EXISTS 
                        Accounts (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    HolderName TEXT,
                                    AccountType TEXT,
                                    Balance REAL
                        )
                ";
                string createTransactionTable = @"
                    CREATE TABLE IF NOT EXISTS
                        Transactions (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            AccountId INTEGER,
                            Date TEXT,
                            Type TEXT,
                            Amount REAL
                        )
                ";

                using (var command = new SQLiteCommand(createAccountsTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SQLiteCommand(createTransactionTable, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            // --------------------------------------------------------------------------------------------------
        }

        // --------------------------------------------------------------------------------------------------

        public static List<BankAccount> GetAccounts()
        {
            var accounts = new List<BankAccount>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Accounts";
                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        accounts.Add(new BankAccount(
                            reader.GetString(1),        // AccountHolder
                            reader.GetString(2),        // AcountType
                            reader.GetDecimal(3)        // Balance
                        )
                        {
                            Id = reader.GetInt32(0)     // Id
                        });
                    }
                }
            }

            return accounts;
        }

        // --------------------------------------------------------------------------------------------------

        public static int AddAccount(string holderName, string accountType, decimal initialBalance)
        {
            // --------------------------------------------------------------------------------------------------

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = @"
                        INSERT INTO Accounts 
                        (
                            HolderName, 
                            AccountType, 
                            Balance
                        )
                        VALUES
                        (
                            @HolderName,
                            @AccountType,
                            @Balance
                        );
                        SELECT last_insert_rowid()
                ";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HolderName", holderName);
                    command.Parameters.AddWithValue("@AccountType", accountType);
                    command.Parameters.AddWithValue("@Balance", initialBalance);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }

            // --------------------------------------------------------------------------------------------------
        }

        // --------------------------------------------------------------------------------------------------

        public static void AddTransaction(int accountId, string type, decimal amount)
        {
            // --------------------------------------------------------------------------------------------------

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = @"
                    INSERT INTO Transactions
                    (
                        AccountId,
                        Date,
                        Type,
                        Amount
                    )
                    VALUES
                    (
                        @AccountId,
                        @Date,
                        @Type,
                        @Amount
                    )
                ";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AccountId", accountId);
                    command.Parameters.AddWithValue("@Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@Type", type);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.ExecuteNonQuery();
                }
            }

            // --------------------------------------------------------------------------------------------------
        }

        // --------------------------------------------------------------------------------------------------

        public static decimal GetBalance(int accountId)
        {
            // --------------------------------------------------------------------------------------------------

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT Balance FROM Accounts WHERE Id = @AccountId";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AccountId", accountId);
                    var result = command.ExecuteScalar();
                    return result != null ? Convert.ToDecimal(result) : 0;
                }
            }

            // --------------------------------------------------------------------------------------------------
        }

        // --------------------------------------------------------------------------------------------------

        public static void UpdateBalance(int accountId, decimal newBalance)
        {
            // --------------------------------------------------------------------------------------------------

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "UPDATE Accounts SET Balance = @Balance WHERE Id = @AccountId";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AccountId", accountId);
                    command.Parameters.AddWithValue("@Balance", newBalance);
                    command.ExecuteNonQuery();
                }
            }

            // --------------------------------------------------------------------------------------------------
        }

        // --------------------------------------------------------------------------------------------------

        public static List<Transaction> GetTransactionsForAccount(int accountId)
        {
            var transactions = new List<Transaction>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id, Date, Type, Amount FROM Transactions WHERE AccountId = @AccountId";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AccountId", accountId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var transaction = new Transaction
                            {
                                Id = reader.GetInt32(0),
                                AccountId = accountId,
                                Date = DateTime.Parse(reader.GetString(1)),
                                Type = reader.GetString(2),
                                Amount = reader.GetDecimal(3)
                            };
                            transactions.Add(transaction);
                        }
                    }
                }
            }
            return transactions;
        }

        // --------------------------------------------------------------------------------------------------
    }
}
