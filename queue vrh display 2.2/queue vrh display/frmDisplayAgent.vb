Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Public Class frmDisplayAgent
    Structure Calling
        Dim QNo As String
        Dim CounterNo As String
    End Structure
    Dim LastCallQ(1) As Calling
    Dim PageRotateDelay As Integer = 4
    Dim RotateCount = 0

    Private Sub frmDisplayAgent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ClearConfig()
        loadConfig()
        tmrImport.Interval = IIf(IsNumeric(config("import_interval")), config("import_interval"), 3) * 1000
        tmrDisplay.Interval = IIf(IsNumeric(config("display_interval")), config("display_interval"), 3) * 1000
        tmrImport.Enabled = IIf(config("state") = "1", True, False)
        tmrDisplay.Enabled = IIf(config("state") = "1", True, False)
        If config("state") = "1" Then bStart() Else bStop()
        Me.Text = Me.Text.Replace("[%V%]", getVersion)

        currentDay = Now.DayOfWeek()

    End Sub

    Public Sub uplog(ByVal argTXT As String)
        If argTXT.Trim <> "" Then
            If txtLog.Text.Length > 30000 Then
                txtLog.Text = WriteLogFile(txtLog, "QDISPLog_")
            End If
            txtLog.Text &= "◊" & vbCrLf & Now & vbTab & argTXT & vbCrLf
            txtLog.SelectionStart = Len(txtLog.Text) : txtLog.ScrollToCaret()
        End If
    End Sub

    Sub ClearConfig()
        config.Clear()

        config.Add("import_data_folder", "")
        config.Add("move_data_folder", "")
        config.Add("flash_data_file", "")
        config.Add("call_expire", "")
        config.Add("missed_call_expire", "")
        config.Add("import_interval", "")
        config.Add("display_interval", "")
        config.Add("state", "")
        config.Add("text_crawling", "")
        config.Add("Sun", "")
        config.Add("Mon", "")
        config.Add("Tue", "")
        config.Add("Wed", "")
        config.Add("Thu", "")
        config.Add("Fri", "")
        config.Add("Sat", "")
        config.Add("VDOFile", "")   'Added to show VDO File [Bee-31-Dec-2009]
        config.Add("Counter", "")
    End Sub

    Private Sub btnSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettings.Click
        frmSettings.ShowDialog()
    End Sub


    Sub bStart()
        btnStart.Enabled = False
        btnStop.Enabled = True
        tmrImport.Enabled = True
        tmrDisplay.Enabled = True
        config("state") = 1
        saveConfig()

        frmDisplay.Show()
        frmDisplay.WindowState = FormWindowState.Maximized
    End Sub

    Sub bStop()
        btnStart.Enabled = True
        btnStop.Enabled = False
        tmrImport.Enabled = False
        tmrDisplay.Enabled = False
        config("state") = 0
        saveConfig()
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        bStart()
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        bStop()
    End Sub

    Sub StartImport()
        Dim st As Boolean = tmrImport.Enabled
        tmrImport.Enabled = False
        Dim files As String()
        Dim File As String
        Try
            files = Directory.GetFiles(config("import_data_folder"), "*.txt")
        Catch ex As Exception
            uplog("[StartImport] - " & ex.Message)
            Exit Sub
        End Try

        'lbFile.Items.Clear()
        For Each File In files
            'lbFile.Items.Add(File)
            If ImportFile(File) Then
                Try
                    IO.File.Move(File, config("move_data_folder") & "\" & Strings.Right(File, File.Length - InStrRev(File, "\")))
                    uplog("[StartImport] - " & File & " - Moved")
                Catch ex As Exception
                    Try
                        IO.File.Delete(File)
                    Catch ex2 As Exception
                        uplog("[StartImport] - " & ex2.Message)
                    End Try
                    uplog("[StartImport] - " & ex.Message)
                End Try
            End If
            Application.DoEvents()
        Next
        File = Nothing
        files = Nothing
        tmrImport.Enabled = st
        'GC.Collect()
    End Sub

    Function ImportFile(ByVal fName As String) As Boolean
        Dim f As String = ""
        Try
            f = File.ReadAllText(fName)
        Catch ex As Exception
            uplog("[ImportFile] - " & ex.Message)
            Exit Function
        End Try

        Dim data As String() = f.Split(",")
        Dim sql As String = ""
        If data.Length = 5 Then
            Dim conn As New OleDbConnection(getConnectionString)
            Dim dt As New DataTable
            Dim da As New OleDbDataAdapter("select * from tbl_QData where queue_no='" & data(2) & "'", conn)
            Try
                conn.Open()
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    sql = "update tbl_QData set status='" & data(3) & "',update_time=#" & data(4) & "# where queue_no='" & data(2) & "' and (status<'" & IIf(IsNumeric(data(3)), data(3), -99) & "' or -10=" & IIf(IsNumeric(data(3)), data(3), -99) & ")"
                    uplog("[ImportFile] - " & fName & " - Update")
                Else
                    sql = "insert into tbl_QData values('" & data(0) & "','" & data(1) & "','" & data(2) & "','" & data(3) & "',#" & data(4) & "#,Null,'" & QueueStatus.Waiting & "',Now)"
                    uplog("[ImportFile] - " & fName & " - Insert")
                End If
                Dim cmd As New OleDbCommand(sql, conn)
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                uplog("[ImportFile] - " & ex.Message)
                Return False
            End Try
            conn.Close()
        End If
        'GC.Collect(0, GCCollectionMode.Forced)
        Return True
    End Function

    Private Sub tmrImport_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrImport.Tick
        StartImport()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtLog.Clear()
    End Sub

    Private Sub tmrDisplay_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDisplay.Tick
        tmrDisplay.Enabled = False
        showDisplay()
        tmrDisplay.Enabled = Me.btnStop.Enabled
    End Sub



    '**************************
    Dim lastMissed As String = ""
    Dim lastWait As String = ""
    '**************************
    Sub showDisplay()
        RotateCount += 1
        Dim QInfo As String = ""
        Dim QInfoVDO As String = ""
        Dim conn As New OleDbConnection(getConnectionString)
        Dim dt As New DataTable
        Dim da As OleDbDataAdapter
        'Dim st As Boolean = tmrDisplay.Enabled

        Try
            conn.Open()
        Catch ex As Exception
            uplog("[Connect] - " & ex.Message)
            'tmrDisplay.Enabled = btnStop.Enabled
            Exit Sub
        End Try

        Dim sql As String = ""

        '*** Update Calling to End if Med Delivered ***
        sql = "update tbl_QData set other_status='" & QueueStatus.Finish & "' where status='" & CustomerStatus.Paid & "' and other_status='" & QueueStatus.Calling & "'  and datediff('d',update_time,Now)=0 "
        Dim cmd As New OleDbCommand(sql, conn)
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            uplog("[UpdateCalling2End] - " & ex.Message)
        End Try

        '*** Update Missed Call ***
        sql = "update tbl_QData set other_status='" & QueueStatus.MissedCall & "' where status='" & CustomerStatus.Packed & "' and other_status='" & QueueStatus.Calling & "' and datediff('s',Calling_Time,Now)>=" & config("call_expire") & " and datediff('d',insert_Time,Now)=0 "
        cmd = New OleDbCommand(sql, conn)
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            uplog("[UpdateCalling2End] - " & ex.Message)
        End Try

        '*** Update Missed Call Expire***
        sql = "update tbl_QData set other_status='" & QueueStatus.Expired & "' where status='" & CustomerStatus.Packed & "' and other_status='" & QueueStatus.MissedCall & "' and datediff('n',Calling_Time,Now)>=" & config("missed_call_expire") & " and datediff('d',update_Time,Now)=0 "
        cmd = New OleDbCommand(sql, conn)
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            uplog("[UpdateCalling2End] - " & ex.Message)
        End Try
        cmd.Dispose()


        '************คิวที่กำลังเรียก***********************************
        Dim Counter() As String = Split(config("Counter"), ",")
        Array.Sort(Counter)
        sql = "select top " & Counter.Length & " datediff('s',insert_Time,Now) as WaitingTime,Other_Status, Queue_No from tbl_QData where status ='" & CustomerStatus.Packed & "' and Other_Status in ('" & QueueStatus.Calling & "', '" & QueueStatus.Waiting & "')  and datediff('d',update_time,Now)=0 order by other_status desc, datediff('s',update_Time,Now) desc"

        da = New OleDbDataAdapter(sql, conn)

        Try
            da.Fill(dt)
        Catch ex As Exception
            uplog("[GetCalling] - " & ex.Message)
            'tmrDisplay.Enabled = btnStop.Enabled
            Exit Sub
        End Try


        If dt.Rows.Count > 0 Then

            If LastCallQ(0).QNo <> dt.Rows(0)("queue_no") And LastCallQ(1).QNo <> dt.Rows(0)("queue_no") Then
                QInfo = "room_no1=ช่องบริการ " & Counter(0) & "&Queue_no011=" & dt.Rows(0)("queue_no")
                QInfoVDO = "room_no1=ช่องบริการ " & Counter(0) & "&Queue_no01" & dt.Rows(0)("queue_no")
                QInfo &= "$"    'Blinking
                QInfoVDO &= "$" 'Blinking
                '*************
                InsertSound(dt.Rows(0)("queue_no"), "ช่องบริการ " & Counter(0))

                LastCallQ(0).QNo = dt.Rows(0)("queue_no")
                sql = "update tbl_QData set other_status='" & QueueStatus.Calling & "',Calling_Time=Now where queue_no='" & dt.Rows(0)("queue_no") & "'  and datediff('d',update_time,Now)=0 "
                Try
                    cmd = New OleDbCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    uplog("[UpdateCalling1] - " & ex.Message)
                End Try
            Else
                If LastCallQ(1).QNo = dt.Rows(0)("queue_no") Then
                    QInfo &= "room_no1=ช่องบริการ " & Counter(0) & "&Queue_no011= "
                    QInfoVDO &= "room_no1=ช่องบริการ " & Counter(0) & "&Queue_no01= "
                    LastCallQ(0).QNo = " "
                Else
                    QInfo = "room_no1=ช่องบริการ " & Counter(0) & "&Queue_no011=" & dt.Rows(0)("queue_no")
                    QInfoVDO = "room_no1=ช่องบริการ " & Counter(0) & "&Queue_no01=" & dt.Rows(0)("queue_no")
                    QInfo &= "$"    'Blinking
                    QInfoVDO &= "$" 'Blinking
                End If

            End If


            If dt.Rows.Count > 1 AndAlso Counter.Length > 1 Then

                QInfo &= "&room_no2=ช่องบริการ " & Counter(1) & "&Queue_no012=" & dt.Rows(1)("queue_no")
                QInfo &= "$"    'Blinking

                QInfoVDO &= "&room_no2=ช่องบริการ " & Counter(1) & "&Queue_no02=" & dt.Rows(1)("queue_no")
                QInfoVDO &= "$"    'Blinking

                If LastCallQ(1).QNo <> dt.Rows(1)("queue_no") Then
                    LastCallQ(1).QNo = dt.Rows(1)("queue_no")
                    '*************
                    InsertSound(dt.Rows(1)("queue_no"), "ช่องบริการ " & Counter(1))

                    sql = "update tbl_QData set other_status='" & QueueStatus.Calling & "',Calling_Time=Now where queue_no='" & dt.Rows(1)("queue_no") & "'  and datediff('d',update_time,Now)=0 "
                    Try
                        cmd = New OleDbCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        uplog("[UpdateCalling2] - " & ex.Message)
                    End Try
                End If
            Else
                If LastCallQ(1).QNo = dt.Rows(0)("queue_no") Then

                    If Counter.Length > 1 Then
                        QInfo &= "&room_no2=ช่องบริการ " & Counter(1) & "&Queue_no012=" & LastCallQ(1).QNo
                        QInfo &= "$"    'Blinking
                        QInfoVDO &= "&room_no2=ช่องบริการ " & Counter(1) & "&Queue_no02=" & LastCallQ(1).QNo
                        QInfoVDO &= "$"    'Blinking
                    Else
                        QInfoVDO &= "&room_no2= &Queue_no02=" & LastCallQ(1).QNo
                        QInfoVDO &= "$"    'Blinking

                        QInfo &= "&room_no2= &Queue_no012=" & LastCallQ(1).QNo
                        QInfo &= "$"    'Blinking
                    End If


                Else

                    If Counter.Length > 1 Then
                        QInfo &= "&room_no2=ช่องบริการ " & Counter(1) & "&Queue_no012= "
                        QInfoVDO &= "&room_no2=ช่องบริการ " & Counter(1) & "&Queue_no02="
                    Else
                        QInfo &= "&room_no2= &Queue_no012= "
                        QInfoVDO &= "&room_no2= &Queue_no02= "
                    End If

                    LastCallQ(1).QNo = " "
                End If

            End If

        Else
            '*** Last Calling1
            dt = New DataTable
            sql = "select queue_no from tbl_QData where queue_no='" & LastCallQ(0).QNo & "' and Other_Status='" & QueueStatus.Calling & "' and datediff('d',update_time,Now)=0 "
            da = New OleDbDataAdapter(sql, conn)
            Try
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    QInfo &= "room_no1=ช่องบริการ " & Counter(0) & "&Queue_no011=" & LastCallQ(0).QNo
                    QInfo &= "$"    'Blinking

                    QInfoVDO &= "room_no1=ช่องบริการ " & Counter(0) & "&Queue_no01=" & LastCallQ(0).QNo
                    QInfoVDO &= "$"    'Blinking
                Else
                    QInfo &= "room_no1=ช่องบริการ " & Counter(0) & "&Queue_no011= "
                    QInfoVDO &= "room_no1=ช่องบริการ " & Counter(0) & "&Queue_no01= "
                    LastCallQ(0).QNo = ""
                End If
            Catch ex As Exception
                uplog("[ChkLastCallQ] - " & ex.Message)
                QInfo &= "room_no1=ช่องบริการ " & Counter(0) & "&Queue_no011= "
                QInfoVDO &= "room_no1=ช่องบริการ " & Counter(0) & "&Queue_no01= "
                LastCallQ(0).QNo = ""
            End Try

            '*** Last Calling2
            dt = New DataTable
            sql = "select queue_no from tbl_QData where queue_no='" & LastCallQ(1).QNo & "' and Other_Status='" & QueueStatus.Calling & "' and datediff('d',update_time,Now)=0 "
            da = New OleDbDataAdapter(sql, conn)
            Try
                da.Fill(dt)
                If dt.Rows.Count > 0 Then

                    If Counter.Length > 1 Then
                        QInfo &= "&room_no2=ช่องบริการ " & Counter(1) & "&Queue_no012=" & LastCallQ(1).QNo
                        QInfo &= "$"    'Blinking
                        QInfoVDO &= "&room_no2=ช่องบริการ " & Counter(1) & "&Queue_no02=" & LastCallQ(1).QNo
                    Else
                        QInfo &= "&room_no2= &Queue_no012=" & LastCallQ(1).QNo
                        QInfo &= "$"    'Blinking
                        QInfoVDO &= "&room_no2= &Queue_no02= "
                    End If

                Else
                    If Counter.Length > 1 Then
                        QInfo &= "&room_no2=ช่องบริการ " & Counter(1) & "&Queue_no012= "
                        QInfoVDO &= "&room_no2=ช่องบริการ " & Counter(1) & "&Queue_no02= "
                    Else
                        QInfo &= "&room_no2= &Queue_no012= "
                        QInfoVDO &= "&room_no2= &Queue_no02= "
                    End If


                    LastCallQ(1).QNo = ""
                End If
            Catch ex As Exception
                uplog("[ChkLastCallQ2] - " & ex.Message)
                QInfo &= "&room_no2= &Queue_no012= "
                QInfoVDO &= "&room_no2= &Queue_no02= "
                LastCallQ(1).QNo = ""
            End Try

        End If


        '***** คิวที่ผ่านไปแล้ว  *****
        Dim tmpsql As String = ""

        If lastMissed <> "" And Not IsVDO Then
            tmpsql = " and queue_no not in (" & lastMissed & ") "
        End If

        sql = "select datediff('s',insert_Time,Now) as WaitingTime,Other_Status, Queue_No from tbl_QData where status ='" & CustomerStatus.Packed & "' and Other_Status ='" & QueueStatus.MissedCall & "'  and datediff('d',update_time,Now)=0 [%TMPSQL%] order by datediff('s',update_time,Now) desc,queue_no asc "
        dt = New DataTable
        da = New OleDbDataAdapter(sql.Replace("[%TMPSQL%]", tmpsql), conn)

        Try
            da.Fill(dt)
            If dt.Rows.Count = 0 Then
                lastMissed = ""
            End If
        Catch ex As Exception
            uplog("[GetPaked] - " & ex.Message)
            'tmrDisplay.Enabled = btnStop.Enabled
            Exit Sub
        End Try


        '*** เพื่อให้ไม่ต้อง scroll ถ้าจำนวนคิวไม่เกิน 5
        Dim isLessThan5 As Boolean = False
        If dt.Rows.Count <= 5 Then isLessThan5 = True

        Dim cnt As Int16 = 0

        QInfoVDO &= "&i="
        If isLessThan5 Then
            QInfoVDO &= "&ii=&iii="
        Else
            QInfoVDO = "ii=&iii=&" & QInfoVDO
        End If


        Dim cntVDO As Int32 = 0

        If RotateCount = PageRotateDelay Then
            RotateCount = 0
        End If


        For i As Integer = 0 To dt.Rows.Count - 1
            cnt += 1
            If cnt <= 18 Then
                QInfo &= "&Queue_no" & cnt.ToString.PadLeft(2, "0") & "=" & dt.Rows(i)("queue_no")
                If RotateCount = 0 Then
                    If lastMissed = "" Then
                        lastMissed &= "'" & dt.Rows(i)("queue_no") & "'"
                    Else
                        lastMissed &= ",'" & dt.Rows(i)("queue_no") & "'"
                    End If
                End If
            Else
                cnt -= 1
            End If
            cntVDO += 1
            'If cnt <= 20 Then
            If cntVDO <= 10 Then
                QInfoVDO &= dt.Rows(i)("queue_no") & "  "
            ElseIf cntVDO = 11 And Not isLessThan5 Then
                QInfoVDO &= "&ii=" & dt.Rows(i)("queue_no") & "  "
            ElseIf cntVDO <= 20 Then
                QInfoVDO &= dt.Rows(i)("queue_no") & "  "
            Else
                Exit For
            End If
            'End If
        Next
        QInfoVDO = Trim(QInfoVDO)
        cntVDO = 0

        For i As Int16 = cnt + 1 To 18
            QInfo &= "&Queue_no" & i.ToString.PadLeft(2, "0") & "= "
        Next


        '***** คิวที่รอจัดยา  *****
        tmpsql = ""
        If lastWait <> "" And Not IsVDO Then
            tmpsql = " and queue_no not in (" & lastWait & ") "
        End If


        sql = "select datediff('s',insert_Time,Now) as WaitingTime,Other_Status, Queue_No from tbl_QData where status in('" & CustomerStatus.Printed & "','" & CustomerStatus.Packed & "') and Other_Status ='" & QueueStatus.Waiting & "'  and datediff('d',update_time,Now)=0 [%TMPSQL%] order by datediff('s',update_time,Now) desc, queue_no asc"
        dt = New DataTable
        da = New OleDbDataAdapter(sql.Replace("[%TMPSQL%]", tmpsql), conn)

        Try
            da.Fill(dt)
            If dt.Rows.Count = 0 Then
                lastWait = ""
            End If
        Catch ex As Exception
            uplog("[GetPrinted] - " & ex.Message)
            'tmrDisplay.Enabled = btnStop.Enabled
            Exit Sub
        End Try

        cnt = 30    'เพื่อไปบวก 1 แล้ได้ 31-48
        If dt.Rows.Count <= 5 Then isLessThan5 = True Else isLessThan5 = False
        QInfoVDO &= "&j="
        If isLessThan5 Then
            QInfoVDO &= "&jj=&jjj="
        Else
            QInfoVDO = "jj=&jjj=&" & QInfoVDO
        End If


        For i As Integer = 0 To dt.Rows.Count - 1
            cnt += 1
            If cnt <= 48 Then
                QInfo &= "&Queue_no" & cnt.ToString.PadLeft(2, "0") & "=" & dt.Rows(i)("queue_no")
                If RotateCount = 0 Then
                    If lastWait = "" Then
                        lastWait &= "'" & dt.Rows(i)("queue_no") & "'"
                    Else
                        lastWait &= ",'" & dt.Rows(i)("queue_no") & "'"
                    End If
                End If
            Else
                cnt -= 1
            End If
            cntVDO += 1
            If cntVDO <= 10 Then
                QInfoVDO &= dt.Rows(i)("queue_no") & "  "
            ElseIf cntVDO = 11 And Not isLessThan5 Then
                QInfoVDO &= "&jj=" & dt.Rows(i)("queue_no") & "  "
            ElseIf cntVDO <= 20 Then
                QInfoVDO &= dt.Rows(i)("queue_no") & "  "
            Else
                Exit For
            End If
        Next
        QInfoVDO = Trim(QInfoVDO)
        cntVDO = 0

        For i As Int16 = cnt + 1 To 48
            QInfo &= "&Queue_no" & i.ToString.PadLeft(2, "0") & "= "
        Next

        QInfo &= "&i=" & config("text_crawling") & "&a=0"    'Text Crawling
        QInfoVDO &= "&a=0"

        '***** Write to Flash Data File *****
        '***** And Set Parameter To Flash *****
        Try
            If IsVDO Then
                '**** Update For VDO Display [Bee-2-Jan-2010]
                frmDisplay.flash.FlashVars = QInfoVDO
                uplog("VDO Info Updated")
                '****
            Else
                frmDisplay.flash.FlashVars = QInfo
                'Dim ff = New FileStream(config("flash_data_file"), FileMode.Create, FileAccess.Write, FileShare.Read)
                'ff.Write(System.Text.Encoding.Default.GetBytes(QInfo), 0, QInfo.Length)
                'ff.close()
                uplog("Display Updated.")
            End If
        Catch ex As Exception
            uplog("[WriteFile] - " & ex.Message)
        End Try
        'tmrDisplay.Enabled = btnStop.Enabled

    End Sub

    Sub InsertSound(ByVal Q As String, ByVal Counter As String)
        Dim conn As New OleDbConnection(getConnectionString)
        Try
            conn.Open()
        Catch ex As Exception
            uplog("[Connect] - " & ex.Message)
            'tmrDisplay.Enabled = btnStop.Enabled
            Exit Sub
        End Try
        Dim sql As String
        sql = "insert into q_Speaker (row_id,queue_no,counter_id,counter_name,nationality,call_date,status) "
        sql &= " values('" & Guid.NewGuid.ToString & "','" & Q & "',-99,'" & Counter & "','THAI',Now,0)"
        Dim cmd As New OleDbCommand(sql, conn)
        Try
            cmd.ExecuteNonQuery()
            conn.Close()
        Catch ex As Exception
            uplog("[InsertSound]" & ex.Message)
        End Try
    End Sub
End Class