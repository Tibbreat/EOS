// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IAudioClient
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Wave;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [Guid("1CB9AD4C-DBFA-4c32-B178-C2F568A703B2")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IAudioClient
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Initialize(
      AudioClientShareMode shareMode,
      AudioClientStreamFlags StreamFlags,
      long hnsBufferDuration,
      long hnsPeriodicity,
      [In] WaveFormat pFormat,
      [In] ref Guid AudioSessionGuid);

    int GetBufferSize(out uint bufferSize);

    [return: MarshalAs(UnmanagedType.I8)]
    long GetStreamLatency();

    int GetCurrentPadding(out int currentPadding);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsFormatSupported(
      AudioClientShareMode shareMode,
      [In] WaveFormat pFormat,
      [MarshalAs(UnmanagedType.LPStruct)] out WaveFormatExtensible closestMatchFormat);

    int GetMixFormat(out IntPtr deviceFormatPointer);

    int GetDevicePeriod(out long defaultDevicePeriod, out long minimumDevicePeriod);

    int Start();

    int Stop();

    int Reset();

    int SetEventHandle(IntPtr eventHandle);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetService([MarshalAs(UnmanagedType.LPStruct), In] Guid interfaceId, [MarshalAs(UnmanagedType.IUnknown)] out object interfacePointer);
  }
}
