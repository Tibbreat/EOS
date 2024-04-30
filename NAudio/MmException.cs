// Decompiled with JetBrains decompiler
// Type: NAudio.MmException
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio
{
  public class MmException : Exception
  {
    private MmResult result;
    private string function;

    public MmException(MmResult result, string function)
      : base(MmException.ErrorMessage(result, function))
    {
      this.result = result;
      this.function = function;
    }

    private static string ErrorMessage(MmResult result, string function)
    {
      return string.Format("{0} calling {1}", (object) result, (object) function);
    }

    public static void Try(MmResult result, string function)
    {
      if (result != MmResult.NoError)
        throw new MmException(result, function);
    }

    public MmResult Result => this.result;
  }
}
