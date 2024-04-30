// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IMMDeviceCollection
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E")]
  internal interface IMMDeviceCollection
  {
    int GetCount(out int numDevices);

    int Item(int deviceNumber, out IMMDevice device);
  }
}
