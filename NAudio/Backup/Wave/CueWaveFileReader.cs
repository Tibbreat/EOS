﻿// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.CueWaveFileReader
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Wave
{
  public class CueWaveFileReader : WaveFileReader
  {
    private CueList cues;

    public CueWaveFileReader(string fileName)
      : base(fileName)
    {
    }

    public CueList Cues
    {
      get
      {
        if (this.cues == null)
          this.cues = CueList.FromChunks((WaveFileReader) this);
        return this.cues;
      }
    }
  }
}
