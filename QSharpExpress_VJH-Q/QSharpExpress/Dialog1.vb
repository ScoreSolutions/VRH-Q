Imports System.Windows.Forms
Imports System.Data.SqlClient

Public Class Dialog1

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim sqlstr As String
        sqlstr = "update ImportData set other_status='" & QueueStatus.MissedCall & "' where Queue_no='" & Me.lblQueue.Text & "'"
        Dim conn As New SqlConnection(frmQueueSharpExpress.formatConnectionString)
        Dim cmd As New SqlCommand(sqlstr, conn)
        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
        Catch ex As Exception

        End Try




        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub


    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim sqlstr As String
        Dim Result As Integer
        sqlstr = "SELECT  count(*) as count" & _
         " FROM ImportData WHERE   Queue_no ='" & Me.lblQueue.Text & "' and Other_Status = '3'"
        Dim conn As New SqlConnection(frmQueueSharpExpress.formatConnectionString)
        conn.Open()
        Dim cmd As New SqlCommand(sqlstr, conn)
        Try
            Result = cmd.ExecuteScalar
            If Result = 1 Then
                Me.Close()
            End If
        Catch ex As Exception

        End Try
        conn.Close()
    End Sub
    Enum CustomerStatus
        Printed = 40
        Packed = 70
        Paid = 80
        Cancel = -10
    End Enum
    Enum PaymentStatus
        Notpaid = 0
        Needpay = 1
        Paid = 2
    End Enum
    Enum QueueStatus
        Calling = 4
        Finish = 3
        Waiting = 1
        MissedCall = 99
        Expired = 100
    End Enum
End Class
