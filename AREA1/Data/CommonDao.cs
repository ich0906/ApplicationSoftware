using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace AREA1.Data {
    public class CommonDao {
        private readonly AppSoftDbContext _context;

        public CommonDao(AppSoftDbContext appSoftDbContext) {
            _context = appSoftDbContext;
        }

        public Dictionary<string, string> SelectOne(string sqlQuery) {

            DataTable dt = SelectTable(sqlQuery);

            Dictionary<string, string> DataDic = new Dictionary<string, string>();

            for (int i = 0; i < dt.Columns.Count; i++) {
                DataDic.Add(dt.Columns[i].ColumnName, dt.Rows[0][i].ToString());
            }

            return DataDic;
        }

        public List<Dictionary<string, string>> SelectList(string sqlQuery) {

            List < Dictionary<string, string> > resultList = new List<Dictionary<string, string>>();

            DataTable dt = SelectTable(sqlQuery);

            Dictionary<string, string> DataDic = new Dictionary<string, string>();

            for (int i = 0; i < dt.Rows.Count; i++) {
                DataDic.Clear();

                for (int j = 0; j < dt.Columns.Count; j++) {
                    DataDic.Add(dt.Columns[j].ColumnName, dt.Rows[i][j].ToString());
                }

                resultList.Add(DataDic);
            }

            return resultList;
        }


        public DataTable SelectTable(string sqlQuery) {
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

        public int Insert(string sqlQuery) {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using (var cmd = dbFactory.CreateCommand()) {
                cmd.Connection = _context.Database.GetDbConnection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;
                using (DbDataAdapter adapter = dbFactory.CreateDataAdapter()) {
                    adapter.InsertCommand = cmd;

                    DataTable dt = new DataTable();
                    int resultRowCnt = adapter.InsertCommand.ExecuteNonQuery();

                    return resultRowCnt;
                }
            }
        }

        public int Update(string sqlQuery) {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using (var cmd = dbFactory.CreateCommand()) {
                cmd.Connection = _context.Database.GetDbConnection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;
                using (DbDataAdapter adapter = dbFactory.CreateDataAdapter()) {
                    adapter.UpdateCommand = cmd;

                    DataTable dt = new DataTable();
                    int resultRowCnt = adapter.UpdateCommand.ExecuteNonQuery();

                    return resultRowCnt;
                }
            }
        }

        public int Delete(string sqlQuery) {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using (var cmd = dbFactory.CreateCommand()) {
                cmd.Connection = _context.Database.GetDbConnection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;
                using (DbDataAdapter adapter = dbFactory.CreateDataAdapter()) {
                    adapter.DeleteCommand = cmd;

                    DataTable dt = new DataTable();
                    int resultRowCnt = adapter.DeleteCommand.ExecuteNonQuery();

                    return resultRowCnt;
                }
            }
        }

    }

}