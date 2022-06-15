using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace desktop_widget
{
    internal static class Program
    {
        /// <summary>
        /// Создаёт диалог выбора файла и возвращает путь, указанный пользователем.
        /// </summary>
        /// <returns></returns>
        private static string FindDB()
        {
            var openDBDialog = new OpenFileDialog
            {
                Filter = "Database files|*.db; *.sqlite; *.sqlite3|All files|*.*",
                Title = "Выберите файл с базой данных",
            };
            openDBDialog.ShowDialog();
            return openDBDialog.FileName;
        }

        /// <summary>
        /// Создает SQLiteConnection для базы данных по указанному пути.
        /// https://devpractice.ru/sqlite-c/
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static SQLiteConnection ConnectDB(string fileName)
        {
            try
            {
                SQLiteConnection connection = new SQLiteConnection("Data Source=" + fileName + ";Version=3; New=False; FailIfMissing=True;");
                connection.Open();
                return connection;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Ошибка доступа к базе данных. Исключение: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Читает все задачи из таблицы TaskManager_task базы данных.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        private static DataTable ReadDB(SQLiteConnection connection)
        {
            try
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM TaskManager_task;";
                DataTable data = new DataTable();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                adapter.Fill(data);
                Console.WriteLine($"Прочитано {data.Rows.Count} записей из таблицы БД");
                foreach (DataRow row in data.Rows)
                {
                    Console.WriteLine($"id = {row.Field<long>("task_id")}; name = {row.Field<string>("name")}");
                }
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Закрывает базу данных.
        /// </summary>
        /// <param name="connection"></param>
        private static void CloseDB(SQLiteConnection connection)
        {
            connection.Close();
            connection.Dispose();
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SQLiteConnection conn;
            string path;
            DataTable data;
            do
            {
                path = FindDB();
                Console.WriteLine(path);
                conn = ConnectDB(path);
                data = ReadDB(conn);
            } while (data == null);
            CloseDB(conn);
            Application.Run(new MainWindow(data));
        }
    }
}
