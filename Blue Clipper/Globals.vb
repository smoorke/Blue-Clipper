Module Globals
    Public BlueProc As Process = GetBluePrinceProc()
    Public BlueThread As UInteger = 0
    Public Function GetBluePrinceProc() As Process
        Dim proc = Process.GetProcessesByName("BLUE PRINCE").FirstOrDefault(Function(p As Process) p.MainWindowTitle = "BLUE PRINCE" AndAlso GetWindowClass(p.MainWindowHandle) = "UnityWndClass")
        BlueThread = GetWindowThreadProcessId(If(proc?.MainWindowHandle, IntPtr.Zero), Nothing)
        Return proc
    End Function
End Module
