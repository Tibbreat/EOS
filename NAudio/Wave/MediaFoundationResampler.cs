// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.MediaFoundationResampler
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Dmo;
using NAudio.MediaFoundation;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave
{
  public class MediaFoundationResampler : MediaFoundationTransform
  {
    private int resamplerQuality;
    private static readonly Guid ResamplerClsid = new Guid("f447b69e-1884-4a7e-8055-346f74d6edb3");
    private static readonly Guid IMFTransformIid = new Guid("bf94c121-5b05-4e6f-8000-ba598961414d");
    private IMFActivate activate;

    private static bool IsPcmOrIeeeFloat(WaveFormat waveFormat)
    {
      WaveFormatExtensible formatExtensible = waveFormat as WaveFormatExtensible;
      if (waveFormat.Encoding == WaveFormatEncoding.Pcm || waveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
        return true;
      if (formatExtensible == null)
        return false;
      return formatExtensible.SubFormat == AudioSubtypes.MFAudioFormat_PCM || formatExtensible.SubFormat == AudioSubtypes.MFAudioFormat_Float;
    }

    public MediaFoundationResampler(IWaveProvider sourceProvider, WaveFormat outputFormat)
      : base(sourceProvider, outputFormat)
    {
      if (!MediaFoundationResampler.IsPcmOrIeeeFloat(sourceProvider.WaveFormat))
        throw new ArgumentException("Input must be PCM or IEEE float", nameof (sourceProvider));
      if (!MediaFoundationResampler.IsPcmOrIeeeFloat(outputFormat))
        throw new ArgumentException("Output must be PCM or IEEE float", nameof (outputFormat));
      MediaFoundationApi.Startup();
      this.ResamplerQuality = 60;
      this.FreeComObject(this.CreateResamplerComObject());
    }

    private void FreeComObject(object comObject)
    {
      if (this.activate != null)
        this.activate.ShutdownObject();
      Marshal.ReleaseComObject(comObject);
    }

    private object CreateResamplerComObject() => (object) new ResamplerMediaComObject();

    private object CreateResamplerComObjectUsingActivator()
    {
      foreach (IMFActivate enumerateTransform in MediaFoundationApi.EnumerateTransforms(MediaFoundationTransformCategories.AudioEffect))
      {
        Guid pguidValue;
        enumerateTransform.GetGUID(MediaFoundationAttributes.MFT_TRANSFORM_CLSID_Attribute, out pguidValue);
        if (pguidValue.Equals(MediaFoundationResampler.ResamplerClsid))
        {
          object ppv;
          enumerateTransform.ActivateObject(MediaFoundationResampler.IMFTransformIid, out ppv);
          this.activate = enumerateTransform;
          return ppv;
        }
      }
      return (object) null;
    }

    public MediaFoundationResampler(IWaveProvider sourceProvider, int outputSampleRate)
      : this(sourceProvider, MediaFoundationResampler.CreateOutputFormat(sourceProvider.WaveFormat, outputSampleRate))
    {
    }

    protected override IMFTransform CreateTransform()
    {
      object resamplerComObject = this.CreateResamplerComObject();
      IMFTransform transform = (IMFTransform) resamplerComObject;
      IMFMediaType typeFromWaveFormat1 = MediaFoundationApi.CreateMediaTypeFromWaveFormat(this.sourceProvider.WaveFormat);
      transform.SetInputType(0, typeFromWaveFormat1, _MFT_SET_TYPE_FLAGS.None);
      Marshal.ReleaseComObject((object) typeFromWaveFormat1);
      IMFMediaType typeFromWaveFormat2 = MediaFoundationApi.CreateMediaTypeFromWaveFormat(this.outputWaveFormat);
      transform.SetOutputType(0, typeFromWaveFormat2, _MFT_SET_TYPE_FLAGS.None);
      Marshal.ReleaseComObject((object) typeFromWaveFormat2);
      ((IWMResamplerProps) resamplerComObject).SetHalfFilterLength(this.ResamplerQuality);
      return transform;
    }

    public int ResamplerQuality
    {
      get => this.resamplerQuality;
      set
      {
        this.resamplerQuality = value >= 1 && value <= 60 ? value : throw new ArgumentOutOfRangeException("Resampler Quality must be between 1 and 60");
      }
    }

    private static WaveFormat CreateOutputFormat(WaveFormat inputFormat, int outputSampleRate)
    {
      if (inputFormat.Encoding == WaveFormatEncoding.Pcm)
        return new WaveFormat(outputSampleRate, inputFormat.BitsPerSample, inputFormat.Channels);
      if (inputFormat.Encoding == WaveFormatEncoding.IeeeFloat)
        return WaveFormat.CreateIeeeFloatWaveFormat(outputSampleRate, inputFormat.Channels);
      throw new ArgumentException("Can only resample PCM or IEEE float");
    }

    protected override void Dispose(bool disposing)
    {
      if (this.activate != null)
      {
        this.activate.ShutdownObject();
        this.activate = (IMFActivate) null;
      }
      base.Dispose(disposing);
    }
  }
}
