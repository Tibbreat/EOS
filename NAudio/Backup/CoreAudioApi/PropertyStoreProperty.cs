// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.PropertyStoreProperty
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class PropertyStoreProperty
  {
    private readonly PropertyKey propertyKey;
    private PropVariant propertyValue;

    internal PropertyStoreProperty(PropertyKey key, PropVariant value)
    {
      this.propertyKey = key;
      this.propertyValue = value;
    }

    public PropertyKey Key => this.propertyKey;

    public object Value => this.propertyValue.Value;
  }
}
