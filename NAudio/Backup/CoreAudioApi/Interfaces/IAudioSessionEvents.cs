// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IAudioSessionEvents
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [Guid("24918ACC-64B3-37C1-8CA9-74A66E9957A8")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IAudioSessionEvents
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr), In] string displayName, [In] ref Guid eventContext);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnIconPathChanged([MarshalAs(UnmanagedType.LPWStr), In] string iconPath, [In] ref Guid eventContext);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnSimpleVolumeChanged([MarshalAs(UnmanagedType.R4), In] float volume, [MarshalAs(UnmanagedType.Bool), In] bool isMuted, [In] ref Guid eventContext);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnChannelVolumeChanged(
      [MarshalAs(UnmanagedType.U4), In] uint channelCount,
      [MarshalAs(UnmanagedType.SysInt), In] IntPtr newVolumes,
      [MarshalAs(UnmanagedType.U4), In] uint channelIndex,
      [In] ref Guid eventContext);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnGroupingParamChanged([In] ref Guid groupingId, [In] ref Guid eventContext);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnStateChanged([In] AudioSessionState state);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnSessionDisconnected([In] AudioSessionDisconnectReason disconnectReason);
  }
}
