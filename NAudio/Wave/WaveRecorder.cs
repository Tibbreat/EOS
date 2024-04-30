// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveRecorder
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave
{
  public class WaveRecorder : IWaveProvider, IDisposable
  {
    private WaveFileWriter writer;
    private IWaveProvider source;

    public WaveRecorder(IWaveProvider source, string destination)
    {
      this.source = source;
      this.writer = new WaveFileWriter(destination, source.WaveFormat);
    }

    public int Read(byte[] buffer, int offset, int count)
    {
      int count1 = this.source.Read(buffer, offset, count);
      this.writer.Write(buffer, offset, count1);
      return count1;
    }

    public WaveFormat WaveFormat => this.source.WaveFormat;

    public void Dispose()
    {
      if (this.writer == null)
        return;
      this.writer.Dispose();
      this.writer = (WaveFileWriter) null;
    }
  }
}
