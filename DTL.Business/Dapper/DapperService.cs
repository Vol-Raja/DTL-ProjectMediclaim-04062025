using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DTL.Business.Dapper
{
    public class DapperService : IDapperService
    {
        private static string connectionString;
        public static Dictionary<Type, DbType> typeMap = new Dictionary<Type, DbType> {
            { typeof(byte), DbType.Byte},
            { typeof(sbyte), DbType.SByte},
            { typeof(short), DbType.Int16},
            { typeof(ushort), DbType.UInt16},
            { typeof(int), DbType.Int32},
            { typeof(uint), DbType.UInt32},
            { typeof(long), DbType.Int64},
            { typeof(ulong), DbType.UInt64},
            { typeof(float), DbType.Single},
            { typeof(double), DbType.Double},
            { typeof(decimal), DbType.Decimal},
            { typeof(bool), DbType.Boolean},
            { typeof(string), DbType.String},
            { typeof(char), DbType.StringFixedLength},
            { typeof(Guid), DbType.Guid},
            { typeof(DateTime), DbType.DateTime},
            { typeof(DateTimeOffset), DbType.DateTimeOffset},
            { typeof(byte[]), DbType.Binary},
            { typeof(byte?), DbType.Byte},
            { typeof(sbyte?), DbType.SByte},
            { typeof(short?), DbType.Int16},
            { typeof(ushort?), DbType.UInt16},
            { typeof(int?), DbType.Int32},
            { typeof(uint?), DbType.UInt32},
            { typeof(long?), DbType.Int64},
            { typeof(ulong?), DbType.UInt64},
            { typeof(float?), DbType.Single},
            { typeof(double?), DbType.Double},
            { typeof(decimal?), DbType.Decimal},
            { typeof(bool?), DbType.Boolean},
            { typeof(char?), DbType.StringFixedLength},
            { typeof(Guid?), DbType.Guid},
            { typeof(DateTime?), DbType.DateTime},
            { typeof(DateTimeOffset?), DbType.DateTimeOffset},
            //{ typeof(System.Data.Linq.Binary), DbType.Binary},
        };

        public DapperService()
        {

            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
            connectionString = root.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

        }


        public DapperService(AutoMapper.Configuration.IConfiguration config)
        {
            _config = config;
        }
        public void Dispose()
        {

        }
        private bool disposedValue;
        private AutoMapper.Configuration.IConfiguration _config;



        /// <summary>
        /// Excute SP
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="parms"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using var db = new SqlConnection(connectionString);
            try
            {

                if (db.State == ConnectionState.Closed)
                    db.Open();

                return db.Execute(sp, parms, commandType: commandType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }

        /// <summary>
        /// Get Method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sp"></param>
        /// <param name="parms"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using var db = new SqlConnection(connectionString);
            try
            {

                if (db.State == ConnectionState.Closed)
                    db.Open();

                return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }

        public DataSet GetDataSet(string sp, Dictionary<string, object> param, CommandType commandType = CommandType.StoredProcedure)
        {
            using var db = new SqlConnection(connectionString);
            try
            {

                if (db.State == ConnectionState.Closed)
                    db.Open();


                SqlCommand cmd = new SqlCommand(sp, db)
                {
                    CommandType = commandType
                };

                if (param != null)
                    foreach (KeyValuePair<string, object> kvp in param)
                        cmd.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                // execute the command
                var reader = cmd.ExecuteReader();
                return ConvertDataReaderToDataSet(reader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }

        public DataSet ConvertDataReaderToDataSet(IDataReader data)
        {
            var ds = new DataSet();
            int i = 0;
            while (!data.IsClosed)
            {
                ds.Tables.Add("Table" + (i + 1));
                ds.EnforceConstraints = false;
                ds.Tables[i].Load(data);
                i++;
            }
            return ds;
        }

        /// <summary>
        /// Get All Method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sp"></param>
        /// <param name="parms"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using var db = new SqlConnection(connectionString);
            try
            {

                //if (db.State == ConnectionState.Closed)
                //    db.Open();

                return db.Query<T>(sp, parms, commandType: commandType).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (db.State == ConnectionState.Open)
                //    db.Close();
            }
        }

        /// <summary>
        /// Get All Method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sp"></param>
        /// <param name="parms"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using var db = new SqlConnection(connectionString);
            try
            {

                //if (db.State == ConnectionState.Closed)
                //    db.Open();

                return await db.QueryAsync<T>(sp, parms, commandType: commandType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (db.State == ConnectionState.Open)
                //    db.Close();
            }
        }

        /// <summary>
        /// DB Connection
        /// </summary>
        /// <returns></returns>
        public DbConnection GetDbconnection()
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Insert Method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sp"></param>
        /// <param name="parms"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(connectionString);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        /// <summary>
        /// Update Method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sp"></param>
        /// <param name="parms"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(connectionString);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        public DynamicParameters GetDynamicParams<T>(T obj, string outParam, IEnumerable<string> IgnoreProperties = null)
        {
            if (IgnoreProperties == null) IgnoreProperties = new List<string>();
            DynamicParameters res = new DynamicParameters();
            var props = typeof(T).GetProperties();
            foreach (var p in props)
            {
                if (IgnoreProperties.Contains(p.Name)) continue;
                if (p.Name == outParam) { res.Add("@" + p.Name, p.GetValue(obj), DbType.Int32, ParameterDirection.Output); continue; }
                res.Add("@" + p.Name, p.GetValue(obj), typeMap[p.PropertyType]);
            }
            return res;
        }

    }
}
