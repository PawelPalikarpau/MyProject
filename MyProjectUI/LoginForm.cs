﻿using MyProjectLibrary;
using MyProjectLibrary.Models;
using MyProjectLibrary.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyProjectUI
{
    public partial class LoginForm : Form
    {
        public UserModel UserModel { get; set; }
        public AccountModel AccountModel { get; set; }

        private LoginFormValidator validator = new LoginFormValidator();

        public LoginForm()
        {
            InitializeComponent();
            emailTextBox.Text = "pawel@gmail.com";
            passwordTextBox.Text = "qwer";
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;

            UserModel userModel = validator.ValidateForm(email, password);

            if (userModel != null)
            {
                this.UserModel = userModel;
                this.AccountModel = GlobalConfig.Connection.AccountsOperations().GetAccountByUserId(this.UserModel.Id);
                this.Close();
            }
        }

        private void registrateButton_Click(object sender, EventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.FormClosed += new FormClosedEventHandler(registrationForm_FormClosed);
            this.Enabled = false;
            this.Visible = false;
            registrationForm.Show();
        }

        void registrationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RegistrationForm registrationForm = (RegistrationForm)sender;
            emailTextBox.Text = registrationForm.EmailText;
            
            this.Visible = true;
            this.Enabled = true;
        }
    }
}
