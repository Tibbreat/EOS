// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.XingHeader
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave
{
  public class XingHeader
  {
    private static int[] sr_table = new int[4]
    {
      44100,
      48000,
      32000,
      99999
    };
    private int vbrScale = -1;
    private int startOffset;
    private int endOffset;
    private int tocOffset = -1;
    private int framesOffset = -1;
    private int bytesOffset = -1;
    private Mp3Frame frame;

    private static int ReadBigEndian(byte[] buffer, int offset)
    {
      return (((int) buffer[offset] << 8 | (int) buffer[offset + 1]) << 8 | (int) buffer[offset + 2]) << 8 | (int) buffer[offset + 3];
    }

    private void WriteBigEndian(byte[] buffer, int offset, int value)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      for (int index = 0; index < 4; ++index)
        buffer[offset + 4 - index] = bytes[index];
    }

    public static XingHeader LoadXingHeader(Mp3Frame frame)
    {
      XingHeader xingHeader = new XingHeader();
      xingHeader.frame = frame;
      int index;
      if (frame.MpegVersion == MpegVersion.Version1)
      {
        index = frame.ChannelMode == ChannelMode.Mono ? 21 : 36;
      }
      else
      {
        if (frame.MpegVersion != MpegVersion.Version2)
          return (XingHeader) null;
        index = frame.ChannelMode == ChannelMode.Mono ? 13 : 21;
      }
      if (frame.RawData[index] != (byte) 88 || frame.RawData[index + 1] != (byte) 105 || frame.RawData[index + 2] != (byte) 110 || frame.RawData[index + 3] != (byte) 103)
        return (XingHeader) null;
      xingHeader.startOffset = index;
      int offset1 = index + 4;
      XingHeader.XingHeaderOptions xingHeaderOptions = (XingHeader.XingHeaderOptions) XingHeader.ReadBigEndian(frame.RawData, offset1);
      int offset2 = offset1 + 4;
      if ((xingHeaderOptions & XingHeader.XingHeaderOptions.Frames) != (XingHeader.XingHeaderOptions) 0)
      {
        xingHeader.framesOffset = offset2;
        offset2 += 4;
      }
      if ((xingHeaderOptions & XingHeader.XingHeaderOptions.Bytes) != (XingHeader.XingHeaderOptions) 0)
      {
        xingHeader.bytesOffset = offset2;
        offset2 += 4;
      }
      if ((xingHeaderOptions & XingHeader.XingHeaderOptions.Toc) != (XingHeader.XingHeaderOptions) 0)
      {
        xingHeader.tocOffset = offset2;
        offset2 += 100;
      }
      if ((xingHeaderOptions & XingHeader.XingHeaderOptions.VbrScale) != (XingHeader.XingHeaderOptions) 0)
      {
        xingHeader.vbrScale = XingHeader.ReadBigEndian(frame.RawData, offset2);
        offset2 += 4;
      }
      xingHeader.endOffset = offset2;
      return xingHeader;
    }

    private XingHeader()
    {
    }

    public int Frames
    {
      get
      {
        return this.framesOffset == -1 ? -1 : XingHeader.ReadBigEndian(this.frame.RawData, this.framesOffset);
      }
      set
      {
        if (this.framesOffset == -1)
          throw new InvalidOperationException("Frames flag is not set");
        this.WriteBigEndian(this.frame.RawData, this.framesOffset, value);
      }
    }

    public int Bytes
    {
      get
      {
        return this.bytesOffset == -1 ? -1 : XingHeader.ReadBigEndian(this.frame.RawData, this.bytesOffset);
      }
      set
      {
        if (this.framesOffset == -1)
          throw new InvalidOperationException("Bytes flag is not set");
        this.WriteBigEndian(this.frame.RawData, this.bytesOffset, value);
      }
    }

    public int VbrScale => this.vbrScale;

    public Mp3Frame Mp3Frame => this.frame;

    [Flags]
    private enum XingHeaderOptions
    {
      Frames = 1,
      Bytes = 2,
      Toc = 4,
      VbrScale = 8,
    }
  }
}
