<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDisplay
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDisplay))
        Me.flash = New AxShockwaveFlashObjects.AxShockwaveFlash
        Me.VDO = New AxWMPLib.AxWindowsMediaPlayer
        Me.tmrChkVDO = New System.Windows.Forms.Timer(Me.components)
        CType(Me.flash, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VDO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'flash
        '
        Me.flash.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flash.Enabled = True
        Me.flash.Location = New System.Drawing.Point(0, 0)
        Me.flash.Name = "flash"
        Me.flash.OcxState = CType(resources.GetObject("flash.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flash.Size = New System.Drawing.Size(820, 581)
        Me.flash.TabIndex = 0
        '
        'VDO
        '
        Me.VDO.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VDO.Enabled = True
        Me.VDO.Location = New System.Drawing.Point(8, 8)
        Me.VDO.Name = "VDO"
        Me.VDO.OcxState = CType(resources.GetObject("VDO.OcxState"), System.Windows.Forms.AxHost.State)
        Me.VDO.Size = New System.Drawing.Size(464, 354)
        Me.VDO.TabIndex = 1
        '
        'tmrChkVDO
        '
        Me.tmrChkVDO.Enabled = True
        Me.tmrChkVDO.Interval = 30000
        '
        'frmDisplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(820, 581)
        Me.ControlBox = False
        Me.Controls.Add(Me.VDO)
        Me.Controls.Add(Me.flash)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmDisplay"
        Me.Text = "frmDisplay"
        CType(Me.flash, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VDO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents flash As AxShockwaveFlashObjects.AxShockwaveFlash
    Friend WithEvents VDO As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents tmrChkVDO As System.Windows.Forms.Timer
End Class
