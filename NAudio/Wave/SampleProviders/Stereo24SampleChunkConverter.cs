// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.Stereo24SampleChunkConverter
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  internal class Stereo24SampleChunkConverter : ISampleChunkConverter
  {
    private int offset;
    private byte[] sourceBuffer;
    private int sourceBytes;

    public bool Supports(WaveFormat waveFormat)
    {
      return waveFormat.Encoding == WaveFormatEncoding.Pcm && waveFormat.BitsPerSample == 24 && waveFormat.Channels == 2;
    }

    public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
    {
      int num = samplePairsRequired * 6;
      this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
      this.sourceBytes = source.Read(this.sourceBuffer, 0, num);
      this.offset = 0;
    }

    public bool GetNextSample(out float sampleLeft, out float sampleRight)
    {
      if (this.offset < this.sourceBytes)
      {
        sampleLeft = (float) ((int) (sbyte) this.sourceBuffer[this.offset + 2] << 16 | (int) this.sourceBuffer[this.offset + 1] << 8 | (int) this.sourceBuffer[this.offset]) / 8388608f;
        this.offset += 3;
        sampleRight = (float) ((int) (sbyte) this.sourceBuffer[this.offset + 2] << 16 | (int) this.sourceBuffer[this.offset + 1] << 8 | (int) this.sourceBuffer[this.offset]) / 8388608f;
        this.offset += 3;
        return true;
      }
      sampleLeft = 0.0f;
      sampleRight = 0.0f;
      return false;
    }
  }
}
