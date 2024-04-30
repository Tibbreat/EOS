// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.DmoMediaType
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Wave;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Dmo
{
  public struct DmoMediaType
  {
    private Guid majortype;
    private Guid subtype;
    private bool bFixedSizeSamples;
    private bool bTemporalCompression;
    private int lSampleSize;
    private Guid formattype;
    private IntPtr pUnk;
    private int cbFormat;
    private IntPtr pbFormat;

    public Guid MajorType => this.majortype;

    public string MajorTypeName => MediaTypes.GetMediaTypeName(this.majortype);

    public Guid SubType => this.subtype;

    public string SubTypeName
    {
      get
      {
        return this.majortype == MediaTypes.MEDIATYPE_Audio ? AudioMediaSubtypes.GetAudioSubtypeName(this.subtype) : this.subtype.ToString();
      }
    }

    public bool FixedSizeSamples => this.bFixedSizeSamples;

    public int SampleSize => this.lSampleSize;

    public Guid FormatType => this.formattype;

    public string FormatTypeName
    {
      get
      {
        if (this.formattype == DmoMediaTypeGuids.FORMAT_None)
          return "None";
        if (this.formattype == Guid.Empty)
          return "Null";
        return this.formattype == DmoMediaTypeGuids.FORMAT_WaveFormatEx ? "WaveFormatEx" : this.FormatType.ToString();
      }
    }

    public WaveFormat GetWaveFormat()
    {
      if (this.formattype == DmoMediaTypeGuids.FORMAT_WaveFormatEx)
        return WaveFormat.MarshalFromPtr(this.pbFormat);
      throw new InvalidOperationException("Not a WaveFormat type");
    }

    public void SetWaveFormat(WaveFormat waveFormat)
    {
      this.majortype = MediaTypes.MEDIATYPE_Audio;
      if (waveFormat is WaveFormatExtensible formatExtensible)
      {
        this.subtype = formatExtensible.SubFormat;
      }
      else
      {
        switch (waveFormat.Encoding)
        {
          case WaveFormatEncoding.Pcm:
            this.subtype = AudioMediaSubtypes.MEDIASUBTYPE_PCM;
            break;
          case WaveFormatEncoding.IeeeFloat:
            this.subtype = AudioMediaSubtypes.MEDIASUBTYPE_IEEE_FLOAT;
            break;
          case WaveFormatEncoding.MpegLayer3:
            this.subtype = AudioMediaSubtypes.WMMEDIASUBTYPE_MP3;
            break;
          default:
            throw new ArgumentException(string.Format("Not a supported encoding {0}", (object) waveFormat.Encoding));
        }
      }
      this.bFixedSizeSamples = this.SubType == AudioMediaSubtypes.MEDIASUBTYPE_PCM || this.SubType == AudioMediaSubtypes.MEDIASUBTYPE_IEEE_FLOAT;
      this.formattype = DmoMediaTypeGuids.FORMAT_WaveFormatEx;
      if (this.cbFormat < Marshal.SizeOf((object) waveFormat))
        throw new InvalidOperationException("Not enough memory assigned for a WaveFormat structure");
      Marshal.StructureToPtr((object) waveFormat, this.pbFormat, false);
    }
  }
}
