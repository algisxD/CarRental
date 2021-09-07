using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using AutoNuoma.ViewModels;
using MySql.Data.MySqlClient;

namespace AutoNuoma.Repos
{
    public class RenginiuRepository
    {
        public List<RenginysViewModel> getRenginiai()
        {
            List<RenginysViewModel> renginysViewModels = new List<RenginysViewModel>();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            //string sqlquery = @"SELECT m.id, m.pavadinimas, mm.pavadinimas AS marke 
            //                    FROM " + Globals.dbPrefix + @"renginiai m
            //                    LEFT JOIN " + Globals.dbPrefix + @"markes mm ON mm.id=m.fk_marke";
            string sqlquery = @"SELECT b.id_RENGINYS as pavadinimas, a.tipas, a.bilieto_kaina, a.id_RENGINYS FROM renginys a LEFT JOIN b ON b.id_RENGINYS = a.id_RENGINYS;";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                renginysViewModels.Add(new RenginysViewModel
                {
                    pavadinimas = Convert.ToString(item["pavadinimas"]),
                    tipas = Convert.ToString(item["tipas"]),
                    bilietoKaina = Convert.ToString(item["bilieto_kaina"]),
                    id = Convert.ToString(item["id_RENGINYS"]),
                    arena = Convert.ToString(item["fk_rengeja"])
                });
            }

            return renginysViewModels;
        }

        public RenginysEditViewModel getRenginys(string id)
        {
            RenginysEditViewModel renginys = new RenginysEditViewModel();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.* 
                                FROM " + Globals.dbPrefix + @"renginiai m WHERE m.id=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                renginys.pavadinimas = Convert.ToString(item["pavadinimas"]);
                renginys.tipas = Convert.ToString(item["tipas"]);
                renginys.bilietoKaina = Convert.ToString(item["bilietoKaina"]);
                renginys.id = Convert.ToString(item["id_RENGINYS"]);
                renginys.fk_arena = Convert.ToString(item["fk_rengeja"]);
            }

            return renginys;
        }

        public bool updateRenginys(RenginysEditViewModel renginys)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE " + Globals.dbPrefix + "renginiai a SET a.pavadinimas=?pavadinimas, a.fk_marke=?marke WHERE a.id=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.String).Value = renginys.pavadinimas;
            mySqlCommand.Parameters.Add("?tipas", MySqlDbType.String).Value = renginys.tipas;
            mySqlCommand.Parameters.Add("?bilietoKaina", MySqlDbType.String).Value = renginys.bilietoKaina;
            mySqlCommand.Parameters.Add("?fk_rengeja", MySqlDbType.String).Value = renginys.fk_arena;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool addRenginys(RenginysEditViewModel renginys)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO " + Globals.dbPrefix + "renginiai(pavadinimas,fk_marke)VALUES(?pavadinimas,?marke)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.String).Value = renginys.pavadinimas;
            mySqlCommand.Parameters.Add("?tipas", MySqlDbType.String).Value = renginys.tipas;
            mySqlCommand.Parameters.Add("?bilietoKaina", MySqlDbType.String).Value = renginys.bilietoKaina;
            mySqlCommand.Parameters.Add("?fk_rengeja", MySqlDbType.String).Value = renginys.fk_arena;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        //public int getRenginysCount(int id)
        //{
        //    int naudota = 0;
        //    string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
        //    MySqlConnection mySqlConnection = new MySqlConnection(conn);
        //    string sqlquery = @"SELECT count(id) as kiekis from " + Globals.dbPrefix + "automobiliai where fk_renginys=" + id;
        //    MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
        //    mySqlConnection.Open();
        //    MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
        //    DataTable dt = new DataTable();
        //    mda.Fill(dt);
        //    mySqlConnection.Close();

        //    foreach (DataRow item in dt.Rows)
        //    {
        //        naudota = Convert.ToInt32(item["kiekis"] == DBNull.Value ? 0 : item["kiekis"]);
        //    }
        //    return naudota;
        //}

        public void deleteRenginys(string id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM " + "renginiai where id=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.String).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }


    }
}