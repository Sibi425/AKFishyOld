using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Database data = new Database();

            data.CreateDatabase();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            

        }

        
    }

    public class Database
    {
        public void CreateDatabase()
        {
            string createQuery = @"CREATE TABLE IF NOT EXISTS
                                 [Mytable](
                                 [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                 [Name] NVARCHAR(50) NULL,
                                 [Location] NVARCHAR(30) NULL)";
            System.Data.SQLite.SQLiteConnection.CreateFile("sample.db3");
            using(System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("data source=sample.db3"))
            {
                using(System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    conn.Open();
                    cmd.CommandText = createQuery;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Mytable(Name,Location) values('Fisch','Irgendwo')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT * from Mytable";
                    using(System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["Name"] + ":" + reader["Location"]);
                        }
                      
                    }
                    conn.Close();

                    Console.ReadLine();
                }
            }
        }
    }
}
