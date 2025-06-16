Imports System.Runtime.InteropServices

Public Enum InputHookType
    Mouse_LL = 14
    Keyboard_LL = 13
End Enum
Public Delegate Function HookProc(nCode As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr

Public Enum MouseState As Integer
    MouseMove = &H200     ' WM_MOUSEMOVE

    LButtonDown = &H201   ' WM_LBUTTONDOWN
    LButtonUp = &H202     ' WM_LBUTTONUP
    'LBUTTONDBLCLK = &H203 ' WM_LBUTTONDBLCLK 

    RButtonDown = &H204   ' WM_RBUTTONDOWN
    RButtonUp = &H205     ' WM_RBUTTONUP
    'RBUTTONDBLCLK = &H206 ' WM_RBUTTONDBLCLK

    MButtonDown = &H207   ' WM_MBUTTONDOWN
    MButtonUp = &H208     ' WM_MBUTTONUP
    'MBUTTONDBLCLK = &H209 ' WM_MBUTTONDBLCLK

    MouseWheel = &H20A    ' WM_MOUSEWHEEL

    XButtonDown = &H20B   ' WM_XBUTTONDOWN
    XButtonUp = &H20C     ' WM_XBUTTONUP
    'XButtonDBLCLK = &H20D ' WM_XBUTTONDBLCLK

    MouseHWheel = &H20E   ' WM_MOUSEHWHEEL
End Enum

Public Enum KeyState
    KeyDown = &H100   ' WM_KEYDOWN
    KeyUp = &H101     ' WM_KEYUP
    SysKeyDown = &H104 ' WM_SYSKEYDOWN
    SysKeyUp = &H105   ' WM_SYSKEYUP
End Enum

<StructLayout(LayoutKind.Sequential)>
Public Structure MSLLHOOKSTRUCT
    Public pt As Point
    Public mousedata As UInteger
    Public flags As UInteger
    Public time As UInteger
    Public dwExtraInfo As IntPtr
End Structure
<StructLayout(LayoutKind.Sequential)>
Public Structure KBDLLHOOKSTRUCT
    Public vkCode As UInteger
    Public scanCode As UInteger
    Public flags As UInteger
    Public time As UInteger
    Public dwExtraInfo As IntPtr
End Structure

Public Class InputHook
    Implements IDisposable

    Private _callback As HookProc
    Private _hookId As IntPtr = IntPtr.Zero

    Private _hookThread As Threading.Thread
    Private _hookThreadId As UInteger
    Private _stopRequested As Boolean = False
    Private _hookType As InputHookType

    Public ReadOnly Property HookHandle As IntPtr
        Get
            Return _hookId
        End Get
    End Property

    ' Windows API functions
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SetWindowsHookEx(idHook As Integer, lpfn As HookProc, hMod As IntPtr, dwThreadId As UInteger) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function UnhookWindowsHookEx(hhk As IntPtr) As Boolean
    End Function

    <Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function CallNextHookEx(hhk As IntPtr, nCode As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function

    <DllImport("kernel32.dll")>
    Public Shared Function GetCurrentThreadId() As UInteger : End Function
    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function GetModuleHandle(lpModuleName As String) As IntPtr
    End Function

    Dim hInstance As IntPtr = GetModuleHandle(Nothing)

    Public Sub New(hookType As InputHookType, callback As HookProc)
        _callback = callback
        _hookType = hookType
        _hookThread = New Threading.Thread(AddressOf HookThreadProc)
        _hookThread.IsBackground = True
        _hookThread.Start()
    End Sub

    Private Sub HookThreadProc()

        _hookId = SetWindowsHookEx(_hookType, _callback, hInstance, 0)
        If _hookId = IntPtr.Zero Then
            Throw New System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error())
        End If

        _hookThreadId = GetCurrentThreadId()
        ' Message loop required for hook to work
        Dim msg As New NativeMessage()
        While Not _stopRequested AndAlso GetMessage(msg, IntPtr.Zero, 0, 0)
            ' You can do additional processing here if needed
            TranslateMessage(msg)
            DispatchMessage(msg)
        End While

        If _hookId <> IntPtr.Zero Then
            UnhookWindowsHookEx(_hookId)
            _hookId = IntPtr.Zero
        End If
    End Sub

    Public Sub UnHook()
        If _hookId <> IntPtr.Zero Then
            UnhookWindowsHookEx(_hookId)
            _hookId = IntPtr.Zero
        End If
    End Sub

    Public Function NextHook(nCode As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr
        Return CallNextHookEx(Me.HookHandle, nCode, wParam, lParam)
    End Function

    ' Native message structure and functions for message loop
    <StructLayout(LayoutKind.Sequential)>
    Private Structure NativeMessage
        Public hwnd As IntPtr
        Public message As UInteger
        Public wParam As IntPtr
        Public lParam As IntPtr
        Public time As UInteger
        Public pt_x As Integer
        Public pt_y As Integer
    End Structure

    <DllImport("user32.dll")>
    Private Shared Function GetMessage(ByRef lpMsg As NativeMessage, hWnd As IntPtr, wMsgFilterMin As UInteger, wMsgFilterMax As UInteger) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function TranslateMessage(ByRef lpMsg As NativeMessage) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function DispatchMessage(ByRef lpMsg As NativeMessage) As IntPtr
    End Function

#Region "iDisposable"

    <DllImport("user32.dll")>
    Private Shared Function PostThreadMessage(idThread As UInteger, Msg As UInteger, wParam As IntPtr, lParam As IntPtr) As Boolean
    End Function

    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            _stopRequested = True

            ' Explicitly unhook before signaling the thread to exit
            If _hookId <> IntPtr.Zero Then
                UnhookWindowsHookEx(_hookId)
                _hookId = IntPtr.Zero
            End If

            If _hookThread IsNot Nothing AndAlso _hookThread.IsAlive Then

                ' Post WM_QUIT to the message loop to exit GetMessage
                PostThreadMessage(_hookThreadId, &H12UI, IntPtr.Zero, IntPtr.Zero) ' WM_QUIT = &H12
                _hookThread.Join()
            End If
            disposedValue = True
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(disposing:=False)
        MyBase.Finalize()
    End Sub
#End Region
End Class
