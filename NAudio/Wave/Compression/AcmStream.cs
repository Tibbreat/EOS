// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Compression.AcmStream
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave.Compression
{
  public class AcmStream : IDisposable
  {
    private IntPtr streamHandle;
    private IntPtr driverHandle;
    private AcmStreamHeader streamHeader;
    private WaveFormat sourceFormat;

    public AcmStream(WaveFormat sourceFormat, WaveFormat destFormat)
    {
      try
      {
        this.streamHandle = IntPtr.Zero;
        this.sourceFormat = sourceFormat;
        int num1 = Math.Max(65536, sourceFormat.AverageBytesPerSecond);
        int num2 = num1 - num1 % sourceFormat.BlockAlign;
        MmException.Try(AcmInterop.acmStreamOpen(out this.streamHandle, IntPtr.Zero, sourceFormat, destFormat, (WaveFilter) null, IntPtr.Zero, IntPtr.Zero, AcmStreamOpenFlags.NonRealTime), "acmStreamOpen");
        int dest = this.SourceToDest(num2);
        this.streamHeader = new AcmStreamHeader(this.streamHandle, num2, dest);
        this.driverHandle = IntPtr.Zero;
      }
      catch
      {
        this.Dispose();
        throw;
      }
    }

    public AcmStream(IntPtr driverId, WaveFormat sourceFormat, WaveFilter waveFilter)
    {
      int num1 = Math.Max(16384, sourceFormat.AverageBytesPerSecond);
      this.sourceFormat = sourceFormat;
      int num2 = num1 - num1 % sourceFormat.BlockAlign;
      MmException.Try(AcmInterop.acmDriverOpen(out this.driverHandle, driverId, 0), "acmDriverOpen");
      MmException.Try(AcmInterop.acmStreamOpen(out this.streamHandle, this.driverHandle, sourceFormat, sourceFormat, waveFilter, IntPtr.Zero, IntPtr.Zero, AcmStreamOpenFlags.NonRealTime), "acmStreamOpen");
      this.streamHeader = new AcmStreamHeader(this.streamHandle, num2, this.SourceToDest(num2));
    }

    public int SourceToDest(int source)
    {
      if (source == 0)
        return 0;
      int outputBufferSize;
      MmException.Try(AcmInterop.acmStreamSize(this.streamHandle, source, out outputBufferSize, AcmStreamSizeFlags.Source), "acmStreamSize");
      return outputBufferSize;
    }

    public int DestToSource(int dest)
    {
      if (dest == 0)
        return 0;
      int outputBufferSize;
      MmException.Try(AcmInterop.acmStreamSize(this.streamHandle, dest, out outputBufferSize, AcmStreamSizeFlags.Destination), "acmStreamSize");
      return outputBufferSize;
    }

    public static WaveFormat SuggestPcmFormat(WaveFormat compressedFormat)
    {
      WaveFormat waveFormat = new WaveFormat(compressedFormat.SampleRate, 16, compressedFormat.Channels);
      MmException.Try(AcmInterop.acmFormatSuggest(IntPtr.Zero, compressedFormat, waveFormat, Marshal.SizeOf((object) waveFormat), AcmFormatSuggestFlags.FormatTag), "acmFormatSuggest");
      return waveFormat;
    }

    public byte[] SourceBuffer => this.streamHeader.SourceBuffer;

    public byte[] DestBuffer => this.streamHeader.DestBuffer;

    public void Reposition() => this.streamHeader.Reposition();

    public int Convert(int bytesToConvert, out int sourceBytesConverted)
    {
      if (bytesToConvert % this.sourceFormat.BlockAlign != 0)
        bytesToConvert -= bytesToConvert % this.sourceFormat.BlockAlign;
      return this.streamHeader.Convert(bytesToConvert, out sourceBytesConverted);
    }

    [Obsolete("Call the version returning sourceBytesConverted instead")]
    public int Convert(int bytesToConvert)
    {
      int sourceBytesConverted;
      int num = this.Convert(bytesToConvert, out sourceBytesConverted);
      if (sourceBytesConverted != bytesToConvert)
        throw new MmException(MmResult.NotSupported, "AcmStreamHeader.Convert didn't convert everything");
      return num;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing && this.streamHeader != null)
      {
        this.streamHeader.Dispose();
        this.streamHeader = (AcmStreamHeader) null;
      }
      if (this.streamHandle != IntPtr.Zero)
      {
        MmResult result = AcmInterop.acmStreamClose(this.streamHandle, 0);
        this.streamHandle = IntPtr.Zero;
        if (result != MmResult.NoError)
          throw new MmException(result, "acmStreamClose");
      }
      if (!(this.driverHandle != IntPtr.Zero))
        return;
      int num = (int) AcmInterop.acmDriverClose(this.driverHandle, 0);
      this.driverHandle = IntPtr.Zero;
    }

    ~AcmStream() => this.Dispose(false);
  }
}
