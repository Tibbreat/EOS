// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.IMFSinkWriter
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.MediaFoundation
{
  [Guid("3137f1cd-fe5e-4805-a5d8-fb477448cb3d")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IMFSinkWriter
  {
    void AddStream([MarshalAs(UnmanagedType.Interface), In] IMFMediaType pTargetMediaType, out int pdwStreamIndex);

    void SetInputMediaType(
      [In] int dwStreamIndex,
      [MarshalAs(UnmanagedType.Interface), In] IMFMediaType pInputMediaType,
      [MarshalAs(UnmanagedType.Interface), In] IMFAttributes pEncodingParameters);

    void BeginWriting();

    void WriteSample([In] int dwStreamIndex, [MarshalAs(UnmanagedType.Interface), In] IMFSample pSample);

    void SendStreamTick([In] int dwStreamIndex, [In] long llTimestamp);

    void PlaceMarker([In] int dwStreamIndex, [In] IntPtr pvContext);

    void NotifyEndOfSegment([In] int dwStreamIndex);

    void Flush([In] int dwStreamIndex);

    void DoFinalize();

    void GetServiceForStream(
      [In] int dwStreamIndex,
      [In] ref Guid guidService,
      [In] ref Guid riid,
      out IntPtr ppvObject);

    void GetStatistics([In] int dwStreamIndex, [In, Out] MF_SINK_WRITER_STATISTICS pStats);
  }
}
