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
    public class RemejasRepository
    {
        public List<Remejas> getRemejai()
        {
            List<Remejas> remejai = new List<Remejas>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            //string sqlquery = "select * from "+Globals.dbPrefix+"darbuotojai";
            string sqlquery = @"SELECT m.pavadinimas, m.salis, m.suma, m.id_REMEJAS
                                FROM " + "remejas m";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                remejai.Add(new Remejas
                {
                    pavadinimas = Convert.ToString(item["pavadinimas"]),
                    salis = Convert.ToString(item["salis"]),
                    suma = Convert.ToString(item["suma"]),
                    id = Convert.ToString(item["id_REMEJAS"])
                });
            }

            return remejai;
        }

        public Remejas getRemejas(string id)
        {
            Remejas remejas = new Remejas();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.pavadinimas, m.salis, m.suma, m.id_REMEJAS 
                                FROM " + "remejas m WHERE m.id_REMEJAS='" + id + "'";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            //mySqlCommand.Parameters.Add("?tab", MySqlDbType.VarChar).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                remejas.pavadinimas = Convert.ToString(item["pavadinimas"]);
                remejas.salis = Convert.ToString(item["salis"]);
                remejas.suma = Convert.ToString(item["suma"]);
                remejas.id = Convert.ToString(item["id_REMEJAS"]);
            }

            return remejas;
        }

        public bool updateRemejas(Remejas remejas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `remejas` SET `pavadinimas` = ?pavadinimas, `salis` = ?salis, `suma` = ?suma WHERE id_REMEJAS=" + remejas.id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.String).Value = remejas.pavadinimas;
            mySqlCommand.Parameters.Add("?salis", MySqlDbType.String).Value = remejas.salis;
            mySqlCommand.Parameters.Add("?suma", MySqlDbType.String).Value = remejas.suma;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool addRemejas(Remejas remejas)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                //string sqlquery = @"INSERT INTO "+Globals.dbPrefix+"darbuotojai(tabelio_nr,vardas,pavarde)VALUES(?tabelio_nr,?vardas,?pavarde);";
                //string sqlquery = @"INSERT INTO `organizacija` ( `salis`, `pavadinimas`) VALUES ( ?salis, ?pavadinimas)";
                string sqlquery = @"INSERT INTO `remejas` ( `pavadinimas`, `salis`, `suma`, `id_REMEJAS`) VALUES ( ?pavadinimas, ?salis, ?suma, ?id_REMEJAS)";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.String).Value = remejas.pavadinimas;
                mySqlCommand.Parameters.Add("?salis", MySqlDbType.String).Value = remejas.salis;
                mySqlCommand.Parameters.Add("?suma", MySqlDbType.String).Value = remejas.suma;
                mySqlCommand.Parameters.Add("?id_REMEJAS", MySqlDbType.String).Value = remejas.id;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int getRemejasSutarciuCount(string id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(nr) as kiekis from "+Globals.dbPrefix+"sutartys where fk_remejas= '" + id+"'";
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

        public void deleteRemejas(string id)
        {
            int temp = int.Parse(id);
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM " + "remejas where id_REMEJAS=" + temp;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.String).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}