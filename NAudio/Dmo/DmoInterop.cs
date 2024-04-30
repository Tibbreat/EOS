// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.DmoInterop
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace NAudio.Dmo
{
  internal static class DmoInterop
  {
    [DllImport("msdmo.dll")]
    public static extern int DMOEnum(
      [In] ref Guid guidCategory,
      DmoEnumFlags flags,
      int inTypes,
      [In] DmoPartialMediaType[] inTypesArray,
      int outTypes,
      [In] DmoPartialMediaType[] outTypesArray,
      out IEnumDmo enumDmo);

    [DllImport("msdmo.dll")]
    public static extern int MoFreeMediaType([In] ref DmoMediaType mediaType);

    [DllImport("msdmo.dll")]
    public static extern int MoInitMediaType([In, Out] ref DmoMediaType mediaType, int formatBlockBytes);

    [DllImport("msdmo.dll")]
    public static extern int DMOGetName([In] ref Guid clsidDMO, [Out] StringBuilder name);
  }
}
