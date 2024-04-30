// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.Stereo16SampleChunkConverter
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  internal class Stereo16SampleChunkConverter : ISampleChunkConverter
  {
    private int sourceSample;
    private byte[] sourceBuffer;
    private WaveBuffer sourceWaveBuffer;
    private int sourceSamples;

    public bool Supports(WaveFormat waveFormat)
    {
      return waveFormat.Encoding == WaveFormatEncoding.Pcm && waveFormat.BitsPerSample == 16 && waveFormat.Channels == 2;
    }

    public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
    {
      int num = samplePairsRequired * 4;
      this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
      this.sourceWaveBuffer = new WaveBuffer(this.sourceBuffer);
      this.sourceSamples = source.Read(this.sourceBuffer, 0, num) / 2;
      this.sourceSample = 0;
    }

    public bool GetNextSample(out float sampleLeft, out float sampleRight)
    {
      if (this.sourceSample < this.sourceSamples)
      {
        sampleLeft = (float) this.sourceWaveBuffer.ShortBuffer[this.sourceSample++] / 32768f;
        sampleRight = (float) this.sourceWaveBuffer.ShortBuffer[this.sourceSample++] / 32768f;
        return true;
      }
      sampleLeft = 0.0f;
      sampleRight = 0.0f;
      return false;
    }
  }
}
