// Decompiled with JetBrains decompiler
// Type: QuestionLib.Entity.TestTemplateDetails
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

#nullable disable
namespace QuestionLib.Entity
{
  public class TestTemplateDetails
  {
    public int TestTemplateDetailsID { get; set; }

    public int TestTemplateID { get; set; }

    public int ChapterId { get; set; }

    public QuestionType QuestionType { get; set; }

    public int NoQInTest { get; set; }
  }
}
