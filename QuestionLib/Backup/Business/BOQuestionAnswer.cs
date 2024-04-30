// Decompiled with JetBrains decompiler
// Type: QuestionLib.Business.BOQuestionAnswer
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using NHibernate;
using System;
using System.Collections;

#nullable disable
namespace QuestionLib.Business
{
  public class BOQuestionAnswer : BOBase
  {
    public BOQuestionAnswer(ISessionFactory sessionFactory)
      : base(sessionFactory)
    {
    }

    public IList LoadAnswer(int qid)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from QuestionAnswer qa Where qa.QID=:qid");
        query.SetParameter(nameof (qid), (object) qid);
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
  }
}
