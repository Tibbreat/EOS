// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.PropertyStore
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class PropertyStore
  {
    private readonly IPropertyStore storeInterface;

    public int Count
    {
      get
      {
        int propCount;
        Marshal.ThrowExceptionForHR(this.storeInterface.GetCount(out propCount));
        return propCount;
      }
    }

    public PropertyStoreProperty this[int index]
    {
      get
      {
        PropertyKey key = this.Get(index);
        PropVariant propVariant;
        Marshal.ThrowExceptionForHR(this.storeInterface.GetValue(ref key, out propVariant));
        return new PropertyStoreProperty(key, propVariant);
      }
    }

    public bool Contains(PropertyKey key)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        PropertyKey propertyKey = this.Get(index);
        if (propertyKey.formatId == key.formatId && propertyKey.propertyId == key.propertyId)
          return true;
      }
      return false;
    }

    public PropertyStoreProperty this[PropertyKey key]
    {
      get
      {
        for (int index = 0; index < this.Count; ++index)
        {
          PropertyKey key1 = this.Get(index);
          if (key1.formatId == key.formatId && key1.propertyId == key.propertyId)
          {
            PropVariant propVariant;
            Marshal.ThrowExceptionForHR(this.storeInterface.GetValue(ref key1, out propVariant));
            return new PropertyStoreProperty(key1, propVariant);
          }
        }
        return (PropertyStoreProperty) null;
      }
    }

    public PropertyKey Get(int index)
    {
      PropertyKey key;
      Marshal.ThrowExceptionForHR(this.storeInterface.GetAt(index, out key));
      return key;
    }

    public PropVariant GetValue(int index)
    {
      PropertyKey key = this.Get(index);
      PropVariant propVariant;
      Marshal.ThrowExceptionForHR(this.storeInterface.GetValue(ref key, out propVariant));
      return propVariant;
    }

    internal PropertyStore(IPropertyStore store) => this.storeInterface = store;
  }
}
