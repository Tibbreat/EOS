// Decompiled with JetBrains decompiler
// Type: QuestionLib.SubmitPaper
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using System;

#nullable disable
namespace QuestionLib
{
  [Serializable]
  public class SubmitPaper
  {
    public string LoginId;
    public int TimeLeft;
    public int IndexFill;
    public int IndexReading;
    public int IndexG;
    public int IndexIndiM;
    public int IndexMatch;
    public bool Finish;
    public Paper SPaper;
    public int TabIndex;
    public DateTime SubmitTime;
    public string ID;

    public override bool Equals(object obj)
    {
      SubmitPaper submitPaper = (SubmitPaper) obj;
      return this.ID.Equals(submitPaper.ID) && this.SPaper.ExamCode.Equals(submitPaper.SPaper.ExamCode);
    }
  }
}
