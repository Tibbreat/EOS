// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.MediaParamCurveType
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Dmo
{
  [Flags]
  internal enum MediaParamCurveType
  {
    MP_CURVE_JUMP = 1,
    MP_CURVE_LINEAR = 2,
    MP_CURVE_SQUARE = 4,
    MP_CURVE_INVSQUARE = 8,
    MP_CURVE_SINE = 16, // 0x00000010
  }
}
