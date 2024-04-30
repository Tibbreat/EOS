// Decompiled with JetBrains decompiler
// Type: QuestionLib.Entity.EssayQuestion
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using System;

#nullable disable
namespace QuestionLib.Entity
{
  [Serializable]
  public class EssayQuestion
  {
    private int _QBID;

    public int EQID { get; set; }

    public string CourseId { get; set; }

    public int ChapterId { get; set; }

    public byte[] Question { get; set; }

    public int QuestionFileSize { get; set; }

    public string Name { get; set; }

    public byte[] Guide2Mark { get; set; }

    public int Guide2MarkFileSize { get; set; }

    public string Development { get; set; }

    public int QBID
    {
      get => this._QBID;
      set => this._QBID = value;
    }

    public void Preapare2Submit()
    {
      this.CourseId = (string) null;
      this.Question = (byte[]) null;
      this.Name = (string) null;
      this.Guide2Mark = (byte[]) null;
    }
  }
}
