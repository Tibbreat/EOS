// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveFormatCustomMarshaler
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave
{
  public sealed class WaveFormatCustomMarshaler : ICustomMarshaler
  {
    private static WaveFormatCustomMarshaler marshaler;

    public static ICustomMarshaler GetInstance(string cookie)
    {
      if (WaveFormatCustomMarshaler.marshaler == null)
        WaveFormatCustomMarshaler.marshaler = new WaveFormatCustomMarshaler();
      return (ICustomMarshaler) WaveFormatCustomMarshaler.marshaler;
    }

    public void CleanUpManagedData(object ManagedObj)
    {
    }

    public void CleanUpNativeData(IntPtr pNativeData) => Marshal.FreeHGlobal(pNativeData);

    public int GetNativeDataSize() => throw new NotImplementedException();

    public IntPtr MarshalManagedToNative(object ManagedObj)
    {
      return WaveFormat.MarshalToPtr((WaveFormat) ManagedObj);
    }

    public object MarshalNativeToManaged(IntPtr pNativeData)
    {
      return (object) WaveFormat.MarshalFromPtr(pNativeData);
    }
  }
}
