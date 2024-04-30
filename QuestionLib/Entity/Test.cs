// Decompiled with JetBrains decompiler
// Type: QuestionLib.Entity.Test
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

#nullable disable
namespace QuestionLib.Entity
{
  public class Test
  {
    private string _testId;
    private string _courseId;
    private string _questions;
    private int _numOfQuestion;
    private float _mark;
    private string _studentGuide;

    public string TestId
    {
      get => this._testId;
      set => this._testId = value;
    }

    public string CourseId
    {
      get => this._courseId;
      set => this._courseId = value;
    }

    public string Questions
    {
      get => this._questions;
      set => this._questions = value;
    }

    public int NumOfQuestion
    {
      get => this._numOfQuestion;
      set => this._numOfQuestion = value;
    }

    public float Mark
    {
      get => this._mark;
      set => this._mark = value;
    }

    public string StudentGuide
    {
      get => this._studentGuide;
      set => this._studentGuide = value;
    }
  }
}
