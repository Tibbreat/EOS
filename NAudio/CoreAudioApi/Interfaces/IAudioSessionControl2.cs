// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IAudioSessionControl2
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
  [Guid("bfb7ff88-7239-4fc9-8fa2-07c950be9c6d")]
  public interface IAudioSessionControl2 : IAudioSessionControl
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    new int GetState(out AudioSessionState state);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    new int GetDisplayName([MarshalAs(UnmanagedType.LPWStr)] out string displayName);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    new int SetDisplayName([MarshalAs(UnmanagedType.LPWStr), In] string displayName, [MarshalAs(UnmanagedType.LPStruct), In] Guid eventContext);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    new int GetIconPath([MarshalAs(UnmanagedType.LPWStr)] out string iconPath);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    new int SetIconPath([MarshalAs(UnmanagedType.LPWStr), In] string iconPath, [MarshalAs(UnmanagedType.LPStruct), In] Guid eventContext);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    new int GetGroupingParam(out Guid groupingId);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    new int SetGroupingParam([MarshalAs(UnmanagedType.LPStruct), In] Guid groupingId, [MarshalAs(UnmanagedType.LPStruct), In] Guid eventContext);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    new int RegisterAudioSessionNotification([In] IAudioSessionEvents client);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    new int UnregisterAudioSessionNotification([In] IAudioSessionEvents client);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetSessionIdentifier([MarshalAs(UnmanagedType.LPWStr)] out string retVal);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetSessionInstanceIdentifier([MarshalAs(UnmanagedType.LPWStr)] out string retVal);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetProcessId(out uint retVal);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsSystemSoundsSession();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetDuckingPreference(bool optOut);
  }
}
