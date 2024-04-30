// Decompiled with JetBrains decompiler
// Type: NAudio.Utils.FieldDescriptionHelper
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Reflection;

#nullable disable
namespace NAudio.Utils
{
  public static class FieldDescriptionHelper
  {
    public static string Describe(Type t, Guid guid)
    {
      foreach (FieldInfo field in t.GetFields(BindingFlags.Static | BindingFlags.Public))
      {
        if (field.IsPublic && field.IsStatic && field.FieldType == typeof (Guid) && (Guid) field.GetValue((object) null) == guid)
        {
          foreach (object customAttribute in field.GetCustomAttributes(false))
          {
            if (customAttribute is FieldDescriptionAttribute descriptionAttribute)
              return descriptionAttribute.Description;
          }
          return field.Name;
        }
      }
      return guid.ToString();
    }
  }
}
