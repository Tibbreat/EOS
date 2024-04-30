// Decompiled with JetBrains decompiler
// Type: IRemote.IRemoteServer
// Assembly: IRemote, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9A047928-93CB-403F-8712-1C57D77846BF
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\IRemote.dll

using QuestionLib;

#nullable disable
namespace IRemote
{
  public interface IRemoteServer
  {
    EOSData ConductExam(RegisterData rd);

    SubmitStatus Submit(SubmitPaper submitPaper, ref string msg);

    void SaveCaptureImage(byte[] img, string examCode, string login);
  }
}
