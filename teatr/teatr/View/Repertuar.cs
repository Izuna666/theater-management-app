using okno_logowania.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Xml.Linq;


public class Repertuar
{
    /*public bool IsSelected { get; set; }*/
    public int ID { get; set; }
    public string? Godzina { get; set; }
    public string? Data { get; set; }
    public string? Sala { get; set; }
    public string? Nazwa { get; set; }
    public string? LiczbaMiejsc { get; set; }
    public string? Cena { get; set; }
}


public class RepertuarRepository
{
    private string connectionString = "server=localhost;port=3306;database=Login;uid=root;password=yhym2137;";

    public void AddRepertuar(Repertuar repertuar)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "INSERT INTO repertuar ( godzina, data, sala, nazwa, liczbaMiejsc, cena) " +
            $"VALUES ('{repertuar.Godzina}','{repertuar.Data}', '{repertuar.Sala}', '{repertuar.Nazwa}', '{repertuar.LiczbaMiejsc}', '{repertuar.Cena}')";
            MySqlCommand createCommand = new MySqlCommand(query, connection);
            createCommand.ExecuteNonQuery();
        }
    }

    public void RemoveRepertuar(int ID)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = $"DELETE FROM repertuar WHERE ID = {ID}";
            MySqlCommand createCommand = new MySqlCommand(query, connection);
            createCommand.ExecuteNonQuery();
        }
    }

    public void UpdateRepertuar(Repertuar repertuar, int ID)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();            
            string query = $"UPDATE repertuar SET godzina = '{repertuar.Godzina}', data = '{repertuar.Data}', sala = '{repertuar.Sala}', nazwa= '{repertuar.Nazwa}', liczbaMiejsc= '{repertuar.LiczbaMiejsc}', cena= '{repertuar.Cena}' WHERE ID = {ID}";
            MySqlCommand createCommand = new MySqlCommand(query, connection);
            createCommand.ExecuteNonQuery();
        }
    }

    public List<Repertuar> GetRepertuarList()
    {
        List<Repertuar> repertuarList = new List<Repertuar>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT ID, godzina, data, sala, nazwa, liczbaMiejsc, cena FROM repertuar";
            MySqlCommand createCommand = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = createCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Repertuar repertuar = new Repertuar()
                    {
                        ID = reader.GetInt16("ID"),
                        Godzina = reader.GetString("godzina"),
                        Data = reader.GetString("data"),
                        Sala = reader.GetString("sala"),
                        Nazwa = reader.GetString("nazwa"),
                        LiczbaMiejsc = reader.GetString("liczbaMiejsc"),
                        Cena = reader.GetString("cena")
                    };
                    repertuarList.Add(repertuar);
                }
            }
        }

        return repertuarList;
    }
}
