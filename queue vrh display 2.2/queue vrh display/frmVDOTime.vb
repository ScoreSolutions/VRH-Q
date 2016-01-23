Imports Queue_VRH_Display.Org.Mentalis.Files
Public Class frmVDOTime

    Private Sub frmVDOTime_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Sub loadSetting()
        For i As Int16 = 0 To 6
            Dim tmp() As String = {}
            If config(dgvTime.Rows(i).HeaderCell.Value) <> "" Then
                tmp = Split(config(dgvTime.Rows(i).HeaderCell.Value), ",")
                For j As Int16 = 0 To tmp.Length - 1
                    If tmp(j) <> "" Then
                        dgvTime.Rows(i).Cells("c" & tmp(j).PadLeft(2, "0")).Selected = True
                    End If
                Next
            End If

        Next
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim tmp As String = ""
        For i As Int16 = 0 To 6
            tmp = ""
            For j As Int16 = 0 To 23
                If dgvTime.Rows(i).Cells(j).Selected Then
                    If tmp = "" Then tmp &= j Else tmp &= "," & j
                End If
            Next
            config(dgvTime.Rows(i).HeaderCell.Value) = tmp
        Next
        saveConfig()
        MsgBox("Saved Completed.")
    End Sub



    Private Sub frmVDOTime_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        dgvTime.Rows.Clear()
        dgvTime.TabStop = False
        dgvTime.Rows.Add(7)
        dgvTime.Rows(0).HeaderCell.Value = "Sun"
        dgvTime.Rows(1).HeaderCell.Value = "Mon"
        dgvTime.Rows(2).HeaderCell.Value = "Tue"
        dgvTime.Rows(3).HeaderCell.Value = "Wed"
        dgvTime.Rows(4).HeaderCell.Value = "Thu"
        dgvTime.Rows(5).HeaderCell.Value = "Fri"
        dgvTime.Rows(6).HeaderCell.Value = "Sat"
        For i As Integer = 0 To dgvTime.RowCount - 1
            For j As Integer = 0 To dgvTime.Rows(i).Cells.Count - 1
                dgvTime.Rows(i).Cells(j).Selected = False
            Next
        Next
        loadSetting()
    End Sub
End Class