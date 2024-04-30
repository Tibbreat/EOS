// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IMMDevice
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("D666063F-1587-4E43-81F1-B948E807363F")]
  internal interface IMMDevice
  {
    int Activate(ref Guid id, ClsCtx clsCtx, IntPtr activationParams, [MarshalAs(UnmanagedType.IUnknown)] out object interfacePointer);

    int OpenPropertyStore(StorageAccessMode stgmAccess, out IPropertyStore properties);

    int GetId([MarshalAs(UnmanagedType.LPWStr)] out string id);

    int GetState(out DeviceState state);
  }
}
