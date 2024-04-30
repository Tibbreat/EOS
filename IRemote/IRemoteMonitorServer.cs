// Decompiled with JetBrains decompiler
// Type: IRemote.IRemoteMonitorServer
// Assembly: IRemote, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9A047928-93CB-403F-8712-1C57D77846BF
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\IRemote.dll

#nullable disable
namespace IRemote
{
  public interface IRemoteMonitorServer
  {
    int SaveScreenImage(byte[] img, int index, string examCode, string login);
  }
}
