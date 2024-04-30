// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveCapabilitiesHelpers
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using Microsoft.Win32;
using System;

#nullable disable
namespace NAudio.Wave
{
  internal static class WaveCapabilitiesHelpers
  {
    public static readonly Guid MicrosoftDefaultManufacturerId = new Guid("d5a47fa8-6d98-11d1-a21a-00a0c9223196");
    public static readonly Guid DefaultWaveOutGuid = new Guid("E36DC310-6D9A-11D1-A21A-00A0C9223196");
    public static readonly Guid DefaultWaveInGuid = new Guid("E36DC311-6D9A-11D1-A21A-00A0C9223196");

    public static string GetNameFromGuid(Guid guid)
    {
      string nameFromGuid = (string) null;
      using (RegistryKey registryKey1 = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\MediaCategories"))
      {
        using (RegistryKey registryKey2 = registryKey1.OpenSubKey(guid.ToString("B")))
        {
          if (registryKey2 != null)
            nameFromGuid = registryKey2.GetValue("Name") as string;
        }
      }
      return nameFromGuid;
    }
  }
}
