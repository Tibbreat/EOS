// Decompiled with JetBrains decompiler
// Type: QuestionLib.Business.BOMatchQuestion
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using NHibernate;
using QuestionLib.Entity;
using System;
using System.Collections;
using System.Data.SqlClient;

#nullable disable
namespace QuestionLib.Business
{
  public class BOMatchQuestion : BOBase
  {
    public BOMatchQuestion(ISessionFactory sessionFactory)
      : base(sessionFactory)
    {
    }

    public MatchQuestion LoadMatch(int mid)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from MatchQuestion mq Where  mq.MID=:mid");
        query.SetParameter(nameof (mid), (object) mid);
        list = query.List();
        this.session.Close();
      }
      catch (Exception ex)
      {
        this.session.Close();
        throw ex;
      }
      return list.Count > 0 ? (MatchQuestion) list[0] : (MatchQuestion) null;
    }

    public IList LoadMatchOfCourse(string courseId)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from MatchQuestion mq Where  mq.CourseId=:courseId");
        query.SetParameter(nameof (courseId), (object) courseId);
        list = query.List();
        this.session.Close();
      }
      catch (Exception ex)
      {
        this.session.Close();
        throw ex;
      }
      return list;
    }

    public bool SaveList(IList list)
    {
      ISession isession = this.sessionFactory.OpenSession();
      ITransaction itransaction = isession.BeginTransaction();
      try
      {
        foreach (MatchQuestion matchQuestion in (IEnumerable) list)
        {
          isession.Save((object) matchQuestion);
          matchQuestion.QuestionLOs = BOLO.RemoveDupLO(matchQuestion.QuestionLOs);
          foreach (QuestionLO questionLo in matchQuestion.QuestionLOs)
          {
            questionLo.QID = matchQuestion.MID;
            questionLo.QType = QuestionType.MATCH;
            isession.Save((object) questionLo);
          }
        }
        itransaction.Commit();
        return true;
      }
      catch
      {
        itransaction.Rollback();
        return false;
      }
    }

    public bool Delete(int chapterID, string conStr)
    {
      int num = 3;
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlTransaction sqlTransaction = connection.BeginTransaction();
      string cmdText1 = "DELETE FROM QuestionLO WHERE QType = " + (object) num + " AND qid IN (SELECT mid FROM MatchQuestion WHERE chapterID=" + (object) chapterID + ")";
      string cmdText2 = "DELETE FROM MatchQuestion WHERE chapterID=" + (object) chapterID;
      SqlCommand sqlCommand1 = new SqlCommand(cmdText1, connection);
      sqlCommand1.Transaction = sqlTransaction;
      SqlCommand sqlCommand2 = new SqlCommand(cmdText2, connection);
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

    public void Delete(MatchQuestion m)
    {
      int mid = m.MID;
      this.session = this.sessionFactory.OpenSession();
      ITransaction itransaction = this.session.BeginTransaction();
      try
      {
        this.session.Delete("from MatchQuestion mq Where mq.MID=" + mid.ToString());
        this.session.Delete("from QuestionLO qlo Where qlo.QType=" + (object) 3 + " and qlo.QID=" + (object) mid);
        itransaction.Commit();
        this.session.Close();
      }
      catch (Exception ex)
      {
        itransaction.Rollback();
        this.session.Close();
        throw ex;
      }
    }
  }
}
