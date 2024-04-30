// Decompiled with JetBrains decompiler
// Type: QuestionLib.Entity.TestTemplate
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using System;

#nullable disable
namespace QuestionLib.Entity
{
  public class TestTemplate
  {
    public int TestTemplateID { get; set; }

    public string CID { get; set; }

    public string TestTemplateName { get; set; }

    public string CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int DistinctWithLastTest { get; set; }

    public int Duration { get; set; }

    public string Note { get; set; }
  }
}
