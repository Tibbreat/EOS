// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.MFT_REGISTER_TYPE_INFO
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.MediaFoundation
{
  [StructLayout(LayoutKind.Sequential)]
  public class MFT_REGISTER_TYPE_INFO
  {
    public Guid guidMajorType;
    public Guid guidSubtype;
  }
}
