using Dapper;
using MISA.VMHUNG.Core.Interface;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.VMHUNG.Infrastructure.DataAccess
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>
    {
        private String _connectionString;
        private String _className;
        protected DbConnection dbConnection;
        public BaseRepository()
        {
            _connectionString = "UserID = dev; " +
                "Host=47.241.69.179; " +
                "DataBase= MF760_VMHUNG_eshop; " +
                "port=3306;" +
                "password=12345678;";
            this._className = typeof(MISAEntity).Name;
            this.dbConnection = new MySqlConnection(_connectionString);
        }
        /// <summary>
        /// Lấy khách hàng theo ID
        /// </summary>
        /// <typeparam name="MISAEntity"></typeparam>
        /// <returns></returns>
        /// Created By: VM Hùng (12/04/2021)
        public MISAEntity GetById(Guid id)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"@{_className}Id", id, DbType.String);
            var storeName = $"Proc_Get{_className}ById";
            var entity = dbConnection.Query<MISAEntity>(storeName, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return entity;
        }

        /// <summary>
        /// lấy tất cả khách hàng
        /// </summary>
        /// <typeparam name="MISAEntity"></typeparam>
        /// <returns></returns>
        /// Created By: VM Hùng (12/04/2021)
        public IEnumerable<MISAEntity> GetAll()
        {
            var storeName = $"Proc_Get{_className}s";
            var entity = dbConnection.Query<MISAEntity>(storeName, commandType: System.Data.CommandType.StoredProcedure);
            return entity;
        }
    }
}
