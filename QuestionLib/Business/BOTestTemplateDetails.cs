// Decompiled with JetBrains decompiler
// Type: QuestionLib.Business.BOTestTemplateDetails
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using NHibernate;
using QuestionLib.Entity;
using System.Data;
using System.Data.SqlClient;

#nullable disable
namespace QuestionLib.Business
{
  public class BOTestTemplateDetails : BOBase
  {
    public BOTestTemplateDetails(ISessionFactory sessionFactory)
      : base(sessionFactory)
    {
    }

    public static DataTable LoadTestTemplateDetails(string testTemplateID, string conStr)
    {
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlCommand selectCommand = new SqlCommand("SELECT tt.TestTemplateName AS 'Test template name', tt.CID, ch.Name AS 'Chapter', ttd.NoQInTest, tmp.QString AS 'Question type' FROM TestTemplateDetails AS ttd INNER JOIN TestTemplate AS tt ON tt.TestTemplateID = ttd.TestTemplateID INNER JOIN Chapter AS ch ON ch.ChID = ttd.ChapterID INNER JOIN (SELECT 0 AS QType, 'Reading' AS QString UNION SELECT 1 AS QType, 'Multiple choice' AS QString UNION SELECT 2 AS QType, 'Indicate mistake' AS QString UNION SELECT 3 AS QType, 'Match' AS QString UNION SELECT 4 AS QType, 'Fill blank' AS QString ) AS tmp ON ttd.QuestionType = tmp.QType WHERE tt.TestTemplateID = @testTemplateID", connection);
      selectCommand.Parameters.Add(nameof (testTemplateID), SqlDbType.Int);
      selectCommand.Parameters[nameof (testTemplateID)].Value = (object) testTemplateID;
      SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
      DataTable dataTable = new DataTable();
      sqlDataAdapter.Fill(dataTable);
      connection.Close();
      return dataTable;
    }

    public static DataTable LoadTestTemplateDetails(
      QuestionType questionType,
      int testTemplateID,
      string conStr)
    {
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlCommand selectCommand = new SqlCommand("SELECT ChapterId,QuestionType,NoQInTest FROM TestTemplateDetails WHERE TestTemplateID = @testTemplateID AND QuestionType = @questionType ", connection);
      selectCommand.Parameters.Add(nameof (testTemplateID), SqlDbType.Int);
      selectCommand.Parameters[nameof (testTemplateID)].Value = (object) testTemplateID;
      selectCommand.Parameters.Add(nameof (questionType), SqlDbType.Int);
      selectCommand.Parameters[nameof (questionType)].Value = (object) questionType;
      SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
      DataTable dataTable = new DataTable();
      sqlDataAdapter.Fill(dataTable);
      connection.Close();
      return dataTable;
    }
  }
}
