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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.txtPath = New System.Windows.Forms.TextBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.fbdDBPath = New System.Windows.Forms.FolderBrowserDialog
        Me.ni = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.cms = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnConfig = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnExit = New System.Windows.Forms.ToolStripMenuItem
        Me.Hotkey = New MCLHotkey.SystemWideHotkeyComponent(Me.components)
        Me.cms.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(12, 32)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(380, 20)
        Me.txtPath.TabIndex = 0
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(268, 62)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(349, 62)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(398, 30)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(26, 23)
        Me.btnBrowse.TabIndex = 3
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(184, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Specify the Path to Queue's Data File"
        '
        'ni
        '
        Me.ni.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ni.BalloonTipText = "VRH Queue Remote Control is Running."
        Me.ni.BalloonTipTitle = "Queue Remote Control"
        Me.ni.ContextMenuStrip = Me.cms
        Me.ni.Icon = CType(resources.GetObject("ni.Icon"), System.Drawing.Icon)
        Me.ni.Text = "Queue Remote Control"
        Me.ni.Visible = True
        '
        'cms
        '
        Me.cms.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnConfig, Me.ToolStripSeparator1, Me.mnExit})
        Me.cms.Name = "cms"
        Me.cms.ShowImageMargin = False
        Me.cms.Size = New System.Drawing.Size(133, 54)
        '
        'mnConfig
        '
        Me.mnConfig.Name = "mnConfig"
        Me.mnConfig.Size = New System.Drawing.Size(132, 22)
        Me.mnConfig.Text = "Configuration..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(129, 6)
        '
        'mnExit
        '
        Me.mnExit.Name = "mnExit"
        Me.mnExit.Size = New System.Drawing.Size(132, 22)
        Me.mnExit.Text = "Exit"
        '
        'Hotkey
        '
        Me.Hotkey.Altkey = False
        Me.Hotkey.ControlKey = False
        Me.Hotkey.HotKey = System.Windows.Forms.Keys.Pause
        Me.Hotkey.ShiftKey = False
        Me.Hotkey.WinKey = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(436, 91)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtPath)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.Text = "Remote Control Configuration"
        Me.cms.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents fbdDBPath As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ni As System.Windows.Forms.NotifyIcon
    Friend WithEvents cms As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnConfig As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Hotkey As MCLHotkey.SystemWideHotkeyComponent

End Class
