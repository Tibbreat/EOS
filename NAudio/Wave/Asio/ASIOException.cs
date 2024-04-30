// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Asio.ASIOException
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave.Asio
{
  internal class ASIOException : Exception
  {
    private ASIOError error;

    public ASIOException()
    {
    }

    public ASIOException(string message)
      : base(message)
    {
    }

    public ASIOException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    public ASIOError Error
    {
      get => this.error;
      set
      {
        this.error = value;
        this.Data[(object) "ASIOError"] = (object) this.error;
      }
    }

    public static string getErrorName(ASIOError error)
    {
      return Enum.GetName(typeof (ASIOError), (object) error);
    }
  }
}
