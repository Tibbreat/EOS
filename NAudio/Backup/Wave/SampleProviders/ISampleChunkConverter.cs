﻿// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.ISampleChunkConverter
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  internal interface ISampleChunkConverter
  {
    bool Supports(WaveFormat format);

    void LoadNextChunk(IWaveProvider sourceProvider, int samplePairsRequired);

    bool GetNextSample(out float sampleLeft, out float sampleRight);
  }
}
