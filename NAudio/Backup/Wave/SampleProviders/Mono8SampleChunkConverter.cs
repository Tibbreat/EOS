// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.Mono8SampleChunkConverter
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  internal class Mono8SampleChunkConverter : ISampleChunkConverter
  {
    private int offset;
    private byte[] sourceBuffer;
    private int sourceBytes;

    public bool Supports(WaveFormat waveFormat)
    {
      return waveFormat.Encoding == WaveFormatEncoding.Pcm && waveFormat.BitsPerSample == 8 && waveFormat.Channels == 1;
    }

    public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
    {
      int num = samplePairsRequired;
      this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
      this.sourceBytes = source.Read(this.sourceBuffer, 0, num);
      this.offset = 0;
    }

    public bool GetNextSample(out float sampleLeft, out float sampleRight)
    {
      if (this.offset < this.sourceBytes)
      {
        sampleLeft = (float) this.sourceBuffer[this.offset] / 256f;
        ++this.offset;
        sampleRight = sampleLeft;
        return true;
      }
      sampleLeft = 0.0f;
      sampleRight = 0.0f;
      return false;
    }
  }
}
