// Decompiled with JetBrains decompiler
// Type: QuestionLib.Business.BOAudio
// Assembly: QuestionLib, Version=1.0.8848.16377, Culture=neutral, PublicKeyToken=null
// MVID: 15158C35-4197-4F45-AC31-A060C56E96B8
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\QuestionLib.dll

using NHibernate;
using QuestionLib.Entity;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

#nullable disable
namespace QuestionLib.Business
{
  public class BOAudio : BOBase
  {
    public BOAudio(ISessionFactory sessionFactory)
      : base(sessionFactory)
    {
    }

    public Audio Load(int auid)
    {
      this.session = this.sessionFactory.OpenSession();
      IList list;
      try
      {
        IQuery query = this.session.CreateQuery("from Audio a Where a.AuID=:auid");
        query.SetParameter(nameof (auid), (object) auid);
        list = query.List();
        this.session.Close();
      }
      catch (Exception ex)
      {
        this.session.Close();
        throw ex;
      }
      return list.Count > 0 ? (Audio) list[0] : (Audio) null;
    }

    public DataTable LoadAllChapterAudio(int chid, string conStr)
    {
      IList list = (IList) new ArrayList();
      SqlConnection selectConnection = new SqlConnection(conStr);
      SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT AuID, AudioFile, AudioSize, AudioLength, RepeatTime, PaddingTime, PlayOrder FROM Audio WHERE ChID = " + (object) chid, selectConnection);
      DataTable dataTable = new DataTable();
      sqlDataAdapter.Fill(dataTable);
      selectConnection.Close();
      return dataTable;
    }

    public Audio LoadAudio(int auid, string conStr)
    {
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlDataReader sqlDataReader = new SqlCommand("SELECT AudioFile, AudioSize, AudioLength, AudioData, RepeatTime, PaddingTime, PlayOrder FROM Audio WHERE AuID= " + (object) auid, connection).ExecuteReader();
      Audio audio = (Audio) null;
      if (sqlDataReader.Read())
      {
        audio = new Audio()
        {
          AudioFile = sqlDataReader["AudioFile"].ToString(),
          AudioSize = (int) sqlDataReader["AudioSize"]
        };
        audio.AudioData = new byte[audio.AudioSize];
        sqlDataReader.GetBytes(3, 0L, audio.AudioData, 0, audio.AudioSize);
        audio.AudioLength = (int) sqlDataReader["AudioLength"];
      }
      sqlDataReader.Close();
      connection.Close();
      return audio;
    }

    public System.Collections.Generic.List<AudioInPaper> LoadChapterAudio(
      int chapterID,
      string conStr)
    {
      System.Collections.Generic.List<AudioInPaper> audioInPaperList = new System.Collections.Generic.List<AudioInPaper>();
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlDataReader sqlDataReader = new SqlCommand("SELECT AudioSize, AudioData, AudioLength, RepeatTime, PaddingTime, PlayOrder FROM Audio WHERE ChID= " + (object) chapterID, connection).ExecuteReader();
      while (sqlDataReader.Read())
      {
        AudioInPaper audioInPaper = new AudioInPaper()
        {
          AudioSize = (int) sqlDataReader["AudioSize"]
        };
        audioInPaper.AudioData = new byte[audioInPaper.AudioSize];
        sqlDataReader.GetBytes(1, 0L, audioInPaper.AudioData, 0, audioInPaper.AudioSize);
        audioInPaper.AudioLength = (int) sqlDataReader["AudioLength"];
        audioInPaper.RepeatTime = (byte) sqlDataReader["RepeatTime"];
        audioInPaper.PaddingTime = (int) sqlDataReader["PaddingTime"];
        audioInPaper.PlayOrder = (byte) sqlDataReader["PlayOrder"];
        audioInPaperList.Add(audioInPaper);
      }
      sqlDataReader.Close();
      connection.Close();
      return audioInPaperList;
    }

    public static bool Delete(int auid, string conStr)
    {
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      int num = new SqlCommand("DELETE FROM Audio WHERE AuID= " + (object) auid, connection).ExecuteNonQuery();
      connection.Close();
      return num > 0;
    }

    public static bool AudioExist(int auid, string conStr)
    {
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlDataReader sqlDataReader = new SqlCommand("SELECT * FROM Audio WHERE AuID= " + (object) auid, connection).ExecuteReader();
      if (sqlDataReader.HasRows)
      {
        sqlDataReader.Close();
        connection.Close();
        return true;
      }
      sqlDataReader.Close();
      connection.Close();
      return false;
    }

    public static bool AudioFileExist(
      int chapterID,
      string audioFile,
      int audioSize,
      int audioLength,
      string conStr)
    {
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Audio WHERE ChID= @chapterID AND AudioFile=@audioFile AND AudioSize = @audioSize AND AudioLength = @audioLength", connection);
      sqlCommand.Parameters.Add(nameof (chapterID), SqlDbType.Int);
      sqlCommand.Parameters[nameof (chapterID)].Value = (object) chapterID;
      sqlCommand.Parameters.Add(nameof (audioFile), SqlDbType.NVarChar);
      sqlCommand.Parameters[nameof (audioFile)].Value = (object) audioFile;
      sqlCommand.Parameters.Add(nameof (audioSize), SqlDbType.Int);
      sqlCommand.Parameters[nameof (audioSize)].Value = (object) audioSize;
      sqlCommand.Parameters.Add(nameof (audioLength), SqlDbType.Int);
      sqlCommand.Parameters[nameof (audioLength)].Value = (object) audioLength;
      SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
      if (sqlDataReader.HasRows)
      {
        sqlDataReader.Close();
        connection.Close();
        return true;
      }
      sqlDataReader.Close();
      connection.Close();
      return false;
    }

    public static string GetAudioFile(int auid, string conStr)
    {
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlDataReader sqlDataReader = new SqlCommand("SELECT AudioFile FROM Audio WHERE AuID= " + (object) auid, connection).ExecuteReader();
      if (sqlDataReader.Read())
      {
        string audioFile = sqlDataReader["AudioFile"].ToString();
        sqlDataReader.Close();
        connection.Close();
        return audioFile;
      }
      sqlDataReader.Close();
      connection.Close();
      return (string) null;
    }

    public static string GetChapterAudioFile(int chapterID, string conStr)
    {
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlDataReader sqlDataReader = new SqlCommand("SELECT AudioFile FROM Audio WHERE ChID= " + (object) chapterID, connection).ExecuteReader();
      string chapterAudioFile = (string) null;
      while (sqlDataReader.Read())
        chapterAudioFile = chapterAudioFile != null ? chapterAudioFile + ", " + sqlDataReader["AudioFile"].ToString() : sqlDataReader["AudioFile"].ToString();
      sqlDataReader.Close();
      connection.Close();
      return chapterAudioFile;
    }

    public static int GetAudioLength(int chapterID, string conStr)
    {
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlDataReader sqlDataReader = new SqlCommand("SELECT AudioLength, RepeatTime, PaddingTime FROM Audio WHERE ChID= " + (object) chapterID, connection).ExecuteReader();
      int audioLength = 0;
      while (sqlDataReader.Read())
      {
        int int32_1 = Convert.ToInt32(sqlDataReader["AudioLength"].ToString());
        int num = (int) Convert.ToByte(sqlDataReader["RepeatTime"].ToString());
        int int32_2 = Convert.ToInt32(sqlDataReader["PaddingTime"].ToString());
        audioLength += (int32_1 + int32_2) * num;
      }
      sqlDataReader.Close();
      connection.Close();
      return audioLength;
    }

    public static int GetFileAudioLength(int auid, string conStr)
    {
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlDataReader sqlDataReader = new SqlCommand("SELECT AudioLength FROM Audio WHERE AuID= " + (object) auid, connection).ExecuteReader();
      int fileAudioLength = 0;
      if (sqlDataReader.Read())
        fileAudioLength = Convert.ToInt32(sqlDataReader["AudioLength"].ToString());
      sqlDataReader.Close();
      connection.Close();
      return fileAudioLength;
    }

    public static bool UpdateAudioPlayingInfo(System.Collections.Generic.List<Audio> listAudio, string conStr)
    {
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlCommand sqlCommand = new SqlCommand("UPDATE Audio SET RepeatTime=@repeatTime, PaddingTime=@paddingTime, PlayOrder=@playOrder WHERE AuID= @auID", connection);
      try
      {
        sqlCommand.Parameters.Add("repeatTime", SqlDbType.TinyInt);
        sqlCommand.Parameters.Add("paddingTime", SqlDbType.Int);
        sqlCommand.Parameters.Add("playOrder", SqlDbType.TinyInt);
        sqlCommand.Parameters.Add("auID", SqlDbType.Int);
        sqlCommand.Transaction = connection.BeginTransaction();
        foreach (Audio audio in listAudio)
        {
          sqlCommand.Parameters["repeatTime"].Value = (object) audio.RepeatTime;
          sqlCommand.Parameters["paddingTime"].Value = (object) audio.PaddingTime;
          sqlCommand.Parameters["playOrder"].Value = (object) audio.PlayOrder;
          sqlCommand.Parameters["auID"].Value = (object) audio.AuID;
          sqlCommand.ExecuteNonQuery();
        }
        sqlCommand.Transaction.Commit();
        connection.Close();
        return true;
      }
      catch
      {
        sqlCommand.Transaction.Rollback();
        connection.Close();
        return false;
      }
    }

    public static bool CheckAudioPlayingInfo(int chapterID, string conStr)
    {
      bool flag = true;
      SqlConnection connection = new SqlConnection(conStr);
      connection.Open();
      SqlDataReader sqlDataReader = new SqlCommand("SELECT AudioLength, RepeatTime, PaddingTime, PlayOrder FROM Audio WHERE ChID= " + (object) chapterID, connection).ExecuteReader();
      while (sqlDataReader.Read())
      {
        if (Convert.ToInt32(sqlDataReader["AudioLength"].ToString()) * (int) Convert.ToByte(sqlDataReader["RepeatTime"].ToString()) * Convert.ToInt32(sqlDataReader["PaddingTime"].ToString()) * Convert.ToInt32(sqlDataReader["PlayOrder"].ToString()) == 0)
        {
          flag = false;
          break;
        }
      }
      sqlDataReader.Close();
      connection.Close();
      return flag;
    }
  }
}
