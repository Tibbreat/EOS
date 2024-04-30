// Decompiled with JetBrains decompiler
// Type: QuestionLib.ImagePaperAnswer
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using System;

#nullable disable
namespace QuestionLib
{
  [Serializable]
  public class ImagePaperAnswer
  {
    public string Answer { get; set; }

    public int PartCount { get; set; }

    public bool IsLongAnswer { get; set; }
  }
}
