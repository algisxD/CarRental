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
    public class ZaidimasRepository
    {
        public List<Zaidimas> getZaidimai()
        {
            List<Zaidimas> remejai = new List<Zaidimas>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            //string sqlquery = "select * from "+Globals.dbPrefix+"darbuotojai";
            string sqlquery = @"SELECT m.pavadinimas, m.zanras, m.versija, m.kurejas, m.id_ZAIDIMAS
                                FROM " + "zaidimas m";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                remejai.Add(new Zaidimas
                {
                    pavadinimas = Convert.ToString(item["pavadinimas"]),
                    zanras = Convert.ToString(item["zanras"]),
                    versija = Convert.ToString(item["versija"]),
                    kurejas = Convert.ToString(item["kurejas"]),
                    id = Convert.ToString(item["id_ZAIDIMAS"])
                });
            }

            return remejai;
        }

        public Zaidimas getZaidimas(string id)
        {
            Zaidimas zaidimas = new Zaidimas();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.pavadinimas, m.zanras, m.versija, m.kurejas, m.id_ZAIDIMAS 
                                FROM " + "zaidimas m WHERE m.id_ZAIDIMAS='" + id + "'";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            //mySqlCommand.Parameters.Add("?tab", MySqlDbType.VarChar).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                zaidimas.pavadinimas = Convert.ToString(item["pavadinimas"]);
                zaidimas.zanras = Convert.ToString(item["zanras"]);
                zaidimas.versija = Convert.ToString(item["versija"]);
                zaidimas.kurejas = Convert.ToString(item["kurejas"]);
                zaidimas.id = Convert.ToString(item["id_ZAIDIMAS"]);
            }

            return zaidimas;
        }

        public bool updateZaidimas(Zaidimas zaidimas)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                //string sqlquery = @"UPDATE "+"darbuotojai a SET a.vardas=?vardas, a.pavarde=?pavarde WHERE a.tabelio_nr=?tab";
                //string sqlquery = @"UPDATE `organizacija` SET `salis` = ?salis, `pavadinimas` = ?pavadinimas WHERE pavadinimas='" + organizacija.pavadinimas + "'";
                string sqlquery = @"UPDATE `zaidimas` SET `pavadinimas` = ?pavadinimas, `zanras` = ?zanras, `versija` = ?versija, `kurejas` = ?kurejas WHERE id_ZAIDIMAS='" + zaidimas.id + "'";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.String).Value = zaidimas.pavadinimas;
                mySqlCommand.Parameters.Add("?zanras", MySqlDbType.String).Value = zaidimas.zanras;
                mySqlCommand.Parameters.Add("?versija", MySqlDbType.String).Value = zaidimas.versija;
                mySqlCommand.Parameters.Add("?kurejas", MySqlDbType.String).Value = zaidimas.kurejas;
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

        public bool addZaidimas(Zaidimas zaidimas)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                //string sqlquery = @"INSERT INTO "+Globals.dbPrefix+"darbuotojai(tabelio_nr,vardas,pavarde)VALUES(?tabelio_nr,?vardas,?pavarde);";
                //string sqlquery = @"INSERT INTO `organizacija` ( `salis`, `pavadinimas`) VALUES ( ?salis, ?pavadinimas)";
                string sqlquery = @"INSERT INTO `zaidimas` ( `pavadinimas`, `zanras`, `versija`, `kurejas`, `id_ZAIDIMAS`) VALUES ( ?pavadinimas, ?zanras, ?versija, ?kurejas, ?id_ZAIDIMAS)";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.String).Value = zaidimas.pavadinimas;
                mySqlCommand.Parameters.Add("?zanras", MySqlDbType.String).Value = zaidimas.zanras;
                mySqlCommand.Parameters.Add("?versija", MySqlDbType.String).Value = zaidimas.versija;
                mySqlCommand.Parameters.Add("?kurejas", MySqlDbType.String).Value = zaidimas.kurejas;
                mySqlCommand.Parameters.Add("?id_ZAIDIMAS", MySqlDbType.String).Value = zaidimas.id;
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

        public int getZaidimasSutarciuCount(string id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(nr) as kiekis from "+Globals.dbPrefix+"sutartys where fk_zaidimas= '" + id+"'";
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

        public void deleteZaidimas(string id)
        {
            int temp = int.Parse(id);
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM " + "zaidimas where id_ZAIDIMAS=" + temp;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.String).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}