// Decompiled with JetBrains decompiler
// Type: QuestionLib.Business.BOBase
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
  public class BOBase
  {
    protected ISessionFactory sessionFactory;
    protected ISession session;

    public BOBase()
    {
    }

    public BOBase(ISessionFactory sessionFactory) => this.sessionFactory = sessionFactory;

    public object SaveOrUpdate(object obj)
    {
      this.session = this.sessionFactory.OpenSession();
      using (ITransaction itransaction = this.session.BeginTransaction())
      {
        try
        {
          this.session.SaveOrUpdate(obj);
          this.session.Flush();
          itransaction.Commit();
          this.session.Close();
          return obj;
        }
        catch (Exception ex)
        {
          itransaction.Rollback();
          this.session.Close();
          throw ex;
        }
      }
    }

    public object Save(object obj)
    {
      this.session = this.sessionFactory.OpenSession();
      using (ITransaction itransaction = this.session.BeginTransaction())
      {
        try
        {
          this.session.Save(obj);
          this.session.Flush();
          itransaction.Commit();
          this.session.Close();
          return obj;
        }
        catch (Exception ex)
        {
          itransaction.Rollback();
          this.session.Close();
          throw ex;
        }
      }
    }

    public object Save(object obj, ISession mySession)
    {
      try
      {
        mySession.Save(obj);
        mySession.Flush();
        return obj;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public object Update(object obj)
    {
      this.session = this.sessionFactory.OpenSession();
      using (ITransaction itransaction = this.session.BeginTransaction())
      {
        try
        {
          this.session.Update(obj);
          this.session.Flush();
          itransaction.Commit();
          this.session.Close();
          return obj;
        }
        catch (Exception ex)
        {
          itransaction.Rollback();
          this.session.Close();
          throw ex;
        }
      }
    }

    public object Update(object obj, ISession mySession)
    {
      try
      {
        mySession.Update(obj);
        mySession.Flush();
        return obj;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void Load(object obj, object id)
    {
      this.session = this.sessionFactory.OpenSession();
      try
      {
        this.session.Load(obj, id);
        this.session.Close();
      }
      catch (Exception ex)
      {
        this.session.Close();
        throw ex;
      }
    }

    public void Delete(object obj)
    {
      this.session = this.sessionFactory.OpenSession();
      using (ITransaction itransaction = this.session.BeginTransaction())
      {
        try
        {
          this.session.Delete(obj);
          this.session.Flush();
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

    public IList List(string typeName)
    {
      IList list = (IList) null;
      this.session = this.sessionFactory.OpenSession();
      using (ITransaction itransaction = this.session.BeginTransaction())
      {
        list = this.session.CreateQuery("from " + typeName).List();
        itransaction.Commit();
        this.session.Close();
      }
      return list;
    }

    public IList ListID(string typeName, QuestionType qt, int chapterID)
    {
      IList list = (IList) null;
      string str1;
      string str2;
      switch (qt)
      {
        case QuestionType.READING:
          str1 = "pid";
          str2 = "=0";
          break;
        case QuestionType.MULTIPLE_CHOICE:
          str1 = "qid";
          str2 = "=1";
          break;
        case QuestionType.INDICATE_MISTAKE:
          str1 = "qid";
          str2 = "=2";
          break;
        case QuestionType.MATCH:
          str1 = "mid";
          str2 = "=3";
          break;
        default:
          str1 = "qid";
          str2 = ">3";
          break;
      }
      "SELECT " + str1 + " FROM " + typeName + " WHERE chapterId=" + (object) chapterID + " AND qType=" + str2;
      SqlConnection connection = (SqlConnection) this.sessionFactory.ConnectionProvider.GetConnection();
      return list;
    }

    public IList ListID(string typeName, QuestionType qt, string courseID) => (IList) null;
  }
}
