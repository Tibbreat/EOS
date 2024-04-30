// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.MediaFoundationApi
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.MediaFoundation
{
  public static class MediaFoundationApi
  {
    private static bool initialized;

    public static void Startup()
    {
      if (MediaFoundationApi.initialized)
        return;
      int num = 2;
      OperatingSystem osVersion = Environment.OSVersion;
      if (osVersion.Version.Major == 6 && osVersion.Version.Minor == 0)
        num = 1;
      MediaFoundationInterop.MFStartup(num << 16 | 112);
      MediaFoundationApi.initialized = true;
    }

    public static IEnumerable<IMFActivate> EnumerateTransforms(Guid category)
    {
      IntPtr interfacesPointer;
      int interfaceCount;
      MediaFoundationInterop.MFTEnumEx(category, _MFT_ENUM_FLAG.MFT_ENUM_FLAG_ALL, (MFT_REGISTER_TYPE_INFO) null, (MFT_REGISTER_TYPE_INFO) null, out interfacesPointer, out interfaceCount);
      IMFActivate[] interfaces = new IMFActivate[interfaceCount];
      for (int index = 0; index < interfaceCount; ++index)
      {
        IntPtr pUnk = Marshal.ReadIntPtr(new IntPtr(interfacesPointer.ToInt64() + (long) (index * Marshal.SizeOf((object) interfacesPointer))));
        interfaces[index] = (IMFActivate) Marshal.GetObjectForIUnknown(pUnk);
      }
      foreach (IMFActivate mfActivate in interfaces)
        yield return mfActivate;
      Marshal.FreeCoTaskMem(interfacesPointer);
    }

    public static void Shutdown()
    {
      if (!MediaFoundationApi.initialized)
        return;
      MediaFoundationInterop.MFShutdown();
      MediaFoundationApi.initialized = false;
    }

    public static IMFMediaType CreateMediaType()
    {
      IMFMediaType ppMFType;
      MediaFoundationInterop.MFCreateMediaType(out ppMFType);
      return ppMFType;
    }

    public static IMFMediaType CreateMediaTypeFromWaveFormat(WaveFormat waveFormat)
    {
      IMFMediaType mediaType = MediaFoundationApi.CreateMediaType();
      try
      {
        MediaFoundationInterop.MFInitMediaTypeFromWaveFormatEx(mediaType, waveFormat, Marshal.SizeOf((object) waveFormat));
      }
      catch (Exception ex)
      {
        Marshal.ReleaseComObject((object) mediaType);
        throw;
      }
      return mediaType;
    }

    public static IMFMediaBuffer CreateMemoryBuffer(int bufferSize)
    {
      IMFMediaBuffer ppBuffer;
      MediaFoundationInterop.MFCreateMemoryBuffer(bufferSize, out ppBuffer);
      return ppBuffer;
    }

    public static IMFSample CreateSample()
    {
      IMFSample ppIMFSample;
      MediaFoundationInterop.MFCreateSample(out ppIMFSample);
      return ppIMFSample;
    }

    public static IMFAttributes CreateAttributes(int initialSize)
    {
      IMFAttributes ppMFAttributes;
      MediaFoundationInterop.MFCreateAttributes(out ppMFAttributes, initialSize);
      return ppMFAttributes;
    }

    public static IMFByteStream CreateByteStream(object stream)
    {
      IMFByteStream ppByteStream;
      MediaFoundationInterop.MFCreateMFByteStreamOnStreamEx(stream, out ppByteStream);
      return ppByteStream;
    }

    public static IMFSourceReader CreateSourceReaderFromByteStream(IMFByteStream byteStream)
    {
      IMFSourceReader ppSourceReader;
      MediaFoundationInterop.MFCreateSourceReaderFromByteStream(byteStream, (IMFAttributes) null, out ppSourceReader);
      return ppSourceReader;
    }
  }
}
