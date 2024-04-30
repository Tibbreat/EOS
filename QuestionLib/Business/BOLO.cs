// Decompiled with JetBrains decompiler
// Type: QuestionLib.Business.BOLO
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
  public class BOLO : BOBase
  {
    public BOLO(ISessionFactory sessionFactory)
      : base(sessionFactory)
    {
    }

    public LO Load(int loid)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from LO lo Where lo.LOID=:loid");
        query.SetParameter(nameof (loid), (object) loid);
        list = query.List();
        this.session.Close();
      }
      catch (Exception ex)
      {
        this.session.Close();
        throw ex;
      }
      return list.Count > 0 ? (LO) list[0] : (LO) null;
    }

    public int GetLOID(string lo_name, string CID)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from LO lo Where lo.LO_Name=:lo_name And lo.CID=:CID");
        query.SetParameter(nameof (lo_name), (object) lo_name.Trim());
        query.SetParameter(nameof (CID), (object) CID);
        list = query.List();
        this.session.Close();
      }
      catch (Exception ex)
      {
        this.session.Close();
        throw ex;
      }
      return ((LO) list[0]).LOID;
    }

    public IList LoadLOByCourse(string courseId)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from LO lo Where lo.CID=:courseId");
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

    public bool IsLOExists(string CID, string lo_name)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from LO lo Where lo.CID=:CID And lo.LO_Name=:lo_name");
        query.SetParameter(nameof (lo_name), (object) lo_name);
        query.SetParameter(nameof (CID), (object) CID);
        list = query.List();
        this.session.Close();
      }
      catch (Exception ex)
      {
        this.session.Close();
        throw ex;
      }
      return list.Count != 0;
    }

    public bool IsLODescriptionExists(string CID, string lo_desc)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from LO lo Where lo.CID=:CID And lo.LO_Desc=:lo_desc");
        query.SetParameter(nameof (lo_desc), (object) lo_desc);
        query.SetParameter(nameof (CID), (object) CID);
        list = query.List();
        this.session.Close();
      }
      catch (Exception ex)
      {
        this.session.Close();
        throw ex;
      }
      return list.Count != 0;
    }

    public static ArrayList RemoveDupLO(ArrayList listLO)
    {
      ArrayList arrayList = new ArrayList();
      foreach (QuestionLO questionLo1 in listLO)
      {
        bool flag = false;
        foreach (QuestionLO questionLo2 in arrayList)
        {
          if (questionLo1.LOID == questionLo2.LOID)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          arrayList.Add((object) questionLo1);
      }
      return arrayList;
    }
  }
}
