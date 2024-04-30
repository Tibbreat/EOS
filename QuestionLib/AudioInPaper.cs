// Decompiled with JetBrains decompiler
// Type: QuestionLib.AudioInPaper
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using System;

#nullable disable
namespace QuestionLib
{
  [Serializable]
  public class AudioInPaper : IComparable<AudioInPaper>
  {
    private int _audioSize;
    private byte[] _audioData;
    private int _audioLength;
    private byte _repeatTime;
    private int _paddingTime;
    private byte _playOrder;

    public int AudioSize
    {
      get => this._audioSize;
      set => this._audioSize = value;
    }

    public byte[] AudioData
    {
      get => this._audioData;
      set => this._audioData = value;
    }

    public int AudioLength
    {
      get => this._audioLength;
      set => this._audioLength = value;
    }

    public byte RepeatTime
    {
      get => this._repeatTime;
      set => this._repeatTime = value;
    }

    public int PaddingTime
    {
      get => this._paddingTime;
      set => this._paddingTime = value;
    }

    public byte PlayOrder
    {
      get => this._playOrder;
      set => this._playOrder = value;
    }

    public int CompareTo(AudioInPaper aip) => this.PlayOrder.CompareTo(aip.PlayOrder);
  }
}
