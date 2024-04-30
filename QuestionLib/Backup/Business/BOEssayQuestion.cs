// Decompiled with JetBrains decompiler
// Type: QuestionLib.Business.BOEssayQuestion
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using NHibernate;
using QuestionLib.Entity;
using System;
using System.Collections;

#nullable disable
namespace QuestionLib.Business
{
  public class BOEssayQuestion : BOBase
  {
    public BOEssayQuestion(ISessionFactory sessionFactory)
      : base(sessionFactory)
    {
    }

    public EssayQuestion Load(int eqid)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from EssayQuestion q Where q.EQID=:eqid");
        query.SetParameter(nameof (eqid), (object) eqid);
        list = query.List();
        this.session.Close();
      }
      catch (Exception ex)
      {
        this.session.Close();
        throw ex;
      }
      return list.Count > 0 ? (EssayQuestion) list[0] : (EssayQuestion) null;
    }

    public IList LoadByCourse(string courseId)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from EssayQuestion q Where CourseId=:courseId");
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

    public IList LoadByChapter(int chapterId)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from EssayQuestion q Where ChapterId=:chapterId");
        query.SetParameter(nameof (chapterId), (object) chapterId);
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

    public void Delete(int eqid)
    {
      this.session = this.sessionFactory.OpenSession();
      ITransaction itransaction = this.session.BeginTransaction();
      try
      {
        this.session.Delete("from EssayQuestion q Where q.EQID=" + eqid.ToString());
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
        foreach (EssayQuestion essayQuestion in (IEnumerable) list)
          isession.Save((object) essayQuestion);
        itransaction.Commit();
        return true;
      }
      catch
      {
        itransaction.Rollback();
        return false;
      }
    }

    public void DeleteQuestionInChapter(int chapterId)
    {
      this.session = this.sessionFactory.OpenSession();
      ITransaction itransaction = this.session.BeginTransaction();
      try
      {
        this.session.Delete("from EssayQuestion q Where q.ChapterId=" + chapterId.ToString());
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
