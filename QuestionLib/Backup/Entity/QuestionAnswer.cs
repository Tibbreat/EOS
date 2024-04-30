// Decompiled with JetBrains decompiler
// Type: QuestionLib.Entity.QuestionAnswer
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using System;

#nullable disable
namespace QuestionLib.Entity
{
  [Serializable]
  public class QuestionAnswer
  {
    private int _qaid;
    private int _qid;
    private string _text;
    private bool _chosen;
    private bool _selected;
    private bool _done;
    private int _QBID;

    public int QBID
    {
      get => this._QBID;
      set => this._QBID = value;
    }

    public QuestionAnswer()
    {
    }

    public QuestionAnswer(string text, bool chosen)
    {
      this._text = text;
      this._chosen = chosen;
    }

    public int QAID
    {
      get => this._qaid;
      set => this._qaid = value;
    }

    public int QID
    {
      get => this._qid;
      set => this._qid = value;
    }

    public string Text
    {
      get => this._text;
      set => this._text = value;
    }

    public bool Chosen
    {
      get => this._chosen;
      set => this._chosen = value;
    }

    public bool Selected
    {
      get => this._selected;
      set => this._selected = value;
    }

    public bool Done
    {
      get => this._done;
      set => this._done = value;
    }
  }
}
