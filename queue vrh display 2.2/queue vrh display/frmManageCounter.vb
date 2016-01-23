Public Class frmManageCounter

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If IsNumeric(txtCounter.Text) Then
            If Not lbCounter.Items.Contains(txtCounter.Text.Trim) Then
                lbCounter.Items.Add(txtCounter.Text.Trim)
                
                If config("Counter") <> "" Then
                    config("Counter") = config("Counter") & "," & txtCounter.Text.Trim
                Else
                    config("Counter") = txtCounter.Text.Trim
                End If
                saveConfig()
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If lbCounter.SelectedIndex >= 0 Then
            lbCounter.Items.Remove(lbCounter.SelectedItem)
            config("Counter") = ""
            For i As Int16 = 0 To lbCounter.Items.Count - 1
                If config("Counter") <> "" Then
                    config("Counter") = config("Counter") & "," & lbCounter.Items.Item(i)
                Else
                    config("Counter") = lbCounter.Items.Item(i)
                End If
            Next
            saveConfig()
        End If
    End Sub

    Private Sub frmManageCounter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lbCounter.Items.Clear()
        If config("Counter") <> "" Then
            Dim tmp() As String = {}
            tmp = Split(config("Counter"), ",")
            For i As Int16 = 0 To tmp.Length - 1
                If tmp(i).Trim <> "" Then lbCounter.Items.Add(tmp(i).Trim)
            Next
        End If
    End Sub
End Class