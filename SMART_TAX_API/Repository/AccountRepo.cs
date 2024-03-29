﻿using SMART_TAX_API.Helpers;
using SMART_TAX_API.Models;
using SMART_TAX_API.Translator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SMART_TAX_API.Repository
{
    public class AccountRepo
    {
        public void InsertUser(string connstring, USER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.NVarChar,50) { Value = "CREATE_USER" },
                  new SqlParameter("@USERNAME", SqlDbType.NVarChar,250) { Value = master.USERNAME},
                  new SqlParameter("@PASSWORD", SqlDbType.NVarChar, 250) { Value = master.PASSWORD },
                  new SqlParameter("@EMPLOYEE_NAME", SqlDbType.NVarChar, 250) { Value = master.EMPLOYEE_NAME },
                  new SqlParameter("@EMPLOYEE_CODE", SqlDbType.NVarChar, 50) { Value = master.EMPLOYEE_CODE },
                  new SqlParameter("@DESIGNATION", SqlDbType.NVarChar, 50) { Value = master.DESIGNATION },
                  new SqlParameter("@EMAIL", SqlDbType.NVarChar, 250) { Value = master.EMAIL },
                  new SqlParameter("@ROLE", SqlDbType.NVarChar, 50) { Value = master.ROLE },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                };

                var UserID = SqlHelper.ExecuteProcedureReturnString(connstring, "SP_MST_USER", parameters);

                DataTable tbl = new DataTable();
                tbl.Columns.Add(new DataColumn("USER_ID", typeof(string)));
                tbl.Columns.Add(new DataColumn("COMPANY_ID", typeof(int)));

                foreach (var i in master.USER_COMPANY)
                {
                    DataRow dr = tbl.NewRow();

                    dr["USER_ID"] = UserID;
                    dr["COMPANY_ID"] = i.ID;

                    tbl.Rows.Add(dr);
                }

                string[] columns = new string[2];
                columns[0] = "USER_ID";
                columns[1] = "COMPANY_ID";

                SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl, "USER_COMPANY_MAPPING", columns);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<USER> GetUserList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.NVarChar, 50) { Value = "GET_USER" },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_MST_USER", parameters);
                List<USER> master = SqlHelper.CreateListFromTable<USER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetUserDetails(string connstring, string ID)
        {
            try
            {
                SqlParameter[] parameters =
                {

                   new SqlParameter("@ID", SqlDbType.NVarChar, 50) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.NVarChar, 50) { Value = "GET_USER_BY_ID" }
                };

                return SqlHelper.ExtecuteProcedureReturnDataSet(connstring, "SP_MST_USER", parameters);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static T GetSingleDataFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateItemFromRow<T>(dataTable.Rows[0]);
        }

        public static List<T> GetListFromDataSet<T>(DataTable dataTable) where T : new()
        {
            return SqlHelper.CreateListFromTable<T>(dataTable);
        }

        public void UpdateUser(string connstring, USER master)
        {
            try
            {
                SqlParameter[] parameters =
               {
                  new SqlParameter("@OPERATION", SqlDbType.NVarChar,50) { Value = "UPDATE_USER" },
                  new SqlParameter("@ID", SqlDbType.NVarChar,250) { Value = master.ID},
                  new SqlParameter("@USERNAME", SqlDbType.NVarChar,250) { Value = master.USERNAME},
                  new SqlParameter("@PASSWORD", SqlDbType.NVarChar, 250) { Value = master.PASSWORD },
                  new SqlParameter("@EMPLOYEE_NAME", SqlDbType.NVarChar, 250) { Value = master.EMPLOYEE_NAME },
                  new SqlParameter("@EMPLOYEE_CODE", SqlDbType.NVarChar, 50) { Value = master.EMPLOYEE_CODE },
                  new SqlParameter("@DESIGNATION", SqlDbType.NVarChar, 50) { Value = master.DESIGNATION },
                  new SqlParameter("@EMAIL", SqlDbType.NVarChar, 250) { Value = master.EMAIL },
                  new SqlParameter("@ROLE", SqlDbType.NVarChar, 50) { Value = master.ROLE },
                  new SqlParameter("@STATUS", SqlDbType.Bit) { Value = master.STATUS },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_MST_USER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteUser(string connstring, string ID)
        {
            SqlParameter[] parameters =
            {
              new SqlParameter("@ID", SqlDbType.NVarChar, 50) { Value = ID },
               new SqlParameter("@OPERATION", SqlDbType.NVarChar, 50) { Value = "DELETE_USER" }
            };

            SqlHelper.ExecuteProcedureReturnString(connstring, "SP_MST_USER", parameters);
        }

        public USER ValidateUser(string connstring, string username, string password)
        {
            SqlParameter[] parameters =
            {
              new SqlParameter("@USERNAME", SqlDbType.VarChar, 100) { Value = username },
              new SqlParameter("@PASSWORD", SqlDbType.VarChar, 100) { Value = password },
              new SqlParameter("@OPERATION", SqlDbType.VarChar, 20) { Value = "VALIDATE_USER" }
            };

            return SqlHelper.ExtecuteProcedureReturnData<USER>(connstring, "SP_MST_USER", r => r.TranslateAsUser(), parameters);
        }

        public void InsertUserMaster(string connstring, List<USER_MASTER> info)
        {
            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add(new DataColumn("PAN", typeof(string)));
                tbl.Columns.Add(new DataColumn("PASSWORD", typeof(string)));
                tbl.Columns.Add(new DataColumn("COMPANY_NAME", typeof(string)));
                tbl.Columns.Add(new DataColumn("ENCRYPT_PASSWORD", typeof(string)));
                tbl.Columns.Add(new DataColumn("IS_ACTIVE", typeof(bool)));

                foreach (var i in info)
                {
                    DataRow dr = tbl.NewRow();

                    dr["PAN"] = i.PAN;
                    dr["PASSWORD"] = i.PASSWORD;
                    dr["COMPANY_NAME"] = i.COMPANY_NAME;
                    dr["ENCRYPT_PASSWORD"] = i.ENCRYPT_PASSWORD;
                    dr["IS_ACTIVE"] = 1;

                    tbl.Rows.Add(dr);
                }

                string[] columns = new string[5];
                columns[0] = "PAN";
                columns[1] = "PASSWORD";
                columns[2] = "COMPANY_NAME";
                columns[3] = "ENCRYPT_PASSWORD";
                columns[4] = "IS_ACTIVE";

                SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl, "USER_MASTER", columns);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CreateSingleUser(string connstring, USER_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.NVarChar,50) { Value = "CREATE_USER_MASTER" },
                  new SqlParameter("@PAN", SqlDbType.NVarChar,250) { Value = master.PAN},
                  new SqlParameter("@PASSWORD", SqlDbType.NVarChar, 250) { Value = master.PASSWORD },
                  new SqlParameter("@COMPANY", SqlDbType.NVarChar, 250) { Value = master.COMPANY_NAME },
                  new SqlParameter("@IS_ACTIVE", SqlDbType.NVarChar, 250) { Value = master.IS_ACTIVE },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_MST_USER", parameters);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<USER_MASTER> GetUserMasterList(string dbConn)
        {
            try
            {
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.NVarChar, 50) { Value = "GET_USER_MASTER" },
                };

                DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(dbConn, "SP_MST_USER", parameters);
                List<USER_MASTER> master = SqlHelper.CreateListFromTable<USER_MASTER>(dataTable);

                return master;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public USER_MASTER GetUserMasterDetails(string connstring, string ID)
        {
            try
            {
                SqlParameter[] parameters =
                {

                   new SqlParameter("@ID", SqlDbType.NVarChar, 50) { Value = ID },
                   new SqlParameter("@OPERATION", SqlDbType.NVarChar, 50) { Value = "GET_USER_MASTER_BY_ID" }
                };

                return SqlHelper.ExtecuteProcedureReturnData<USER_MASTER>(connstring, "SP_MST_USER", r => r.TranslateAsUserMaster(), parameters);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateUserMaster(string connstring, USER_MASTER master)
        {
            try
            {
                SqlParameter[] parameters =
               {
                  new SqlParameter("@OPERATION", SqlDbType.NVarChar,50) { Value = "UPDATE_USER_MASTER" },
                  new SqlParameter("@ID", SqlDbType.NVarChar,250) { Value = master.ID},
                  new SqlParameter("@PAN", SqlDbType.NVarChar,250) { Value = master.PAN},
                  new SqlParameter("@PASSWORD", SqlDbType.NVarChar, 250) { Value = master.PASSWORD },
                  new SqlParameter("@COMPANY", SqlDbType.NVarChar, 250) { Value = master.COMPANY_NAME },
                  new SqlParameter("@IS_ACTIVE", SqlDbType.NVarChar, 50) { Value = master.IS_ACTIVE },
                };

                SqlHelper.ExecuteProcedureReturnString(connstring, "SP_MST_USER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
