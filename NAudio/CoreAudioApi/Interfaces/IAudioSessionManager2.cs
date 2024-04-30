// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IAudioSessionManager2
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IAudioSessionManager2 : IAudioSessionManager
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    new int GetAudioSessionControl(
      [MarshalAs(UnmanagedType.LPStruct), In, Optional] Guid sessionId,
      [MarshalAs(UnmanagedType.U4), In] uint streamFlags,
      [MarshalAs(UnmanagedType.Interface)] out IAudioSessionControl sessionControl);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    new int GetSimpleAudioVolume(
      [MarshalAs(UnmanagedType.LPStruct), In, Optional] Guid sessionId,
      [MarshalAs(UnmanagedType.U4), In] uint streamFlags,
      [MarshalAs(UnmanagedType.Interface)] out ISimpleAudioVolume audioVolume);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetSessionEnumerator(out IAudioSessionEnumerator sessionEnum);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RegisterSessionNotification(IAudioSessionNotification sessionNotification);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int UnregisterSessionNotification(IAudioSessionNotification sessionNotification);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RegisterDuckNotification(
      string sessionID,
      IAudioSessionNotification audioVolumeDuckNotification);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int UnregisterDuckNotification(IntPtr audioVolumeDuckNotification);
  }
}
