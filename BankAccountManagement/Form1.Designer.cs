
using System.Windows.Forms;

namespace BankAccountManagement
{
    // --------------------------------------------------------------------------------------------------
    partial class Form1
    {
        // --------------------------------------------------------------------------------------------------
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private TextBox txtAccountHolder;
        private ComboBox cboAccountType;
        private Button btnCreateAccount;
        private Button btnDeposit;
        private Button btnWithdraw;
        private Button btnCheckBalance;
        private TextBox txtBalance;
        private Label lblTitle;
        private DataGridView dgvAccounts;
        private DataGridView dgvTransactions;

        // --------------------------------------------------------------------------------------------------

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // --------------------------------------------------------------------------------------------------

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        /// 
        // --------------------------------------------------------------------------------------------------       
        private void InitializeComponent()
        {

            this.components = new System.ComponentModel.Container();

            // --------------------------------------------------------------------------------------------------
            this.txtAccountHolder = new TextBox();
            this.cboAccountType = new ComboBox();
            this.btnCreateAccount = new Button();
            this.btnDeposit = new Button();
            this.btnWithdraw = new Button();
            this.btnCheckBalance = new Button();
            this.txtBalance = new TextBox();
            this.lblTitle = new Label();
            this.dgvAccounts = new DataGridView();
            this.dgvTransactions = new DataGridView();

            // --------------------------------------------------------------------------------------------------

            // Title Label
            this.lblTitle.Text = "Bank Account Management System";
            this.lblTitle.Font = new System.Drawing.Font("Arial", 14);
            this.lblTitle.Location = new System.Drawing.Point(100, 20);
            this.Controls.Add(this.lblTitle);

            // --------------------------------------------------------------------------------------------------

            // Account Holder TextBox
            this.txtAccountHolder.Location = new System.Drawing.Point(100, 60);
            this.txtAccountHolder.Name = "txtAccountHolder";
            this.txtAccountHolder.PlaceholderText = "Enter Account Holder Name";
            this.Controls.Add(this.txtAccountHolder);

            // --------------------------------------------------------------------------------------------------

            // Account Type ComboBox
            this.cboAccountType.Location = new System.Drawing.Point(100, 100);
            this.cboAccountType.Items.AddRange(new string[] { "Savings", "Checkings" });
            this.cboAccountType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Controls.Add(this.cboAccountType);

            // --------------------------------------------------------------------------------------------------

            // Create Account Button
            this.btnCreateAccount.Text = "Create Account";
            this.btnCreateAccount.Location = new System.Drawing.Point(100, 140);
            this.btnCreateAccount.Click += new System.EventHandler(this.BtnCreateAccount_Click);
            this.Controls.Add(this.btnCreateAccount);

            // --------------------------------------------------------------------------------------------------

            // Deposit Button
            this.btnDeposit.Text = "Deposit";
            this.btnDeposit.Location = new System.Drawing.Point(100, 180);
            this.btnDeposit.Click += new System.EventHandler(this.BtnDeposit_Click);
            this.Controls.Add(this.btnDeposit);

            // --------------------------------------------------------------------------------------------------

            // Withdraw Button
            this.btnWithdraw.Text = "Withdraw";
            this.btnWithdraw.Location = new System.Drawing.Point(100, 220);
            this.btnWithdraw.Click += new System.EventHandler(this.BtnWithdraw_Click);
            this.Controls.Add(this.btnWithdraw);

            // --------------------------------------------------------------------------------------------------

            // Check Balance Button
            this.btnCheckBalance.Text = "Check Balance";
            this.btnCheckBalance.Location = new System.Drawing.Point(100, 260);
            this.btnCheckBalance.Click += new System.EventHandler(this.BtnCheckBalance_Click);
            this.Controls.Add(this.btnCheckBalance);

            // --------------------------------------------------------------------------------------------------

            // Balance Textbox
            this.txtBalance.Location = new System.Drawing.Point(100, 300);
            this.txtBalance.ReadOnly = true;
            this.Controls.Add(this.txtBalance);

            // --------------------------------------------------------------------------------------------------

            // dgvAccounts setup
            this.dgvAccounts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccounts.Location = new System.Drawing.Point(300, 60);
            this.dgvAccounts.Name = "dgvAccounts";
            this.dgvAccounts.Size = new System.Drawing.Size(400, 200);
            this.dgvAccounts.TabIndex = 0;
            this.dgvAccounts.CellClick += new DataGridViewCellEventHandler(this.dgvAccounts_CellClick);
            this.Controls.Add(this.dgvAccounts);

            // --------------------------------------------------------------------------------------------------

            // dgvTransactions setup
            this.dgvTransactions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransactions.Location = new System.Drawing.Point(300, 280);
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.Size = new System.Drawing.Size(400, 200);
            this.dgvTransactions.TabIndex = 1;
            this.Controls.Add(this.dgvTransactions);

            // --------------------------------------------------------------------------------------------------

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Text = "Form1";

            // --------------------------------------------------------------------------------------------------
        }

        #endregion
    }
}