// Decompiled with JetBrains decompiler
// Type: IRemote.EOSData
// Assembly: IRemote, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9A047928-93CB-403F-8712-1C57D77846BF
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\IRemote.dll

using QuestionLib;
using System;

#nullable disable
namespace IRemote
{
  [Serializable]
  public class EOSData
  {
    public RegisterStatus Status;
    public Paper ExamPaper;
    public SubmitPaper StudentSubmitPaper;
    public byte[] GUI;
    public int OriginSize;
    public ServerInfo ServerInfomation;
    public RegisterData RegData;
  }
}
