﻿// Decompiled with JetBrains decompiler
// Type: QuestionLib.QuestionDistribution
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using System;

#nullable disable
namespace QuestionLib
{
  [Serializable]
  public class QuestionDistribution
  {
    public int MultipleChoices { get; set; }

    public int Reading { get; set; }

    public int FillBlank { get; set; }

    public int Matching { get; set; }

    public int IndicateMistake { get; set; }
  }
}
