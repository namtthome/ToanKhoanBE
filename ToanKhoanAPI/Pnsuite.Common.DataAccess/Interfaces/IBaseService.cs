using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace vn.com.pnsuite.common.dataaccess.interfaces
{
    public interface IBaseService : IDisposable
    {
        DbConnection Connection();
        T GetById<T>(string proc, long id);
        T GetSingle<T>(string proc, DynamicParameters param);
        List<T> GetList<T>(string proc, DynamicParameters param);
        List<T> GetList<T>(string proc);
        List<Object> GetMultiResult(string proc, DynamicParameters param);
        void Delete(string proc, long id, long userId);
        void Update(string proc, DynamicParameters param);
        Task<List<T>> GetListAsync<T>(string proc, DynamicParameters param);
        Task<List<T>> GetListAsync<T>(string proc);
        Task ExecuteAsync(string proc, DynamicParameters param);
        Task<T> GetSingleAsync<T>(string proc, DynamicParameters param);

        Task<GridReader> QueryMultipleAsync(string proc, DynamicParameters param);

    }
}
