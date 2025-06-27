Imports System.Runtime.InteropServices

Public Class frmMain

#Region "Startup Sequence"

    Dim mh As InputHook = Nothing

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or WindowStylesEx.WS_EX_TOOLWINDOW 'Or WindowStylesEx.WS_EX_TRANSPARENT
            Return cp
        End Get
    End Property

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Debug.Print($"Startup: {If(BlueProc?.Id, 0)}")

        SetThemedMenu()

        Me.Opacity = 0
        If BlueProc IsNot Nothing Then
            mh = New InputHook(InputHookType.Mouse_LL, AddressOf MouseHookCallBack)
        Else
            If My.Settings.AutoLaunch Then LaunchBluePrinceToolStripMenuItem.PerformClick()
        End If
    End Sub
    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.Hide()
    End Sub

    Private Sub SetThemedMenu()
        cmsTray.RenderMode = ToolStripRenderMode.Professional
        cmsTray.Renderer = New CustomRenderer()
        SetPaintRecurse(cmsTray.Items, Sub(sender As ToolStripMenuItem, e As PaintEventArgs)
                                           If sender.Selected Then
                                               sender.ForeColor = Color.DarkSlateBlue
                                           Else
                                               sender.ForeColor = Color.LightBlue
                                           End If
                                       End Sub)
    End Sub

    Private Sub SetPaintRecurse(col As ToolStripItemCollection, act As PaintEventHandler)
        For Each item As ToolStripMenuItem In col.OfType(Of ToolStripMenuItem)
            AddHandler item.Paint, act
            If item.HasDropDownItems Then SetPaintRecurse(item.DropDownItems, act)
        Next
    End Sub

#End Region
#Region "MouseHookCallBack"


    Dim GTI As New GUITHREADINFO With {.cbSize = Marshal.SizeOf(GetType(GUITHREADINFO))}
    Private Function MouseHookCallBack(nCode As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr
        'Note: you may have to tweak the order of these early returns to mitigate false positives on virustotal
        If nCode <> 0 Then Return mh.NextHook(nCode, wParam, lParam)
        If wParam.ToInt32() <> MouseState.MouseMove Then Return mh.NextHook(nCode, wParam, lParam)
        If My.Computer.Keyboard.AltKeyDown Then Return mh.NextHook(nCode, wParam, lParam)
        If BlueProc Is Nothing OrElse BlueProc.HasExitedSafe() Then Return mh.NextHook(nCode, wParam, lParam)

        Dim bpHwnd = BlueProc.MainWindowHandle()

        If GetForegroundWindow() <> bpHwnd Then Return mh.NextHook(nCode, wParam, lParam)
        If GetGUIThreadInfo(BlueThread, GTI) AndAlso GTI.flags <> GUI_FLAGS.GUI_NONE Then Return mh.NextHook(nCode, wParam, lParam)

        'Dim mhs As MSLLHOOKSTRUCT = Marshal.PtrToStructure(Of MSLLHOOKSTRUCT)(lParam)
        'Dim cpos = mhs.pt 

        'We only care about the first member so we marshal directly to a point
        Dim cpos = Marshal.PtrToStructure(Of Point)(lParam) 'future cursor position

        'top left corner
        Dim ptCTL As Point
        ClientToScreen(bpHwnd, ptCTL)

        'bottom right corner
        Dim rcC As RECT
        GetClientRect(bpHwnd, rcC)
        Dim ptCBR = New Point(rcC.right - 1, rcC.bottom) 'needs -1 or we get cursor on right edge
        ClientToScreen(bpHwnd, ptCBR)

        'clamp position
        Dim clampX = Math.Max(ptCTL.X, Math.Min(ptCBR.X, cpos.X))
        Dim clampY = Math.Max(ptCTL.Y, Math.Min(ptCBR.Y, cpos.Y))
        Cursor.Position = New Point(clampX, clampY)
        Return 1 'block move
    End Function

#End Region

    Private Sub tmrTick_Tick(sender As Object, e As EventArgs) Handles tmrTick.Tick 'interval 1337
        BlueProc = GetBluePrinceProc()

        If mh Is Nothing AndAlso BlueProc IsNot Nothing Then
            mh = New InputHook(InputHookType.Mouse_LL, AddressOf MouseHookCallBack)
        ElseIf BlueProc Is Nothing AndAlso mh IsNot Nothing Then
            mh.UnHook()
            mh = Nothing
            If My.Settings.CloseOnExit Then Me.Close()
        End If
    End Sub

#Region "TrayMenu"
    Private Sub CloseBlueClipperToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseBlueClipperToolStripMenuItem.Click
        mh?.UnHook()
        Me.Close()
    End Sub

    Private Sub cmsTray_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsTray.Opening
        BlueProc = GetBluePrinceProc()
        LaunchBluePrinceToolStripMenuItem.Enabled = BlueProc Is Nothing
    End Sub

    Private Sub LaunchBluePrinceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaunchBluePrinceToolStripMenuItem.Click
        'https://store.steampowered.com/app/1569580/Blue_Prince/
        Dim pp As Process = Nothing
        Try
            pp = Process.Start("explorer", "steam://rungameid/1569580")
        Catch ex As Exception
            Debug.Print($"bab0 starting BluePrince {ex.Message}")
        Finally
            pp?.Dispose()
        End Try
    End Sub

    Private Sub TrayIcon_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TrayIcon.MouseDoubleClick
        BlueProc = GetBluePrinceProc()
        If BlueProc IsNot Nothing Then
            If IsIconic(BlueProc.MainWindowHandle) Then
                SendMessage(BlueProc.MainWindowHandle, WM_SYSCOMMAND, SC_RESTORE, IntPtr.Zero)
            End If
            SetForegroundWindow(BlueProc.MainWindowHandle)
        Else
            LaunchBluePrinceToolStripMenuItem.PerformClick()
        End If
    End Sub

    Private Sub AutoLaunchToolStripMenuItem_Click(sender As ToolStripMenuItem, e As ToolStripItemClickedEventArgs) Handles SettingsToolStripMenuItem.DropDownItemClicked
        Dim item As ToolStripMenuItem = e.ClickedItem
        item.Checked = Not item.Checked
        Select Case item.Name
            Case AutoLaunchToolStripMenuItem.Name
                My.Settings.AutoLaunch = item.Checked
            Case CloseOnExitToolStripMenuItem.Name
                My.Settings.CloseOnExit = item.Checked
        End Select
        My.Settings.Save()
    End Sub

    Private Sub SettingsToolStripMenuItem_DropDownOpening(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.DropDownOpening
        AutoLaunchToolStripMenuItem.Checked = My.Settings.AutoLaunch
        CloseOnExitToolStripMenuItem.Checked = My.Settings.CloseOnExit
    End Sub

#End Region
End Class
