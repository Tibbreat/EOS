// Decompiled with JetBrains decompiler
// Type: QuestionLib.Business.BOPassage
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
  public class BOPassage : BOBase
  {
    public BOPassage(ISessionFactory sessionFactory)
      : base(sessionFactory)
    {
    }

    public Passage LoadPassage(int pid)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from Passage p Where p.PID=:pid");
        query.SetParameter(nameof (pid), (object) pid);
        list = query.List();
        this.session.Close();
      }
      catch (Exception ex)
      {
        this.session.Close();
        throw ex;
      }
      return list.Count > 0 ? (Passage) list[0] : (Passage) null;
    }

    public IList LoadPassageByCourse(string courseId)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from Passage p Where CourseId=:courseId");
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

    public void Delete(int pid, int[] qid_list)
    {
      this.session = this.sessionFactory.OpenSession();
      ITransaction itransaction = this.session.BeginTransaction();
      try
      {
        this.session.Delete("from Passage p Where p.PID=" + pid.ToString());
        this.session.Delete("from Question q Where q.PID=" + pid.ToString());
        foreach (int qid in qid_list)
        {
          this.session.Delete("from QuestionAnswer qa Where qa.QID=" + qid.ToString());
          this.session.Delete("from QuestionLO qLO Where Qtype=" + (object) 0 + " and qLO.QID=" + qid.ToString());
        }
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

    public bool Delete(int chapterID, string conStr)
    {
      int num = 0;
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlTransaction sqlTransaction = connection.BeginTransaction();
      string cmdText1 = "DELETE FROM QuestionLO WHERE QType = " + (object) num + " AND qid IN (SELECT qid FROM Question WHERE pid IN (SELECT pid FROM Passage WHERE chapterID=" + (object) chapterID + "))";
      string cmdText2 = "DELETE FROM QuestionAnswer WHERE qid IN (SELECT qid FROM Question WHERE pid IN (SELECT pid FROM Passage WHERE chapterID=" + (object) chapterID + "))";
      string cmdText3 = "DELETE FROM Question WHERE pid IN (SELECT pid FROM Passage WHERE chapterID=" + (object) chapterID + ")";
      string cmdText4 = "DELETE FROM Passage WHERE chapterID=" + (object) chapterID;
      SqlCommand sqlCommand1 = new SqlCommand(cmdText1, connection);
      sqlCommand1.Transaction = sqlTransaction;
      SqlCommand sqlCommand2 = new SqlCommand(cmdText2, connection);
      sqlCommand2.Transaction = sqlTransaction;
      SqlCommand sqlCommand3 = new SqlCommand(cmdText3, connection);
      sqlCommand3.Transaction = sqlTransaction;
      SqlCommand sqlCommand4 = new SqlCommand(cmdText4, connection);
      sqlCommand4.Transaction = sqlTransaction;
      try
      {
        sqlCommand1.ExecuteNonQuery();
        sqlCommand2.ExecuteNonQuery();
        sqlCommand3.ExecuteNonQuery();
        sqlCommand4.ExecuteNonQuery();
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

    public bool SaveList(IList list)
    {
      ISession isession = this.sessionFactory.OpenSession();
      ITransaction itransaction = isession.BeginTransaction();
      try
      {
        foreach (Passage passage in (IEnumerable) list)
        {
          isession.Save((object) passage);
          foreach (Question passageQuestion in passage.PassageQuestions)
          {
            passageQuestion.PID = passage.PID;
            isession.Save((object) passageQuestion);
            foreach (QuestionAnswer questionAnswer in passageQuestion.QuestionAnswers)
            {
              questionAnswer.QID = passageQuestion.QID;
              isession.Save((object) questionAnswer);
            }
            passageQuestion.QuestionLOs = BOLO.RemoveDupLO(passageQuestion.QuestionLOs);
            foreach (QuestionLO questionLo in passageQuestion.QuestionLOs)
            {
              questionLo.QID = passageQuestion.QID;
              questionLo.QType = passageQuestion.QType;
              isession.Save((object) questionLo);
            }
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
  }
}
