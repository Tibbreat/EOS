// Decompiled with JetBrains decompiler
// Type: QuestionLib.PaperSection
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace QuestionLib
{
  [Serializable]
  public class PaperSection
  {
    public int SectionNo { get; set; }

    public int QFrom { get; set; }

    public int QTo { get; set; }

    public string InAnyOrderGroup { get; set; }

    public List<ImagePaperAnswer> Answers { get; set; }

    public int GetAnswerCount() => this.QTo - this.QFrom + 1;

    public PaperSection() => this.Answers = new List<ImagePaperAnswer>();
  }
}
