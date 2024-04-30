// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IAudioSessionControl
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [Guid("F4B1A599-7266-4319-A8CA-E70ACB11E8CD")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IAudioSessionControl
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetState(out AudioSessionState state);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDisplayName([MarshalAs(UnmanagedType.LPWStr)] out string displayName);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetDisplayName([MarshalAs(UnmanagedType.LPWStr), In] string displayName, [MarshalAs(UnmanagedType.LPStruct), In] Guid eventContext);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetIconPath([MarshalAs(UnmanagedType.LPWStr)] out string iconPath);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetIconPath([MarshalAs(UnmanagedType.LPWStr), In] string iconPath, [MarshalAs(UnmanagedType.LPStruct), In] Guid eventContext);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetGroupingParam(out Guid groupingId);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetGroupingParam([MarshalAs(UnmanagedType.LPStruct), In] Guid groupingId, [MarshalAs(UnmanagedType.LPStruct), In] Guid eventContext);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RegisterAudioSessionNotification([In] IAudioSessionEvents client);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int UnregisterAudioSessionNotification([In] IAudioSessionEvents client);
  }
}
