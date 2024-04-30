// Decompiled with JetBrains decompiler
// Type: QuestionLib.Paper
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using QuestionLib.Entity;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace QuestionLib
{
  [Serializable]
  public class Paper
  {
    private TestTypeEnum _testType;
    private string _examCode;
    private int _duration;
    private float _mark;
    private int _noOfQuestion;
    private ArrayList _reading;
    private ArrayList _grammar;
    private ArrayList _match;
    private ArrayList _indicateMistake;
    private ArrayList _fillBlank;
    private EssayQuestion _essay;
    private bool _isShuffleReading;
    private bool _isShuffleGrammer;
    private bool _isShuffleMatch;
    private bool _isShuffleIndicateMistake;
    private bool _isShuffleFillBlank;
    private string _studentGuide;
    private string _listenCode;
    private string _pwd;
    private List<AudioInPaper> _listAudio;
    private byte[] _oneSecSilence;
    private int _audioHeadPadding;
    private ImagePaper _imagePaper;

    public bool IsShuffleReading
    {
      get => this._isShuffleReading;
      set => this._isShuffleReading = value;
    }

    public bool IsShuffleGrammer
    {
      get => this._isShuffleGrammer;
      set => this._isShuffleGrammer = value;
    }

    public bool IsShuffleMatch
    {
      get => this._isShuffleMatch;
      set => this._isShuffleMatch = value;
    }

    public QuestionDistribution QD { get; set; }

    public bool IsShuffleIndicateMistake
    {
      get => this._isShuffleIndicateMistake;
      set => this._isShuffleIndicateMistake = value;
    }

    public bool IsShuffleFillBlank
    {
      get => this._isShuffleFillBlank;
      set => this._isShuffleFillBlank = value;
    }

    public Paper()
    {
      this._reading = new ArrayList();
      this._grammar = new ArrayList();
      this._match = new ArrayList();
      this._indicateMistake = new ArrayList();
      this._fillBlank = new ArrayList();
      this.TestType = TestTypeEnum.NOT_WRITING;
    }

    public int Duration
    {
      get => this._duration;
      set => this._duration = value;
    }

    public string ExamCode
    {
      get => this._examCode;
      set => this._examCode = value;
    }

    public float Mark
    {
      get => this._mark;
      set => this._mark = value;
    }

    public int NoOfQuestion
    {
      get => this._noOfQuestion;
      set => this._noOfQuestion = value;
    }

    public ArrayList ReadingQuestions
    {
      get => this._reading;
      set => this._reading = value;
    }

    public ArrayList GrammarQuestions
    {
      get => this._grammar;
      set => this._grammar = value;
    }

    public ArrayList MatchQuestions
    {
      get => this._match;
      set => this._match = value;
    }

    public ArrayList IndicateMQuestions
    {
      get => this._indicateMistake;
      set => this._indicateMistake = value;
    }

    public ArrayList FillBlankQuestions
    {
      get => this._fillBlank;
      set => this._fillBlank = value;
    }

    public EssayQuestion EssayQuestion
    {
      get => this._essay;
      set => this._essay = value;
    }

    public string StudentGuide
    {
      get => this._studentGuide;
      set => this._studentGuide = value;
    }

    public string ListenCode
    {
      get => this._listenCode;
      set => this._listenCode = value;
    }

    public string Password
    {
      get => this._pwd;
      set => this._pwd = value;
    }

    public TestTypeEnum TestType
    {
      get => this._testType;
      set => this._testType = value;
    }

    public ImagePaper ImgPaper
    {
      get => this._imagePaper;
      set => this._imagePaper = value;
    }

    public List<AudioInPaper> ListAudio
    {
      get => this._listAudio;
      set => this._listAudio = value;
    }

    public byte[] OneSecSilence
    {
      get => this._oneSecSilence;
      set => this._oneSecSilence = value;
    }

    public int AudioHeadPadding
    {
      get => this._audioHeadPadding;
      set => this._audioHeadPadding = value;
    }
  }
}
