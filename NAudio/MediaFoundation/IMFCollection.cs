// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.IMFCollection
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.MediaFoundation
{
  [Guid("5BC8A76B-869A-46A3-9B03-FA218A66AEBE")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IMFCollection
  {
    void GetElementCount(out int pcElements);

    void GetElement([In] int dwElementIndex, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnkElement);

    void AddElement([MarshalAs(UnmanagedType.IUnknown), In] object pUnkElement);

    void RemoveElement([In] int dwElementIndex, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnkElement);

    void InsertElementAt([In] int dwIndex, [MarshalAs(UnmanagedType.IUnknown), In] object pUnknown);

    void RemoveAllElements();
  }
}
