// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveFormatConversionStream
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Wave.Compression;
using System;

#nullable disable
namespace NAudio.Wave
{
  public class WaveFormatConversionStream : WaveStream
  {
    private AcmStream conversionStream;
    private WaveStream sourceStream;
    private WaveFormat targetFormat;
    private long length;
    private long position;
    private int preferredSourceReadSize;
    private int leftoverDestBytes;
    private int leftoverDestOffset;
    private int leftoverSourceBytes;

    public static WaveStream CreatePcmStream(WaveStream sourceStream)
    {
      if (sourceStream.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
        return sourceStream;
      WaveFormat targetFormat = AcmStream.SuggestPcmFormat(sourceStream.WaveFormat);
      if (targetFormat.SampleRate < 8000)
      {
        if (sourceStream.WaveFormat.Encoding != WaveFormatEncoding.G723)
          throw new InvalidOperationException("Invalid suggested output format, please explicitly provide a target format");
        targetFormat = new WaveFormat(8000, 16, 1);
      }
      return (WaveStream) new WaveFormatConversionStream(targetFormat, sourceStream);
    }

    public WaveFormatConversionStream(WaveFormat targetFormat, WaveStream sourceStream)
    {
      this.sourceStream = sourceStream;
      this.targetFormat = targetFormat;
      this.conversionStream = new AcmStream(sourceStream.WaveFormat, targetFormat);
      this.length = this.EstimateSourceToDest((long) (int) sourceStream.Length);
      this.position = 0L;
      this.preferredSourceReadSize = Math.Min(sourceStream.WaveFormat.AverageBytesPerSecond, this.conversionStream.SourceBuffer.Length);
      this.preferredSourceReadSize -= this.preferredSourceReadSize % sourceStream.WaveFormat.BlockAlign;
    }

    [Obsolete("can be unreliable, use of this method not encouraged")]
    public int SourceToDest(int source) => (int) this.EstimateSourceToDest((long) source);

    private long EstimateSourceToDest(long source)
    {
      long num = source * (long) this.targetFormat.AverageBytesPerSecond / (long) this.sourceStream.WaveFormat.AverageBytesPerSecond;
      return num - num % (long) this.targetFormat.BlockAlign;
    }

    private long EstimateDestToSource(long dest)
    {
      long num = dest * (long) this.sourceStream.WaveFormat.AverageBytesPerSecond / (long) this.targetFormat.AverageBytesPerSecond;
      return (long) (int) (num - num % (long) this.sourceStream.WaveFormat.BlockAlign);
    }

    [Obsolete("can be unreliable, use of this method not encouraged")]
    public int DestToSource(int dest) => (int) this.EstimateDestToSource((long) dest);

    public override long Length => this.length;

    public override long Position
    {
      get => this.position;
      set
      {
        value -= value % (long) this.BlockAlign;
        this.sourceStream.Position = this.EstimateDestToSource(value);
        this.position = this.EstimateSourceToDest(this.sourceStream.Position);
        this.leftoverDestBytes = 0;
        this.leftoverDestOffset = 0;
        this.conversionStream.Reposition();
      }
    }

    public override WaveFormat WaveFormat => this.targetFormat;

    public override int Read(byte[] buffer, int offset, int count)
    {
      int num = 0;
      if (count % this.BlockAlign != 0)
        count -= count % this.BlockAlign;
      int length1;
      for (; num < count; num += length1)
      {
        int length2 = Math.Min(count - num, this.leftoverDestBytes);
        if (length2 > 0)
        {
          Array.Copy((Array) this.conversionStream.DestBuffer, this.leftoverDestOffset, (Array) buffer, offset + num, length2);
          this.leftoverDestOffset += length2;
          this.leftoverDestBytes -= length2;
          num += length2;
        }
        if (num < count)
        {
          int leftoverSourceBytes = this.leftoverSourceBytes;
          int bytesToConvert = this.sourceStream.Read(this.conversionStream.SourceBuffer, 0, this.preferredSourceReadSize);
          if (bytesToConvert != 0)
          {
            int sourceBytesConverted;
            int val1 = this.conversionStream.Convert(bytesToConvert, out sourceBytesConverted);
            if (sourceBytesConverted != 0)
            {
              if (sourceBytesConverted < bytesToConvert)
                this.sourceStream.Position -= (long) (bytesToConvert - sourceBytesConverted);
              if (val1 > 0)
              {
                int val2 = count - num;
                length1 = Math.Min(val1, val2);
                if (length1 < val1)
                {
                  this.leftoverDestBytes = val1 - length1;
                  this.leftoverDestOffset = length1;
                }
                Array.Copy((Array) this.conversionStream.DestBuffer, 0, (Array) buffer, num + offset, length1);
              }
              else
                break;
            }
            else
              break;
          }
          else
            break;
        }
        else
          break;
      }
      this.position += (long) num;
      return num;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.conversionStream != null)
        {
          this.conversionStream.Dispose();
          this.conversionStream = (AcmStream) null;
        }
        if (this.sourceStream != null)
        {
          this.sourceStream.Dispose();
          this.sourceStream = (WaveStream) null;
        }
      }
      base.Dispose(disposing);
    }
  }
}
