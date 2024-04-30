// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveInProvider
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave
{
  public class WaveInProvider : IWaveProvider
  {
    private IWaveIn waveIn;
    private BufferedWaveProvider bufferedWaveProvider;

    public WaveInProvider(IWaveIn waveIn)
    {
      this.waveIn = waveIn;
      waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(this.waveIn_DataAvailable);
      this.bufferedWaveProvider = new BufferedWaveProvider(this.WaveFormat);
    }

    private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
    {
      this.bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
    }

    public int Read(byte[] buffer, int offset, int count)
    {
      return this.bufferedWaveProvider.Read(buffer, 0, count);
    }

    public WaveFormat WaveFormat => this.waveIn.WaveFormat;
  }
}
