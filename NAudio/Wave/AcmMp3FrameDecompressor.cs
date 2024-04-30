// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.AcmMp3FrameDecompressor
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Wave.Compression;
using System;

#nullable disable
namespace NAudio.Wave
{
  public class AcmMp3FrameDecompressor : IMp3FrameDecompressor, IDisposable
  {
    private readonly AcmStream conversionStream;
    private readonly WaveFormat pcmFormat;
    private bool disposed;

    public AcmMp3FrameDecompressor(WaveFormat sourceFormat)
    {
      this.pcmFormat = AcmStream.SuggestPcmFormat(sourceFormat);
      try
      {
        this.conversionStream = new AcmStream(sourceFormat, this.pcmFormat);
      }
      catch (Exception ex)
      {
        this.disposed = true;
        GC.SuppressFinalize((object) this);
        throw;
      }
    }

    public WaveFormat OutputFormat => this.pcmFormat;

    public int DecompressFrame(Mp3Frame frame, byte[] dest, int destOffset)
    {
      if (frame == null)
        throw new ArgumentNullException(nameof (frame), "You must provide a non-null Mp3Frame to decompress");
      Array.Copy((Array) frame.RawData, (Array) this.conversionStream.SourceBuffer, frame.FrameLength);
      int sourceBytesConverted = 0;
      int length = this.conversionStream.Convert(frame.FrameLength, out sourceBytesConverted);
      if (sourceBytesConverted != frame.FrameLength)
        throw new InvalidOperationException(string.Format("Couldn't convert the whole MP3 frame (converted {0}/{1})", (object) sourceBytesConverted, (object) frame.FrameLength));
      Array.Copy((Array) this.conversionStream.DestBuffer, 0, (Array) dest, destOffset, length);
      return length;
    }

    public void Reset() => this.conversionStream.Reposition();

    public void Dispose()
    {
      if (this.disposed)
        return;
      this.disposed = true;
      if (this.conversionStream != null)
        this.conversionStream.Dispose();
      GC.SuppressFinalize((object) this);
    }

    ~AcmMp3FrameDecompressor() => this.Dispose();
  }
}
