// Decompiled with JetBrains decompiler
// Type: NAudio.FileFormats.Mp3.DmoMp3FrameDecompressor
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Dmo;
using NAudio.Wave;
using System;

#nullable disable
namespace NAudio.FileFormats.Mp3
{
  public class DmoMp3FrameDecompressor : IMp3FrameDecompressor, IDisposable
  {
    private WindowsMediaMp3Decoder mp3Decoder;
    private WaveFormat pcmFormat;
    private MediaBuffer inputMediaBuffer;
    private DmoOutputDataBuffer outputBuffer;
    private bool reposition;

    public DmoMp3FrameDecompressor(WaveFormat sourceFormat)
    {
      this.mp3Decoder = new WindowsMediaMp3Decoder();
      if (!this.mp3Decoder.MediaObject.SupportsInputWaveFormat(0, sourceFormat))
        throw new ArgumentException("Unsupported input format");
      this.mp3Decoder.MediaObject.SetInputWaveFormat(0, sourceFormat);
      this.pcmFormat = new WaveFormat(sourceFormat.SampleRate, sourceFormat.Channels);
      if (!this.mp3Decoder.MediaObject.SupportsOutputWaveFormat(0, this.pcmFormat))
        throw new ArgumentException(string.Format("Unsupported output format {0}", (object) this.pcmFormat));
      this.mp3Decoder.MediaObject.SetOutputWaveFormat(0, this.pcmFormat);
      this.inputMediaBuffer = new MediaBuffer(sourceFormat.AverageBytesPerSecond);
      this.outputBuffer = new DmoOutputDataBuffer(this.pcmFormat.AverageBytesPerSecond);
    }

    public WaveFormat OutputFormat => this.pcmFormat;

    public int DecompressFrame(Mp3Frame frame, byte[] dest, int destOffset)
    {
      this.inputMediaBuffer.LoadData(frame.RawData, frame.FrameLength);
      if (this.reposition)
      {
        this.mp3Decoder.MediaObject.Flush();
        this.reposition = false;
      }
      this.mp3Decoder.MediaObject.ProcessInput(0, (IMediaBuffer) this.inputMediaBuffer, DmoInputDataBufferFlags.None, 0L, 0L);
      this.outputBuffer.MediaBuffer.SetLength(0);
      this.outputBuffer.StatusFlags = DmoOutputDataBufferFlags.None;
      this.mp3Decoder.MediaObject.ProcessOutput(DmoProcessOutputFlags.None, 1, new DmoOutputDataBuffer[1]
      {
        this.outputBuffer
      });
      if (this.outputBuffer.Length == 0)
        return 0;
      this.outputBuffer.RetrieveData(dest, destOffset);
      return this.outputBuffer.Length;
    }

    public void Reset() => this.reposition = true;

    public void Dispose()
    {
      if (this.inputMediaBuffer != null)
      {
        this.inputMediaBuffer.Dispose();
        this.inputMediaBuffer = (MediaBuffer) null;
      }
      this.outputBuffer.Dispose();
      if (this.mp3Decoder == null)
        return;
      this.mp3Decoder.Dispose();
      this.mp3Decoder = (WindowsMediaMp3Decoder) null;
    }
  }
}
