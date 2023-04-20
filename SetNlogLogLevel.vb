'C# source https://gist.github.com/pmullins/f21c3d83e96b9fd8a720
' <summary>
' Reconfigures the NLog logging level.
' </summary>
' <paramname="level">The <seecref="LogLevel"/> to be set.</param>
Private Shared Sub SetNlogLogLevel(ByVal level As LogLevel)
  ' Uncomment these to enable NLog logging. NLog exceptions are swallowed by default.
  ' NLog.Common.InternalLogger.LogFile = @"C:\Temp\nlog.debug.log";
  ' NLog.Common.InternalLogger.LogLevel = LogLevel.Debug;

    If level Is LogLevel.Off Then
        LogManager.DisableLogging()
    Else
        If Not LogManager.IsLoggingEnabled() Then
            LogManager.EnableLogging()
        End If

        For Each rule In LogManager.Configuration.LoggingRules
            ' Iterate over all levels up to and including the target, (re)enabling them.
            For i = level.Ordinal To 5
                rule.EnableLoggingForLevel(LogLevel.FromOrdinal(i))
            Next
        Next
    End If

    LogManager.ReconfigExistingLoggers()
End Sub
