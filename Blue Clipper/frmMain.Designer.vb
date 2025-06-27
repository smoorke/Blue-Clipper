<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.tmrTick = New System.Windows.Forms.Timer(Me.components)
        Me.TrayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.cmsTray = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.LaunchBluePrinceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutoLaunchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseOnExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.CloseBlueClipperToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsTray.SuspendLayout()
        Me.SuspendLayout()
        '
        'tmrTick
        '
        Me.tmrTick.Enabled = True
        Me.tmrTick.Interval = 1337
        '
        'TrayIcon
        '
        Me.TrayIcon.ContextMenuStrip = Me.cmsTray
        Me.TrayIcon.Icon = CType(resources.GetObject("TrayIcon.Icon"), System.Drawing.Icon)
        Me.TrayIcon.Text = "Blue Clipper"
        Me.TrayIcon.Visible = True
        '
        'cmsTray
        '
        Me.cmsTray.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LaunchBluePrinceToolStripMenuItem, Me.ToolStripMenuItem1, Me.SettingsToolStripMenuItem, Me.ToolStripMenuItem2, Me.CloseBlueClipperToolStripMenuItem})
        Me.cmsTray.Name = "cmsTray"
        Me.cmsTray.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.cmsTray.Size = New System.Drawing.Size(181, 104)
        '
        'LaunchBluePrinceToolStripMenuItem
        '
        Me.LaunchBluePrinceToolStripMenuItem.Image = Global.Blue_Clipper.My.Resources.Resources.BluePrince
        Me.LaunchBluePrinceToolStripMenuItem.Name = "LaunchBluePrinceToolStripMenuItem"
        Me.LaunchBluePrinceToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.LaunchBluePrinceToolStripMenuItem.Text = "Launch Blue Prince"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(177, 6)
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AutoLaunchToolStripMenuItem, Me.CloseOnExitToolStripMenuItem})
        Me.SettingsToolStripMenuItem.Image = Global.Blue_Clipper.My.Resources.Resources.gear_wheel
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'AutoLaunchToolStripMenuItem
        '
        Me.AutoLaunchToolStripMenuItem.Name = "AutoLaunchToolStripMenuItem"
        Me.AutoLaunchToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.AutoLaunchToolStripMenuItem.Text = "Auto Launch"
        '
        'CloseOnExitToolStripMenuItem
        '
        Me.CloseOnExitToolStripMenuItem.Name = "CloseOnExitToolStripMenuItem"
        Me.CloseOnExitToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.CloseOnExitToolStripMenuItem.Text = "Close On Exit"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(177, 6)
        '
        'CloseBlueClipperToolStripMenuItem
        '
        Me.CloseBlueClipperToolStripMenuItem.Image = Global.Blue_Clipper.My.Resources.Resources.close
        Me.CloseBlueClipperToolStripMenuItem.Name = "CloseBlueClipperToolStripMenuItem"
        Me.CloseBlueClipperToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.CloseBlueClipperToolStripMenuItem.Text = "Close Blue Clipper"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.Text = "Blue Clipper"
        Me.cmsTray.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tmrTick As Timer
    Friend WithEvents TrayIcon As NotifyIcon
    Friend WithEvents cmsTray As ContextMenuStrip
    Friend WithEvents CloseBlueClipperToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LaunchBluePrinceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AutoLaunchToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CloseOnExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
End Class
