// Decompiled with JetBrains decompiler
// Type: QuestionLib.Entity.QuestionLO
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

#nullable disable
namespace QuestionLib.Entity
{
  public class QuestionLO
  {
    private int _QuestionLOID;
    private QuestionType _QType;
    private int _QID;
    private int _LOID;

    public int QuestionLOID
    {
      get => this._QuestionLOID;
      set => this._QuestionLOID = value;
    }

    public QuestionType QType
    {
      get => this._QType;
      set => this._QType = value;
    }

    public int QID
    {
      get => this._QID;
      set => this._QID = value;
    }

    public int LOID
    {
      get => this._LOID;
      set => this._LOID = value;
    }
  }
}
