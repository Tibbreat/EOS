// Decompiled with JetBrains decompiler
// Type: WebEye.Controls.WinForms.WebCameraControl.DirectShowProxy
// Assembly: WebEye.Controls.WinForms.WebCameraControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70BBE54F-449A-4821-AB1C-B17F193D7D13
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\WebEye.Controls.WinForms.WebCameraControl.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using WebEye.Controls.WinForms.WebCameraControl.Properties;

#nullable disable
namespace WebEye.Controls.WinForms.WebCameraControl
{
  internal sealed class DirectShowProxy : IDisposable
  {
    private DirectShowProxy.EnumVideoInputDevicesDelegate _enumVideoInputDevices;
    private DirectShowProxy.BuildCaptureGraphDelegate _buildCaptureGraph;
    private DirectShowProxy.AddRenderFilterDelegate _addRenderFilter;
    private DirectShowProxy.AddCaptureFilterDelegate _addCaptureFilter;
    private DirectShowProxy.ResetCaptureGraphDelegate _resetCaptureGraph;
    private DirectShowProxy.StartDelegate _start;
    private DirectShowProxy.GetCurrentImageDelegate _getCurrentImage;
    private DirectShowProxy.GetVideoSizeDelegate _getVideoSize;
    private DirectShowProxy.StopDelegate _stop;
    private DirectShowProxy.DestroyCaptureGraphDelegate _destroyCaptureGraph;
    private string _dllFile = string.Empty;
    private IntPtr _hDll = IntPtr.Zero;

    private bool IsX86Platform => IntPtr.Size == 4;

    [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr LoadLibrary(string lpFileName);

    private void LoadDll()
    {
      this._dllFile = Path.GetTempFileName();
      using (FileStream output = new FileStream(this._dllFile, FileMode.Create, FileAccess.Write))
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
          binaryWriter.Write(this.IsX86Platform ? Resources.DirectShowFacade : Resources.DirectShowFacade64);
      }
      this._hDll = DirectShowProxy.LoadLibrary(this._dllFile);
      if (this._hDll == IntPtr.Zero)
        throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

    private void BindToDll(IntPtr hDll)
    {
      this._enumVideoInputDevices = (DirectShowProxy.EnumVideoInputDevicesDelegate) Marshal.GetDelegateForFunctionPointer(DirectShowProxy.GetProcAddress(hDll, "EnumVideoInputDevices"), typeof (DirectShowProxy.EnumVideoInputDevicesDelegate));
      this._buildCaptureGraph = (DirectShowProxy.BuildCaptureGraphDelegate) Marshal.GetDelegateForFunctionPointer(DirectShowProxy.GetProcAddress(hDll, "BuildCaptureGraph"), typeof (DirectShowProxy.BuildCaptureGraphDelegate));
      this._addRenderFilter = (DirectShowProxy.AddRenderFilterDelegate) Marshal.GetDelegateForFunctionPointer(DirectShowProxy.GetProcAddress(hDll, "AddRenderFilter"), typeof (DirectShowProxy.AddRenderFilterDelegate));
      this._addCaptureFilter = (DirectShowProxy.AddCaptureFilterDelegate) Marshal.GetDelegateForFunctionPointer(DirectShowProxy.GetProcAddress(hDll, "AddCaptureFilter"), typeof (DirectShowProxy.AddCaptureFilterDelegate));
      this._resetCaptureGraph = (DirectShowProxy.ResetCaptureGraphDelegate) Marshal.GetDelegateForFunctionPointer(DirectShowProxy.GetProcAddress(hDll, "ResetCaptureGraph"), typeof (DirectShowProxy.ResetCaptureGraphDelegate));
      this._start = (DirectShowProxy.StartDelegate) Marshal.GetDelegateForFunctionPointer(DirectShowProxy.GetProcAddress(hDll, "Start"), typeof (DirectShowProxy.StartDelegate));
      this._getCurrentImage = (DirectShowProxy.GetCurrentImageDelegate) Marshal.GetDelegateForFunctionPointer(DirectShowProxy.GetProcAddress(hDll, "GetCurrentImage"), typeof (DirectShowProxy.GetCurrentImageDelegate));
      this._getVideoSize = (DirectShowProxy.GetVideoSizeDelegate) Marshal.GetDelegateForFunctionPointer(DirectShowProxy.GetProcAddress(hDll, "GetVideoSize"), typeof (DirectShowProxy.GetVideoSizeDelegate));
      this._stop = (DirectShowProxy.StopDelegate) Marshal.GetDelegateForFunctionPointer(DirectShowProxy.GetProcAddress(hDll, "Stop"), typeof (DirectShowProxy.StopDelegate));
      this._destroyCaptureGraph = (DirectShowProxy.DestroyCaptureGraphDelegate) Marshal.GetDelegateForFunctionPointer(DirectShowProxy.GetProcAddress(hDll, "DestroyCaptureGraph"), typeof (DirectShowProxy.DestroyCaptureGraphDelegate));
    }

    internal DirectShowProxy()
    {
      this.LoadDll();
      this.BindToDll(this._hDll);
    }

    internal void EnumVideoInputDevices(
      DirectShowProxy.EnumVideoInputDevicesCallback callback)
    {
      this._enumVideoInputDevices(callback);
    }

    private static void ThrowExceptionForResult(int hresult, string message)
    {
      if (hresult < 0)
        throw new DirectShowException(message, hresult);
    }

    internal void BuildCaptureGraph()
    {
      DirectShowProxy.ThrowExceptionForResult(this._buildCaptureGraph(), "Failed to build a video capture graph.");
    }

    internal void AddRenderFilter(IntPtr hWnd)
    {
      DirectShowProxy.ThrowExceptionForResult(this._addRenderFilter(hWnd), "Failed to setup a render filter.");
    }

    internal void AddCaptureFilter(string devicePath)
    {
      DirectShowProxy.ThrowExceptionForResult(this._addCaptureFilter(devicePath), "Failed to add a video capture filter.");
    }

    internal void ResetCaptureGraph()
    {
      DirectShowProxy.ThrowExceptionForResult(this._resetCaptureGraph(), "Failed to reset a video capture graph.");
    }

    internal void Start()
    {
      DirectShowProxy.ThrowExceptionForResult(this._start(), "Failed to run a capture graph.");
    }

    internal Bitmap GetCurrentImage()
    {
      IntPtr dibPtr;
      DirectShowProxy.ThrowExceptionForResult(this._getCurrentImage(out dibPtr), "Failed to get the current image.");
      try
      {
        DirectShowProxy.BITMAPINFOHEADER structure = (DirectShowProxy.BITMAPINFOHEADER) Marshal.PtrToStructure(dibPtr, typeof (DirectShowProxy.BITMAPINFOHEADER));
        int num1 = structure.biWidth * ((int) structure.biBitCount / 8);
        int num2 = num1 % 4 > 0 ? 4 - num1 % 4 : 0;
        int stride = num1 + num2;
        PixelFormat format = PixelFormat.Undefined;
        switch (structure.biBitCount)
        {
          case 1:
            format = PixelFormat.Format1bppIndexed;
            break;
          case 4:
            format = PixelFormat.Format4bppIndexed;
            break;
          case 8:
            format = PixelFormat.Format8bppIndexed;
            break;
          case 16:
            format = PixelFormat.Format16bppRgb555;
            break;
          case 24:
            format = PixelFormat.Format24bppRgb;
            break;
          case 32:
            format = PixelFormat.Format32bppRgb;
            break;
        }
        Bitmap currentImage = new Bitmap(structure.biWidth, structure.biHeight, stride, format, (IntPtr) (dibPtr.ToInt64() + (long) Marshal.SizeOf((object) structure)));
        currentImage.RotateFlip(RotateFlipType.Rotate180FlipX);
        return currentImage;
      }
      finally
      {
        if (dibPtr != IntPtr.Zero)
          Marshal.FreeCoTaskMem(dibPtr);
      }
    }

    internal Size GetVideoSize()
    {
      int width;
      int height;
      DirectShowProxy.ThrowExceptionForResult(this._getVideoSize(out width, out height), "Failed to get the video size.");
      return new Size(width, height);
    }

    internal void Stop()
    {
      DirectShowProxy.ThrowExceptionForResult(this._stop(), "Failed to stop a video capture graph.");
    }

    internal void DestroyCaptureGraph() => this._destroyCaptureGraph();

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool FreeLibrary(IntPtr hModule);

    public void Dispose()
    {
      if (this._hDll != IntPtr.Zero)
      {
        DirectShowProxy.FreeLibrary(this._hDll);
        this._hDll = IntPtr.Zero;
      }
      if (!File.Exists(this._dllFile))
        return;
      File.Delete(this._dllFile);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct VideoInputDeviceInfo
    {
      [MarshalAs(UnmanagedType.BStr)]
      internal string FriendlyName;
      [MarshalAs(UnmanagedType.BStr)]
      internal string DevicePath;
    }

    internal delegate void EnumVideoInputDevicesCallback(
      ref DirectShowProxy.VideoInputDeviceInfo info);

    private delegate void EnumVideoInputDevicesDelegate(
      DirectShowProxy.EnumVideoInputDevicesCallback callback);

    private delegate int BuildCaptureGraphDelegate();

    private delegate int AddRenderFilterDelegate(IntPtr hWnd);

    private delegate int AddCaptureFilterDelegate([MarshalAs(UnmanagedType.BStr)] string devicePath);

    private delegate int ResetCaptureGraphDelegate();

    private delegate int StartDelegate();

    private delegate int GetCurrentImageDelegate(out IntPtr dibPtr);

    private delegate int GetVideoSizeDelegate(out int width, out int height);

    private delegate int StopDelegate();

    private delegate void DestroyCaptureGraphDelegate();

    private struct BITMAPINFOHEADER
    {
      public uint biSize;
      public int biWidth;
      public int biHeight;
      public ushort biPlanes;
      public ushort biBitCount;
      public uint biCompression;
      public uint biSizeImage;
      public int biXPelsPerMeter;
      public int biYPelsPerMeter;
      public uint biClrUsed;
      public uint biClrImportant;
    }
  }
}
