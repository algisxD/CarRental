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
    public class ArenaRepository
    {
        public List<Arena> getArenos()
        {
            List<Arena> arenos = new List<Arena>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            //string sqlquery = "select * from "+Globals.dbPrefix+"darbuotojai";
            string sqlquery = @"SELECT m.pavadinimas, m.salis, m.adresas, m.vietu_skaicius, m.id_ARENA
                                FROM " + "arena m";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                arenos.Add(new Arena
                {
                    pavadinimas = Convert.ToString(item["pavadinimas"]),
                    salis = Convert.ToString(item["salis"]),
                    adresas = Convert.ToString(item["adresas"]),
                    vietuSkaicius = Convert.ToString(item["vietu_skaicius"]),
                    id = Convert.ToString(item["id_ARENA"])
                });
            }

            return arenos;
        }

        public Arena getArena(string id)
        {
            Arena arena = new Arena();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.pavadinimas, m.salis, m.adresas, m.vietu_skaicius, m.id_ARENA 
                                FROM " + "arena m WHERE m.id_ARENA='" + id + "'";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            //mySqlCommand.Parameters.Add("?tab", MySqlDbType.VarChar).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                arena.pavadinimas = Convert.ToString(item["pavadinimas"]);
                arena.salis = Convert.ToString(item["salis"]);
                arena.adresas = Convert.ToString(item["adresas"]);
                arena.vietuSkaicius = Convert.ToString(item["vietu_skaicius"]);
                arena.id = Convert.ToString(item["id_ARENA"]);
            }

            return arena;
        }

        public bool updateArena(Arena arena)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `arena` SET `pavadinimas` = ?pavadinimas, `salis` = ?salis, `adresas` = ?adresas, `vietu_skaicius` = ?vietu_skaicius WHERE id_ARENA=" + arena.id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.String).Value = arena.pavadinimas;
            mySqlCommand.Parameters.Add("?salis", MySqlDbType.String).Value = arena.salis;
            mySqlCommand.Parameters.Add("?adresas", MySqlDbType.String).Value = arena.adresas;
            mySqlCommand.Parameters.Add("?vietu_skaicius", MySqlDbType.String).Value = arena.vietuSkaicius;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool addArena(Arena arena)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);

                string sqlquery = @"INSERT INTO `arena` ( `pavadinimas`, `salis`, `adresas`, `vietu_skaicius`, `id_ARENA`) VALUES ( ?pavadinimas, ?salis, ?adresas, ?vietu_skaicius, ?id_ARENA)";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.String).Value = arena.pavadinimas;
                mySqlCommand.Parameters.Add("?salis", MySqlDbType.String).Value = arena.salis;
                mySqlCommand.Parameters.Add("?adresas", MySqlDbType.String).Value = arena.adresas;
                mySqlCommand.Parameters.Add("?vietu_skaicius", MySqlDbType.String).Value = arena.vietuSkaicius;
                mySqlCommand.Parameters.Add("?id_ARENA", MySqlDbType.String).Value = arena.id;
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

        public int getArenaSutarciuCount(string id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(nr) as kiekis from "+Globals.dbPrefix+"sutartys where fk_arena= '" + id+"'";
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

        public void deleteArena(string id)
        {
            int temp = int.Parse(id);
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM " + "arena where id_ARENA=" + temp;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.String).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}