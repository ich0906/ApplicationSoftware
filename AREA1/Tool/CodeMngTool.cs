using AREA1.Data;
using AREA1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace AREA1.Tool
{
    public class CodeMngTool
    {
        private readonly AppSoftDbContext _context;
        private readonly CommonDao _commonDao;

        public CodeMngTool(AppSoftDbContext context)
        {
            _context = context;
            _commonDao = new CommonDao(context);
        }

        public List<Dictionary<string, string>> getCode(string group_nm)
        {
            string query = "SELECT A.GROUP_ID, A.CODE_ID, A.CODE_NM " +
                            "FROM OP_CODE A JOIN OP_CODE_GROUP B ON " +
                            "A.GROUP_ID = B.GROUP_ID " +
                            "WHERE B.GROUP_NM=@group_nm:VARCHAR";

            var groupName = new Dictionary<string, string>()
            {
                {"group_nm", group_nm}
            };
            
            List<Dictionary<string, string>> resultList = _commonDao.SelectList(query, groupName);

            return resultList;
        }
        public string getCode(string group_nm, string code_nm)
        {
            string query = "SELECT A.CODE_ID " +
                            "FROM OP_CODE A JOIN OP_CODE_GROUP B ON " +
                            "A.GROUP_ID = B.GROUP_ID " +
                            "WHERE A.CODE_NM=@code_nm:VARCHAR AND " +
                            "B.GROUP_NM=@group_nm:VARCHAR";

            var Name = new Dictionary<string, string>()
            {
                {"code_nm", code_nm},
                {"group_nm", group_nm}
            };
            Dictionary<string, string> resultDic = _commonDao.SelectOne(query, Name);
            string result = resultDic["CODE_ID"];

            return result;
        }

        public int Insert()
        {
            string query = "INSERT INTO OP_CODE VALUES (@group_id:NUMBER, " +
                                                        "@code_id:NUMBER, " +
                                                        "@code_nm:VARCHAR2" +
                                                        ")";
            string query2 = "INSERT INTO OP_CODE_GROUP VALUES (@group_id:NUMBER, " +
                                                                "@group_nm:VARCHAR2" +
                                                                ")";

            int resultRowCnt = _commonDao.Insert(query);
            int resultRowCnt2 = _commonDao.Insert(query2);
            return resultRowCnt;
        }

        public int Update()
        {
            string query = "UPDATE OP_CODE " +
                            "SET GROUP_ID=@group_id:NUMBER, " +
                            "CODE_ID=@code_id:NUMBER, " +
                            "CODE_NM=@code_nm:VARCHAR2";
            string query2 = "UPDATE OP_GROUP_CODE " +
                            "SET GROUP_ID=@group_id:NUMBER, " +
                            "GROUP_NM=@group_nm:VARCHAR2";

            int resultRowCnt = _commonDao.Update(query);
            int resultRowCnt2 = _commonDao.Update(query2);

            return resultRowCnt;
        }

        public int Delete()
        {
            string query = "DELETE FROM OP_CODE " +
                            "WHERE GROUP_ID=@group_id:NUMBER, " +
                            "OR GROUP_NM=@group_nm:VARCHAR2";

            int resultRowCnt = _commonDao.Delete(query);

            return resultRowCnt;
        }

    }
}
