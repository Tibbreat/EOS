// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.MediaType
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;
using NAudio.Wave;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.MediaFoundation
{
  public class MediaType
  {
    private readonly IMFMediaType mediaType;

    public MediaType(IMFMediaType mediaType) => this.mediaType = mediaType;

    public MediaType() => this.mediaType = MediaFoundationApi.CreateMediaType();

    public MediaType(WaveFormat waveFormat)
    {
      this.mediaType = MediaFoundationApi.CreateMediaTypeFromWaveFormat(waveFormat);
    }

    private int GetUInt32(Guid key)
    {
      int punValue;
      this.mediaType.GetUINT32(key, out punValue);
      return punValue;
    }

    private Guid GetGuid(Guid key)
    {
      Guid pguidValue;
      this.mediaType.GetGUID(key, out pguidValue);
      return pguidValue;
    }

    public int TryGetUInt32(Guid key, int defaultValue = -1)
    {
      int punValue = defaultValue;
      try
      {
        this.mediaType.GetUINT32(key, out punValue);
      }
      catch (COMException ex)
      {
        if (ex.GetHResult() != -1072875802)
        {
          if (ex.GetHResult() == -1072875843)
            throw new ArgumentException("Not a UINT32 parameter");
          throw;
        }
      }
      return punValue;
    }

    public int SampleRate
    {
      get => this.GetUInt32(MediaFoundationAttributes.MF_MT_AUDIO_SAMPLES_PER_SECOND);
      set
      {
        this.mediaType.SetUINT32(MediaFoundationAttributes.MF_MT_AUDIO_SAMPLES_PER_SECOND, value);
      }
    }

    public int ChannelCount
    {
      get => this.GetUInt32(MediaFoundationAttributes.MF_MT_AUDIO_NUM_CHANNELS);
      set => this.mediaType.SetUINT32(MediaFoundationAttributes.MF_MT_AUDIO_NUM_CHANNELS, value);
    }

    public int BitsPerSample
    {
      get => this.GetUInt32(MediaFoundationAttributes.MF_MT_AUDIO_BITS_PER_SAMPLE);
      set => this.mediaType.SetUINT32(MediaFoundationAttributes.MF_MT_AUDIO_BITS_PER_SAMPLE, value);
    }

    public int AverageBytesPerSecond
    {
      get => this.GetUInt32(MediaFoundationAttributes.MF_MT_AUDIO_AVG_BYTES_PER_SECOND);
    }

    public Guid SubType
    {
      get => this.GetGuid(MediaFoundationAttributes.MF_MT_SUBTYPE);
      set => this.mediaType.SetGUID(MediaFoundationAttributes.MF_MT_SUBTYPE, value);
    }

    public Guid MajorType
    {
      get => this.GetGuid(MediaFoundationAttributes.MF_MT_MAJOR_TYPE);
      set => this.mediaType.SetGUID(MediaFoundationAttributes.MF_MT_MAJOR_TYPE, value);
    }

    public IMFMediaType MediaFoundationObject => this.mediaType;
  }
}
