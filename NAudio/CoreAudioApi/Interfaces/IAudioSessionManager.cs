// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IAudioSessionManager
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [Guid("BFA971F1-4D5E-40BB-935E-967039BFBEE4")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IAudioSessionManager
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetAudioSessionControl(
      [MarshalAs(UnmanagedType.LPStruct), In, Optional] Guid sessionId,
      [MarshalAs(UnmanagedType.U4), In] uint streamFlags,
      [MarshalAs(UnmanagedType.Interface)] out IAudioSessionControl sessionControl);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetSimpleAudioVolume([MarshalAs(UnmanagedType.LPStruct), In, Optional] Guid sessionId, [MarshalAs(UnmanagedType.U4), In] uint streamFlags, [MarshalAs(UnmanagedType.Interface)] out ISimpleAudioVolume audioVolume);
  }
}
