// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.DmoOutputDataBufferFlags
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Dmo
{
  [Flags]
  public enum DmoOutputDataBufferFlags
  {
    None = 0,
    SyncPoint = 1,
    Time = 2,
    TimeLength = 4,
    Incomplete = 16777216, // 0x01000000
  }
}
