// Decompiled with JetBrains decompiler
// Type: EOSClient.Properties.Resources
// Assembly: EOSClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7FF5B269-3EA1-40F1-AEFB-0F2D36888412
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\EOSClient.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace EOSClient.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (EOSClient.Properties.Resources.resourceMan == null)
          EOSClient.Properties.Resources.resourceMan = new ResourceManager("EOSClient.Properties.Resources", typeof (EOSClient.Properties.Resources).Assembly);
        return EOSClient.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => EOSClient.Properties.Resources.resourceCulture;
      set => EOSClient.Properties.Resources.resourceCulture = value;
    }
  }
}
