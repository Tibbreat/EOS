// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.RawSourceWaveStream
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.IO;

#nullable disable
namespace NAudio.Wave
{
  public class RawSourceWaveStream : WaveStream
  {
    private Stream sourceStream;
    private WaveFormat waveFormat;

    public RawSourceWaveStream(Stream sourceStream, WaveFormat waveFormat)
    {
      this.sourceStream = sourceStream;
      this.waveFormat = waveFormat;
    }

    public override WaveFormat WaveFormat => this.waveFormat;

    public override long Length => this.sourceStream.Length;

    public override long Position
    {
      get => this.sourceStream.Position;
      set => this.sourceStream.Position = value;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      return this.sourceStream.Read(buffer, offset, count);
    }
  }
}
