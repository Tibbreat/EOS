// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.MediaEventType
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.MediaFoundation
{
  public enum MediaEventType
  {
    MEUnknown = 0,
    MEError = 1,
    MEExtendedType = 2,
    MENonFatalError = 3,
    MESessionUnknown = 100, // 0x00000064
    MESessionTopologySet = 101, // 0x00000065
    MESessionTopologiesCleared = 102, // 0x00000066
    MESessionStarted = 103, // 0x00000067
    MESessionPaused = 104, // 0x00000068
    MESessionStopped = 105, // 0x00000069
    MESessionClosed = 106, // 0x0000006A
    MESessionEnded = 107, // 0x0000006B
    MESessionRateChanged = 108, // 0x0000006C
    MESessionScrubSampleComplete = 109, // 0x0000006D
    MESessionCapabilitiesChanged = 110, // 0x0000006E
    MESessionTopologyStatus = 111, // 0x0000006F
    MESessionNotifyPresentationTime = 112, // 0x00000070
    MENewPresentation = 113, // 0x00000071
    MELicenseAcquisitionStart = 114, // 0x00000072
    MELicenseAcquisitionCompleted = 115, // 0x00000073
    MEIndividualizationStart = 116, // 0x00000074
    MEIndividualizationCompleted = 117, // 0x00000075
    MEEnablerProgress = 118, // 0x00000076
    MEEnablerCompleted = 119, // 0x00000077
    MEPolicyError = 120, // 0x00000078
    MEPolicyReport = 121, // 0x00000079
    MEBufferingStarted = 122, // 0x0000007A
    MEBufferingStopped = 123, // 0x0000007B
    MEConnectStart = 124, // 0x0000007C
    MEConnectEnd = 125, // 0x0000007D
    MEReconnectStart = 126, // 0x0000007E
    MEReconnectEnd = 127, // 0x0000007F
    MERendererEvent = 128, // 0x00000080
    MESessionStreamSinkFormatChanged = 129, // 0x00000081
    MESourceUnknown = 200, // 0x000000C8
    MESourceStarted = 201, // 0x000000C9
    MEStreamStarted = 202, // 0x000000CA
    MESourceSeeked = 203, // 0x000000CB
    MEStreamSeeked = 204, // 0x000000CC
    MENewStream = 205, // 0x000000CD
    MEUpdatedStream = 206, // 0x000000CE
    MESourceStopped = 207, // 0x000000CF
    MEStreamStopped = 208, // 0x000000D0
    MESourcePaused = 209, // 0x000000D1
    MEStreamPaused = 210, // 0x000000D2
    MEEndOfPresentation = 211, // 0x000000D3
    MEEndOfStream = 212, // 0x000000D4
    MEMediaSample = 213, // 0x000000D5
    MEStreamTick = 214, // 0x000000D6
    MEStreamThinMode = 215, // 0x000000D7
    MEStreamFormatChanged = 216, // 0x000000D8
    MESourceRateChanged = 217, // 0x000000D9
    MEEndOfPresentationSegment = 218, // 0x000000DA
    MESourceCharacteristicsChanged = 219, // 0x000000DB
    MESourceRateChangeRequested = 220, // 0x000000DC
    MESourceMetadataChanged = 221, // 0x000000DD
    MESequencerSourceTopologyUpdated = 222, // 0x000000DE
    MESinkUnknown = 300, // 0x0000012C
    MEStreamSinkStarted = 301, // 0x0000012D
    MEStreamSinkStopped = 302, // 0x0000012E
    MEStreamSinkPaused = 303, // 0x0000012F
    MEStreamSinkRateChanged = 304, // 0x00000130
    MEStreamSinkRequestSample = 305, // 0x00000131
    MEStreamSinkMarker = 306, // 0x00000132
    MEStreamSinkPrerolled = 307, // 0x00000133
    MEStreamSinkScrubSampleComplete = 308, // 0x00000134
    MEStreamSinkFormatChanged = 309, // 0x00000135
    MEStreamSinkDeviceChanged = 310, // 0x00000136
    MEQualityNotify = 311, // 0x00000137
    MESinkInvalidated = 312, // 0x00000138
    MEAudioSessionNameChanged = 313, // 0x00000139
    MEAudioSessionVolumeChanged = 314, // 0x0000013A
    MEAudioSessionDeviceRemoved = 315, // 0x0000013B
    MEAudioSessionServerShutdown = 316, // 0x0000013C
    MEAudioSessionGroupingParamChanged = 317, // 0x0000013D
    MEAudioSessionIconChanged = 318, // 0x0000013E
    MEAudioSessionFormatChanged = 319, // 0x0000013F
    MEAudioSessionDisconnected = 320, // 0x00000140
    MEAudioSessionExclusiveModeOverride = 321, // 0x00000141
    METrustUnknown = 400, // 0x00000190
    MEPolicyChanged = 401, // 0x00000191
    MEContentProtectionMessage = 402, // 0x00000192
    MEPolicySet = 403, // 0x00000193
    MEWMDRMLicenseBackupCompleted = 500, // 0x000001F4
    MEWMDRMLicenseBackupProgress = 501, // 0x000001F5
    MEWMDRMLicenseRestoreCompleted = 502, // 0x000001F6
    MEWMDRMLicenseRestoreProgress = 503, // 0x000001F7
    MEWMDRMLicenseAcquisitionCompleted = 506, // 0x000001FA
    MEWMDRMIndividualizationCompleted = 508, // 0x000001FC
    MEWMDRMIndividualizationProgress = 513, // 0x00000201
    MEWMDRMProximityCompleted = 514, // 0x00000202
    MEWMDRMLicenseStoreCleaned = 515, // 0x00000203
    MEWMDRMRevocationDownloadCompleted = 516, // 0x00000204
    METransformUnknown = 600, // 0x00000258
    METransformNeedInput = 601, // 0x00000259
    METransformHaveOutput = 602, // 0x0000025A
    METransformDrainComplete = 603, // 0x0000025B
    METransformMarker = 604, // 0x0000025C
  }
}
