// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.MediaFoundationInterop
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Wave;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.MediaFoundation
{
  public static class MediaFoundationInterop
  {
    public const int MF_SOURCE_READER_ALL_STREAMS = -2;
    public const int MF_SOURCE_READER_FIRST_AUDIO_STREAM = -3;
    public const int MF_SOURCE_READER_FIRST_VIDEO_STREAM = -4;
    public const int MF_SOURCE_READER_MEDIASOURCE = -1;
    public const int MF_SDK_VERSION = 2;
    public const int MF_API_VERSION = 112;
    public const int MF_VERSION = 131184;

    [DllImport("mfplat.dll", PreserveSig = false)]
    public static extern void MFStartup(int version, int dwFlags = 0);

    [DllImport("mfplat.dll", PreserveSig = false)]
    public static extern void MFShutdown();

    [DllImport("mfplat.dll", PreserveSig = false)]
    internal static extern void MFCreateMediaType(out IMFMediaType ppMFType);

    [DllImport("mfplat.dll", PreserveSig = false)]
    internal static extern void MFInitMediaTypeFromWaveFormatEx(
      [In] IMFMediaType pMFType,
      [In] WaveFormat pWaveFormat,
      [In] int cbBufSize);

    [DllImport("mfplat.dll", PreserveSig = false)]
    internal static extern void MFCreateWaveFormatExFromMFMediaType(
      IMFMediaType pMFType,
      ref IntPtr ppWF,
      ref int pcbSize,
      int flags = 0);

    [DllImport("mfreadwrite.dll", PreserveSig = false)]
    public static extern void MFCreateSourceReaderFromURL(
      [MarshalAs(UnmanagedType.LPWStr), In] string pwszURL,
      [In] IMFAttributes pAttributes,
      [MarshalAs(UnmanagedType.Interface)] out IMFSourceReader ppSourceReader);

    [DllImport("mfreadwrite.dll", PreserveSig = false)]
    public static extern void MFCreateSourceReaderFromByteStream(
      [In] IMFByteStream pByteStream,
      [In] IMFAttributes pAttributes,
      [MarshalAs(UnmanagedType.Interface)] out IMFSourceReader ppSourceReader);

    [DllImport("mfreadwrite.dll", PreserveSig = false)]
    public static extern void MFCreateSinkWriterFromURL(
      [MarshalAs(UnmanagedType.LPWStr), In] string pwszOutputURL,
      [In] IMFByteStream pByteStream,
      [In] IMFAttributes pAttributes,
      out IMFSinkWriter ppSinkWriter);

    [DllImport("mfplat.dll", PreserveSig = false)]
    public static extern void MFCreateMFByteStreamOnStreamEx(
      [MarshalAs(UnmanagedType.IUnknown)] object punkStream,
      out IMFByteStream ppByteStream);

    [DllImport("mfplat.dll", PreserveSig = false)]
    public static extern void MFTEnumEx(
      [In] Guid guidCategory,
      [In] _MFT_ENUM_FLAG flags,
      [In] MFT_REGISTER_TYPE_INFO pInputType,
      [In] MFT_REGISTER_TYPE_INFO pOutputType,
      out IntPtr pppMFTActivate,
      out int pcMFTActivate);

    [DllImport("mfplat.dll", PreserveSig = false)]
    internal static extern void MFCreateSample(out IMFSample ppIMFSample);

    [DllImport("mfplat.dll", PreserveSig = false)]
    internal static extern void MFCreateMemoryBuffer(int cbMaxLength, out IMFMediaBuffer ppBuffer);

    [DllImport("mfplat.dll", PreserveSig = false)]
    internal static extern void MFCreateAttributes(
      [MarshalAs(UnmanagedType.Interface)] out IMFAttributes ppMFAttributes,
      [In] int cInitialSize);

    [DllImport("mf.dll", PreserveSig = false)]
    public static extern void MFTranscodeGetAudioOutputAvailableTypes(
      [MarshalAs(UnmanagedType.LPStruct), In] Guid guidSubType,
      [In] _MFT_ENUM_FLAG dwMFTFlags,
      [In] IMFAttributes pCodecConfig,
      [MarshalAs(UnmanagedType.Interface)] out IMFCollection ppAvailableTypes);
  }
}
