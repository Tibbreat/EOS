// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.MediaObject
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Dmo
{
  public class MediaObject : IDisposable
  {
    private IMediaObject mediaObject;
    private int inputStreams;
    private int outputStreams;

    internal MediaObject(IMediaObject mediaObject)
    {
      this.mediaObject = mediaObject;
      mediaObject.GetStreamCount(out this.inputStreams, out this.outputStreams);
    }

    public int InputStreamCount => this.inputStreams;

    public int OutputStreamCount => this.outputStreams;

    public DmoMediaType? GetInputType(int inputStream, int inputTypeIndex)
    {
      try
      {
        DmoMediaType mediaType;
        if (this.mediaObject.GetInputType(inputStream, inputTypeIndex, out mediaType) == 0)
        {
          DmoInterop.MoFreeMediaType(ref mediaType);
          return new DmoMediaType?(mediaType);
        }
      }
      catch (COMException ex)
      {
        if (ex.GetHResult() != -2147220986)
          throw;
      }
      return new DmoMediaType?();
    }

    public DmoMediaType? GetOutputType(int outputStream, int outputTypeIndex)
    {
      try
      {
        DmoMediaType mediaType;
        if (this.mediaObject.GetOutputType(outputStream, outputTypeIndex, out mediaType) == 0)
        {
          DmoInterop.MoFreeMediaType(ref mediaType);
          return new DmoMediaType?(mediaType);
        }
      }
      catch (COMException ex)
      {
        if (ex.GetHResult() != -2147220986)
          throw;
      }
      return new DmoMediaType?();
    }

    public DmoMediaType GetOutputCurrentType(int outputStreamIndex)
    {
      DmoMediaType mediaType;
      int outputCurrentType = this.mediaObject.GetOutputCurrentType(outputStreamIndex, out mediaType);
      switch (outputCurrentType)
      {
        case -2147220989:
          throw new InvalidOperationException("Media type was not set.");
        case 0:
          DmoInterop.MoFreeMediaType(ref mediaType);
          return mediaType;
        default:
          throw Marshal.GetExceptionForHR(outputCurrentType);
      }
    }

    public IEnumerable<DmoMediaType> GetInputTypes(int inputStreamIndex)
    {
      DmoMediaType? mediaType;
      for (int typeIndex = 0; (mediaType = this.GetInputType(inputStreamIndex, typeIndex)).HasValue; ++typeIndex)
        yield return mediaType.Value;
    }

    public IEnumerable<DmoMediaType> GetOutputTypes(int outputStreamIndex)
    {
      DmoMediaType? mediaType;
      for (int typeIndex = 0; (mediaType = this.GetOutputType(outputStreamIndex, typeIndex)).HasValue; ++typeIndex)
        yield return mediaType.Value;
    }

    public bool SupportsInputType(int inputStreamIndex, DmoMediaType mediaType)
    {
      return this.SetInputType(inputStreamIndex, mediaType, DmoSetTypeFlags.DMO_SET_TYPEF_TEST_ONLY);
    }

    private bool SetInputType(int inputStreamIndex, DmoMediaType mediaType, DmoSetTypeFlags flags)
    {
      switch (this.mediaObject.SetInputType(inputStreamIndex, ref mediaType, flags))
      {
        case -2147220991:
          throw new ArgumentException("Invalid stream index");
        case 0:
          return true;
        default:
          return false;
      }
    }

    public void SetInputType(int inputStreamIndex, DmoMediaType mediaType)
    {
      if (!this.SetInputType(inputStreamIndex, mediaType, DmoSetTypeFlags.None))
        throw new ArgumentException("Media Type not supported");
    }

    public void SetInputWaveFormat(int inputStreamIndex, WaveFormat waveFormat)
    {
      DmoMediaType typeForWaveFormat = this.CreateDmoMediaTypeForWaveFormat(waveFormat);
      bool flag = this.SetInputType(inputStreamIndex, typeForWaveFormat, DmoSetTypeFlags.None);
      DmoInterop.MoFreeMediaType(ref typeForWaveFormat);
      if (!flag)
        throw new ArgumentException("Media Type not supported");
    }

    public bool SupportsInputWaveFormat(int inputStreamIndex, WaveFormat waveFormat)
    {
      DmoMediaType typeForWaveFormat = this.CreateDmoMediaTypeForWaveFormat(waveFormat);
      bool flag = this.SetInputType(inputStreamIndex, typeForWaveFormat, DmoSetTypeFlags.DMO_SET_TYPEF_TEST_ONLY);
      DmoInterop.MoFreeMediaType(ref typeForWaveFormat);
      return flag;
    }

    private DmoMediaType CreateDmoMediaTypeForWaveFormat(WaveFormat waveFormat)
    {
      DmoMediaType mediaType = new DmoMediaType();
      int formatBlockBytes = Marshal.SizeOf((object) waveFormat);
      DmoInterop.MoInitMediaType(ref mediaType, formatBlockBytes);
      mediaType.SetWaveFormat(waveFormat);
      return mediaType;
    }

    public bool SupportsOutputType(int outputStreamIndex, DmoMediaType mediaType)
    {
      return this.SetOutputType(outputStreamIndex, mediaType, DmoSetTypeFlags.DMO_SET_TYPEF_TEST_ONLY);
    }

    public bool SupportsOutputWaveFormat(int outputStreamIndex, WaveFormat waveFormat)
    {
      DmoMediaType typeForWaveFormat = this.CreateDmoMediaTypeForWaveFormat(waveFormat);
      bool flag = this.SetOutputType(outputStreamIndex, typeForWaveFormat, DmoSetTypeFlags.DMO_SET_TYPEF_TEST_ONLY);
      DmoInterop.MoFreeMediaType(ref typeForWaveFormat);
      return flag;
    }

    private bool SetOutputType(
      int outputStreamIndex,
      DmoMediaType mediaType,
      DmoSetTypeFlags flags)
    {
      int errorCode = this.mediaObject.SetOutputType(outputStreamIndex, ref mediaType, flags);
      switch (errorCode)
      {
        case -2147220987:
          return false;
        case 0:
          return true;
        default:
          throw Marshal.GetExceptionForHR(errorCode);
      }
    }

    public void SetOutputType(int outputStreamIndex, DmoMediaType mediaType)
    {
      if (!this.SetOutputType(outputStreamIndex, mediaType, DmoSetTypeFlags.None))
        throw new ArgumentException("Media Type not supported");
    }

    public void SetOutputWaveFormat(int outputStreamIndex, WaveFormat waveFormat)
    {
      DmoMediaType typeForWaveFormat = this.CreateDmoMediaTypeForWaveFormat(waveFormat);
      bool flag = this.SetOutputType(outputStreamIndex, typeForWaveFormat, DmoSetTypeFlags.None);
      DmoInterop.MoFreeMediaType(ref typeForWaveFormat);
      if (!flag)
        throw new ArgumentException("Media Type not supported");
    }

    public MediaObjectSizeInfo GetInputSizeInfo(int inputStreamIndex)
    {
      int size;
      int maxLookahead;
      int alignment;
      Marshal.ThrowExceptionForHR(this.mediaObject.GetInputSizeInfo(inputStreamIndex, out size, out maxLookahead, out alignment));
      return new MediaObjectSizeInfo(size, maxLookahead, alignment);
    }

    public MediaObjectSizeInfo GetOutputSizeInfo(int outputStreamIndex)
    {
      int size;
      int alignment;
      Marshal.ThrowExceptionForHR(this.mediaObject.GetOutputSizeInfo(outputStreamIndex, out size, out alignment));
      return new MediaObjectSizeInfo(size, 0, alignment);
    }

    public void ProcessInput(
      int inputStreamIndex,
      IMediaBuffer mediaBuffer,
      DmoInputDataBufferFlags flags,
      long timestamp,
      long duration)
    {
      Marshal.ThrowExceptionForHR(this.mediaObject.ProcessInput(inputStreamIndex, mediaBuffer, flags, timestamp, duration));
    }

    public void ProcessOutput(
      DmoProcessOutputFlags flags,
      int outputBufferCount,
      DmoOutputDataBuffer[] outputBuffers)
    {
      Marshal.ThrowExceptionForHR(this.mediaObject.ProcessOutput(flags, outputBufferCount, outputBuffers, out int _));
    }

    public void AllocateStreamingResources()
    {
      Marshal.ThrowExceptionForHR(this.mediaObject.AllocateStreamingResources());
    }

    public void FreeStreamingResources()
    {
      Marshal.ThrowExceptionForHR(this.mediaObject.FreeStreamingResources());
    }

    public long GetInputMaxLatency(int inputStreamIndex)
    {
      long referenceTimeMaxLatency;
      Marshal.ThrowExceptionForHR(this.mediaObject.GetInputMaxLatency(inputStreamIndex, out referenceTimeMaxLatency));
      return referenceTimeMaxLatency;
    }

    public void Flush() => Marshal.ThrowExceptionForHR(this.mediaObject.Flush());

    public void Discontinuity(int inputStreamIndex)
    {
      Marshal.ThrowExceptionForHR(this.mediaObject.Discontinuity(inputStreamIndex));
    }

    public bool IsAcceptingData(int inputStreamIndex)
    {
      DmoInputStatusFlags flags;
      Marshal.ThrowExceptionForHR(this.mediaObject.GetInputStatus(inputStreamIndex, out flags));
      return (flags & DmoInputStatusFlags.DMO_INPUT_STATUSF_ACCEPT_DATA) == DmoInputStatusFlags.DMO_INPUT_STATUSF_ACCEPT_DATA;
    }

    public void Dispose()
    {
      if (this.mediaObject == null)
        return;
      Marshal.ReleaseComObject((object) this.mediaObject);
      this.mediaObject = (IMediaObject) null;
    }
  }
}
