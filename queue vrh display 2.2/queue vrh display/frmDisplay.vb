Imports System.IO
Public Class frmDisplay

    Private Sub frmDisplay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmDisplay_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ShowDisplay()
    End Sub

    Sub ShowDisplay()
        If config(DayName(Now.DayOfWeek)) <> "" Then

            If InStr("," & config(DayName(Now.DayOfWeek)) & ",", "," & Now.Hour & ",") > 0 Then
                If Not IsVDO Then
                    IsVDO = True
                End If
                If VDO.playState <> WMPLib.WMPPlayState.wmppsPlaying Then showVDO()
            Else
                IsVDO = False
                showFlash()
            End If
        Else
            IsVDO = False
            showFlash()
        End If
    End Sub



    Sub showVDO()
        'MsgBox(Now.AddDays(-1).DayOfWeek)
        'If IsVDO Then
        Me.flash.Movie = Application.StartupPath & "\" & "vrh_display_2_vdo.swf"
        flash.Play()
        flash.Playing = True
        VDO.Visible = True
        'Else
        'Me.flash.Movie = Application.StartupPath & "\" & "vrh_display_2.swf"
        'flash.Play()
        'flash.Playing = True
        'VDO.Visible = False
        'End If

        Me.flash.Play()
        Me.flash.Playing = True

        If config("VDOFile").Trim <> "" AndAlso File.Exists(config("VDOFile").Trim) AndAlso IsVDO Then
            VDO.settings.setMode("loop", True)
            VDO.URL = config("VDOFile").Trim
            Try
                VDO.uiMode = "none"
                VDO.Ctlcontrols.play()
            Catch ex As Exception
                frmDisplayAgent.uplog(ex.Message)
            End Try

        End If
    End Sub

    Sub showFlash()
        Me.flash.Movie = Application.StartupPath & "\" & "vrh_display_2_flash.swf"
        Me.flash.Play()
        Me.flash.Playing = True
        VDO.Visible = False
        VDO.Ctlcontrols.stop()
    End Sub


    Private Sub tmrChkVDO_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrChkVDO.Tick
        ShowDisplay()
    End Sub

    Private Sub flash_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flash.Enter

    End Sub

End Class