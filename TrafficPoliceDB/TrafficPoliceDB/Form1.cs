using System;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using OfficeOpenXml;
using Xceed.Document.NET;
using Xceed.Words.NET;
using System.Diagnostics;

namespace TrafficPoliceDB
{
    public partial class Form1 : Form
    {
        // Название файла с базой данных.
        private string database_file = "DB.mdb";
        
        // Конструктор для формы.
        public Form1()
        {
            InitializeComponent();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            LanguageBox.Text = GetKeyboardLayoutId();
            CapsLockBox.Text = GetCapsLockStatus();

            timer1 = new Timer();
            timer1.Interval = 300;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        // Метод для отслеживания языка ввода.
        private string GetKeyboardLayoutId()
        {
            if (InputLanguage.CurrentInputLanguage.LayoutName == "США")
                return "Английский";
            return "Русский";
        }

        // Метод для отслеживания кнопки Caps-Lock.
        private string GetCapsLockStatus()
        {
            if (System.Windows.Forms.Control.IsKeyLocked(Keys.CapsLock))
                return "Клавиша CapsLock нажата";
            return " ";
        }

        // Таймер для отслеживания языка и кнопки Caps-Lock.
        private void timer1_Tick(object sender, EventArgs e)
        {
            LanguageBox.Text = GetKeyboardLayoutId();
            CapsLockBox.Text =  GetCapsLockStatus();
        }

        // Метод для получения кода пользователя из базы данных.
        private int GetUserID(string login, string password)
        {
            string connection_args = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database_file;
            OleDbConnection current_connection = new OleDbConnection(connection_args);

            string query = "SELECT Код_пользователя FROM Пользователи WHERE Логин = @login AND Пароль = @password";
            OleDbCommand command = new OleDbCommand(query, current_connection);
            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@password", password);

            current_connection.Open();

            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["Код_пользователя"];
                current_connection.Close();

                return id;
            }

            current_connection.Close();

            return 0;
        }

        // Метод для хэширования пароля.
        public static string CreateMD5(string password)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] input_bytes = System.Text.Encoding.ASCII.GetBytes(password);
                byte[] hash_bytes = md5.ComputeHash(input_bytes);

                return BitConverter.ToString(hash_bytes);
            }
        }

        // Метод для кнопки "Войти".
        private void EnterButton_Click(object sender, EventArgs e)
        {
            if (LoginBox.Text.Contains(' ') || PasswordBox.Text.Contains(' '))
            {
                MessageBox.Show("Пункты не могут содержать пробелы!");
                return;
            }

            string password = CreateMD5(PasswordBox.Text);        

            int id = GetUserID(LoginBox.Text, password);

            if (id == 0)
                MessageBox.Show("Неверное имя пользователя или пароль!");
            else if (id > 0)
            {
                MenuBuilder menu = new MenuBuilder(id);
                
                this.Hide();
                menu.ShowForm();
                this.Show();
            }
        }

        // Метод для кнопки регистрации.
        private void registration_button_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();

            this.Hide();
            form.ShowDialog();
            this.Show();
        }
    }
}
