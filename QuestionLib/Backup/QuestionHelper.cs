// Decompiled with JetBrains decompiler
// Type: QuestionLib.QuestionHelper
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using QuestionLib.Entity;
using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace QuestionLib
{
  public class QuestionHelper
  {
    public static char[] lo_deli = new char[1]{ ';' };
    public static string[] MultipleChoiceQuestionType = new string[2]
    {
      "Grammar",
      "Indicate Mistake"
    };
    public static string fillBlank_Pattern = "\\([0-9a-zA-Z;:=?<>/`,'’ .+_~!@#$%^&*\\r\\n-]+\\)";
    private static char[] splitChars = new char[3]
    {
      ' ',
      '-',
      '\t'
    };
    public static int LineWidth = 100;

    public static void SaveSubmitPaper(string folder, SubmitPaper submitPaper)
    {
      submitPaper.SubmitTime = DateTime.Now;
      FileStream serializationStream = new FileStream(folder + submitPaper.LoginId + ".dat", FileMode.Create, FileAccess.Write);
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) submitPaper);
      serializationStream.Flush();
      serializationStream.Close();
    }

    public static SubmitPaper LoadSubmitPaper(string savedFile)
    {
      try
      {
        FileStream serializationStream = new FileStream(savedFile, FileMode.Open, FileAccess.Read);
        SubmitPaper submitPaper = (SubmitPaper) new BinaryFormatter().Deserialize((Stream) serializationStream);
        serializationStream.Close();
        return submitPaper;
      }
      catch (Exception ex)
      {
        return (SubmitPaper) null;
      }
    }

    private static Passage GetPassage(Paper oPaper, int pid)
    {
      foreach (Passage readingQuestion in oPaper.ReadingQuestions)
      {
        if (readingQuestion.PID == pid)
          return readingQuestion;
      }
      return (Passage) null;
    }

    private static bool ReConstructQuestion(Question sq, Question oq)
    {
      if (sq.QID != oq.QID)
        return false;
      sq.QBID = oq.QBID;
      sq.CourseId = oq.CourseId;
      sq.Text = oq.Text;
      sq.Mark = oq.Mark;
      sq.ImageData = oq.ImageData;
      sq.ImageSize = oq.ImageSize;
      bool flag = false;
      if (sq.QType == QuestionType.FILL_BLANK_ALL)
        flag = true;
      if (sq.QType == QuestionType.FILL_BLANK_GROUP)
        flag = true;
      if (sq.QType == QuestionType.FILL_BLANK_EMPTY)
        flag = true;
      foreach (QuestionAnswer questionAnswer1 in sq.QuestionAnswers)
      {
        foreach (QuestionAnswer questionAnswer2 in oq.QuestionAnswers)
        {
          if (questionAnswer1.QAID == questionAnswer2.QAID)
          {
            if (flag)
            {
              if (QuestionHelper.RemoveSpaces(questionAnswer1.Text).Trim().ToLower().Equals(QuestionHelper.RemoveSpaces(questionAnswer2.Text).Trim().ToLower()))
              {
                questionAnswer1.Chosen = true;
                questionAnswer1.Selected = true;
                break;
              }
              break;
            }
            questionAnswer1.Text = questionAnswer2.Text;
            questionAnswer1.Chosen = questionAnswer2.Chosen;
            break;
          }
        }
      }
      return true;
    }

    private static void ReConstructEssay(EssayQuestion sEssay, EssayQuestion oEssay)
    {
      if (sEssay.EQID != oEssay.EQID)
        return;
      sEssay.QBID = oEssay.QBID;
      sEssay.CourseId = oEssay.CourseId;
      sEssay.Question = oEssay.Question;
    }

    private static void ReConstructImagePaper(ImagePaper sIP, ImagePaper oIP)
    {
      sIP.Image = oIP.Image;
    }

    public static Paper Re_ConstructPaper(Paper oPaper, SubmitPaper submitPaper)
    {
      Paper spaper = submitPaper.SPaper;
      foreach (Passage readingQuestion in spaper.ReadingQuestions)
      {
        Passage passage = QuestionHelper.GetPassage(oPaper, readingQuestion.PID);
        readingQuestion.QBID = passage.QBID;
        readingQuestion.Text = passage.Text;
        readingQuestion.CourseId = passage.CourseId;
        foreach (Question passageQuestion1 in readingQuestion.PassageQuestions)
        {
          foreach (Question passageQuestion2 in passage.PassageQuestions)
          {
            if (QuestionHelper.ReConstructQuestion(passageQuestion1, passageQuestion2))
              break;
          }
        }
      }
      foreach (MatchQuestion matchQuestion1 in spaper.MatchQuestions)
      {
        foreach (MatchQuestion matchQuestion2 in oPaper.MatchQuestions)
        {
          if (matchQuestion1.MID == matchQuestion2.MID)
          {
            matchQuestion1.QBID = matchQuestion2.QBID;
            matchQuestion1.CourseId = matchQuestion2.CourseId;
            matchQuestion1.ColumnA = matchQuestion2.ColumnA;
            matchQuestion1.ColumnB = matchQuestion2.ColumnB;
            matchQuestion1.Solution = matchQuestion2.Solution;
            matchQuestion1.Mark = matchQuestion2.Mark;
            break;
          }
        }
      }
      foreach (Question grammarQuestion1 in spaper.GrammarQuestions)
      {
        foreach (Question grammarQuestion2 in oPaper.GrammarQuestions)
        {
          if (QuestionHelper.ReConstructQuestion(grammarQuestion1, grammarQuestion2))
            break;
        }
      }
      foreach (Question indicateMquestion1 in spaper.IndicateMQuestions)
      {
        foreach (Question indicateMquestion2 in oPaper.IndicateMQuestions)
        {
          if (QuestionHelper.ReConstructQuestion(indicateMquestion1, indicateMquestion2))
            break;
        }
      }
      foreach (Question fillBlankQuestion1 in spaper.FillBlankQuestions)
      {
        foreach (Question fillBlankQuestion2 in oPaper.FillBlankQuestions)
        {
          if (QuestionHelper.ReConstructQuestion(fillBlankQuestion1, fillBlankQuestion2))
            break;
        }
      }
      if (oPaper.EssayQuestion != null)
        QuestionHelper.ReConstructEssay(spaper.EssayQuestion, oPaper.EssayQuestion);
      if (oPaper.ImgPaper != null)
        QuestionHelper.ReConstructImagePaper(spaper.ImgPaper, oPaper.ImgPaper);
      spaper.OneSecSilence = oPaper.OneSecSilence;
      spaper.ListAudio = oPaper.ListAudio;
      return spaper;
    }

    public static string RemoveSpaces(string s)
    {
      s = s.Trim();
      string str;
      do
      {
        str = s;
        s = s.Replace("  ", " ");
      }
      while (s.Length != str.Length);
      return s;
    }

    public static string RemoveAllSpaces(string s)
    {
      s = s.Trim();
      string str;
      do
      {
        str = s;
        s = s.Replace(" ", "");
      }
      while (s.Length != str.Length);
      return s;
    }

    public static bool IsFillBlank(QuestionType qt)
    {
      switch (qt)
      {
        case QuestionType.FILL_BLANK_ALL:
          return true;
        case QuestionType.FILL_BLANK_GROUP:
          return true;
        case QuestionType.FILL_BLANK_EMPTY:
          return true;
        default:
          return false;
      }
    }

    private static string RemoveNewLine(string s)
    {
      s = s.Replace(Environment.NewLine, "");
      s = QuestionHelper.RemoveSpaces(s);
      return s;
    }

    public static string WordWrap(string str, int width)
    {
      Regex regex1 = new Regex(QuestionHelper.fillBlank_Pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
      MatchCollection matchCollection = regex1.Matches(str);
      str = regex1.Replace(str, "(###)");
      string[] strArray = QuestionHelper.SplitLines(str);
      StringBuilder stringBuilder = new StringBuilder();
      for (int index1 = 0; index1 < strArray.Length; ++index1)
      {
        string str1 = strArray[index1];
        if (index1 < strArray.Length - 1)
          str1 = strArray[index1] + Environment.NewLine;
        ArrayList arrayList = QuestionHelper.Explode(str1, QuestionHelper.splitChars);
        int num = 0;
        for (int index2 = 0; index2 < arrayList.Count; ++index2)
        {
          string str2 = (string) arrayList[index2];
          if (num + str2.Length > width)
          {
            if (num > 0)
            {
              if (!stringBuilder.ToString().EndsWith(Environment.NewLine))
                stringBuilder.Append(Environment.NewLine);
              num = 0;
            }
            while (str2.Length > width)
            {
              stringBuilder.Append(str2.Substring(0, width - 1) + "-");
              str2 = str2.Substring(width - 1);
              if (!stringBuilder.ToString().EndsWith(Environment.NewLine))
                stringBuilder.Append(Environment.NewLine);
              stringBuilder.Append(Environment.NewLine);
            }
            str2 = str2.TrimStart();
          }
          stringBuilder.Append(str2);
          num += str2.Length;
        }
      }
      str = stringBuilder.ToString();
      Regex regex2 = new Regex("\\(###\\)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
      for (int i = 0; i < matchCollection.Count; ++i)
      {
        string replacement = QuestionHelper.RemoveNewLine(matchCollection[i].Value);
        str = regex2.Replace(str, replacement, 1);
      }
      return str;
    }

    private static ArrayList Explode(string str, char[] splitChars)
    {
      ArrayList arrayList = new ArrayList();
      int startIndex1 = 0;
      while (true)
      {
        int startIndex2 = str.IndexOfAny(splitChars, startIndex1);
        if (startIndex2 != -1)
        {
          string str1 = str.Substring(startIndex1, startIndex2 - startIndex1);
          char c = str.Substring(startIndex2, 1)[0];
          if (char.IsWhiteSpace(c))
          {
            arrayList.Add((object) str1);
            arrayList.Add((object) c.ToString());
          }
          else
            arrayList.Add((object) (str1 + c.ToString()));
          startIndex1 = startIndex2 + 1;
        }
        else
          break;
      }
      arrayList.Add((object) str.Substring(startIndex1));
      return arrayList;
    }

    private static string[] SplitLines(string str)
    {
      return new Regex(Environment.NewLine, RegexOptions.IgnoreCase | RegexOptions.Compiled).Split(str);
    }

    public static string[] GetFillBlankWord(string text)
    {
      MatchCollection matchCollection = new Regex(QuestionHelper.fillBlank_Pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).Matches(text);
      string[] fillBlankWord = new string[matchCollection.Count];
      for (int i = 0; i < matchCollection.Count; ++i)
      {
        string str1 = matchCollection[i].Value.Remove(0, 1);
        string str2 = str1.Remove(str1.Length - 1, 1);
        fillBlankWord[i] = str2;
      }
      return fillBlankWord;
    }

    public static string Sec2TimeString(int sec)
    {
      int num1 = sec / 3600;
      int num2 = sec % 3600 / 60;
      int num3 = sec % 60;
      string str1 = "0" + (object) num1;
      string str2 = str1.Substring(str1.Length - 2, 2);
      string str3 = "0" + (object) num2;
      string str4 = str3.Substring(str3.Length - 2, 2);
      string str5 = "0" + (object) num3;
      string str6 = str5.Substring(str5.Length - 2, 2);
      return str2 + ":" + str4 + ":" + str6;
    }
  }
}
