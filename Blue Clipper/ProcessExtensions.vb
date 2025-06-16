Module ProcessExtensions



    Private Enum ProcessAccessFlags As UInteger
        All = &H1FFFFF
        Terminate = &H1
        CreateThread = &H2
        VMOperation = &H8
        VMRead = &H10
        VMWrite = &H20
        DupHandle = &H40
        SetInformation = &H200
        QueryInformation = &H400
        QueryLimitedInformation = &H1000
        Synchronize = &H100000
    End Enum
    <System.Runtime.InteropServices.DllImport("kernel32.dll")>
    Private Function OpenProcess(dwDesiredAccess As ProcessAccessFlags, bInheritHandle As Boolean, dwProcessId As Integer) As IntPtr : End Function
    <System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError:=True)>
    Private Function CloseHandle(hHandle As IntPtr) As Boolean : End Function
    <System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError:=True)>
    Private Function GetExitCodeProcess(hHandle As IntPtr, ByRef eCode As Integer) As Boolean : End Function


    <System.Runtime.CompilerServices.Extension()>
    Public Function HasExitedSafe(ByVal this As Process) As Boolean
        Dim exitCode As Integer = 0
        Dim processHandle As IntPtr = OpenProcess(ProcessAccessFlags.QueryLimitedInformation, False, this.Id)
        Try
            If processHandle <> IntPtr.Zero AndAlso GetExitCodeProcess(processHandle, exitCode) Then Return exitCode <> 259
        Catch
            Debug.Print("Exception on HasExitedSafe")
        Finally
            CloseHandle(processHandle)
        End Try
        Return True
    End Function
End Module
