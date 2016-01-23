<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDisplayAgent
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDisplayAgent))
        Me.txtLog = New System.Windows.Forms.TextBox
        Me.ts1 = New System.Windows.Forms.ToolStrip
        Me.btnStart = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnStop = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnClear = New System.Windows.Forms.ToolStripButton
        Me.btnSettings = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.tmrDisplay = New System.Windows.Forms.Timer(Me.components)
        Me.tmrImport = New System.Windows.Forms.Timer(Me.components)
        Me.ts1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtLog
        '
        Me.txtLog.BackColor = System.Drawing.Color.DarkBlue
        Me.txtLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLog.ForeColor = System.Drawing.Color.White
        Me.txtLog.Location = New System.Drawing.Point(0, 54)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ReadOnly = True
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtLog.Size = New System.Drawing.Size(517, 212)
        Me.txtLog.TabIndex = 1
        Me.txtLog.WordWrap = False
        '
        'ts1
        '
        Me.ts1.BackColor = System.Drawing.Color.White
        Me.ts1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnStart, Me.ToolStripSeparator1, Me.btnStop, Me.ToolStripSeparator2, Me.btnClear, Me.btnSettings, Me.ToolStripSeparator3})
        Me.ts1.Location = New System.Drawing.Point(0, 0)
        Me.ts1.Name = "ts1"
        Me.ts1.Size = New System.Drawing.Size(517, 54)
        Me.ts1.TabIndex = 0
        Me.ts1.Text = "ToolStrip1"
        '
        'btnStart
        '
        Me.btnStart.Image = CType(resources.GetObject("btnStart.Image"), System.Drawing.Image)
        Me.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(36, 51)
        Me.btnStart.Text = "Start"
        Me.btnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 54)
        '
        'btnStop
        '
        Me.btnStop.Image = CType(resources.GetObject("btnStop.Image"), System.Drawing.Image)
        Me.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(36, 51)
        Me.btnStop.Text = "Stop"
        Me.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 54)
        '
        'btnClear
        '
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(38, 51)
        Me.btnClear.Text = "Clear"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btnSettings
        '
        Me.btnSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnSettings.Image = CType(resources.GetObject("btnSettings.Image"), System.Drawing.Image)
        Me.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.Size = New System.Drawing.Size(53, 51)
        Me.btnSettings.Text = "Settings"
        Me.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 54)
        '
        'tmrDisplay
        '
        '
        'tmrImport
        '
        '
        'frmDisplayAgent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(517, 266)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.ts1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDisplayAgent"
        Me.Text = "Main Display Agent (VRH-2) v[%V%]"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.ts1.ResumeLayout(False)
        Me.ts1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtLog As System.Windows.Forms.TextBox
    Friend WithEvents ts1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnStart As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnStop As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnClear As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSettings As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tmrDisplay As System.Windows.Forms.Timer
    Friend WithEvents tmrImport As System.Windows.Forms.Timer
End Class
