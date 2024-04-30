// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IAudioStreamVolume
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("93014887-242D-4068-8A15-CF5E93B90FE3")]
  internal interface IAudioStreamVolume
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetChannelCount(out uint dwCount);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetChannelVolume([In] uint dwIndex, [In] float fLevel);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetChannelVolume([In] uint dwIndex, out float fLevel);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetAllVoumes([In] uint dwCount, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4), In] float[] fVolumes);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetAllVolumes([In] uint dwCount, [MarshalAs(UnmanagedType.LPArray)] float[] pfVolumes);
  }
}
