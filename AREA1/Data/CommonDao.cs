using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace AREA1.Data {
    public class CommonDao {
        private readonly AppSoftDbContext _context;

        public CommonDao(AppSoftDbContext appSoftDbContext) { 
            _context = appSoftDbContext;
        }

        public DataTable selectOne(string sqlQuery, DataSet param) {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using (var cmd = dbFactory.CreateCommand()) {
                cmd.Connection = _context.Database.GetDbConnection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;
                using (DbDataAdapter adapter = dbFactory.CreateDataAdapter()) {
                    adapter.SelectCommand = cmd;

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
    }
}