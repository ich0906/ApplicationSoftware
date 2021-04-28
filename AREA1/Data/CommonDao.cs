using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

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

        public Dictionary<string, string> SelectOne(string sqlQuery, IFormCollection param) {

            DataTable dt = SelectTable(sqlQuery, param);

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

        public List<Dictionary<string, string>> SelectList(string sqlQuery, IFormCollection param) {

            List<Dictionary<string, string>> resultList = new List<Dictionary<string, string>>();

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

        public DataTable SelectTable(string sqlQuery, IFormCollection param) {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using (var cmd = dbFactory.CreateCommand()) {
                cmd.Connection = _context.Database.GetDbConnection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;

                Regex reg = new Regex("@([_]?[a-z])*:[A-Z]*");
                MatchCollection resultColl = reg.Matches(cmd.CommandText);

                foreach (Match mm in resultColl) {
                    Group g = mm.Groups[0];

                    string prmNm = g.ToString().Split(":")[0].Replace("@", ":");
                    string prmType = g.ToString().Split(":")[1];
                    cmd.CommandText = cmd.CommandText.Replace(g.ToString(), prmNm);

                    OracleParameter sqlparam = null;

                    if (prmType.Contains("VARCHAR")) {
                        sqlparam = new OracleParameter(prmNm, OracleDbType.Varchar2);
                        sqlparam.Value = param[prmNm.Replace(":", "")];
                    } else if (prmType.Contains("NUMBER")) {
                        sqlparam = new OracleParameter(prmNm, OracleDbType.Int32);
                        sqlparam.Value = param[prmNm.Replace(":", "")].ToString();
                    } else if (prmType.Contains("DATE")) {
                        sqlparam = new OracleParameter(prmNm, OracleDbType.Date);
                        sqlparam.Value = param[prmNm.Replace(":", "")].ToString();
                    }


                    cmd.Parameters.Add(sqlparam);
                }

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

        public int Insert(string sqlQuery, IFormCollection param) {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using (var cmd = dbFactory.CreateCommand()) {
                cmd.Connection = _context.Database.GetDbConnection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;

                Regex reg = new Regex("@([_]?[a-z])*:[A-Z]*");
                MatchCollection resultColl = reg.Matches(cmd.CommandText);

                foreach(Match mm in resultColl) {
                    Group g = mm.Groups[0];

                    string prmNm = g.ToString().Split(":")[0].Replace("@", ":");
                    string prmType = g.ToString().Split(":")[1];
                    cmd.CommandText = cmd.CommandText.Replace(g.ToString(), prmNm);

                    OracleParameter sqlparam = null;

                    if (prmType.Contains("VARCHAR")) {
                        sqlparam = new OracleParameter(prmNm, OracleDbType.Varchar2);
                        sqlparam.Value = param[prmNm.Replace(":", "")];
                    } else if (prmType.Contains("NUMBER")) {
                        sqlparam = new OracleParameter(prmNm, OracleDbType.Int32);
                        sqlparam.Value = param[prmNm.Replace(":", "")].ToString();
                    } else if (prmType.Contains("DATE")) {
                        sqlparam = new OracleParameter(prmNm, OracleDbType.Date);
                        sqlparam.Value = param[prmNm.Replace(":", "")].ToString();
                    }

                   
                    cmd.Parameters.Add(sqlparam);
                }


                using (DbDataAdapter adapter = dbFactory.CreateDataAdapter()) {
                    adapter.InsertCommand = cmd;

                    DataTable dt = new DataTable();
                    adapter.InsertCommand.Prepare();

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

        public int Update(string sqlQuery, IFormCollection param) {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using (var cmd = dbFactory.CreateCommand()) {
                cmd.Connection = _context.Database.GetDbConnection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;

                Regex reg = new Regex("@([_]?[a-z])*:[A-Z]*");
                MatchCollection resultColl = reg.Matches(cmd.CommandText);

                foreach (Match mm in resultColl) {
                    Group g = mm.Groups[0];

                    string prmNm = g.ToString().Split(":")[0].Replace("@", ":");
                    string prmType = g.ToString().Split(":")[1];
                    cmd.CommandText = cmd.CommandText.Replace(g.ToString(), prmNm);

                    OracleParameter sqlparam = null;

                    if (prmType.Contains("VARCHAR")) {
                        sqlparam = new OracleParameter(prmNm, OracleDbType.Varchar2);
                        sqlparam.Value = param[prmNm.Replace(":", "")];
                    } else if (prmType.Contains("NUMBER")) {
                        sqlparam = new OracleParameter(prmNm, OracleDbType.Int32);
                        sqlparam.Value = param[prmNm.Replace(":", "")].ToString();
                    } else if (prmType.Contains("DATE")) {
                        sqlparam = new OracleParameter(prmNm, OracleDbType.Date);
                        sqlparam.Value = param[prmNm.Replace(":", "")].ToString();
                    }


                    cmd.Parameters.Add(sqlparam);
                }


                using (DbDataAdapter adapter = dbFactory.CreateDataAdapter()) {
                    adapter.UpdateCommand = cmd;

                    DataTable dt = new DataTable();
                    adapter.UpdateCommand.Prepare();

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

        public int Delete(string sqlQuery, IFormCollection param) {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(_context.Database.GetDbConnection());

            using (var cmd = dbFactory.CreateCommand()) {
                cmd.Connection = _context.Database.GetDbConnection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;

                Regex reg = new Regex("@([_]?[a-z])*:[A-Z]*");
                MatchCollection resultColl = reg.Matches(cmd.CommandText);

                foreach (Match mm in resultColl) {
                    Group g = mm.Groups[0];

                    string prmNm = g.ToString().Split(":")[0].Replace("@", ":");
                    string prmType = g.ToString().Split(":")[1];
                    cmd.CommandText = cmd.CommandText.Replace(g.ToString(), prmNm);

                    OracleParameter sqlparam = null;

                    if (prmType.Contains("VARCHAR")) {
                        sqlparam = new OracleParameter(prmNm, OracleDbType.Varchar2);
                        sqlparam.Value = param[prmNm.Replace(":", "")];
                    } else if (prmType.Contains("NUMBER")) {
                        sqlparam = new OracleParameter(prmNm, OracleDbType.Int32);
                        sqlparam.Value = param[prmNm.Replace(":", "")].ToString();
                    } else if (prmType.Contains("DATE")) {
                        sqlparam = new OracleParameter(prmNm, OracleDbType.Date);
                        sqlparam.Value = param[prmNm.Replace(":", "")].ToString();
                    }


                    cmd.Parameters.Add(sqlparam);
                }


                using (DbDataAdapter adapter = dbFactory.CreateDataAdapter()) {
                    adapter.DeleteCommand = cmd;

                    DataTable dt = new DataTable();
                    adapter.DeleteCommand.Prepare();

                    int resultRowCnt = adapter.DeleteCommand.ExecuteNonQuery();

                    return resultRowCnt;
                }
            }
        }

    }

}