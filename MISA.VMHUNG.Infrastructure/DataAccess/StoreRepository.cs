using Dapper;
using MISA.VMHUNG.Core.Entity;
using MISA.VMHUNG.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.VMHUNG.Infrastructure.DataAccess
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        
        public int DeleteStore(Guid storeId)
        {
            var storeName = "Proc_DeleteStore";
            var storeEffect = dbConnection.Execute(storeName, new { StoreId = storeId }, commandType: System.Data.CommandType.StoredProcedure);

            return storeEffect;
        }

        public IEnumerable<Store> GetStoreByIndexOffset(int positionStart, int offSet)
        {
            var storeName = "Proc_GetStoreByIndexOffset";
            //Tạo mới object 
            var parameters = new DynamicParameters();
            parameters.Add($"@positionStart", positionStart, DbType.String);
            parameters.Add($"@offSet", offSet, DbType.String);

            //Thực hiện update
            var stores = dbConnection.Query<Store>(storeName, parameters, commandType: CommandType.StoredProcedure);
            return stores;
        }

        public IEnumerable<Store> GetStoreFilter(StoreFilter storeFilter)
        {var storeName = "Proc_GetStoreFilter";
            var stores = dbConnection.Query<Store>(storeName, storeFilter, commandType: CommandType.StoredProcedure);
            return stores;
        }
       
        public int InsertStore(Store store)
        {
            var storeName = "Proc_InsertStore";
            var rowEffect = dbConnection.Execute(storeName, store, commandType: CommandType.StoredProcedure);
            return rowEffect;
        }

        public int UpdateStore(Guid storeId, Store store)
        {
            var storeName = "Proc_UpdateStore";
            store.StoreId = storeId;
            var rowEffect = dbConnection.Execute(storeName, store, commandType: CommandType.StoredProcedure);
            return rowEffect;
        }
        public int GetCountStores()
        {
            int numberStores = dbConnection.ExecuteScalar<int>($"Proc_GetCountStores", commandType: CommandType.StoredProcedure);
            return numberStores;
        }

        public Store GetStoreByStoreCode(string storeCode)
        {
            var storeName = "Proc_GetStoreByStoreCode";
            var parameters = new DynamicParameters();
            parameters.Add($"@StoreCode", storeCode, DbType.String);
            var entity = dbConnection.Query<Store>(storeName, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return entity;
        }

        public StorePaging GetStorePaging(int pageSize, int pageIndex, StoreFilter storeFilter)
        {
            var storeName = "Proc_GetStorePaging";
            var parameters = new DynamicParameters();
            parameters.Add($"@PageSize", pageSize);
            parameters.Add($"@PageIndex", pageIndex);
            parameters.Add($"@TotalRecord",dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            parameters.Add($"@TotalPage",dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            parameters.Add($"@StoreCode", storeFilter.StoreCode, DbType.String);
            parameters.Add($"@StoreName", storeFilter.StoreName, DbType.String);
            parameters.Add($"@Address", storeFilter.Address, DbType.String);
            parameters.Add($"@PhoneNumber", storeFilter.PhoneNumber, DbType.String);
            parameters.Add($"@Status", storeFilter.Status, DbType.String);
            var storePaging = new StorePaging();
            var store = dbConnection.Query<Store>(storeName, parameters, commandType: CommandType.StoredProcedure);
            storePaging.Data = store.ToList();
            storePaging.TotalPage = parameters.Get<int>("@TotalPage");
            storePaging.TotalRecord = parameters.Get<int>("@TotalRecord");
            return storePaging;
        }
    }
}
