// Decompiled with JetBrains decompiler
// Type: QuestionLib.Entity.Question
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using NHibernate;
using QuestionLib.Business;
using System;
using System.Collections;

#nullable disable
namespace QuestionLib.Entity
{
  [Serializable]
  public class Question
  {
    private int _qid;
    private string _courseId;
    private int _chapterId;
    private int _pid;
    private string _text;
    private float _mark;
    private ArrayList _questionAnswers;
    private QuestionType _qType;
    private bool _lock;
    private byte[] _imageData;
    private int _imageSize;
    private ArrayList _questionLOs;
    private int _QBID;

    public int QBID
    {
      get => this._QBID;
      set => this._QBID = value;
    }

    public Question()
    {
      this._questionAnswers = new ArrayList();
      this._questionLOs = new ArrayList();
    }

    public int QID
    {
      get => this._qid;
      set => this._qid = value;
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

    public int PID
    {
      get => this._pid;
      set => this._pid = value;
    }

    public string Text
    {
      get => this._text;
      set => this._text = value;
    }

    public float Mark
    {
      get => this._mark;
      set => this._mark = value;
    }

    public ArrayList QuestionAnswers
    {
      get => this._questionAnswers;
      set => this._questionAnswers = value;
    }

    public QuestionType QType
    {
      get => this._qType;
      set => this._qType = value;
    }

    public bool Lock
    {
      get => this._lock;
      set => this._lock = value;
    }

    public byte[] ImageData
    {
      get => this._imageData;
      set => this._imageData = value;
    }

    public int ImageSize
    {
      get => this._imageSize;
      set => this._imageSize = value;
    }

    public ArrayList QuestionLOs
    {
      get => this._questionLOs;
      set => this._questionLOs = value;
    }

    public override string ToString() => this._text;

    public void LoadAnswers(ISessionFactory sessionFactory)
    {
      this._questionAnswers = (ArrayList) new BOQuestionAnswer(sessionFactory).LoadAnswer(this._qid);
    }

    public void Preapare2Submit()
    {
      this.Text = (string) null;
      this.CourseId = (string) null;
      this.ImageData = (byte[]) null;
      this.ImageSize = 0;
      if (this.QType == QuestionType.FILL_BLANK_ALL || this.QType == QuestionType.FILL_BLANK_GROUP || this.QType == QuestionType.FILL_BLANK_EMPTY)
        return;
      foreach (QuestionAnswer questionAnswer in this.QuestionAnswers)
        questionAnswer.Text = (string) null;
    }
  }
}
