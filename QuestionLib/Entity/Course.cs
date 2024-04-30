// Decompiled with JetBrains decompiler
// Type: QuestionLib.Entity.Course
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

#nullable disable
namespace QuestionLib.Entity
{
  public class Course
  {
    private string _cid;
    private string _name;

    public Course()
    {
    }

    public Course(string _cid, string _name)
    {
      this._cid = _cid;
      this._name = _name;
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
  }
}
