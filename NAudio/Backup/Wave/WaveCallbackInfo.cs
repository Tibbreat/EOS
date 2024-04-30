// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveCallbackInfo
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave
{
  public class WaveCallbackInfo
  {
    private WaveWindow waveOutWindow;
    private WaveWindowNative waveOutWindowNative;

    public WaveCallbackStrategy Strategy { get; private set; }

    public IntPtr Handle { get; private set; }

    public static WaveCallbackInfo FunctionCallback()
    {
      return new WaveCallbackInfo(WaveCallbackStrategy.FunctionCallback, IntPtr.Zero);
    }

    public static WaveCallbackInfo NewWindow()
    {
      return new WaveCallbackInfo(WaveCallbackStrategy.NewWindow, IntPtr.Zero);
    }

    public static WaveCallbackInfo ExistingWindow(IntPtr handle)
    {
      return !(handle == IntPtr.Zero) ? new WaveCallbackInfo(WaveCallbackStrategy.ExistingWindow, handle) : throw new ArgumentException("Handle cannot be zero");
    }

    private WaveCallbackInfo(WaveCallbackStrategy strategy, IntPtr handle)
    {
      this.Strategy = strategy;
      this.Handle = handle;
    }

    internal void Connect(WaveInterop.WaveCallback callback)
    {
      if (this.Strategy == WaveCallbackStrategy.NewWindow)
      {
        this.waveOutWindow = new WaveWindow(callback);
        this.waveOutWindow.CreateControl();
        this.Handle = this.waveOutWindow.Handle;
      }
      else
      {
        if (this.Strategy != WaveCallbackStrategy.ExistingWindow)
          return;
        this.waveOutWindowNative = new WaveWindowNative(callback);
        this.waveOutWindowNative.AssignHandle(this.Handle);
      }
    }

    internal MmResult WaveOutOpen(
      out IntPtr waveOutHandle,
      int deviceNumber,
      WaveFormat waveFormat,
      WaveInterop.WaveCallback callback)
    {
      return this.Strategy != WaveCallbackStrategy.FunctionCallback ? WaveInterop.waveOutOpenWindow(out waveOutHandle, (IntPtr) deviceNumber, waveFormat, this.Handle, IntPtr.Zero, WaveInterop.WaveInOutOpenFlags.CallbackWindow) : WaveInterop.waveOutOpen(out waveOutHandle, (IntPtr) deviceNumber, waveFormat, callback, IntPtr.Zero, WaveInterop.WaveInOutOpenFlags.CallbackFunction);
    }

    internal MmResult WaveInOpen(
      out IntPtr waveInHandle,
      int deviceNumber,
      WaveFormat waveFormat,
      WaveInterop.WaveCallback callback)
    {
      return this.Strategy != WaveCallbackStrategy.FunctionCallback ? WaveInterop.waveInOpenWindow(out waveInHandle, (IntPtr) deviceNumber, waveFormat, this.Handle, IntPtr.Zero, WaveInterop.WaveInOutOpenFlags.CallbackWindow) : WaveInterop.waveInOpen(out waveInHandle, (IntPtr) deviceNumber, waveFormat, callback, IntPtr.Zero, WaveInterop.WaveInOutOpenFlags.CallbackFunction);
    }

    internal void Disconnect()
    {
      if (this.waveOutWindow != null)
      {
        this.waveOutWindow.Close();
        this.waveOutWindow = (WaveWindow) null;
      }
      if (this.waveOutWindowNative == null)
        return;
      this.waveOutWindowNative.ReleaseHandle();
      this.waveOutWindowNative = (WaveWindowNative) null;
    }
  }
}
