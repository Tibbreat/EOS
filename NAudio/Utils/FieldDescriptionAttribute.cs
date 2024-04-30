// Decompiled with JetBrains decompiler
// Type: NAudio.Utils.FieldDescriptionAttribute
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Utils
{
  [AttributeUsage(AttributeTargets.Field)]
  public class FieldDescriptionAttribute : Attribute
  {
    public string Description { get; private set; }

    public FieldDescriptionAttribute(string description) => this.Description = description;

    public override string ToString() => this.Description;
  }
}
