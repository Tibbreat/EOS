// Decompiled with JetBrains decompiler
// Type: WebEye.Controls.WinForms.WebCameraControl.WebCameraId
// Assembly: WebEye.Controls.WinForms.WebCameraControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70BBE54F-449A-4821-AB1C-B17F193D7D13
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\WebEye.Controls.WinForms.WebCameraControl.dll

using System;

#nullable disable
namespace WebEye.Controls.WinForms.WebCameraControl
{
  public sealed class WebCameraId : IEquatable<WebCameraId>
  {
    private readonly string _name;
    private readonly string _devicePath;

    internal WebCameraId(DirectShowProxy.VideoInputDeviceInfo info)
    {
      this._name = info.FriendlyName;
      this._devicePath = info.DevicePath;
    }

    public string Name => this._name;

    internal string DevicePath => this._devicePath;

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if ((object) this == obj)
        return true;
      return obj.GetType() == typeof (WebCameraId) && this.Equals((WebCameraId) obj);
    }

    public bool Equals(WebCameraId other)
    {
      if ((object) other == null)
        return false;
      if ((object) this == (object) other)
        return true;
      return object.Equals((object) other._name, (object) this._name) && object.Equals((object) other._devicePath, (object) this._devicePath);
    }

    public override int GetHashCode()
    {
      return this._name.GetHashCode() * 397 ^ this._devicePath.GetHashCode();
    }

    public static bool operator ==(WebCameraId left, WebCameraId right)
    {
      return object.Equals((object) left, (object) right);
    }

    public static bool operator !=(WebCameraId left, WebCameraId right)
    {
      return !object.Equals((object) left, (object) right);
    }
  }
}
