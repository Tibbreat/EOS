// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.IMFSourceReader
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.MediaFoundation
{
  [Guid("70ae66f2-c809-4e4f-8915-bdcb406b7993")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IMFSourceReader
  {
    void GetStreamSelection([In] int dwStreamIndex, [MarshalAs(UnmanagedType.Bool)] out bool pSelected);

    void SetStreamSelection([In] int dwStreamIndex, [MarshalAs(UnmanagedType.Bool), In] bool pSelected);

    void GetNativeMediaType([In] int dwStreamIndex, [In] int dwMediaTypeIndex, out IMFMediaType ppMediaType);

    void GetCurrentMediaType([In] int dwStreamIndex, out IMFMediaType ppMediaType);

    void SetCurrentMediaType([In] int dwStreamIndex, IntPtr pdwReserved, [In] IMFMediaType pMediaType);

    void SetCurrentPosition([MarshalAs(UnmanagedType.LPStruct), In] Guid guidTimeFormat, [In] ref PropVariant varPosition);

    void ReadSample(
      [In] int dwStreamIndex,
      [In] int dwControlFlags,
      out int pdwActualStreamIndex,
      out MF_SOURCE_READER_FLAG pdwStreamFlags,
      out ulong pllTimestamp,
      out IMFSample ppSample);

    void Flush([In] int dwStreamIndex);

    void GetServiceForStream([In] int dwStreamIndex, [MarshalAs(UnmanagedType.LPStruct), In] Guid guidService, [MarshalAs(UnmanagedType.LPStruct), In] Guid riid, out IntPtr ppvObject);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetPresentationAttribute(
      [In] int dwStreamIndex,
      [MarshalAs(UnmanagedType.LPStruct), In] Guid guidAttribute,
      out PropVariant pvarAttribute);
  }
}
