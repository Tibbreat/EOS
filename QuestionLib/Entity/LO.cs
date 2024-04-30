// Decompiled with JetBrains decompiler
// Type: QuestionLib.Entity.LO
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

#nullable disable
namespace QuestionLib.Entity
{
  public class LO
  {
    private int _LOID;
    private string _CID;
    private string _LO_Name;
    private string _LO_Desc;
    private string _Dec_No;

    public int LOID
    {
      get => this._LOID;
      set => this._LOID = value;
    }

    public string CID
    {
      get => this._CID;
      set => this._CID = value;
    }

    public string LO_Name
    {
      get => this._LO_Name;
      set => this._LO_Name = value;
    }

    public string LO_Desc
    {
      get => this._LO_Desc;
      set => this._LO_Desc = value;
    }

    public string Dec_No
    {
      get => this._Dec_No;
      set => this._Dec_No = value;
    }

    public override string ToString() => this.LO_Name + " - " + this.LO_Desc;
  }
}
