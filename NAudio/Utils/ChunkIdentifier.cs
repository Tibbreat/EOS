// Decompiled with JetBrains decompiler
// Type: NAudio.Utils.ChunkIdentifier
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Text;

#nullable disable
namespace NAudio.Utils
{
  public class ChunkIdentifier
  {
    public static int ChunkIdentifierToInt32(string s)
    {
      byte[] numArray = s.Length == 4 ? Encoding.UTF8.GetBytes(s) : throw new ArgumentException("Must be a four character string");
      return numArray.Length == 4 ? BitConverter.ToInt32(numArray, 0) : throw new ArgumentException("Must encode to exactly four bytes");
    }
  }
}
