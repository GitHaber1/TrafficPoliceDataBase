using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficPoliceDB
{
    public partial class Form2 : Form
    {
        // Параметры для подключения к базе данных.
        private string connection_args = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DB.mdb";

        // Конструктор для формы.
        public Form2()
        {
            InitializeComponent();
        }

        // Метод для проверки корректности ФИО.
        private bool IsNum(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            foreach (char c in text)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }

            return false;
        }

        // Метод для проверки наличия такого же пользователя.
        private bool IsAnotherSameUser()
        {
            OleDbConnection connection = new OleDbConnection(connection_args);
            connection.Open();

            string password = Form1.CreateMD5(password_box.Text);

            OleDbCommand command = new OleDbCommand("SELECT COUNT(*) FROM Пользователи WHERE Логин = @login AND Пароль = @pass", connection);
            command.Parameters.AddWithValue("@login", login_box.Text);
            command.Parameters.AddWithValue("@pass", password);
            int count = (int)command.ExecuteScalar();

            connection.Close();

            return count > 0;
        }

        // Метод для полной проверки введенных данных.
        private bool CheckAllInfo()
        {
            if (last_name_box.Text.Length == 0 || first_name_box.Text.Length == 0 || patr_box.Text.Length == 0 || login_box.Text.Length == 0 || password_box.Text.Length == 0)
            {
                MessageBox.Show("Введите всю информацию!");
                return false;
            }

            if (IsNum(last_name_box.Text) || IsNum(first_name_box.Text) || IsNum(patr_box.Text))
            {
                MessageBox.Show("Проверьте правильность введенных данных!");
                return false;
            }

            if (IsAnotherSameUser())
            {
                MessageBox.Show("Пользователь с таким логином уже есть в базе данных!");
                return false;
            }

            return true;
        }

        // Метод для кнопки "Зарегистрироваться".
        private void registr_button_Click(object sender, EventArgs e)
        {
            if (!CheckAllInfo())
            {
                return;
            }

            OleDbConnection connection = new OleDbConnection(connection_args);
            connection.Open();

            string password = Form1.CreateMD5(password_box.Text);
            OleDbCommand command = new OleDbCommand("INSERT INTO Пользователи (Логин, Пароль) VALUES (@login, @pass)", connection);
            command.Parameters.AddWithValue("@login", login_box.Text);
            command.Parameters.AddWithValue("@pass", password);
            command.ExecuteNonQuery();

            command = new OleDbCommand("SELECT MAX(Код_пользователя) FROM Пользователи", connection);
            int new_user_id = (int)command.ExecuteScalar();

            command = new OleDbCommand("INSERT INTO Сотрудник_ГИБДД (Фамилия, Имя, Отчество, Код_должности_сотрудника) VALUES (@last, @first, @patr, @id)", connection);
            command.Parameters.AddWithValue("@last", last_name_box.Text);
            command.Parameters.AddWithValue("@first", first_name_box.Text);
            command.Parameters.AddWithValue("@patr", patr_box.Text);
            command.Parameters.AddWithValue("@id", 4);
            command.ExecuteNonQuery();

            command = new OleDbCommand("SELECT MAX(Код_сотрудника_ГИБДД) FROM Сотрудник_ГИБДД", connection);
            int new_empl_id = (int)command.ExecuteScalar();

            command = new OleDbCommand("INSERT INTO Данные_пользователя (Код_пользователя, Код_сотрудника_ГИБДД) VALUES (@user_id, @empl_id)", connection);
            command.Parameters.AddWithValue("@user_id", new_user_id);
            command.Parameters.AddWithValue("@empl_id", new_empl_id);
            command.ExecuteNonQuery();

            string query = "INSERT INTO Доступ_пользователя (Код_пользователя, Код_пункта_меню, R, W, E, D) " + 
                "VALUES (@user_id, @menu_id, @R, @W, @E, @D)";
            command = new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", new_user_id);
            command.Parameters.AddWithValue("@menu_id", 5);
            command.Parameters.AddWithValue("@R", true);
            command.Parameters.AddWithValue("@W", true);
            command.Parameters.AddWithValue("@E", true);
            command.Parameters.AddWithValue("@D", true);
            command.ExecuteNonQuery();

            command = new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", new_user_id);
            command.Parameters.AddWithValue("@menu_id", 6);
            command.Parameters.AddWithValue("@R", true);
            command.Parameters.AddWithValue("@W", true);
            command.Parameters.AddWithValue("@E", true);
            command.Parameters.AddWithValue("@D", true);
            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Регистрация прошла успешно!");

            this.Close();
        }
    }
}
