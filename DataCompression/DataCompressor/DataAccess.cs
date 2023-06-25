using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace DataCompressor
{
    public class DataAccess
    {
        Database database;
        IDbConnection connection;

        public DataAccess() 
        {
            database = (Database)Activator.CreateInstance(Type.GetType("Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase," +
                                                                       "Microsoft.Practices.EnterpriseLibrary.Data," +
                                                                       "Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"),
                                                                       "Data Source = localhost\\SQLExpress; Initial Catalog = TCC; Integrated Security = true");
            connection = database.CreateConnection();
            OpenDatabase();
        } 

        private void OpenDatabase()
        {
            if (connection == null || connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseDatabase()
        {
            if ( connection.State == ConnectionState.Open )
            {
                connection.Close();
            }
        }

        public string GetLastCoilProduced()
        {
            string coilCode = string.Empty;
            DataSet dscoils = database.ExecuteDataSet(CommandType.Text, "SELECT TOP 1 CoilCode FROM ProductionData WHERE RollingEnd IS NOT NULL AND CoilCode NOT IN (SELECT CoilCode FROM CoilDataBin) ORDER BY RollingEnd");

            if(dscoils.Tables.Count > 0 && dscoils.Tables[0].Rows.Count > 0)
            {
                coilCode = dscoils.Tables[0].Rows[0][0].ToString();
            }

            return coilCode;
        }

        public DataSet GetCoilData(string coilCode)
        {
            DataSet dsCoil = database.ExecuteDataSet(CommandType.Text, $"SELECT CONVERT(datetime,[Timestamp],120) [Timestamp]," +
                                                                       $"[CoilCode]," +
                                                                       $"[Signal1]," +
                                                                       $"[Signal2]," +
                                                                       $"[Signal3]," +
                                                                       $"[Signal4]," +
                                                                       $"[Signal5]," +
                                                                       $"[Signal6]," +
                                                                       $"[Signal7]," +
                                                                       $"[Signal8] FROM CoilData WHERE CoilCode = '{coilCode}'");

            return dsCoil;
        }

        public void InsertBlob(string coilCode, byte[] blob)
        {
            object coilData = blob;
            string query = $"INSERT INTO CoilDataBin VALUES (GETDATE(), '{coilCode}', @P0)";
            DbCommand dbCommand = database.GetSqlStringCommand(query);
            database.AddInParameter( dbCommand, "P0", DbType.Binary, coilData );
            database.ExecuteDataSet( dbCommand );
        }

        public DataSet GetCoilDataBlob( string coilCode )
        {
            DataSet dsCoil = database.ExecuteDataSet(CommandType.Text, $"SELECT Blob FROM CoilDataBin WHERE CoilCode = '{coilCode}'");

            return dsCoil;
        }

        public DataSet GetCoils()
        {
            DataSet dsCoils = database.ExecuteDataSet(CommandType.Text, $"SELECT CoilCode, Timestamp FROM CoilDataBin");

            return dsCoils;
        }

        public DataSet GetCoilBin( string coilCode )
        {
            DataSet dsCoils = database.ExecuteDataSet(CommandType.Text, $"SELECT CoilCode, Timestamp FROM CoilDataBin WHERE CoilCode = '{coilCode}'");

            return dsCoils;
        }
    }
}
