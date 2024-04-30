// Decompiled with JetBrains decompiler
// Type: QuestionLib.Entity.Passage
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
  public class Passage
  {
    private int _pid;
    private string _courseId;
    private int _chapterId;
    private string _text;
    private ArrayList _passageQuestions;
    private int _QBID;

    public int QBID
    {
      get => this._QBID;
      set => this._QBID = value;
    }

    public Passage() => this._passageQuestions = new ArrayList();

    public int PID
    {
      get => this._pid;
      set => this._pid = value;
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

    public string Text
    {
      get => this._text;
      set => this._text = value;
    }

    public ArrayList PassageQuestions
    {
      get => this._passageQuestions;
      set => this._passageQuestions = value;
    }

    public override string ToString() => this._pid.ToString();

    public void LoadQuestions(ISessionFactory sessionFactory)
    {
      this._passageQuestions = (ArrayList) new BOQuestion(sessionFactory).LoadPassageQuestion(this._pid);
    }

    public void Preapare2Submit()
    {
      this.Text = (string) null;
      this.CourseId = (string) null;
      foreach (Question passageQuestion in this.PassageQuestions)
        passageQuestion.Preapare2Submit();
    }
  }
}
