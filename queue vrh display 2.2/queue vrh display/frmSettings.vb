
Public Class frmSettings

    Private Sub frmSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'loadConfig()
        applyConfig()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        config("import_data_folder") = txtImpDir.Text
        config("move_data_folder") = txtMoveDir.Text
        config("flash_data_file") = txtFlashData.Text
        config("call_expire") = nudCallExpire.Value
        config("import_interval") = nudImport.Value
        config("display_interval") = nudDisplay.Value
        config("text_crawling") = txtCrawling.Text
        config("VDOFile") = txtVDOFile.Text
        config("missed_call_expire") = nudMX.Value

        saveConfig()
        frmDisplayAgent.tmrImport.Interval = IIf(IsNumeric(config("import_interval")), config("import_interval"), 3) * 1000
        frmDisplayAgent.tmrDisplay.Interval = IIf(IsNumeric(config("display_interval")), config("display_interval"), 3) * 1000
        MessageBox.Show("Configurations Saved Successfully.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Sub applyConfig()
        txtImpDir.Text = config("import_data_folder")
        txtMoveDir.Text = config("move_data_folder")
        txtFlashData.Text = config("flash_data_file")
        txtCrawling.Text = config("text_crawling")
        txtVDOFile.Text = config("VDOFile")
        nudImport.Value = IIf(Not IsNumeric(config("import_interval")), 30, config("import_interval"))
        nudDisplay.Value = IIf(Not IsNumeric(config("display_interval")), 3, config("display_interval"))
        nudCallExpire.Value = IIf(Not IsNumeric(config("call_expire")), 3, config("call_expire"))
        nudMX.Value = IIf(Not IsNumeric(config("missed_call_expire")), 3, config("missed_call_expire"))
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseIMP.Click

        If fbdBrowse.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            txtImpDir.Text = fbdBrowse.SelectedPath
        End If

    End Sub

    Private Sub btnBrowseMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseMove.Click
        If fbdBrowse.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            txtMoveDir.Text = fbdBrowse.SelectedPath
        End If
    End Sub

    Private Sub btnBrowseFlash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseFlash.Click
        If ofdBrowse.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtFlashData.Text = ofdBrowse.FileName
        End If
    End Sub

    Private Sub btnVDO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVDO.Click
        frmVDOTime.ShowDialog()
    End Sub

    Private Sub btnBrowseVDO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseVDO.Click
        ofdBrowse.Filter = "All Supported Media|*.*"
        If ofdBrowse.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtVDOFile.Text = ofdBrowse.FileName
        End If
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        frmManageCounter.ShowDialog()
    End Sub
End Class