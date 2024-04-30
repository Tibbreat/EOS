// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.MMDeviceEnumerator
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class MMDeviceEnumerator
  {
    private readonly IMMDeviceEnumerator realEnumerator;

    public MMDeviceEnumerator()
    {
      if (Environment.OSVersion.Version.Major < 6)
        throw new NotSupportedException("This functionality is only supported on Windows Vista or newer.");
      this.realEnumerator = new MMDeviceEnumeratorComObject() as IMMDeviceEnumerator;
    }

    public MMDeviceCollection EnumerateAudioEndPoints(DataFlow dataFlow, DeviceState dwStateMask)
    {
      IMMDeviceCollection devices;
      Marshal.ThrowExceptionForHR(this.realEnumerator.EnumAudioEndpoints(dataFlow, dwStateMask, out devices));
      return new MMDeviceCollection(devices);
    }

    public MMDevice GetDefaultAudioEndpoint(DataFlow dataFlow, Role role)
    {
      IMMDevice endpoint = (IMMDevice) null;
      Marshal.ThrowExceptionForHR(this.realEnumerator.GetDefaultAudioEndpoint(dataFlow, role, out endpoint));
      return new MMDevice(endpoint);
    }

    public bool HasDefaultAudioEndpoint(DataFlow dataFlow, Role role)
    {
      IMMDevice endpoint = (IMMDevice) null;
      int defaultAudioEndpoint = this.realEnumerator.GetDefaultAudioEndpoint(dataFlow, role, out endpoint);
      switch (defaultAudioEndpoint)
      {
        case -2147023728:
          return false;
        case 0:
          Marshal.ReleaseComObject((object) endpoint);
          return true;
        default:
          Marshal.ThrowExceptionForHR(defaultAudioEndpoint);
          return false;
      }
    }

    public MMDevice GetDevice(string id)
    {
      IMMDevice deviceName = (IMMDevice) null;
      Marshal.ThrowExceptionForHR(this.realEnumerator.GetDevice(id, out deviceName));
      return new MMDevice(deviceName);
    }

    public int RegisterEndpointNotificationCallback([MarshalAs(UnmanagedType.Interface), In] IMMNotificationClient client)
    {
      return this.realEnumerator.RegisterEndpointNotificationCallback(client);
    }

    public int UnregisterEndpointNotificationCallback([MarshalAs(UnmanagedType.Interface), In] IMMNotificationClient client)
    {
      return this.realEnumerator.UnregisterEndpointNotificationCallback(client);
    }
  }
}
