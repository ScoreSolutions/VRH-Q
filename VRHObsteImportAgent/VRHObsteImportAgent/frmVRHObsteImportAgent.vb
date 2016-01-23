Imports System.IO
Imports System.Data.SqlClient
Imports VRHObsteImportAgent.Org.Mentalis.Files
Imports System.Globalization

Public Class frmVRHObsteImportAgent
    Dim SourceFolderIni As String
    Dim MoveFolderFileIni As String
    Dim FlashPath As String
    Dim IPserver As String
    Dim DBName As String
    Dim Username As String
    Dim Password As String
    Dim MedicPack As String
    Dim Misscall As String
    Dim MissCallExpire As String
    Dim CloseAppHour As String
    Dim CloseAppMin As String
    Dim SwarpMisscall As Long = 0
    Dim FlagSwarpMisscall As Long = 0
    Dim SwarpCount As Long = 0
    Dim SwarpCount1 As Long = 3
    Dim MissCallChkSumRecord As Long = 0
    Dim NewTotalMisscall As Long = 0
    Dim OldTotalMisscall As Long = 0
    Dim FlagSwarp As Boolean = False
    Dim iniReaderObj As New IniReader(Application.StartupPath & "\config.ini")
    Dim DTV As New DataView
    Dim DVRoom As New DataView
    Dim QinfoMisscall As String
    Dim FlashContent As String
    Dim CallCounter1 As String
    Dim CallCounter2 As String

    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Dim pProcess() As Process = System.Diagnostics.Process.GetProcessesByName(FlashContent.Replace(".exe", ""))

        For Each p As Process In pProcess
            p.Kill()
        Next
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        LoadConfig()
        TextBox1.Text = SourceFolderIni
        TextBox2.Text = MoveFolderFileIni

        'Check DB Connection
        Dim IsConnect As Boolean = False
        Do
            txtIPServer.Text = IPserver
            txtDBName.Text = DBName
            txtUsername.Text = Username
            txtPassword.Text = Password
            IsConnect = CheckDBConnect()
        Loop Until IsConnect = True

        Threading.Thread.Sleep(10000)
        tmrImport.Enabled = True
        TimerShowDisplay.Enabled = True
        TimerCheckMisscall.Enabled = True
        'Process.Start(Application.StartupPath & "\VJH-CS5_v0r02_TonG311012.exe")
        Process.Start(Application.StartupPath & "\" & FlashContent)

    End Sub

    Public Function formatConnectionString() As String
        Return "Data Source=" & IPserver & ";Initial Catalog=" & DBName & ";User ID=" & Username & "; Password=" & Password & ";"
    End Function
    Sub StartImport()
        Dim files As String()
        Dim File As String
        Try
            If Directory.Exists(SourceFolderIni) = False Then
                Directory.CreateDirectory(SourceFolderIni)
            End If
            files = Directory.GetFiles(SourceFolderIni, "*.txt")
        Catch ex As Exception
            uplog("[StartImport] - " & ex.Message)
            Exit Sub
        End Try

        Dim vDate As DateTime = DateTime.Now
        Dim vYear As String = vDate.ToString("yyyy", New Globalization.CultureInfo("en-US"))
        Dim vMonth As String = vDate.ToString("yyyyMM", New Globalization.CultureInfo("en-US"))
        Dim vDay As String = vDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
        Dim MoveFileDir As String = MoveFolderFileIni & "\" & vYear & "\" & vMonth & "\" & vDay & "\"
        If Directory.Exists(MoveFileDir) = False Then
            Try
                Directory.CreateDirectory(MoveFileDir)
            Catch ex As Exception

            End Try
        End If

        'Delete Temp Movefile
        For Each f As String In Directory.GetFiles(MoveFolderFileIni, "*.txt")
            Try
                IO.File.SetAttributes(f, FileAttributes.Normal)
                IO.File.Delete(f)
            Catch ex As Exception

            End Try
        Next

        'lbFile.Items.Clear()
        For Each File In files
            'lbFile.Items.Add(File)
            If ImportFile(File) Then
                Try
                    IO.File.Move(File, MoveFileDir & Strings.Right(File, File.Length - InStrRev(File, "\")))
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
        'tmrImport.Enabled = st
        'GC.Collect()
    End Sub

    Private Function CheckDBConnect() As Boolean
        Dim ret As Boolean = False
        Try
            Dim conn As New SqlConnection(formatConnectionString)
            conn.Open()
            ret = True
        Catch ex As Exception

        End Try
        Return ret
    End Function

    Function ImportFile(ByVal fName As String) As Boolean
        System.Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-us")
        'Dim myDTFI = New CultureInfo("en-US", False).DateTimeFormat
        Dim f As String = ""
        'Dim update As DateTime
        Dim insert As DateTime
        Try
            f = File.ReadAllText(fName)
        Catch ex As Exception
            uplog("[ImportFile] - " & ex.Message)
            Exit Function
        End Try

        Dim data As String() = f.Split(",")
        Dim sql As String = ""
        If data.Length = 7 Then
            'If data.Length = 5 Then
            Dim conn As New SqlConnection(formatConnectionString)
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter("select Id from ImportData where Queue_no='" & data(2) & "' AND (DATEDIFF(D, Update_Time, GETDATE()) = 0)", conn)
            Try
                conn.Open()
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    'update = CDate(data(4))
                    'sql = "update tbl_QData set status='" & data(3) & "',update_time=#" & data(4) & "# where queue_no='" & data(2) & "' and (status<'" & IIf(IsNumeric(data(3)), data(3), -99) & "' or -10=" & IIf(IsNumeric(data(3)), data(3), -99) & ")"
                    sql = "UPDATE ImportData "
                    sql += " SET Status = '" & data(3) & "'"
                    sql += " , Update_Time = @UpdateTime "
                    sql += " , PaidStatus = '" & data(6) & "' "
                    sql += " WHERE Queue_no = '" & data(2) & "'  "
                    sql += " AND DATEDIFF(D, Update_Time, GETDATE()) = 0"
                    uplog("[ImportFile] - " & fName & " - Update")
                Else
                    'update = Convert.ToDateTime(data(4), "dd/MM/yyyy hh:mm:ss")
                    'update = Now.ToUniversalTime
                    insert = Now
                    sql = "INSERT INTO  ImportData(Field1, Field2, Queue_no, "
                    sql += " Status, Update_Time, "
                    sql += " Calling_Time, Other_Status, "
                    sql += " Insert_Time, Medicpack, PaidStatus) "
                    sql += " VALUES ('" & data(0) & "', '" & data(1) & "', '" & data(2) & "', "
                    sql += " '" & data(3) & "', @UpdateTime, "
                    sql += " NULL, '" & QueueStatus.Waiting & "', "
                    sql += " GETDATE(), '" & data(5) & "', '" & data(6) & "')"

                    uplog("[ImportFile] - " & fName & " - Insert")
                End If

                Dim param As New SqlParameter("@UpdateTime", SqlDbType.DateTime)
                Dim cmd As New SqlCommand(sql, conn)
                param.Value = New DateTime(data(4).Substring(6, 4), data(4).Substring(3, 2), data(4).Substring(0, 2), data(4).Substring(11, 2), data(4).Substring(14, 2), data(4).Substring(17, 2))
                cmd.Parameters.Add(param)
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch ex As Exception
                uplog("[ImportFile] - " & ex.Message)
                Return False
                conn.Close()
            End Try

        End If

        Return True
    End Function
    Dim LastMiss As String
    Dim LongWait As String
    Private Sub ShowDisplay()
        Dim Qinfo As String = ""
        Dim sql As String
        Dim conn As New SqlConnection(formatConnectionString)
        '*** Update Calling to End if Med Delivered ***
        'Sql = "update tbl_QData set other_status='" & QueueStatus.Finish & "' where status='" & CustomerStatus.Paid & "' and other_status='" & QueueStatus.Calling & "'  and datediff('d',update_time,Now)=0 "
        sql = "Update ImportData "
        sql += " SET Other_Status = '" & QueueStatus.Finish & "' "
        sql += " WHERE Other_Status = '" & QueueStatus.Calling & "' "
        sql += " AND Status = '" & CustomerStatus.Paid & "' "
        sql += " AND DATEDIFF(D, Update_Time, GETDATE()) = 0"

        Dim cmd As New SqlCommand(sql, conn)
        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
        Catch ex As Exception
            uplog("[UpdateCalling2End] - " & ex.Message)
            conn.Close()
        End Try
        '*** Update Miss Calling ***
        'Sql = "update tbl_QData set other_status='" & QueueStatus.Finish & "' where status='" & CustomerStatus.Paid & "' and other_status='" & QueueStatus.Calling & "'  and datediff('d',update_time,Now)=0 "
        sql = "Update ImportData "
        sql += " SET Other_Status = '" & QueueStatus.MissedCall & "' "
        sql += " WHERE Other_Status = '" & QueueStatus.Calling & "' "
        sql += " AND DATEDIFF(minute,Calling_Time,GETDATE()) >" & Misscall & " "
        sql += " AND DATEDIFF(D, Update_Time, GETDATE()) = 0"

        cmd = New SqlCommand(sql, conn)
        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
        Catch ex As Exception
            uplog("[UpdateCalling2End] - " & ex.Message)
            conn.Close()
        End Try

        Dim Sqlstr As String = ""
        Dim CounterPam As String = ""
        Dim DTCounter As New DataTable
        Try
            Sqlstr = "SELECT  Counter  "
            Sqlstr += " FROM ImportData "
            Sqlstr += " WHERE Other_Status = '4' AND DATEDIFF(D, Update_Time, GETDATE()) = 0 "
            Sqlstr += " GROUP BY Counter"
            Dim daCounter As New SqlDataAdapter(Sqlstr, conn)
            conn.Open()
            daCounter.Fill(DTCounter)
            If DTCounter.Rows.Count > 0 Then
                'ห้องยาเฝือกมีอยู่ 2 Counter
                For i As Integer = 0 To 4
                    If i < DTCounter.Rows.Count Then
                        If CounterPam = "" Then
                            CounterPam &= "'" & DTCounter.Rows(i).Item("Counter").ToString.Substring(DTCounter.Rows(i).Item("Counter").ToString.Length - 1) & "'"
                        Else
                            CounterPam &= ",'" & DTCounter.Rows(i).Item("Counter").ToString.Substring(DTCounter.Rows(i).Item("Counter").ToString.Length - 1) & "'"
                        End If
                    Else
                        If CounterPam = "" Then
                            CounterPam &= "'" & DTCounter.Rows(i).Item("Counter").ToString.Substring(DTCounter.Rows(i).Item("Counter").ToString.Length - 1) & "'"
                        Else
                            CounterPam &= ",'0'"
                        End If
                    End If
                Next
            End If
            daCounter = Nothing
            DTCounter = Nothing
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        Catch ex As Exception

        End Try

        If CounterPam = "" Then
            CounterPam = "'0','0','0','0','0'"
        End If
        'Show Current Calling
        sql = "EXEC dbo.ShowQueue " & CounterPam

        Dim da As New SqlDataAdapter(sql, conn)
        Dim DTQ As New DataTable

        Try
            conn.Open()
            da.Fill(DTQ)

            'DTV = DTQ.DefaultView
            'DVRoom = DTQ.DefaultView
            'DTV.Sort = "Counter_name"
            'DVRoom.Sort = "Counter_name"
            Qinfo = ""
            'Dim LastCounter As String = ""
            'Dim CounterList As New ArrayList

            Try
                If DTQ.Rows.Count > 0 Then
                    'มี 2 Counter
                    DTQ.DefaultView.RowFilter = "Counter_name='" & CallCounter1 & "'"
                    Dim QCnt As Integer = DTQ.DefaultView.Count
                    If QCnt > 0 Then
                        Qinfo &= "&CallCounter1=" & CallCounter1
                        Qinfo += "&Queue_no011=" & DTQ.DefaultView(QCnt - 1)("Queue_no") & "$"
                    Else
                        Qinfo &= "&CallCounter1="
                        Qinfo += "&Queue_no011="
                    End If

                    DTQ.DefaultView.RowFilter = "Counter_name='" & CallCounter2 & "'"
                    QCnt = DTQ.DefaultView.Count
                    If QCnt > 0 Then
                        Qinfo &= "&CallCounter2=" & CallCounter2
                        Qinfo += "&Queue_no012=" & DTQ.DefaultView(QCnt - 1)("Queue_no") & "$"
                    Else
                        Qinfo &= "&CallCounter2="
                        Qinfo += "&Queue_no012="
                    End If
                ElseIf DTQ.Rows.Count = 0 Then
                    Qinfo &= "&CallCounter1=" & "&Queue_no011=" & "&CallCounter2=" & "&Queue_no012="
                End If
                DTQ.Clear()
                conn.Close()
            Catch ex As Exception
                conn.Close()
            End Try
        Catch ex As Exception
            conn.Close()
        End Try

        'ตามหาคิวที่ Miss Call
        sql = "SELECT TOP 16  Queue_no, Other_Status, Status   "
        sql += " FROM ImportData "
        sql += " WHERE Other_Status = '99' "
        'Sql += " AND (Status IN('70')) "
        sql += " AND (DATEDIFF(D,  Insert_Time, GETDATE()) = 0) "
        sql += " Order by Queue_no DESC"
        Dim da1 As SqlDataAdapter
        da1 = New SqlDataAdapter(sql, conn)
        Try
            conn.Open()
            da1.Fill(DTQ)
            If DTQ.Rows.Count > 0 Then
                Dim j As Integer = 0
                For j = 0 To DTQ.Rows.Count - 1
                    Qinfo += "&Queue_no" & (j + 1).ToString.PadLeft(2, "0") & "=" & DTQ.Rows(j)("Queue_no")
                Next

                If (j + 1) < 16 Then
                    For j = (j + 1) To 20
                        Qinfo += "&Queue_no" & j.ToString.PadLeft(2, "0") & "="
                    Next
                End If
            Else
                For j As Integer = 0 To 19
                    Qinfo += "&Queue_no" & j.ToString.PadLeft(2, "0") & "="
                Next
            End If
            'Qinfo &= "&Queue_no26=" & QinfoMisscall '& "&Queue_no28= **กรุณาติดต่อเจ้าหน้าที่**&text22=คิวที่อาจรอนาน"
        Catch ex As Exception

        End Try
        DTQ = Nothing
        da = Nothing

        SaveTextToFile(Qinfo, Application.StartupPath & "\queueVJH_info.txt")
    End Sub
    Public Function SaveTextToFile(ByVal strData As String, ByVal FullPath As String, Optional ByVal ErrInfo As String = "") As Boolean
        Dim bAns As Boolean = False
        Dim objReader As StreamWriter

        Try

            objReader = New StreamWriter(FullPath, False, System.Text.Encoding.UTF8, strData.Length)
            objReader.Write(strData)
            objReader.Close()
            bAns = True
        Catch Ex As Exception
            ErrInfo = Ex.Message

        End Try
        Return bAns
    End Function
    Public Sub uplog(ByVal argTXT As String)
        If argTXT.Trim <> "" Then
            txtLog.Text &= "◊" & vbCrLf & Now & vbTab & argTXT & vbCrLf
            txtLog.SelectionStart = Len(txtLog.Text) : txtLog.ScrollToCaret()
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If FolderBrowserDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If FolderBrowserDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            TextBox2.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        iniReaderObj.Write("Setting", "ImportPath", TextBox1.Text)
        iniReaderObj.Write("Setting", "MoveFolderPath", TextBox2.Text)
        iniReaderObj.Write("Setting", "IPserver", txtIPServer.Text)
        iniReaderObj.Write("Setting", "DBName", txtDBName.Text)
        iniReaderObj.Write("Setting", "Username", txtUsername.Text)
        iniReaderObj.Write("Setting", "Password", txtPassword.Text)
        LoadConfig()
    End Sub
    Private Sub LoadConfig()
        SourceFolderIni = iniReaderObj.ReadString("Setting", "ImportPath", "")
        MoveFolderFileIni = iniReaderObj.ReadString("Setting", "MoveFolderPath", "")
        MedicPack = iniReaderObj.ReadString("Setting", "MedicPack", "")
        IPserver = iniReaderObj.ReadString("Setting", "IPServer", "")
        DBName = iniReaderObj.ReadString("Setting", "DBname", "")
        Username = iniReaderObj.ReadString("Setting", "Username", "")
        Password = iniReaderObj.ReadString("Setting", "Password", "")
        Misscall = iniReaderObj.ReadString("Setting", "MissCall", "")
        MissCallExpire = iniReaderObj.ReadString("Setting", "MissCallExpire", "")
        CloseAppHour = iniReaderObj.ReadString("Setting", "CloseAppHour", "")
        CloseAppMin = iniReaderObj.ReadString("Setting", "CloseAppMin", "")
        FlashContent = iniReaderObj.ReadString("Setting", "FlashContent", "")
        CallCounter1 = iniReaderObj.ReadString("Setting", "CallCounter1", "")
        CallCounter2 = iniReaderObj.ReadString("Setting", "CallCounter2", "")

        TextBox1.Text = SourceFolderIni
        TextBox2.Text = MoveFolderFileIni
        txtIPServer.Text = IPserver
        txtDBName.Text = DBName
        txtUsername.Text = Username
        txtPassword.Text = Password
    End Sub
    Private Sub StartToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartToolStripMenuItem1.Click
        tmrImport.Enabled = True
    End Sub

    Private Sub StopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopToolStripMenuItem.Click
        tmrImport.Enabled = False
    End Sub

    Private Sub tmrImport_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrImport.Tick
        tmrImport.Enabled = False
        StartImport()
        tmrImport.Enabled = True
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim sqldelsound As String = ""
        Dim sqldelqueue As String = ""
        Dim conn As New SqlConnection(formatConnectionString)
        sqldelsound = "DELETE FROM TB_Speaker WHERE DateDiff(D, Call_date, GETDATE()) <> 0"
        Dim cmd As New SqlCommand(sqldelsound, conn)
        sqldelqueue = "DELETE FROM ImportData WHERE DATEDIFF(D, Update_Time, GETDATE()) <> 0"
        Dim cmd1 As New SqlCommand(sqldelqueue, conn)

        While True
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
                conn.Open()
                cmd1.ExecuteNonQuery()
                conn.Close()
                Threading.Thread.Sleep(360000)
            Catch ex As Exception

            End Try

        End While
    End Sub

    Private Sub TimerShowDisplay_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerShowDisplay.Tick
        TimerShowDisplay.Enabled = False
        'ตรวจสอบเวลาปิดทำการ ถ้าเป็นเวลาปิดทำการแล้ว ก็ให้ปิดโปรแกรมเลย
        If Now.Hour = CInt(CloseAppHour) And Now.Minute >= CInt(CloseAppMin) Then
            tmrImport.Stop()
            TimerShowDisplay.Stop()
            TimerCheckMisscall.Stop()
            tmrImport.Enabled = False
            TimerShowDisplay.Enabled = False
            TimerCheckMisscall.Enabled = False
            End
        Else
            ShowDisplay()
        End If
        TimerShowDisplay.Enabled = True

    End Sub

    'Private Sub TimerCheckMisscall_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerCheckMisscall.Tick
    '    TimerCheckMisscall.Enabled = False

    '    Dim da1 As SqlDataAdapter
    '    Dim Sql As String = ""
    '    Dim ReplaceStr As String = ""
    '    Dim DTQ As New DataTable
    '    Dim Conn As New SqlConnection(formatConnectionString)

    '    If LastMiss <> "" Then
    '        ReplaceStr = " AND Queue_no not in (" & LastMiss & ")"
    '    End If

    '    Sql = "SELECT TOP(8)  Queue_no, Other_Status, Status   "
    '    Sql += " FROM ImportData "
    '    Sql += " WHERE Other_Status = '99' "
    '    'Sql += " AND (Status IN('70')) "
    '    Sql += " [%TMPSQL%] "
    '    Sql += " AND (DATEDIFF(D,  Insert_Time, GETDATE()) = 0) "
    '    Sql += " Order by Queue_no DESC"
    '    da1 = New SqlDataAdapter(Sql.Replace("[%TMPSQL%]", ReplaceStr), Conn)

    '    'QinfoMisscall &= "&Queue_no21="
    '    Try
    '        Conn.Open()
    '        da1.Fill(DTQ)
    '        If DTQ.Rows.Count = 0 Then
    '            LastMiss = ""
    '            QinfoMisscall = QinfoMisscall
    '        Else
    '            QinfoMisscall = ""
    '        End If

    '        For Each dtrow As DataRow In DTQ.Rows
    '            If LastMiss = "" Then
    '                LastMiss &= "'" & dtrow.Item("Queue_no") & "'"
    '            Else
    '                LastMiss &= ",'" & dtrow.Item("Queue_no") & "'"
    '            End If
    '            QinfoMisscall &= " " & dtrow.Item("Queue_no") & " "
    '        Next
    '        DTQ.Clear()
    '        Conn.Close()
    '    Catch ex As Exception
    '        Conn.Close()
    '    End Try
    '    TimerCheckMisscall.Interval = 30000
    '    'QinfoMisscall &= "&Queue_no28= **กรุณาติดต่อเจ้าหน้าที่**"
    '    DTQ = Nothing
    '    Conn = Nothing
    '    da1 = Nothing

    '    TimerCheckMisscall.Enabled = True
    'End Sub
End Class
