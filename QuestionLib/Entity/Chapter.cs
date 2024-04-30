// Decompiled with JetBrains decompiler
// Type: QuestionLib.Entity.Chapter
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

#nullable disable
namespace QuestionLib.Entity
{
  public class Chapter
  {
    private int _chid;
    private string _cid;
    private string _name;

    public Chapter()
    {
    }

    public Chapter(int _chid, string _cid, string _name)
    {
      this._chid = _chid;
      this._cid = _cid;
      this._name = _name;
    }

    public int ChID
    {
      get => this._chid;
      set => this._chid = value;
    }

    public string CID
    {
      get => this._cid;
      set => this._cid = value;
    }

    public string Name
    {
      get => this._name;
      set => this._name = value;
    }

    public override string ToString() => this._name;
  }
}
