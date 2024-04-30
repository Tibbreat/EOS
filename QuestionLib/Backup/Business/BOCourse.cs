// Decompiled with JetBrains decompiler
// Type: QuestionLib.Business.BOCourse
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using NHibernate;
using System;
using System.Collections;

#nullable disable
namespace QuestionLib.Business
{
  public class BOCourse : BOBase
  {
    public BOCourse(ISessionFactory sessionFactory)
      : base(sessionFactory)
    {
    }

    public IList LoadChapterByCourse(string courseId)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from Chapter ch Where CID=:courseId");
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

    public bool IsCourseExists(string courseId)
    {
      bool flag = false;
      this.session = this.sessionFactory.OpenSession();
      try
      {
        IQuery query = this.session.CreateQuery("from Course c Where CID=:courseId");
        query.SetParameter(nameof (courseId), (object) courseId);
        IList list = query.List();
        this.session.Close();
        if (list.Count > 0)
          flag = true;
      }
      catch (Exception ex)
      {
        this.session.Close();
        throw ex;
      }
      return flag;
    }
  }
}
