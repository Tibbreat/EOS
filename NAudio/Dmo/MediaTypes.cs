// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.MediaTypes
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Dmo
{
  internal static class MediaTypes
  {
    public static readonly Guid MEDIATYPE_AnalogAudio = new Guid("0482DEE1-7817-11cf-8a03-00aa006ecb65");
    public static readonly Guid MEDIATYPE_AnalogVideo = new Guid("0482DDE1-7817-11cf-8A03-00AA006ECB65");
    public static readonly Guid MEDIATYPE_Audio = new Guid("73647561-0000-0010-8000-00AA00389B71");
    public static readonly Guid MEDIATYPE_AUXLine21Data = new Guid("670AEA80-3A82-11d0-B79B-00AA003767A7");
    public static readonly Guid MEDIATYPE_File = new Guid("656c6966-0000-0010-8000-00AA00389B71");
    public static readonly Guid MEDIATYPE_Interleaved = new Guid("73766169-0000-0010-8000-00AA00389B71");
    public static readonly Guid MEDIATYPE_Midi = new Guid("7364696D-0000-0010-8000-00AA00389B71");
    public static readonly Guid MEDIATYPE_ScriptCommand = new Guid("73636d64-0000-0010-8000-00AA00389B71");
    public static readonly Guid MEDIATYPE_Stream = new Guid("e436eb83-524f-11ce-9f53-0020af0ba770");
    public static readonly Guid MEDIATYPE_Text = new Guid("73747874-0000-0010-8000-00AA00389B71");
    public static readonly Guid MEDIATYPE_Timecode = new Guid("0482DEE3-7817-11cf-8a03-00aa006ecb65");
    public static readonly Guid MEDIATYPE_Video = new Guid("73646976-0000-0010-8000-00AA00389B71");
    public static readonly Guid[] MajorTypes = new Guid[12]
    {
      MediaTypes.MEDIATYPE_AnalogAudio,
      MediaTypes.MEDIATYPE_AnalogVideo,
      MediaTypes.MEDIATYPE_Audio,
      MediaTypes.MEDIATYPE_AUXLine21Data,
      MediaTypes.MEDIATYPE_File,
      MediaTypes.MEDIATYPE_Interleaved,
      MediaTypes.MEDIATYPE_Midi,
      MediaTypes.MEDIATYPE_ScriptCommand,
      MediaTypes.MEDIATYPE_Stream,
      MediaTypes.MEDIATYPE_Text,
      MediaTypes.MEDIATYPE_Timecode,
      MediaTypes.MEDIATYPE_Video
    };
    public static readonly string[] MajorTypeNames = new string[12]
    {
      "Analog Audio",
      "Analog Video",
      "Audio",
      "AUXLine21Data",
      "File",
      "Interleaved",
      "Midi",
      "ScriptCommand",
      "Stream",
      "Text",
      "Timecode",
      "Video"
    };

    public static string GetMediaTypeName(Guid majorType)
    {
      for (int index = 0; index < MediaTypes.MajorTypes.Length; ++index)
      {
        if (majorType == MediaTypes.MajorTypes[index])
          return MediaTypes.MajorTypeNames[index];
      }
      throw new ArgumentException("Major Type not found");
    }
  }
}
