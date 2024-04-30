// Decompiled with JetBrains decompiler
// Type: WebEye.Controls.WinForms.WebCameraControl.WebCameraControl
// Assembly: WebEye.Controls.WinForms.WebCameraControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70BBE54F-449A-4821-AB1C-B17F193D7D13
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\WebEye.Controls.WinForms.WebCameraControl.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace WebEye.Controls.WinForms.WebCameraControl
{
  public sealed class WebCameraControl : UserControl
  {
    private DirectShowProxy _proxy;
    private readonly List<WebCameraId> _captureDevices = new List<WebCameraId>();
    private bool _isCapturing;
    private bool _captureGraphInitialized;
    private WebCameraId _currentCamera;
    private IContainer components;

    public WebCameraControl() => this.InitializeComponent();

    private DirectShowProxy Proxy => this._proxy ?? (this._proxy = new DirectShowProxy());

    private void SaveVideoDevice(ref DirectShowProxy.VideoInputDeviceInfo info)
    {
      if (string.IsNullOrEmpty(info.DevicePath))
        return;
      this._captureDevices.Add(new WebCameraId(info));
    }

    public IEnumerable<WebCameraId> GetVideoCaptureDevices()
    {
      this._captureDevices.Clear();
      this.Proxy.EnumVideoInputDevices(new DirectShowProxy.EnumVideoInputDevicesCallback(this.SaveVideoDevice));
      return (IEnumerable<WebCameraId>) new List<WebCameraId>((IEnumerable<WebCameraId>) this._captureDevices);
    }

    private void InitializeCaptureGraph()
    {
      this.Proxy.BuildCaptureGraph();
      this.Proxy.AddRenderFilter(this.Handle);
    }

    [Browsable(false)]
    public bool IsCapturing => this._isCapturing;

    public void StartCapture(WebCameraId camera)
    {
      if (camera == (WebCameraId) null)
        throw new ArgumentNullException();
      if (!this._captureGraphInitialized)
      {
        this.InitializeCaptureGraph();
        this._captureGraphInitialized = true;
      }
      if (this._isCapturing)
      {
        if (this._currentCamera == camera)
          return;
        this.StopCapture();
      }
      if (this._currentCamera != (WebCameraId) null)
      {
        this.Proxy.ResetCaptureGraph();
        this._currentCamera = (WebCameraId) null;
      }
      this.Proxy.AddCaptureFilter(camera.DevicePath);
      this._currentCamera = camera;
      try
      {
        this.Proxy.Start();
        this._isCapturing = true;
      }
      catch (DirectShowException ex)
      {
        this.Proxy.ResetCaptureGraph();
        this._currentCamera = (WebCameraId) null;
        throw;
      }
    }

    public Bitmap GetCurrentImage()
    {
      if (!this._isCapturing)
        throw new InvalidOperationException();
      return this.Proxy.GetCurrentImage();
    }

    [Browsable(false)]
    public Size VideoSize => !this._isCapturing ? new Size(0, 0) : this.Proxy.GetVideoSize();

    public void StopCapture()
    {
      if (!this._isCapturing)
        throw new InvalidOperationException();
      this.Proxy.Stop();
      this._isCapturing = false;
      this.Proxy.ResetCaptureGraph();
      this._currentCamera = (WebCameraId) null;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this._proxy != null)
      {
        if (this._isCapturing)
          this.StopCapture();
        this.Proxy.DestroyCaptureGraph();
        this.Proxy.Dispose();
      }
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.Font;
    }
  }
}
