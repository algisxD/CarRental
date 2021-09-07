using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using AutoNuoma.Models;
using MySql.Data.MySqlClient;

namespace AutoNuoma.Repos
{
    public class OrganizacijaRepository
    {
        public List<Organizacija> getOrganizacijos()
        {
            List<Organizacija> organizacijos = new List<Organizacija>();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.salis, m.pavadinimas, m.id_ORGANIZACIJA
                                FROM "+"organizacija m";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                organizacijos.Add(new Organizacija
                {
                    salis = Convert.ToString(item["salis"]),
                    pavadinimas = Convert.ToString(item["pavadinimas"]),
                    id = Convert.ToString(item["id_ORGANIZACIJA"])
                });
            }

            return organizacijos;
        }

        public Organizacija getOrganizacija(string id)
        {
            Organizacija organizacija = new Organizacija();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.salis, m.pavadinimas, m.id_ORGANIZACIJA
                                FROM " + "organizacija m WHERE m.id_ORGANIZACIJA='" + id + "'";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {

                organizacija.salis = Convert.ToString(item["salis"]);
                organizacija.pavadinimas = Convert.ToString(item["pavadinimas"]);
                organizacija.id = Convert.ToString(item["id_ORGANIZACIJA"]);
            }

            return organizacija;
        }

        public bool updateOrganizacija(Organizacija organizacija)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `organizacija` SET `salis` = ?salis, `pavadinimas` = ?pavadinimas WHERE id_ORGANIZACIJA=" + organizacija.id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?salis", MySqlDbType.String).Value = organizacija.salis;
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.String).Value = organizacija.pavadinimas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool addOrganizacija(Organizacija organizacija)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `organizacija` ( `salis`, `pavadinimas`, `id_ORGANIZACIJA`) VALUES ( ?salis, ?pavadinimas, ?id_ORGANIZACIJA)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.String).Value = organizacija.pavadinimas;
            mySqlCommand.Parameters.Add("?salis", MySqlDbType.String).Value = organizacija.salis;
            mySqlCommand.Parameters.Add("?id_ORGANIZACIJA", MySqlDbType.String).Value = organizacija.id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public int getOrganizacijaCount(string salis)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(salis) as kiekis from "+"modeliai where fk_organizacija=" + salis;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                naudota = Convert.ToInt32(item["kiekis"] == DBNull.Value ? 0 : item["kiekis"]);
            }
            return naudota;
        }

        public void deleteOrganizacija(string id)
        {
            int temp = int.Parse(id);
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM " + "organizacija where id_ORGANIZACIJA=" + temp;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.String).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

    }
}