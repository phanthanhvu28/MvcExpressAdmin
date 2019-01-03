using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


public class Conn
{
    public SqlConnection Connct()
    {
        //System.Data.Odbc.SqlConnection cn = new System.Data.Odbc.SqlConnection("" + ConfigurationSettings.AppSettings["mycon"]);
        // string cnnString = ConfigurationManager.ConnectionStrings["ConnectDB"].ToString();
        SqlConnection cn = new SqlConnection("" + ConfigurationManager.ConnectionStrings["NewsDataContext"]);
        cn.Open();
        return cn;
    }
    public static String Right(String s, int len)
    {
        if (s.Length <= len) return s.ToString();
        else
        {
            String s2 = s.Substring(s.Length - len, len).ToString();
            return s2;
        }
    }   
    public static int CVInt(object value)
    {
        try
        {

            if (value != null)
            {
                int num = int.Parse("" + value);
                return num;
            }
        }
        catch
        {
            return 0;
        }
        return 0;
    }
    public static short CVShort(object value)
    {
        try
        {

            if (value != null)
            {
                short num = short.Parse("" + value);
                return num;
            }
        }
        catch
        {
            return 0;
        }
        return 0;
    }
    public static float CVFloat(object value)
    {
        try
        {

            if (value != null)
            {
                float num = float.Parse("" + value);
                return num;
            }
        }
        catch
        {
            return 0;
        }
        return 0;
    }
    public static double CVDouble(object value)
    {
        try
        {

            if (value != null)
            {
                double num = double.Parse("" + value);
                return num;
            }
        }
        catch
        {
            return 0;
        }
        return 0;
    }

    public static DataTable getTable(string sql)
    {
        DataTable dt = new DataTable();
        Conn fns = new Conn();
        SqlConnection conn = fns.Connct();
        try
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
        }
        catch
        { }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return dt;
    }
    public static string getInfo(string cmd)
    {
        DataTable rs = Conn.getTable(cmd);
        if (rs.Rows.Count > 0)
        {
            return "" + rs.Rows[0][0];
        }
        return "";
    }
    public static DataTable getTable(SqlCommand cmd)
    {
        DataTable dt = new DataTable();
        Conn fns = new Conn();
        SqlConnection conn = fns.Connct();
        try
        {

            cmd.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();

        }
        catch
        { }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return dt;
    }
    public static bool ExecuteNonQuery(SqlCommand cmd)
    {
        Conn fns = new Conn();
        SqlConnection conn = fns.Connct();
        try
        {

            cmd.Connection = conn;
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
            {
                conn.Open();
            }
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            return true;
        }
        catch
        {

            return false;
        }
        finally
        {
            conn.Close();
            conn.Dispose();

        }
    }
    public static bool DeleteRowData(string table, string clum, string data)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete from " + table + " where " + clum + "=@column";
            cmd.Parameters.AddWithValue("@column", data);
            if (Conn.ExecuteNonQuery(cmd) == true)
            {
                return true;
            }
            cmd.Dispose();
        }
        catch
        { }
        return false;
    }

    public static bool DeleteRowData(string sql)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            if (Conn.ExecuteNonQuery(cmd) == true)
            {
                return true;
            }
            cmd.Dispose();
        }
        catch
        { }
        return false;
    }
    public static bool UpdateRowData(string sql)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;          
            if (Conn.ExecuteNonQuery(cmd) == true)
            {
                return true;
            }
            cmd.Dispose();
        }
        catch
        { }
        return false;
    }
    public static bool InsertBatch(string sql)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = 600;
            if (Conn.ExecuteNonQuery(cmd) == true)
            {
                return true;
            }
            cmd.Dispose();
        }
        catch
        { }
        return false;
    }

    public static string CreateCode(string chuoi, string bang, string cot)
    {
        string trave = chuoi + "01";
        try
        {
            DataTable rs = Conn.getTable("select isnull(Max(convert(numeric,replace(" + cot + ",'" + chuoi + "',''))+1),1) from " + bang + " where left(" + cot + ",len('" + chuoi + "'))='" + chuoi + "'");//  order  by convert(numeric,replace(" + cot + ",'" + chuoi + "','')) desc");
            if (rs.Rows.Count > 0)
            {
                if (CVDecimal("" + rs.Rows[0][0]) > 9)
                {
                    trave = chuoi + "" + rs.Rows[0][0];
                }
                else
                { trave = chuoi + "0" + rs.Rows[0][0]; }
            }
            rs.Dispose();
        }
        catch
        { }
        return trave;
    }
    public static decimal CVDecimal(object value)
    {
        try
        {

            if (value != null)
            {
                string gt = "" + value;

                gt = gt.Replace(".", "");
                gt = gt.Replace(",", ".");

                decimal num = decimal.Parse(gt);
                return num;
            }
        }
        catch
        {
            return 0;
        }
        return 0;
    }
    public static DateTime CVDate(object value)
    {
        try
        {
            if (value != null)
            {
                DateTime num = DateTime.Parse("" + value);
                return num;
            }
        }
        catch
        {

        }
        return Convert.ToDateTime("01/01/1900");
    }
  
}