// Decompiled with JetBrains decompiler
// Type: WebEye.Controls.WinForms.WebCameraControl.DirectShowException
// Assembly: WebEye.Controls.WinForms.WebCameraControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70BBE54F-449A-4821-AB1C-B17F193D7D13
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\WebEye.Controls.WinForms.WebCameraControl.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace WebEye.Controls.WinForms.WebCameraControl
{
  public sealed class DirectShowException : Exception
  {
    internal DirectShowException(string message, int hresult)
      : base(message, Marshal.GetExceptionForHR(hresult))
    {
    }
  }
}
