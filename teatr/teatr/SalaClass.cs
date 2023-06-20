using okno_logowania.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

namespace okno_logowania
{
    public class SalaClass
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        private string? name;
        public string? Nazwa
        {
            get { return name; }
            set { name = value; }
        }
        private string? seats;
        public string? LiczbaMiejsc
        {
            get { return seats; }
            set { seats = value; }
        }

        public SalaClass(int id, string name, string seats)
        {
            this.id = id;
            this.name = name;
            this.seats = seats;
        }
    }

    public class SaleRepository
    {
        private string connectionString = "server=localhost;port=3306;database=Login;uid=root;password=yhym2137;";
        private int id1;
        private string name1;
        private string seats1;

        public List<SalaClass> GetSaleList()
        {
            List<SalaClass> ListaSali = new List<SalaClass>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ID, NazwaSali, LiczbaMiejsc FROM Sale";
                MySqlCommand createCommand = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = createCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SalaClass Sale = new SalaClass(id1, name1, seats1)
                        {
                            ID = reader.GetInt16("ID"),
                            Nazwa = reader.GetString("NazwaSali"),
                            LiczbaMiejsc = reader.GetString("LiczbaMiejsc")
                        };
                        ListaSali.Add(Sale);
                    }
                }

            }
            return ListaSali;
        }
    }

}
