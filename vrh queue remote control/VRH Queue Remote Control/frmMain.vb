Imports VRH_Queue_Remote_Control.Org.Mentalis.Files
Public Class frmMain
    Dim isExit As Boolean = False
    Dim INIFName As String = Application.StartupPath & "\remote.ini"
    Dim isFirstRun As Boolean = True
    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        If fbdDBPath.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtPath.Text = fbdDBPath.SelectedPath
        End If
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If isExit Then
            Application.Exit()
        Else
            e.Cancel = True
            Me.WindowState = FormWindowState.Minimized
        End If
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub mnConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnConfig.Click
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub mnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnExit.Click
        isExit = True
        Me.Close()
    End Sub


    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            ni.Visible = True
            If isFirstRun Then ni.ShowBalloonTip(4000)
            If isFirstRun Then isFirstRun = False
        Else
            ni.Visible = False
        End If
    End Sub


    Private Sub frmMain_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin

    End Sub

    Private Sub ni_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ni.DoubleClick
        mnConfig.PerformClick()
    End Sub


    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.WindowState = FormWindowState.Minimized
        Me.Hide()
    End Sub

    Sub loadConfig()
        Dim ini As New IniReader(INIFName)
        ini.Section = "SETTING"
        txtPath.Text = ini.ReadString("Data_Path")
    End Sub

    Sub saveConfig()
        Dim ini As New IniReader(INIFName)
        ini.Section = "SETTING"
        ini.Write("Data_Path", txtPath.Text)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        saveConfig()
        MsgBox("Configuration Saved Successfully.")
    End Sub

    Private Sub Hotkey_HotkeyPressed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Hotkey.HotkeyPressed
        frmChooseQueue.Show()
        frmChooseQueue.BringToFront()
    End Sub


    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadConfig()
    End Sub
End Class
