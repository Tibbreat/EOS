// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.RiffChunk
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Text;

#nullable disable
namespace NAudio.Wave
{
  public class RiffChunk
  {
    public RiffChunk(int identifier, int length, long streamPosition)
    {
      this.Identifier = identifier;
      this.Length = length;
      this.StreamPosition = streamPosition;
    }

    public int Identifier { get; private set; }

    public string IdentifierAsString
    {
      get => Encoding.UTF8.GetString(BitConverter.GetBytes(this.Identifier));
    }

    public int Length { get; private set; }

    public long StreamPosition { get; private set; }
  }
}
