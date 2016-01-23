<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.nudMX = New System.Windows.Forms.NumericUpDown
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnBrowseVDO = New System.Windows.Forms.Button
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtVDOFile = New System.Windows.Forms.TextBox
        Me.btnVDO = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtCrawling = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.nudDisplay = New System.Windows.Forms.NumericUpDown
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.nudImport = New System.Windows.Forms.NumericUpDown
        Me.btnBrowseFlash = New System.Windows.Forms.Button
        Me.btnBrowseMove = New System.Windows.Forms.Button
        Me.btnBrowseIMP = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.nudCallExpire = New System.Windows.Forms.NumericUpDown
        Me.txtFlashData = New System.Windows.Forms.TextBox
        Me.txtMoveDir = New System.Windows.Forms.TextBox
        Me.txtImpDir = New System.Windows.Forms.TextBox
        Me.fbdBrowse = New System.Windows.Forms.FolderBrowserDialog
        Me.ofdBrowse = New System.Windows.Forms.OpenFileDialog
        Me.GroupBox1.SuspendLayout()
        CType(Me.nudMX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCallExpire, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.nudMX)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.btnBrowseVDO)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtVDOFile)
        Me.GroupBox1.Controls.Add(Me.btnVDO)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtCrawling)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.nudDisplay)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.nudImport)
        Me.GroupBox1.Controls.Add(Me.btnBrowseFlash)
        Me.GroupBox1.Controls.Add(Me.btnBrowseMove)
        Me.GroupBox1.Controls.Add(Me.btnBrowseIMP)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.nudCallExpire)
        Me.GroupBox1.Controls.Add(Me.txtFlashData)
        Me.GroupBox1.Controls.Add(Me.txtMoveDir)
        Me.GroupBox1.Controls.Add(Me.txtImpDir)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(478, 263)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(171, 180)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(123, 13)
        Me.Label12.TabIndex = 30
        Me.Label12.Text = "Minutes (Since Last Call)"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(16, 180)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(92, 13)
        Me.Label13.TabIndex = 29
        Me.Label13.Text = "Missed Call Expire"
        '
        'nudMX
        '
        Me.nudMX.Location = New System.Drawing.Point(114, 178)
        Me.nudMX.Maximum = New Decimal(New Integer() {6000, 0, 0, 0})
        Me.nudMX.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudMX.Name = "nudMX"
        Me.nudMX.Size = New System.Drawing.Size(51, 20)
        Me.nudMX.TabIndex = 28
        Me.nudMX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nudMX.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(368, 152)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(99, 23)
        Me.Button1.TabIndex = 27
        Me.Button1.Text = "Mange Counter"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnBrowseVDO
        '
        Me.btnBrowseVDO.Location = New System.Drawing.Point(336, 124)
        Me.btnBrowseVDO.Name = "btnBrowseVDO"
        Me.btnBrowseVDO.Size = New System.Drawing.Size(29, 23)
        Me.btnBrowseVDO.TabIndex = 26
        Me.btnBrowseVDO.Text = "..."
        Me.btnBrowseVDO.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(59, 129)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(49, 13)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "VDO File"
        '
        'txtVDOFile
        '
        Me.txtVDOFile.Location = New System.Drawing.Point(114, 126)
        Me.txtVDOFile.Name = "txtVDOFile"
        Me.txtVDOFile.Size = New System.Drawing.Size(216, 20)
        Me.txtVDOFile.TabIndex = 24
        '
        'btnVDO
        '
        Me.btnVDO.Location = New System.Drawing.Point(368, 123)
        Me.btnVDO.Name = "btnVDO"
        Me.btnVDO.Size = New System.Drawing.Size(99, 23)
        Me.btnVDO.TabIndex = 23
        Me.btnVDO.Text = "VDO Schedule"
        Me.btnVDO.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(37, 100)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 13)
        Me.Label10.TabIndex = 22
        Me.Label10.Text = "Text Crawling"
        '
        'txtCrawling
        '
        Me.txtCrawling.Location = New System.Drawing.Point(114, 97)
        Me.txtCrawling.Name = "txtCrawling"
        Me.txtCrawling.Size = New System.Drawing.Size(353, 20)
        Me.txtCrawling.TabIndex = 21
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(171, 235)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Seconds"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(29, 235)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(79, 13)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Display Interval"
        '
        'nudDisplay
        '
        Me.nudDisplay.Location = New System.Drawing.Point(114, 233)
        Me.nudDisplay.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
        Me.nudDisplay.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudDisplay.Name = "nudDisplay"
        Me.nudDisplay.Size = New System.Drawing.Size(51, 20)
        Me.nudDisplay.TabIndex = 18
        Me.nudDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nudDisplay.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(171, 206)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Seconds"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(34, 206)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Import Interval"
        '
        'nudImport
        '
        Me.nudImport.Location = New System.Drawing.Point(114, 204)
        Me.nudImport.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
        Me.nudImport.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudImport.Name = "nudImport"
        Me.nudImport.Size = New System.Drawing.Size(51, 20)
        Me.nudImport.TabIndex = 15
        Me.nudImport.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nudImport.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'btnBrowseFlash
        '
        Me.btnBrowseFlash.Location = New System.Drawing.Point(438, 69)
        Me.btnBrowseFlash.Name = "btnBrowseFlash"
        Me.btnBrowseFlash.Size = New System.Drawing.Size(29, 23)
        Me.btnBrowseFlash.TabIndex = 14
        Me.btnBrowseFlash.Text = "..."
        Me.btnBrowseFlash.UseVisualStyleBackColor = True
        '
        'btnBrowseMove
        '
        Me.btnBrowseMove.Location = New System.Drawing.Point(438, 43)
        Me.btnBrowseMove.Name = "btnBrowseMove"
        Me.btnBrowseMove.Size = New System.Drawing.Size(29, 23)
        Me.btnBrowseMove.TabIndex = 13
        Me.btnBrowseMove.Text = "..."
        Me.btnBrowseMove.UseVisualStyleBackColor = True
        '
        'btnBrowseIMP
        '
        Me.btnBrowseIMP.Location = New System.Drawing.Point(438, 17)
        Me.btnBrowseIMP.Name = "btnBrowseIMP"
        Me.btnBrowseIMP.Size = New System.Drawing.Size(29, 23)
        Me.btnBrowseIMP.TabIndex = 12
        Me.btnBrowseIMP.Text = "..."
        Me.btnBrowseIMP.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(392, 234)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 11
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(94, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Import Data Folder"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Move Data File To"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(31, 74)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Flash Data File"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(171, 154)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Seconds"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(52, 154)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Call Expire"
        '
        'nudCallExpire
        '
        Me.nudCallExpire.Location = New System.Drawing.Point(114, 152)
        Me.nudCallExpire.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
        Me.nudCallExpire.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudCallExpire.Name = "nudCallExpire"
        Me.nudCallExpire.Size = New System.Drawing.Size(51, 20)
        Me.nudCallExpire.TabIndex = 4
        Me.nudCallExpire.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nudCallExpire.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'txtFlashData
        '
        Me.txtFlashData.Location = New System.Drawing.Point(114, 71)
        Me.txtFlashData.Name = "txtFlashData"
        Me.txtFlashData.Size = New System.Drawing.Size(322, 20)
        Me.txtFlashData.TabIndex = 2
        '
        'txtMoveDir
        '
        Me.txtMoveDir.Location = New System.Drawing.Point(114, 45)
        Me.txtMoveDir.Name = "txtMoveDir"
        Me.txtMoveDir.Size = New System.Drawing.Size(322, 20)
        Me.txtMoveDir.TabIndex = 1
        '
        'txtImpDir
        '
        Me.txtImpDir.Location = New System.Drawing.Point(114, 19)
        Me.txtImpDir.Name = "txtImpDir"
        Me.txtImpDir.Size = New System.Drawing.Size(322, 20)
        Me.txtImpDir.TabIndex = 0
        '
        'ofdBrowse
        '
        Me.ofdBrowse.Filter = "Text File|*.txt|All File|*.*"
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 271)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmSettings"
        Me.Text = "Main Display Settings"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nudMX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudCallExpire, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtFlashData As System.Windows.Forms.TextBox
    Friend WithEvents txtMoveDir As System.Windows.Forms.TextBox
    Friend WithEvents txtImpDir As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents nudCallExpire As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnBrowseFlash As System.Windows.Forms.Button
    Friend WithEvents btnBrowseMove As System.Windows.Forms.Button
    Friend WithEvents btnBrowseIMP As System.Windows.Forms.Button
    Friend WithEvents fbdBrowse As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ofdBrowse As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents nudDisplay As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents nudImport As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCrawling As System.Windows.Forms.TextBox
    Friend WithEvents btnVDO As System.Windows.Forms.Button
    Friend WithEvents btnBrowseVDO As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtVDOFile As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents nudMX As System.Windows.Forms.NumericUpDown
End Class
