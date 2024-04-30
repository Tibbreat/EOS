// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.SampleDataChunk
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.IO;

#nullable disable
namespace NAudio.SoundFont
{
  internal class SampleDataChunk
  {
    private byte[] sampleData;

    public SampleDataChunk(RiffChunk chunk)
    {
      string str = chunk.ReadChunkID();
      if (str != "sdta")
        throw new InvalidDataException(string.Format("Not a sample data chunk ({0})", (object) str));
      this.sampleData = chunk.GetData();
    }

    public byte[] SampleData => this.sampleData;
  }
}
