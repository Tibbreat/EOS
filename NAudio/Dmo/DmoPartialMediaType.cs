// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.DmoPartialMediaType
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Dmo
{
  internal struct DmoPartialMediaType
  {
    private Guid type;
    private Guid subtype;

    public Guid Type
    {
      get => this.type;
      internal set => this.type = value;
    }

    public Guid Subtype
    {
      get => this.subtype;
      internal set => this.subtype = value;
    }
  }
}
