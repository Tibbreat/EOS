// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.IMp3FrameDecompressor
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave
{
  public interface IMp3FrameDecompressor : IDisposable
  {
    int DecompressFrame(Mp3Frame frame, byte[] dest, int destOffset);

    void Reset();

    WaveFormat OutputFormat { get; }
  }
}
