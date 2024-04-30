// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveOutSupport
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave
{
  [Flags]
  internal enum WaveOutSupport
  {
    Pitch = 1,
    PlaybackRate = 2,
    Volume = 4,
    LRVolume = 8,
    Sync = 16, // 0x00000010
    SampleAccurate = 32, // 0x00000020
  }
}
