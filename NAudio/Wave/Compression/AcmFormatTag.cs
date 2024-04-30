// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Compression.AcmFormatTag
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Wave.Compression
{
  public class AcmFormatTag
  {
    private AcmFormatTagDetails formatTagDetails;

    internal AcmFormatTag(AcmFormatTagDetails formatTagDetails)
    {
      this.formatTagDetails = formatTagDetails;
    }

    public int FormatTagIndex => this.formatTagDetails.formatTagIndex;

    public WaveFormatEncoding FormatTag => (WaveFormatEncoding) this.formatTagDetails.formatTag;

    public int FormatSize => this.formatTagDetails.formatSize;

    public AcmDriverDetailsSupportFlags SupportFlags => this.formatTagDetails.supportFlags;

    public int StandardFormatsCount => this.formatTagDetails.standardFormatsCount;

    public string FormatDescription => this.formatTagDetails.formatDescription;
  }
}
