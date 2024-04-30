// Decompiled with JetBrains decompiler
// Type: QuestionLib.Business.BOTestTemplate
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using NHibernate;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

#nullable disable
namespace QuestionLib.Business
{
  public class BOTestTemplate : BOBase
  {
    public BOTestTemplate(ISessionFactory sessionFactory)
      : base(sessionFactory)
    {
    }

    public bool IsTestTemplateExists(string testTemplateName)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from TestTemplate t Where TestTemplateName=:testTemplateName");
        query.SetParameter(nameof (testTemplateName), (object) testTemplateName);
        list = query.List();
        this.session.Close();
      }
      catch (Exception ex)
      {
        this.session.Close();
        throw ex;
      }
      return list.Count != 0;
    }

    public static DataTable LoadTestTemplate(string CID, string conStr)
    {
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlCommand selectCommand = new SqlCommand("SELECT tt.TestTemplateID, tt.TestTemplateName AS 'Template name', tt.CID, c.Name AS 'Course name', tt.CreatedBy, tt.CreatedDate, tt.DistinctWithLastTest AS 'Distinct last tests',tt.Duration, tt.Note FROM TestTemplate AS tt INNER JOIN Course AS c ON tt.CID = c.CID WHERE tt.CID = @CID", connection);
      selectCommand.Parameters.Add(nameof (CID), SqlDbType.NVarChar);
      selectCommand.Parameters[nameof (CID)].Value = (object) CID;
      SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
      DataTable dataTable = new DataTable();
      sqlDataAdapter.Fill(dataTable);
      connection.Close();
      return dataTable;
    }

    public static bool Delete(int testTemplateID, string conStr)
    {
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlTransaction sqlTransaction = connection.BeginTransaction();
      string cmdText1 = "DELETE FROM TestTemplateDetails WHERE TestTemplateID = @testTemplateID";
      string cmdText2 = "DELETE FROM TestTemplate WHERE TestTemplateID = @testTemplateID";
      SqlCommand sqlCommand1 = new SqlCommand(cmdText1, connection);
      sqlCommand1.Parameters.Add(nameof (testTemplateID), SqlDbType.Int);
      sqlCommand1.Parameters[nameof (testTemplateID)].Value = (object) testTemplateID;
      sqlCommand1.Transaction = sqlTransaction;
      SqlCommand sqlCommand2 = new SqlCommand(cmdText2, connection);
      sqlCommand2.Parameters.Add(nameof (testTemplateID), SqlDbType.Int);
      sqlCommand2.Parameters[nameof (testTemplateID)].Value = (object) testTemplateID;
      sqlCommand2.Transaction = sqlTransaction;
      try
      {
        sqlCommand1.ExecuteNonQuery();
        sqlCommand2.ExecuteNonQuery();
        sqlTransaction.Commit();
        connection.Close();
        return true;
      }
      catch (Exception ex)
      {
        sqlTransaction.Rollback();
        connection.Close();
        throw ex;
      }
    }

    public static System.Collections.Generic.List<string> GetDistinctTestIds(
      string courseID,
      int testTemplateID,
      string conStr)
    {
      System.Collections.Generic.List<string> distinctTestIds = new System.Collections.Generic.List<string>();
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlCommand sqlCommand1 = new SqlCommand("SELECT DistinctWithLastTest FROM TestTemplate WHERE TestTemplateID = @testTemplateID", connection);
      sqlCommand1.Parameters.Add(nameof (testTemplateID), SqlDbType.Int);
      sqlCommand1.Parameters[nameof (testTemplateID)].Value = (object) testTemplateID;
      SqlCommand sqlCommand2 = new SqlCommand("SELECT TOP " + (object) Convert.ToInt32(sqlCommand1.ExecuteScalar().ToString()) + " TestID FROM Test WHERE CourseID=@courseID ORDER BY InsertOrder DESC", connection);
      sqlCommand2.Parameters.Add(nameof (courseID), SqlDbType.NVarChar);
      sqlCommand2.Parameters[nameof (courseID)].Value = (object) courseID;
      SqlDataReader sqlDataReader = sqlCommand2.ExecuteReader();
      while (sqlDataReader.Read())
        distinctTestIds.Add(sqlDataReader.GetString(0));
      sqlDataReader.Close();
      connection.Close();
      return distinctTestIds;
    }
  }
}
