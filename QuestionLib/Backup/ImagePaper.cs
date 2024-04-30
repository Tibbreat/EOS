// Decompiled with JetBrains decompiler
// Type: QuestionLib.ImagePaper
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace QuestionLib
{
  [Serializable]
  public class ImagePaper
  {
    public List<PaperSection> Sections { get; set; }

    public byte[] Image { get; set; }

    public int NumberOfPage { get; set; }

    public string LongAnswerGuide { get; set; }

    public void Preapare2Submit() => this.Image = (byte[]) null;
  }
}
