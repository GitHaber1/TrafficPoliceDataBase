using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace TrafficPoliceDB
{
    // Структура для удобной записи информации из базы данных.
    struct MenuItem
    {
        public int item_id;
        public int parent_id;
        public string name;
        public string dll;
        public string func;
        public bool R;
        public bool W;
        public bool E;
        public bool D;
        public int menu_index;
    }
    public class MenuBuilder
    {
        // Переменная, в которую будет записываться код пользователя.
        private int user_id;
        // Имя файла с базой данных.
        private string database_file = "DB.mdb";
        string connection_args;

        // Список доступных пользователю пунктов меню.
        private List<MenuItem> menu_items = new List<MenuItem>();

        private Form form;
        private Panel content_panel;
        private OleDbConnection connection;

        // Метод, в котором задаются параметры для формы.
        private void SetParameters()
        {
            form.Text = "База данных ГИБДД";

            form.Size = new System.Drawing.Size(1000, 600);
            form.MaximumSize = form.Size;
            form.MinimumSize = form.Size;

            form.DesktopLocation = new System.Drawing.Point(0, 0);
            form.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        }

        // Метод, который присваивает каждому пункту меню соответствующее действие при нажатии на него.
        private void LoadAndInvoke(MenuItem item)
        {
            try
            {
                if (item.dll == "NULL" || item.func == "NULL")
                    return;

                string folder_path = AppDomain.CurrentDomain.BaseDirectory;
                string dll_path = Path.Combine(folder_path, item.dll);
                Assembly assembly = Assembly.LoadFile(dll_path);

                Type type = null;
                foreach (Type t in assembly.GetTypes())
                {
                    MethodInfo info = t.GetMethod(item.func);
                    if (info != null)
                    {
                        type = t;
                        break;
                    }
                }

                if (item.dll.Equals("ChangePass.dll"))
                {
                    object obj = Activator.CreateInstance(type);
                    MethodInfo method = type.GetMethod(item.func);

                    object[] parameters = { user_id, item.R, item.W, item.E, item.D };

                    object result = method.Invoke(obj, parameters);
                }
                else if (item.dll.Equals("AccessLevel.dll"))
                {
                    object obj = Activator.CreateInstance(type);
                    MethodInfo method = type.GetMethod(item.func);

                    object[] parameters = { user_id, item.R, item.W, item.E, item.D };

                    object result = method.Invoke(obj, parameters);

                    if (result is IEnumerable<Control> controls)
                    {
                        content_panel.Controls.Clear();
                        foreach (Control control in controls)
                        {
                            content_panel.Controls.Add(control);
                        }
                    }
                }
                else if (type != null)
                {
                    object obj = Activator.CreateInstance(type);
                    MethodInfo method = type.GetMethod(item.func);
                    
                    object[] parameters = { item.R, item.W, item.E, item.D};

                    object result = method.Invoke(obj, parameters);

                    if (result is IEnumerable<Control> controls)
                    {
                        content_panel.Controls.Clear();
                        foreach (Control control in controls)
                        {
                            content_panel.Controls.Add(control);
                        }
                    }
                }
            }
            catch 
            {
                MessageBox.Show("Ошибка вызова метода!");
            }
        }

        // Метод для добавления пунктов меню.
        private void AddToMenu(MenuStrip menu)
        {
            MenuItem item;
            for (int i = 0; i < menu_items.Count; i++)
            {
                if (menu_items[i].parent_id == 0 && menu_items[i].R)
                {
                    menu.Items.Add(menu_items[i].name);
                    menu.Items[menu.Items.Count - 1].Enabled = menu_items[i].R;
                    menu.Items[menu.Items.Count - 1].Visible = menu_items[i].R;

                    item = menu_items[i];
                    item.menu_index = menu.Items.Count - 1;
                    menu_items[i] = item;

                    MenuItem local_item = item;
                    menu.Items[menu.Items.Count - 1].Click += (sender, e) => LoadAndInvoke(local_item);
                }
            }
        }

        // Метод для добавления подпунктов меню.
        private void AddToSubMenu(MenuStrip menu)
        {
            for (int i = 0; i < menu_items.Count; i++)
            {
                if (menu_items[i].parent_id > 0 && menu_items[i].R)
                {
                    int index_to_find = menu_items[i].parent_id;
                    int index = menu_items.FindIndex(menu_items => menu_items.item_id == index_to_find);

                    if (index > menu_items.Count - 1 || index < 0)
                        return;

                    ToolStripMenuItem item = (ToolStripMenuItem)menu.Items[menu_items[index].menu_index];                 
                    
                    item.DropDownItems.Add(menu_items[i].name);

                    MenuItem local_item = menu_items[i];
                    item.DropDownItems[item.DropDownItems.Count - 1].Click += (sender, e) => LoadAndInvoke(local_item);

                    item.Enabled = menu_items[i].R;
                }
            }
        }

        // Метод, задающий параметр панели, на которой будут отображаться элементы управления.
        private void SetPanel()
        {
            content_panel = new Panel();

            content_panel.Size = new System.Drawing.Size(1000, 550);
            content_panel.Location = new System.Drawing.Point(0, 50);

            form.Controls.Add(content_panel);
        }

        // Метод, в котором полностью создается меню.
        private void SetMenu()
        {
            MenuStrip menu = new MenuStrip();
            menu.BackColor = System.Drawing.SystemColors.ActiveCaption;

            connection = new OleDbConnection(connection_args);

            string query = "SELECT Код_пункта_меню, R, W, E, D FROM Доступ_пользователя WHERE Код_пользователя = @id";
            OleDbCommand command = new OleDbCommand(query, connection);
            OleDbDataReader reader;
            command.Parameters.AddWithValue("@id", user_id);

            connection.Open();

            reader = command.ExecuteReader();

            MenuItem item;

            while (reader.Read())
            {
                item = new MenuItem();
                query = "SELECT * FROM Меню WHERE Код_пункта_меню = @id";
                OleDbCommand menu_command = new OleDbCommand(query, connection);
                menu_command.Parameters.AddWithValue("@id", reader["Код_пункта_меню"]);

                OleDbDataReader menu_reader = menu_command.ExecuteReader();
                menu_reader.Read();

                item.item_id = (int)menu_reader["Код_пункта_меню"];
                item.parent_id = (int)menu_reader["Код_родительского_пункта"];
                item.name = menu_reader["Название_пункта"].ToString();
                item.dll = menu_reader["DLL"].ToString();
                item.func = menu_reader["Название_функции"].ToString();
                item.R = (bool)reader["R"];
                item.W = (bool)reader["W"];
                item.E = (bool)reader["E"];
                item.D = (bool)reader["D"];
                item.menu_index = Convert.ToInt32(menu_reader["Порядок"]);
                menu_items.Add(item);
            }
            menu_items = menu_items.OrderBy(it => it.menu_index).ToList();
            AddToMenu(menu);
            AddToSubMenu(menu);

            form.Controls.Add(menu);

            connection.Close();
        }

        // Конструктор класса.
        public MenuBuilder(int id) 
        {
            form = new Form();
            connection_args = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database_file;
            user_id = id;

            SetParameters();
            SetPanel();
            SetMenu();
        }

        public void ShowForm()
        {
            form.ShowDialog();
        }
    }
}
