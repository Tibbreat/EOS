// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.MFT_OUTPUT_DATA_BUFFER
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.MediaFoundation
{
  public struct MFT_OUTPUT_DATA_BUFFER
  {
    public int dwStreamID;
    public IMFSample pSample;
    public _MFT_OUTPUT_DATA_BUFFER_FLAGS dwStatus;
    public IMFCollection pEvents;
  }
}
