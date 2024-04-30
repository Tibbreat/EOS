// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IMMDeviceEnumerator
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IMMDeviceEnumerator
  {
    int EnumAudioEndpoints(
      DataFlow dataFlow,
      DeviceState stateMask,
      out IMMDeviceCollection devices);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDefaultAudioEndpoint(DataFlow dataFlow, Role role, out IMMDevice endpoint);

    int GetDevice(string id, out IMMDevice deviceName);

    int RegisterEndpointNotificationCallback(IMMNotificationClient client);

    int UnregisterEndpointNotificationCallback(IMMNotificationClient client);
  }
}
