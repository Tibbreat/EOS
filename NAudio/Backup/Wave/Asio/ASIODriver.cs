// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Asio.ASIODriver
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using Microsoft.Win32;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace NAudio.Wave.Asio
{
  internal class ASIODriver
  {
    private IntPtr pASIOComObject;
    private IntPtr pinnedcallbacks;
    private ASIODriver.ASIODriverVTable asioDriverVTable;

    private ASIODriver()
    {
    }

    public static string[] GetASIODriverNames()
    {
      RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ASIO");
      string[] asioDriverNames = new string[0];
      if (registryKey != null)
      {
        asioDriverNames = registryKey.GetSubKeyNames();
        registryKey.Close();
      }
      return asioDriverNames;
    }

    public static ASIODriver GetASIODriverByName(string name)
    {
      return ASIODriver.GetASIODriverByGuid(new Guid((Registry.LocalMachine.OpenSubKey("SOFTWARE\\ASIO\\" + name) ?? throw new ArgumentException(string.Format("Driver Name {0} doesn't exist", (object) name))).GetValue("CLSID").ToString()));
    }

    public static ASIODriver GetASIODriverByGuid(Guid guid)
    {
      ASIODriver asioDriverByGuid = new ASIODriver();
      asioDriverByGuid.initFromGuid(guid);
      return asioDriverByGuid;
    }

    public bool init(IntPtr sysHandle)
    {
      return this.asioDriverVTable.init(this.pASIOComObject, sysHandle) == 1;
    }

    public string getDriverName()
    {
      StringBuilder name = new StringBuilder(256);
      this.asioDriverVTable.getDriverName(this.pASIOComObject, name);
      return name.ToString();
    }

    public int getDriverVersion() => this.asioDriverVTable.getDriverVersion(this.pASIOComObject);

    public string getErrorMessage()
    {
      StringBuilder errorMessage = new StringBuilder(256);
      this.asioDriverVTable.getErrorMessage(this.pASIOComObject, errorMessage);
      return errorMessage.ToString();
    }

    public void start()
    {
      this.handleException(this.asioDriverVTable.start(this.pASIOComObject), nameof (start));
    }

    public ASIOError stop() => this.asioDriverVTable.stop(this.pASIOComObject);

    public void getChannels(out int numInputChannels, out int numOutputChannels)
    {
      this.handleException(this.asioDriverVTable.getChannels(this.pASIOComObject, out numInputChannels, out numOutputChannels), nameof (getChannels));
    }

    public ASIOError GetLatencies(out int inputLatency, out int outputLatency)
    {
      return this.asioDriverVTable.getLatencies(this.pASIOComObject, out inputLatency, out outputLatency);
    }

    public void getBufferSize(
      out int minSize,
      out int maxSize,
      out int preferredSize,
      out int granularity)
    {
      this.handleException(this.asioDriverVTable.getBufferSize(this.pASIOComObject, out minSize, out maxSize, out preferredSize, out granularity), nameof (getBufferSize));
    }

    public bool canSampleRate(double sampleRate)
    {
      ASIOError error = this.asioDriverVTable.canSampleRate(this.pASIOComObject, sampleRate);
      switch (error)
      {
        case ASIOError.ASE_NoClock:
          return false;
        case ASIOError.ASE_OK:
          return true;
        default:
          this.handleException(error, nameof (canSampleRate));
          return false;
      }
    }

    public double getSampleRate()
    {
      double sampleRate;
      this.handleException(this.asioDriverVTable.getSampleRate(this.pASIOComObject, out sampleRate), nameof (getSampleRate));
      return sampleRate;
    }

    public void setSampleRate(double sampleRate)
    {
      this.handleException(this.asioDriverVTable.setSampleRate(this.pASIOComObject, sampleRate), nameof (setSampleRate));
    }

    public void getClockSources(out long clocks, int numSources)
    {
      this.handleException(this.asioDriverVTable.getClockSources(this.pASIOComObject, out clocks, numSources), nameof (getClockSources));
    }

    public void setClockSource(int reference)
    {
      this.handleException(this.asioDriverVTable.setClockSource(this.pASIOComObject, reference), "setClockSources");
    }

    public void getSamplePosition(out long samplePos, ref ASIO64Bit timeStamp)
    {
      this.handleException(this.asioDriverVTable.getSamplePosition(this.pASIOComObject, out samplePos, ref timeStamp), nameof (getSamplePosition));
    }

    public ASIOChannelInfo getChannelInfo(int channelNumber, bool trueForInputInfo)
    {
      ASIOChannelInfo info = new ASIOChannelInfo()
      {
        channel = channelNumber,
        isInput = trueForInputInfo
      };
      this.handleException(this.asioDriverVTable.getChannelInfo(this.pASIOComObject, ref info), nameof (getChannelInfo));
      return info;
    }

    public void createBuffers(
      IntPtr bufferInfos,
      int numChannels,
      int bufferSize,
      ref ASIOCallbacks callbacks)
    {
      this.pinnedcallbacks = Marshal.AllocHGlobal(Marshal.SizeOf((object) callbacks));
      Marshal.StructureToPtr((object) callbacks, this.pinnedcallbacks, false);
      this.handleException(this.asioDriverVTable.createBuffers(this.pASIOComObject, bufferInfos, numChannels, bufferSize, this.pinnedcallbacks), nameof (createBuffers));
    }

    public ASIOError disposeBuffers()
    {
      ASIOError asioError = this.asioDriverVTable.disposeBuffers(this.pASIOComObject);
      Marshal.FreeHGlobal(this.pinnedcallbacks);
      return asioError;
    }

    public void controlPanel()
    {
      this.handleException(this.asioDriverVTable.controlPanel(this.pASIOComObject), nameof (controlPanel));
    }

    public void future(int selector, IntPtr opt)
    {
      this.handleException(this.asioDriverVTable.future(this.pASIOComObject, selector, opt), nameof (future));
    }

    public ASIOError outputReady() => this.asioDriverVTable.outputReady(this.pASIOComObject);

    public void ReleaseComASIODriver() => Marshal.Release(this.pASIOComObject);

    private void handleException(ASIOError error, string methodName)
    {
      if (error != ASIOError.ASE_OK && error != ASIOError.ASE_SUCCESS)
        throw new ASIOException(string.Format("Error code [{0}] while calling ASIO method <{1}>, {2}", (object) ASIOException.getErrorName(error), (object) methodName, (object) this.getErrorMessage()))
        {
          Error = error
        };
    }

    private void initFromGuid(Guid ASIOGuid)
    {
      int instance = ASIODriver.CoCreateInstance(ref ASIOGuid, IntPtr.Zero, 1U, ref ASIOGuid, out this.pASIOComObject);
      if (instance != 0)
        throw new COMException("Unable to instantiate ASIO. Check if STAThread is set", instance);
      IntPtr ptr = Marshal.ReadIntPtr(this.pASIOComObject);
      this.asioDriverVTable = new ASIODriver.ASIODriverVTable();
      FieldInfo[] fields = typeof (ASIODriver.ASIODriverVTable).GetFields();
      for (int index = 0; index < fields.Length; ++index)
      {
        FieldInfo fieldInfo = fields[index];
        object forFunctionPointer = (object) Marshal.GetDelegateForFunctionPointer(Marshal.ReadIntPtr(ptr, (index + 3) * IntPtr.Size), fieldInfo.FieldType);
        fieldInfo.SetValue((object) this.asioDriverVTable, forFunctionPointer);
      }
    }

    [DllImport("ole32.Dll")]
    private static extern int CoCreateInstance(
      ref Guid clsid,
      IntPtr inner,
      uint context,
      ref Guid uuid,
      out IntPtr rReturnedComObject);

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    private class ASIODriverVTable
    {
      public ASIODriver.ASIODriverVTable.ASIOInit init;
      public ASIODriver.ASIODriverVTable.ASIOgetDriverName getDriverName;
      public ASIODriver.ASIODriverVTable.ASIOgetDriverVersion getDriverVersion;
      public ASIODriver.ASIODriverVTable.ASIOgetErrorMessage getErrorMessage;
      public ASIODriver.ASIODriverVTable.ASIOstart start;
      public ASIODriver.ASIODriverVTable.ASIOstop stop;
      public ASIODriver.ASIODriverVTable.ASIOgetChannels getChannels;
      public ASIODriver.ASIODriverVTable.ASIOgetLatencies getLatencies;
      public ASIODriver.ASIODriverVTable.ASIOgetBufferSize getBufferSize;
      public ASIODriver.ASIODriverVTable.ASIOcanSampleRate canSampleRate;
      public ASIODriver.ASIODriverVTable.ASIOgetSampleRate getSampleRate;
      public ASIODriver.ASIODriverVTable.ASIOsetSampleRate setSampleRate;
      public ASIODriver.ASIODriverVTable.ASIOgetClockSources getClockSources;
      public ASIODriver.ASIODriverVTable.ASIOsetClockSource setClockSource;
      public ASIODriver.ASIODriverVTable.ASIOgetSamplePosition getSamplePosition;
      public ASIODriver.ASIODriverVTable.ASIOgetChannelInfo getChannelInfo;
      public ASIODriver.ASIODriverVTable.ASIOcreateBuffers createBuffers;
      public ASIODriver.ASIODriverVTable.ASIOdisposeBuffers disposeBuffers;
      public ASIODriver.ASIODriverVTable.ASIOcontrolPanel controlPanel;
      public ASIODriver.ASIODriverVTable.ASIOfuture future;
      public ASIODriver.ASIODriverVTable.ASIOoutputReady outputReady;

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate int ASIOInit(IntPtr _pUnknown, IntPtr sysHandle);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate void ASIOgetDriverName(IntPtr _pUnknown, StringBuilder name);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate int ASIOgetDriverVersion(IntPtr _pUnknown);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate void ASIOgetErrorMessage(IntPtr _pUnknown, StringBuilder errorMessage);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate ASIOError ASIOstart(IntPtr _pUnknown);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate ASIOError ASIOstop(IntPtr _pUnknown);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate ASIOError ASIOgetChannels(
        IntPtr _pUnknown,
        out int numInputChannels,
        out int numOutputChannels);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate ASIOError ASIOgetLatencies(
        IntPtr _pUnknown,
        out int inputLatency,
        out int outputLatency);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate ASIOError ASIOgetBufferSize(
        IntPtr _pUnknown,
        out int minSize,
        out int maxSize,
        out int preferredSize,
        out int granularity);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate ASIOError ASIOcanSampleRate(IntPtr _pUnknown, double sampleRate);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate ASIOError ASIOgetSampleRate(IntPtr _pUnknown, out double sampleRate);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate ASIOError ASIOsetSampleRate(IntPtr _pUnknown, double sampleRate);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate ASIOError ASIOgetClockSources(
        IntPtr _pUnknown,
        out long clocks,
        int numSources);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate ASIOError ASIOsetClockSource(IntPtr _pUnknown, int reference);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate ASIOError ASIOgetSamplePosition(
        IntPtr _pUnknown,
        out long samplePos,
        ref ASIO64Bit timeStamp);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate ASIOError ASIOgetChannelInfo(IntPtr _pUnknown, ref ASIOChannelInfo info);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate ASIOError ASIOcreateBuffers(
        IntPtr _pUnknown,
        IntPtr bufferInfos,
        int numChannels,
        int bufferSize,
        IntPtr callbacks);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate ASIOError ASIOdisposeBuffers(IntPtr _pUnknown);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate ASIOError ASIOcontrolPanel(IntPtr _pUnknown);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate ASIOError ASIOfuture(IntPtr _pUnknown, int selector, IntPtr opt);

      [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
      public delegate ASIOError ASIOoutputReady(IntPtr _pUnknown);
    }
  }
}
