// Decompiled with JetBrains decompiler
// Type: IRemote.ServerInfo
// Assembly: IRemote, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9A047928-93CB-403F-8712-1C57D77846BF
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\IRemote.dll

using System;

#nullable disable
namespace IRemote
{
  [Serializable]
  public class ServerInfo
  {
    private string _ip;
    private int _port;
    private string _serverAlias;
    private string _version;
    private string _monitor_IP;
    private int _monitor_port;
    private string _ip_range_wlan;
    private string _public_ip;

    public ServerInfo()
    {
    }

    public ServerInfo(
      string ip,
      int port,
      string serverAlias,
      string version,
      string ip_range_wlan)
    {
      this._ip = ip;
      this._port = port;
      this._serverAlias = serverAlias;
      this._version = version;
      this._ip_range_wlan = ip_range_wlan;
    }

    public string IP
    {
      get => this._ip;
      set => this._ip = value;
    }

    public int Port
    {
      get => this._port;
      set => this._port = value;
    }

    public string ServerAlias
    {
      get => this._serverAlias;
      set => this._serverAlias = value;
    }

    public string Version
    {
      get => this._version;
      set => this._version = value;
    }

    public string MonitorServer_IP
    {
      get => this._monitor_IP;
      set => this._monitor_IP = value;
    }

    public int MonitorServer_Port
    {
      get => this._monitor_port;
      set => this._monitor_port = value;
    }

    public string IP_Range_WLAN
    {
      get => this._ip_range_wlan;
      set => this._ip_range_wlan = value;
    }

    public string Public_IP
    {
      get => this._public_ip;
      set => this._public_ip = value;
    }
  }
}
