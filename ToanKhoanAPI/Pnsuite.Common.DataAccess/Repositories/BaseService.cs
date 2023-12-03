using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using vn.com.pnsuite.common.dataaccess.interfaces;
using vn.com.pnsuite.common.models;
using static Dapper.SqlMapper;

namespace vn.com.pnsuite.common.dataaccess.repositories
{
    public class BaseService : IBaseService
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "DefaultConnection";
        public BaseService(IConfiguration config)
        {
            _config = config;
        }
        public DbConnection Connection()
        {
            return new SqlConnection(_config.GetConnectionString(Connectionstring));
        }
        public void Delete(string proc, long id, long userId)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            var param = new DynamicParameters();
            param.Add("@Id", dbType: DbType.Int64, value: id, direction: ParameterDirection.Input);
            param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);

            db.Execute(proc, param: param, commandType: CommandType.StoredProcedure);
        }
        public void Dispose()
        {
        }
        public T GetById<T>(string proc, long id)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            var param = new DynamicParameters();
            param.Add("@Id", dbType: DbType.Int64, value: id, direction: ParameterDirection.Input);

            return db.Query<T>(proc, param: param, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
        public T GetSingle<T>(string proc, DynamicParameters param)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return db.Query<T>(proc, param: param, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
        public List<T> GetList<T>(string proc, DynamicParameters param)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return db.Query<T>(proc, param: param, commandType: CommandType.StoredProcedure).ToList();
        }
        public List<T> GetList<T>(string proc)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return db.Query<T>(proc, commandType: CommandType.StoredProcedure).ToList();
        }
        public void Update(string proc, DynamicParameters param)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            db.Execute(proc, param: param, commandType: CommandType.StoredProcedure);
        }
        public List<Object> GetMultiResult(string proc, DynamicParameters param)
        {
            List<Object> lstResult = new List<object>();
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            var result = db.QueryMultiple(proc, param: param, commandType: CommandType.StoredProcedure);

            var sqlActionResult = result.ReadFirst<SqlActionResult>();
            lstResult.Add(sqlActionResult);

            if (sqlActionResult.HasData)
            {
                var data = result.Read();
                lstResult.Add(data);
            }

            return lstResult;
        }

        public async Task<List<T>> GetListAsync<T>(string proc, DynamicParameters param)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return (await db.QueryAsync<T>(proc, param: param, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<T>> GetListAsync<T>(string proc)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return (await db.QueryAsync<T>(proc, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task ExecuteAsync(string proc, DynamicParameters param)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            await db.ExecuteAsync(proc, param: param, commandType: CommandType.StoredProcedure);
        }

        public async Task<T> GetSingleAsync<T>(string proc, DynamicParameters param)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return (await db.QueryAsync<T>(proc, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();
        }

        public async Task<GridReader> QueryMultipleAsync(string proc, DynamicParameters param)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return await db.QueryMultipleAsync(proc, param: param, commandType: CommandType.StoredProcedure);
        }

    }
}
