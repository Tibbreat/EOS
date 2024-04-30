// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.ISimpleAudioVolume
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("87CE5498-68D6-44E5-9215-6DA47EF883D8")]
  internal interface ISimpleAudioVolume
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetMasterVolume([MarshalAs(UnmanagedType.R4), In] float levelNorm, [MarshalAs(UnmanagedType.LPStruct), In] Guid eventContext);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetMasterVolume([MarshalAs(UnmanagedType.R4)] out float levelNorm);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetMute([MarshalAs(UnmanagedType.Bool), In] bool isMuted, [MarshalAs(UnmanagedType.LPStruct), In] Guid eventContext);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetMute([MarshalAs(UnmanagedType.Bool)] out bool isMuted);
  }
}
