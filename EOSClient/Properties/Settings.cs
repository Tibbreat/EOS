// Decompiled with JetBrains decompiler
// Type: EOSClient.Properties.Settings
// Assembly: EOSClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7FF5B269-3EA1-40F1-AEFB-0F2D36888412
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\EOSClient.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

#nullable disable
namespace EOSClient.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.1.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings defaultInstance = Settings.defaultInstance;
        return defaultInstance;
      }
    }
  }
}
