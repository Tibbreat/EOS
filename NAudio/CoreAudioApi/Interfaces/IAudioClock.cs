// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IAudioClock
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("CD63314F-3FBA-4a1b-812C-EF96358728E7")]
  internal interface IAudioClock
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetFrequency(out ulong frequency);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetPosition(out ulong devicePosition, out ulong qpcPosition);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCharacteristics(out uint characteristics);
  }
}
