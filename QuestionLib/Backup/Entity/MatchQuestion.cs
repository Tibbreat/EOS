// Decompiled with JetBrains decompiler
// Type: QuestionLib.Entity.MatchQuestion
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using System;
using System.Collections;

#nullable disable
namespace QuestionLib.Entity
{
  [Serializable]
  public class MatchQuestion
  {
    private int _mid;
    private string _courseId;
    private int _chapterId;
    private string _columnA;
    private string _columnB;
    private string _solution;
    private float _mark;
    private string _studentAnswer;
    private ArrayList _questionLOs;
    private int _QBID;

    public int QBID
    {
      get => this._QBID;
      set => this._QBID = value;
    }

    public MatchQuestion() => this._questionLOs = new ArrayList();

    public int MID
    {
      get => this._mid;
      set => this._mid = value;
    }

    public string CourseId
    {
      get => this._courseId;
      set => this._courseId = value;
    }

    public int ChapterId
    {
      get => this._chapterId;
      set => this._chapterId = value;
    }

    public string ColumnA
    {
      get => this._columnA;
      set => this._columnA = value;
    }

    public string ColumnB
    {
      get => this._columnB;
      set => this._columnB = value;
    }

    public string Solution
    {
      get => this._solution;
      set => this._solution = value;
    }

    public float Mark
    {
      get => this._mark;
      set => this._mark = value;
    }

    public string SudentAnswer
    {
      get => this._studentAnswer;
      set => this._studentAnswer = value;
    }

    public ArrayList QuestionLOs
    {
      get => this._questionLOs;
      set => this._questionLOs = value;
    }

    public override string ToString() => this._mid.ToString();

    public void Preapare2Submit()
    {
      this.Solution = (string) null;
      this.ColumnA = (string) null;
      this.ColumnB = (string) null;
      this.CourseId = (string) null;
    }
  }
}
