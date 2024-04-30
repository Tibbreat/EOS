// Decompiled with JetBrains decompiler
// Type: QuestionLib.Business.BOChapter
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
  public class BOChapter : BOBase
  {
    public BOChapter(ISessionFactory sessionFactory)
      : base(sessionFactory)
    {
    }

    public IList LoadFillBlankQuestionByChapter(int chapterId)
    {
      QuestionType questionType1 = QuestionType.FILL_BLANK_ALL;
      QuestionType questionType2 = QuestionType.FILL_BLANK_GROUP;
      QuestionType questionType3 = QuestionType.FILL_BLANK_EMPTY;
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from Question q Where (q.QType=:type1 OR q.QType=:type2 OR q.QType=:type3)  AND ChapterId=:chapterId");
        query.SetParameter("type1", (object) questionType1);
        query.SetParameter("type2", (object) questionType2);
        query.SetParameter("type3", (object) questionType3);
        query.SetParameter(nameof (chapterId), (object) chapterId.ToString());
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

    public IList LoadQuestionByChapter(QuestionType qt, int chapterId)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from Question q Where q.QType=:type and ChapterId=:chapterId");
        query.SetParameter("type", (object) qt);
        query.SetParameter(nameof (chapterId), (object) chapterId.ToString());
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

    public IList LoadPassageByChapter(int chapterId)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from Passage p Where ChapterId=:chapterId");
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

    public IList LoadMatchQuestionByChapter(int chapterId)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from MatchQuestion m Where ChapterId=:chapterId");
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
  }
}
