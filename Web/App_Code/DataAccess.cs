﻿using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
/// <summary>
/// DataAccess 的摘要描述 TODO
/// </summary>
public class DataAccess
{
    private static DbConnection Connection { get ; set; }
    private static DbCommand Command { get; set; }
    private static DbParameter Parameter { get; set; }
    public DataAccess(SqlConnection connection)
    {
        Connection = connection;
        Command = new SqlCommand();
        Parameter = new SqlParameter();
    }

    public DataAccess(SQLiteConnection connection)
    {
        Connection = connection;
        Command = new SQLiteCommand();
        Parameter = new SQLiteParameter();
    }

    #region DB Data Access
    /// <summary>
    /// GetSingleValueForSQL
    /// </summary>
    /// <param name="cnString">連線字串</param>
    /// <param name="sqlString">SQL語法</param>
    /// <returns>查詢結果</returns>
    public static string GetSingleValueForSQL(string cnString, string sqlString)
    {
        SqlConnection conn = null;
        string result = "";

        try
        {
            conn = new SqlConnection(cnString);
            conn.Open();

            var cm = new SqlCommand(sqlString, conn);
            cm.CommandTimeout = 180;

            result = cm.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            //TODO: catch exception
        }
        finally
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        return result;
    }
    /// <summary>
    /// GetSingleValueForSQL
    /// </summary>
    /// <param name="cnString">連線字串</param>
    /// <param name="sqlString">SQL語法</param>
    /// <param name="parameter">查詢參數</param>
    /// <returns>查詢結果</returns>
    public static string GetSingleValueForSQL(string cnString, string sqlString, SqlParameter parameter)
    {
        var conn = Connection;
        string result = "";

        try
        {
            conn = Connection;
            conn.Open();

            var cm = Command;
            cm.Connection = conn;
            cm.CommandText = sqlString;
            cm.Parameters.Clear();
            cm.Parameters.Add(parameter);
            cm.CommandTimeout = 180;
            result = cm.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            //TODO: catch exception
        }
        finally
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        return result;
    }
    /// <summary>
    /// GetSingleValueForSQL
    /// </summary>
    /// <param name="cnString">連線字串</param>
    /// <param name="sqlString">SQL語法</param>
    /// <param name="_params">查詢參數陣列</param>
    /// <returns>查詢結果</returns>
    public static string GetSingleValueForSQL(string cnString, string sqlString, SqlParameter[] _params)
    {
        SqlConnection conn = null;
        string result = "";

        try
        {
            conn = new SqlConnection(cnString);
            conn.Open();

            var cm = new SqlCommand(sqlString, conn);
            cm.Parameters.Clear();
            foreach (SqlParameter param in _params)
            {
                cm.Parameters.Add(param);
            }

            cm.CommandTimeout = 180;
            result = cm.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            //TODO: catch exception
        }
        finally
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        return result;
    }
    /// <summary>
    /// ExecuteSQLForSQL
    /// </summary>
    /// <param name="cnString">連線字串</param>
    /// <param name="sqlString">SQL語法</param>
    /// <returns>執行是否成功?</returns>
    public static bool ExecuteSQLForSQL(string cnString, string sqlString)
    {
        SqlConnection conn = null;
        bool result = false;

        try
        {
            conn = new SqlConnection(cnString);
            conn.Open();

            var cm = new SqlCommand(sqlString, conn);
            cm.CommandTimeout = 180;
            cm.ExecuteNonQuery();
            result = true;
        }
        catch (Exception ex)
        {
            //TODO: catch exception
        }
        finally
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        return result;
    }
    /// <summary>
    /// ExecuteSQLForSQL
    /// </summary>
    /// <param name="cnString">連線字串</param>
    /// <param name="sqlString">SQL語法</param>
    /// <param name="parameter">查詢參數</param>
    /// <returns>執行是否成功?</returns>
    public static bool ExecuteSQLForSQL(string cnString, string sqlString, SqlParameter parameter) {
        SqlConnection conn = null;
        bool result = false;

        try
        {
            conn = new SqlConnection(cnString);
            conn.Open();

            var cm = new SqlCommand(sqlString, conn);
            cm.Parameters.Clear();
            cm.Parameters.Add(parameter);            
            cm.CommandTimeout = 180;
            cm.ExecuteNonQuery();
            result = true;
        }
        catch (Exception ex)
        {
            //TODO: catch exception
        }
        finally
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        return result;
    }
    /// <summary>
    /// ExecuteSQLForSQL
    /// </summary>
    /// <param name="cnString">連線字串</param>
    /// <param name="sqlString">SQL語法</param>
    /// <param name="_params">參數陣列</param>
    /// <returns>執行是否成功?</returns>
    public static bool ExecuteSQLForSQL(string cnString, string sqlString, SqlParameter[] _params)
    {
        SqlConnection conn = null;
        bool result = false;

        try
        {
            conn = new SqlConnection(cnString);
            conn.Open();

            var cm = new SqlCommand(sqlString, conn);
            cm.Parameters.Clear();
            foreach (var param in _params)
            {
                cm.Parameters.Add(param);
            }
            cm.CommandTimeout = 180;
            cm.ExecuteNonQuery();
            result = true;
        }
        catch (Exception ex)
        {
            //TODO: catch exception
        }
        finally
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        return result;
    }

    /// <summary>
    /// GetDataTableForSQL
    /// </summary>
    /// <param name="cnString">連線字串</param>
    /// <param name="sqlString">SQL語法</param>
    /// <returns>查詢結果</returns>
    public static System.Data.DataTable GetDataTableForSQL(string cnString, string sqlString)
    {
        return GetDataTableForSQL(cnString, sqlString, "Table");
    }
    /// <summary>
    /// GetDataTableForSQL
    /// </summary>
    /// <param name="cnString">連線字串</param>
    /// <param name="sqlString">SQL語法</param>
    /// <param name="tableName">table Name</param>
    /// <returns>查詢結果</returns>
    public static System.Data.DataTable GetDataTableForSQL(string cnString, string sqlString, string tableName)
    {
        DbConnection conn = null;
        DataTable result = new DataTable();

        try
        {
            conn = Connection;
            conn.ConnectionString = cnString;
            conn.Open();

            var cm = Command;
            cm.Connection = conn;
            cm.CommandText = sqlString;                
            result.Load(cm.ExecuteReader());
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        return result;
    }
    /// <summary>
    /// GetDataTableForSQL
    /// </summary>
    /// <param name="cnString">連線字串</param>
    /// <param name="sqlString">SQL語法</param>
    /// <param name="_params">查詢參數陣列</param>
    /// <returns>查詢結果</returns>
    public static System.Data.DataTable GetDataTableForSQL(string cnString, string sqlString, SqlParameter[] _params)
    {
        SqlConnection conn = null;
        DataTable result = new DataTable();

        try
        {
            conn = new SqlConnection(cnString);
            conn.Open();

            var cm = new SqlCommand(sqlString, conn);
            foreach (var param in _params)
            {
                cm.Parameters.Add(param);
            }
            result.Load(cm.ExecuteReader());
        }
        catch (Exception ex)
        {
            //TODO: catch exception
        }
        finally
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        return result;
    }
    /// <summary>
    /// GetDataTableForSQL
    /// </summary>
    /// <param name="cnString">連線字串</param>
    /// <param name="sqlString">SQL語法</param>
    /// <param name="parameter">查詢參數</param>
    /// <returns>查詢結果</returns>
    public static System.Data.DataTable GetDataTableForSQL(string cnString, string sqlString, SqlParameter parameter)
    {
        DbConnection conn = null;
        DataTable result = new DataTable();

        try
        {
            conn = Connection;
            conn.Open();

            var cm = Command;
            Command.Connection = conn;
            Command.CommandText = sqlString;
            cm.Parameters.Add(parameter);
            result.Load(cm.ExecuteReader());
        }
        catch (Exception ex)
        {
            //TODO: catch exception
        }
        finally
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        return result;
    }

    #endregion
}