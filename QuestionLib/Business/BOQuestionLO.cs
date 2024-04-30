// Decompiled with JetBrains decompiler
// Type: QuestionLib.Business.BOQuestionLO
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
  public class BOQuestionLO : BOBase
  {
    public BOQuestionLO(ISessionFactory sessionFactory)
      : base(sessionFactory)
    {
    }

    public IList LoadLO(QuestionType qType, int qid)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from QuestionLO qlo Where qlo.QType=:qType and qlo.QID=:qid");
        query.SetParameter(nameof (qType), (object) qType);
        query.SetParameter(nameof (qid), (object) qid);
        list = query.List();
        this.session.Close();
      }
      catch (Exception ex)
      {
        this.session.Close();
        throw ex;
      }
      ArrayList arrayList = new ArrayList();
      BOLO bolo = new BOLO(NHHelper.GetSessionFactory());
      foreach (QuestionLO questionLo in (IEnumerable) list)
      {
        LO lo = bolo.Load(questionLo.LOID);
        arrayList.Add((object) lo);
      }
      return (IList) arrayList;
    }

    public void DeleteQuestionLO(int qid, QuestionType qType)
    {
      this.session = this.sessionFactory.OpenSession();
      ITransaction itransaction = this.session.BeginTransaction();
      int num = (int) qType;
      try
      {
        this.session.Delete("from QuestionLO qlo Where qlo.QType=" + (object) num + " and qlo.QID=" + (object) qid);
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
