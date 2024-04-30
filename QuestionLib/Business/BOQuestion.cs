// Decompiled with JetBrains decompiler
// Type: QuestionLib.Business.BOQuestion
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
  public class BOQuestion : BOBase
  {
    public BOQuestion(ISessionFactory sessionFactory)
      : base(sessionFactory)
    {
    }

    public IList LoadPassageQuestion(int pid)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from Question q Where q.PID=:pid");
        query.SetParameter(nameof (pid), (object) pid);
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

    public Question Load(int qid)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from Question q Where q.QID=:qid");
        query.SetParameter(nameof (qid), (object) qid);
        list = query.List();
        this.session.Close();
      }
      catch (Exception ex)
      {
        this.session.Close();
        throw ex;
      }
      return list.Count > 0 ? (Question) list[0] : (Question) null;
    }

    public IList LoadByType(QuestionType type)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from Question q Where q.QType=:type");
        query.SetParameter(nameof (type), (object) type);
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

    public IList LoadByTypeOfCourse(QuestionType type, string courseId)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from Question q Where q.QType=:type and CourseId=:courseId");
        query.SetParameter(nameof (type), (object) type);
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

    public IList LoadFillBlankByTypeOfCourse(string courseId)
    {
      QuestionType questionType1 = QuestionType.FILL_BLANK_ALL;
      QuestionType questionType2 = QuestionType.FILL_BLANK_EMPTY;
      QuestionType questionType3 = QuestionType.FILL_BLANK_GROUP;
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from Question q Where (q.QType=:type1 OR q.QType=:type2 OR q.QType=:type3) and CourseId=:courseId");
        query.SetParameter("type1", (object) questionType1);
        query.SetParameter("type2", (object) questionType2);
        query.SetParameter("type3", (object) questionType3);
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

    public void Delete(int qid, QuestionType qt)
    {
      this.session = this.sessionFactory.OpenSession();
      ITransaction itransaction = this.session.BeginTransaction();
      try
      {
        this.session.Delete("from Question q Where q.QID=" + qid.ToString());
        this.session.Delete("from QuestionAnswer qa Where qa.QID=" + qid.ToString());
        this.session.Delete("from QuestionLO qlo Where qlo.QType=" + (object) (int) qt + " and qlo.QID=" + (object) qid);
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

    public bool SaveList(IList list)
    {
      ISession isession = this.sessionFactory.OpenSession();
      ITransaction itransaction = isession.BeginTransaction();
      try
      {
        foreach (Question question in (IEnumerable) list)
        {
          isession.Save((object) question);
          foreach (QuestionAnswer questionAnswer in question.QuestionAnswers)
          {
            questionAnswer.QID = question.QID;
            isession.Save((object) questionAnswer);
          }
          question.QuestionLOs = BOLO.RemoveDupLO(question.QuestionLOs);
          foreach (QuestionLO questionLo in question.QuestionLOs)
          {
            questionLo.QID = question.QID;
            questionLo.QType = question.QType;
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

    public bool Delete(int chapterID, QuestionType qt, string conStr)
    {
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlTransaction sqlTransaction = connection.BeginTransaction();
      string cmdText1 = "DELETE FROM QuestionAnswer WHERE qid in (SELECT qid FROM Question WHERE QType=" + (object) (int) qt + " AND chapterId=" + (object) chapterID + ")";
      string cmdText2 = "DELETE FROM QuestionLO WHERE qid in (SELECT qid FROM Question WHERE QType=" + (object) (int) qt + " AND chapterId=" + (object) chapterID + ")";
      string cmdText3 = "DELETE FROM Question WHERE QType=" + (object) (int) qt + " AND chapterID=" + (object) chapterID;
      SqlCommand sqlCommand1 = new SqlCommand(cmdText1, connection);
      sqlCommand1.Transaction = sqlTransaction;
      SqlCommand sqlCommand2 = new SqlCommand(cmdText2, connection);
      sqlCommand2.Transaction = sqlTransaction;
      SqlCommand sqlCommand3 = new SqlCommand(cmdText3, connection);
      sqlCommand3.Transaction = sqlTransaction;
      try
      {
        sqlCommand1.ExecuteNonQuery();
        sqlCommand2.ExecuteNonQuery();
        sqlCommand3.ExecuteNonQuery();
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
  }
}
