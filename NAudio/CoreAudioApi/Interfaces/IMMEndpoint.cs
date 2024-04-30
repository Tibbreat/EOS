// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IMMEndpoint
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [Guid("1BE09788-6894-4089-8586-9A2A6C265AC5")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IMMEndpoint
  {
    int GetDataFlow(out DataFlow dataFlow);
  }
}
