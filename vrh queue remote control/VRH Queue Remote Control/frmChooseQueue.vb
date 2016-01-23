Imports System.Data
Imports System.Data.OleDb
Imports System.io
Public Class frmChooseQueue
    Enum CustomerStatus
        Printed = 40
        Packed = 70
        Paid = 80
        Cancelled = -10
    End Enum

    Enum QueueStatus
        Calling = 4
        Finish = 3
        Waiting = 1
        MissedCall = 99
        Expired = 100
    End Enum
    Private Sub frmChooseQueue_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            If lbQueue.SelectedIndex > -1 Then
                Dim conn As New OleDbConnection(getConnectionString)
                Try
                    conn.Open()
                    Dim sql As String = "update tbl_QData set other_status='" & QueueStatus.MissedCall & "' where Queue_no='" & lbQueue.SelectedValue.ToString & "' and Other_Status='" & QueueStatus.Calling & "' and datediff('d',update_time,Now)=0"
                    Dim cmd As New OleDbCommand(sql, conn)
                    Try
                        cmd.ExecuteNonQuery()
                        conn.Close()
                        frmMain.ni.ShowBalloonTip(3000, "Force Missed Call", "The Queue No: " & lbQueue.SelectedValue.ToString & " Has been Forced to be Missed Call.", ToolTipIcon.Info)
                    Catch exx As Exception
                        MsgBox(exx.Message)
                    End Try
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

                Me.Close()
            Else
                lbQueue.Focus()
            End If
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub


    Private Sub frmChooseQueue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BringToFront()
        Me.Activate()
        Me.lbQueue.Focus()

        showCallingQueue()
    End Sub

    Sub showCallingQueue()
        Dim conn As New OleDbConnection(getConnectionString)
        Try
            conn.Open()
            Dim sql As String = "select queue_no from tbl_QData where status='" & CustomerStatus.Packed & "' and Other_Status='" & QueueStatus.Calling & "' and datediff('d',update_time,now)=0"
            Dim dt As New DataTable
            Dim da As New OleDbDataAdapter(sql, conn)
            Try
                da.Fill(dt)

                lbQueue.ValueMember = "queue_no"
                lbQueue.DisplayMember = "queue_no"
                lbQueue.DataSource = dt
                conn.Close()
            Catch exx As Exception
                MsgBox(exx.Message)
            End Try
            Try
                conn.Close()
            Catch ex As Exception : End Try

        Catch ex As Exception
            MsgBox(ex.Message)
            Me.Close()
        End Try
    End Sub

    Function getConnectionString() As String
        'Dim templateFile As String = Application.StartupPath & "\template.mdb"
        Dim f As String = frmMain.txtPath.Text & "\" & fixDate(Now.Date) & ".mdb"
        Return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " & f
    End Function

    Public Function fixDate(ByVal argTXT As String) As String
        Dim d As String = ""
        Dim m As String = ""
        Dim y As String = ""
        If IsDate(argTXT) Then
            Dim dmy As Date = CDate(argTXT)
            d = dmy.Day
            m = dmy.Month
            y = dmy.Year
            If y > 2500 Then
                y = y - 543
            End If
            Return y.ToString & m.ToString.PadLeft(2, "0") & d.ToString.PadLeft(2, "0")
        Else
            Return ""
        End If
    End Function
End Class