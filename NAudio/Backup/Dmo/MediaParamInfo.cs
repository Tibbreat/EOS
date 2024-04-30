// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.MediaParamInfo
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Dmo
{
  internal struct MediaParamInfo
  {
    public MediaParamType mpType;
    public MediaParamCurveType mopCaps;
    public float mpdMinValue;
    public float mpdMaxValue;
    public float mpdNeutralValue;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string szUnitText;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string szLabel;
  }
}
