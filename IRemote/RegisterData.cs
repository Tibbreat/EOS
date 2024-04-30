// Decompiled with JetBrains decompiler
// Type: IRemote.RegisterData
// Assembly: IRemote, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9A047928-93CB-403F-8712-1C57D77846BF
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\IRemote.dll

using System;

#nullable disable
namespace IRemote
{
  [Serializable]
  public class RegisterData
  {
    private string _login;
    private string _password;
    private DateTime _startTime;
    private string _machine;
    private string _examCode;

    public string Login
    {
      get => this._login;
      set => this._login = value;
    }

    public string Password
    {
      get => this._password;
      set => this._password = value;
    }

    public DateTime StartDate
    {
      get => this._startTime;
      set => this._startTime = value;
    }

    public string Machine
    {
      get => this._machine;
      set => this._machine = value;
    }

    public string ExamCode
    {
      get => this._examCode;
      set => this._examCode = value;
    }
  }
}
