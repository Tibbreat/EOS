// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.NotifyingSampleProvider
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  public class NotifyingSampleProvider : ISampleProvider, ISampleNotifier
  {
    private ISampleProvider source;
    private SampleEventArgs sampleArgs = new SampleEventArgs(0.0f, 0.0f);
    private int channels;

    public NotifyingSampleProvider(ISampleProvider source)
    {
      this.source = source;
      this.channels = this.WaveFormat.Channels;
    }

    public WaveFormat WaveFormat => this.source.WaveFormat;

    public int Read(float[] buffer, int offset, int sampleCount)
    {
      int num = this.source.Read(buffer, offset, sampleCount);
      if (this.Sample != null)
      {
        for (int index = 0; index < num; index += this.channels)
        {
          this.sampleArgs.Left = buffer[offset + index];
          this.sampleArgs.Right = this.channels > 1 ? buffer[offset + index + 1] : this.sampleArgs.Left;
          this.Sample((object) this, this.sampleArgs);
        }
      }
      return num;
    }

    public event EventHandler<SampleEventArgs> Sample;
  }
}
