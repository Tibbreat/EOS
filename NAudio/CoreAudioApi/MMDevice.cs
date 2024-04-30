// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.MMDevice
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class MMDevice
  {
    private readonly IMMDevice deviceInterface;
    private PropertyStore propertyStore;
    private AudioMeterInformation audioMeterInformation;
    private AudioEndpointVolume audioEndpointVolume;
    private AudioSessionManager audioSessionManager;
    private static Guid IID_IAudioMeterInformation = new Guid("C02216F6-8C67-4B5B-9D00-D008E73E0064");
    private static Guid IID_IAudioEndpointVolume = new Guid("5CDF2C82-841E-4546-9722-0CF74078229A");
    private static Guid IID_IAudioClient = new Guid("1CB9AD4C-DBFA-4c32-B178-C2F568A703B2");
    private static Guid IDD_IAudioSessionManager = new Guid("BFA971F1-4D5E-40BB-935E-967039BFBEE4");

    private void GetPropertyInformation()
    {
      IPropertyStore properties;
      Marshal.ThrowExceptionForHR(this.deviceInterface.OpenPropertyStore(StorageAccessMode.Read, out properties));
      this.propertyStore = new PropertyStore(properties);
    }

    private AudioClient GetAudioClient()
    {
      object interfacePointer;
      Marshal.ThrowExceptionForHR(this.deviceInterface.Activate(ref MMDevice.IID_IAudioClient, ClsCtx.ALL, IntPtr.Zero, out interfacePointer));
      return new AudioClient(interfacePointer as IAudioClient);
    }

    private void GetAudioMeterInformation()
    {
      object interfacePointer;
      Marshal.ThrowExceptionForHR(this.deviceInterface.Activate(ref MMDevice.IID_IAudioMeterInformation, ClsCtx.ALL, IntPtr.Zero, out interfacePointer));
      this.audioMeterInformation = new AudioMeterInformation(interfacePointer as IAudioMeterInformation);
    }

    private void GetAudioEndpointVolume()
    {
      object interfacePointer;
      Marshal.ThrowExceptionForHR(this.deviceInterface.Activate(ref MMDevice.IID_IAudioEndpointVolume, ClsCtx.ALL, IntPtr.Zero, out interfacePointer));
      this.audioEndpointVolume = new AudioEndpointVolume(interfacePointer as IAudioEndpointVolume);
    }

    private void GetAudioSessionManager()
    {
      object interfacePointer;
      Marshal.ThrowExceptionForHR(this.deviceInterface.Activate(ref MMDevice.IDD_IAudioSessionManager, ClsCtx.ALL, IntPtr.Zero, out interfacePointer));
      this.audioSessionManager = new AudioSessionManager(interfacePointer as IAudioSessionManager);
    }

    public AudioClient AudioClient => this.GetAudioClient();

    public AudioMeterInformation AudioMeterInformation
    {
      get
      {
        if (this.audioMeterInformation == null)
          this.GetAudioMeterInformation();
        return this.audioMeterInformation;
      }
    }

    public AudioEndpointVolume AudioEndpointVolume
    {
      get
      {
        if (this.audioEndpointVolume == null)
          this.GetAudioEndpointVolume();
        return this.audioEndpointVolume;
      }
    }

    public AudioSessionManager AudioSessionManager
    {
      get
      {
        if (this.audioSessionManager == null)
          this.GetAudioSessionManager();
        return this.audioSessionManager;
      }
    }

    public PropertyStore Properties
    {
      get
      {
        if (this.propertyStore == null)
          this.GetPropertyInformation();
        return this.propertyStore;
      }
    }

    public string FriendlyName
    {
      get
      {
        if (this.propertyStore == null)
          this.GetPropertyInformation();
        return this.propertyStore.Contains(PropertyKeys.PKEY_Device_FriendlyName) ? (string) this.propertyStore[PropertyKeys.PKEY_Device_FriendlyName].Value : "Unknown";
      }
    }

    public string DeviceFriendlyName
    {
      get
      {
        if (this.propertyStore == null)
          this.GetPropertyInformation();
        return this.propertyStore.Contains(PropertyKeys.PKEY_DeviceInterface_FriendlyName) ? (string) this.propertyStore[PropertyKeys.PKEY_DeviceInterface_FriendlyName].Value : "Unknown";
      }
    }

    public string IconPath
    {
      get
      {
        if (this.propertyStore == null)
          this.GetPropertyInformation();
        return this.propertyStore.Contains(PropertyKeys.PKEY_Device_IconPath) ? (string) this.propertyStore[PropertyKeys.PKEY_Device_IconPath].Value : "Unknown";
      }
    }

    public string ID
    {
      get
      {
        string id;
        Marshal.ThrowExceptionForHR(this.deviceInterface.GetId(out id));
        return id;
      }
    }

    public DataFlow DataFlow
    {
      get
      {
        DataFlow dataFlow;
        (this.deviceInterface as IMMEndpoint).GetDataFlow(out dataFlow);
        return dataFlow;
      }
    }

    public DeviceState State
    {
      get
      {
        DeviceState state;
        Marshal.ThrowExceptionForHR(this.deviceInterface.GetState(out state));
        return state;
      }
    }

    internal MMDevice(IMMDevice realDevice) => this.deviceInterface = realDevice;

    public override string ToString() => this.FriendlyName;
  }
}
