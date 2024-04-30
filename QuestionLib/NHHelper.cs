// Decompiled with JetBrains decompiler
// Type: QuestionLib.NHHelper
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

#nullable disable
namespace QuestionLib
{
  public class NHHelper
  {
    public static string ConnectionString = "";
    public ISessionFactory SessionFactory;

    public void Configure()
    {
      Configuration configuration = new Configuration().Configure();
      configuration.AddAssembly("QuestionLib");
      configuration.Properties[(object) "hibernate.connection.connection_string"] = (object) NHHelper.ConnectionString;
      configuration.Properties[(object) "connection.connection_string"] = (object) NHHelper.ConnectionString;
      this.SessionFactory = configuration.BuildSessionFactory();
    }

    public void ExportTables()
    {
      Configuration configuration = new Configuration().Configure();
      configuration.AddAssembly("QuestionLib");
      new SchemaExport(configuration).Create(true, true);
    }

    public static ISessionFactory GetSessionFactory()
    {
      NHHelper nhHelper = new NHHelper();
      nhHelper.Configure();
      return nhHelper.SessionFactory;
    }
  }
}
