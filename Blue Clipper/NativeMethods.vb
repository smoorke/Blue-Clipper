Imports System.Runtime.InteropServices

Module NativeMethods
    <System.Runtime.InteropServices.DllImport("user32.dll", CharSet:=System.Runtime.InteropServices.CharSet.Auto)>
    Public Function GetClassName(ByVal hWnd As System.IntPtr, ByVal lpClassName As System.Text.StringBuilder, ByVal nMaxCount As Integer) As Integer : End Function
    Public Function GetWindowClass(ByVal hwnd As IntPtr) As String
        Static sClassName As New System.Text.StringBuilder("", 256)
        Call GetClassName(hwnd, sClassName, 256)
        Return sClassName.ToString
    End Function
    <Flags()> Public Enum WindowStyles As UInteger
        ''' <summary>
        ''' The window has a thin-line border.
        ''' </summary>
        WS_BORDER = &H800000

        ''' <summary>
        ''' The window has a title bar (includes the WS_BORDER style).
        ''' </summary>
        WS_CAPTION = &HC00000

        ''' <summary>
        ''' The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.
        ''' </summary>
        WS_CHILD = &H40000000

        ''' <summary>
        ''' Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window.
        ''' </summary>
        WS_CLIPCHILDREN = &H2000000

        ''' <summary>
        ''' Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated. If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window, to draw within the client area of a neighboring child window.
        ''' </summary>
        WS_CLIPSIBLINGS = &H4000000

        ''' <summary>
        ''' The window is initially disabled. A disabled window cannot receive input from the user.
        ''' </summary>
        WS_DISABLED = &H8000000

        ''' <summary>
        ''' The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar.
        ''' </summary>
        WS_DLGFRAME = &H400000

        ''' <summary>
        ''' The window is the first control of a group of controls. The group consists of this first control and all controls defined after it, up to the next control with the WS_GROUP style. The first control in each group usually has the WS_TABSTOP style so that the user can move from group to group. The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.
        ''' </summary>
        WS_GROUP = &H20000

        ''' <summary>
        ''' The window has a horizontal scroll bar.
        ''' </summary>
        WS_HSCROLL = &H100000

        ''' <summary>
        ''' The window is initially maximized.
        ''' </summary>
        WS_MAXIMIZE = &H1000000

        ''' <summary>
        ''' The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.
        ''' </summary>
        WS_MAXIMIZEBOX = &H10000

        ''' <summary>
        ''' The window is initially minimized.
        ''' </summary>
        WS_MINIMIZE = &H20000000

        ''' <summary>
        ''' The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.
        ''' </summary>
        WS_MINIMIZEBOX = &H20000

        ''' <summary>
        ''' The window is an overlapped window. An overlapped window has a title bar and a border. Same as the WS_TILED style.
        ''' </summary>
        WS_OVERLAPPED = &H0

        ''' <summary>
        ''' The window is an overlapped window. Same as the WS_TILEDWINDOW style.
        ''' </summary>
        WS_OVERLAPPEDWINDOW = WS_OVERLAPPED Or WS_CAPTION Or WS_SYSMENU Or WS_SIZEFRAME Or WS_MINIMIZEBOX Or WS_MAXIMIZEBOX

        ''' <summary>
        ''' The window is a pop-up window. This style cannot be used with the WS_CHILD style.
        ''' </summary>
        WS_POPUP = &H80000000UI

        ''' <summary>
        ''' The window is a pop-up window. The WS_BORDER, WS_POPUP, and WS_SYSMENU styles must be combined to make the window menu visible.
        ''' </summary>
        WS_POPUPWINDOW = WS_POPUP Or WS_BORDER Or WS_SYSMENU

        ''' <summary>
        ''' The window has a sizing border. Same as the WS_THICKFRAME style.
        ''' </summary>
        WS_SIZEFRAME = &H40000

        ''' <summary>
        ''' The window has a window menu on its title bar. The WS_CAPTION style must also be specified.
        ''' </summary>
        WS_SYSMENU = &H80000

        ''' <summary>
        ''' The window is a control that can receive the keyboard focus when the user presses the TAB key. Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.
        ''' </summary>
        WS_TABSTOP = &H10000

        ''' <summary>
        ''' The window is initially visible. This style can be turned on and off by using the ShowWindow or SetWindowPos function.
        ''' </summary>
        WS_VISIBLE = &H10000000

        ''' <summary>
        ''' The window has a vertical scroll bar.
        ''' </summary>
        WS_VSCROLL = &H200000
    End Enum
    <Flags()> Public Enum WindowStylesEx As UInteger
        ''' <summary>Specifies a window that accepts drag-drop files.</summary>
        WS_EX_ACCEPTFILES = &H10

        ''' <summary>Forces a top-level window onto the taskbar when the window is visible.</summary>
        WS_EX_APPWINDOW = &H40000

        ''' <summary>Specifies a window that has a border with a sunken edge.</summary>
        WS_EX_CLIENTEDGE = &H200

        ''' <summary>
        ''' Specifies a window that paints all descendants in bottom-to-top painting order using double-buffering.
        ''' This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC. This style is not supported in Windows 2000.
        ''' </summary>
        ''' <remarks>
        ''' With WS_EX_COMPOSITED set, all descendants of a window get bottom-to-top painting order using double-buffering.
        ''' Bottom-to-top painting order allows a descendent window to have translucency (alpha) and transparency (color-key) effects,
        ''' but only if the descendent window also has the WS_EX_TRANSPARENT bit set.
        ''' Double-buffering allows the window and its descendents to be painted without flicker.
        ''' </remarks>
        WS_EX_COMPOSITED = &H2000000

        ''' <summary>
        ''' Specifies a window that includes a question mark in the title bar. When the user clicks the question mark,
        ''' the cursor changes to a question mark with a pointer. If the user then clicks a child window, the child receives a WM_HELP message.
        ''' The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command.
        ''' The Help application displays a pop-up window that typically contains help for the child window.
        ''' WS_EX_CONTEXTHELP cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.
        ''' </summary>
        WS_EX_CONTEXTHELP = &H400

        ''' <summary>
        ''' Specifies a window which contains child windows that should take part in dialog box navigation.
        ''' If this style is specified, the dialog manager recurses into children of this window when performing navigation operations
        ''' such as handling the TAB key, an arrow key, or a keyboard mnemonic.
        ''' </summary>
        WS_EX_CONTROLPARENT = &H10000

        ''' <summary>Specifies a window that has a double border.</summary>
        WS_EX_DLGMODALFRAME = &H1

        ''' <summary>
        ''' Specifies a window that is a layered window.
        ''' This cannot be used for child windows or if the window has a class style of either CS_OWNDC or CS_CLASSDC.
        ''' </summary>
        WS_EX_LAYERED = &H80000

        ''' <summary>
        ''' Specifies a window with the horizontal origin on the right edge. Increasing horizontal values advance to the left.
        ''' The shell language must support reading-order alignment for this to take effect.
        ''' </summary>
        WS_EX_LAYOUTRTL = &H400000

        ''' <summary>Specifies a window that has generic left-aligned properties. This is the default.</summary>
        WS_EX_LEFT = &H0

        ''' <summary>
        ''' Specifies a window with the vertical scroll bar (if present) to the left of the client area.
        ''' The shell language must support reading-order alignment for this to take effect.
        ''' </summary>
        WS_EX_LEFTSCROLLBAR = &H4000

        ''' <summary>
        ''' Specifies a window that displays text using left-to-right reading-order properties. This is the default.
        ''' </summary>
        WS_EX_LTRREADING = &H0

        ''' <summary>
        ''' Specifies a multiple-document interface (MDI) child window.
        ''' </summary>
        WS_EX_MDICHILD = &H40

        ''' <summary>
        ''' Specifies a top-level window created with this style does not become the foreground window when the user clicks it.
        ''' The system does not bring this window to the foreground when the user minimizes or closes the foreground window.
        ''' The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the WS_EX_APPWINDOW style.
        ''' To activate the window, use the SetActiveWindow or SetForegroundWindow function.
        ''' </summary>
        WS_EX_NOACTIVATE = &H8000000

        ''' <summary>
        ''' Specifies a window which does not pass its window layout to its child windows.
        ''' </summary>
        WS_EX_NOINHERITLAYOUT = &H100000

        ''' <summary>
        ''' Specifies that a child window created with this style does not send the WM_PARENTNOTIFY message to its parent window when it is created or destroyed.
        ''' </summary>
        WS_EX_NOPARENTNOTIFY = &H4

        ''' <summary>
        ''' The window does not render to a redirection surface.
        ''' This is for windows that do not have visible content or that use mechanisms other than surfaces to provide their visual.
        ''' </summary>
        WS_EX_NOREDIRECTIONBITMAP = &H200000

        ''' <summary>Specifies an overlapped window.</summary>
        WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE Or WS_EX_CLIENTEDGE

        ''' <summary>Specifies a palette window, which is a modeless dialog box that presents an array of commands.</summary>
        WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE Or WS_EX_TOOLWINDOW Or WS_EX_TOPMOST

        ''' <summary>
        ''' Specifies a window that has generic "right-aligned" properties. This depends on the window class.
        ''' The shell language must support reading-order alignment for this to take effect.
        ''' Using the WS_EX_RIGHT style has the same effect as using the SS_RIGHT (static), ES_RIGHT (edit), and BS_RIGHT/BS_RIGHTBUTTON (button) control styles.
        ''' </summary>
        WS_EX_RIGHT = &H1000

        ''' <summary>Specifies a window with the vertical scroll bar (if present) to the right of the client area. This is the default.</summary>
        WS_EX_RIGHTSCROLLBAR = &H0

        ''' <summary>
        ''' Specifies a window that displays text using right-to-left reading-order properties.
        ''' The shell language must support reading-order alignment for this to take effect.
        ''' </summary>
        WS_EX_RTLREADING = &H2000

        ''' <summary>Specifies a window with a three-dimensional border style intended to be used for items that do not accept user input.</summary>
        WS_EX_STATICEDGE = &H20000

        ''' <summary>
        ''' Specifies a window that is intended to be used as a floating toolbar.
        ''' A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font.
        ''' A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB.
        ''' If a tool window has a system menu, its icon is not displayed on the title bar.
        ''' However, you can display the system menu by right-clicking or by typing ALT+SPACE.
        ''' </summary>
        WS_EX_TOOLWINDOW = &H80

        ''' <summary>
        ''' Specifies a window that should be placed above all non-topmost windows and should stay above them, even when the window is deactivated.
        ''' To add or remove this style, use the SetWindowPos function.
        ''' </summary>
        WS_EX_TOPMOST = &H8

        ''' <summary>
        ''' Specifies a window that should not be painted until siblings beneath the window (that were created by the same thread) have been painted.
        ''' The window appears transparent because the bits of underlying sibling windows have already been painted.
        ''' To achieve transparency without these restrictions, use the SetWindowRgn function.
        ''' </summary>
        WS_EX_TRANSPARENT = &H20

        ''' <summary>Specifies a window that has a border with a raised edge.</summary>
        WS_EX_WINDOWEDGE = &H100
    End Enum
    <StructLayout(LayoutKind.Sequential)>
    Public Structure RECT
        Public left, top, right, bottom As Integer
        Public Sub New(left As Integer, top As Integer, right As Integer, bottom As Integer)
            Me.left = left
            Me.top = top
            Me.right = right
            Me.bottom = bottom
        End Sub
        Public Sub New(ByVal rct As Rectangle)
            Me.New(rct.Left, rct.Top, rct.Right, rct.Bottom)
        End Sub

        Public Property Width() As Integer
            Get
                Return Me.right - Me.left
            End Get
            Set(value As Integer)
                Me.right = Me.left + value
            End Set
        End Property

        Public Property Height() As Integer
            Get
                Return Me.bottom - Me.top
            End Get
            Set(value As Integer)
                Me.bottom = Me.top + value
            End Set
        End Property

        Public Function ToRectangle() As Rectangle
            Return Rectangle.FromLTRB(Me.left, Me.top, Me.right, Me.bottom)
        End Function
        Public Overrides Function ToString() As String
            Return $"{{{Me.left},{Me.top},{Me.right},{Me.bottom}}}"
        End Function
    End Structure
    <DllImport("user32.dll", SetLastError:=True)>
    Public Function GetClientRect(hWnd As IntPtr, ByRef lpRect As RECT) As Boolean : End Function
    <DllImport("user32.dll", SetLastError:=True)>
    Public Function ClientToScreen(hWnd As IntPtr, ByRef lpPoint As Point) As Boolean : End Function
    <DllImport("user32.dll", SetLastError:=False)>
    Public Function GetForegroundWindow() As IntPtr : End Function


    <DllImport("user32.dll", SetLastError:=True)>
    Public Function GetWindowThreadProcessId(hwnd As IntPtr, ByRef lpdwProcessId As UInteger) As UInteger : End Function
    <DllImport("user32.dll", SetLastError:=True)>
    Public Function GetGUIThreadInfo(idThread As UInteger, ByRef lpgui As GUITHREADINFO) As <MarshalAs(UnmanagedType.Bool)> Boolean : End Function
    <Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)>
    Public Structure GUITHREADINFO
        Public cbSize As Integer
        Public flags As GUI_FLAGS
        Public hwndActive As IntPtr
        Public hwndFocus As IntPtr
        Public hwndCapture As IntPtr
        Public hwndMenuOwner As IntPtr
        Public hwndMoveSize As IntPtr
        Public hwndCaret As IntPtr
        Public rcCaret As RECT
    End Structure
    <Flags>
    Public Enum GUI_FLAGS As UInteger
        GUI_NONE = &H0
        GUI_CARETBLINKING = &H1 ' The caret is blinking.
        GUI_INMOVESIZE = &H2 ' The thread is in a move or size loop.
        GUI_INMENUMODE = &H4 ' The thread is in menu mode.
        GUI_SYSTEMMENUMODE = &H8 ' The thread is in the system menu mode.
        GUI_POPUPMENUMODE = &H10 ' The thread is in a popup menu loop.
    End Enum

    <DllImport("user32.dll", SetLastError:=True)>
    Public Function SetForegroundWindow(hWnd As IntPtr) As Boolean : End Function

    <DllImport("user32.dll")>
    Public Function IsIconic(hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean : End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr : End Function
    Public Const WM_SYSCOMMAND = &H112
    Public Const SC_RESTORE As Integer = &HF120
End Module
