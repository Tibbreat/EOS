// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.IEnumDmo
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Dmo
{
  [Guid("2c3cd98a-2bfa-4a53-9c27-5249ba64ba0f")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IEnumDmo
  {
    int Next(int itemsToFetch, out Guid clsid, out IntPtr name, out int itemsFetched);

    int Skip(int itemsToSkip);

    int Reset();

    int Clone(out IEnumDmo enumPointer);
  }
}
