// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.DmoDescriptor
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Dmo
{
  public class DmoDescriptor
  {
    public string Name { get; private set; }

    public Guid Clsid { get; private set; }

    public DmoDescriptor(string name, Guid clsid)
    {
      this.Name = name;
      this.Clsid = clsid;
    }
  }
}
