// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Asio.ASIOCallbacks
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave.Asio
{
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  internal struct ASIOCallbacks
  {
    public ASIOCallbacks.ASIOBufferSwitchCallBack pbufferSwitch;
    public ASIOCallbacks.ASIOSampleRateDidChangeCallBack psampleRateDidChange;
    public ASIOCallbacks.ASIOAsioMessageCallBack pasioMessage;
    public ASIOCallbacks.ASIOBufferSwitchTimeInfoCallBack pbufferSwitchTimeInfo;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void ASIOBufferSwitchCallBack(int doubleBufferIndex, bool directProcess);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void ASIOSampleRateDidChangeCallBack(double sRate);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int ASIOAsioMessageCallBack(
      ASIOMessageSelector selector,
      int value,
      IntPtr message,
      IntPtr opt);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr ASIOBufferSwitchTimeInfoCallBack(
      IntPtr asioTimeParam,
      int doubleBufferIndex,
      bool directProcess);
  }
}
