// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IPropertyStore
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [Guid("886d8eeb-8cf2-4446-8d02-cdba1dbdcf99")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IPropertyStore
  {
    int GetCount(out int propCount);

    int GetAt(int property, out PropertyKey key);

    int GetValue(ref PropertyKey key, out PropVariant value);

    int SetValue(ref PropertyKey key, ref PropVariant value);

    int Commit();
  }
}
