Imports QSharpExpress.Org.Mentalis.Files
Imports System.Data.SqlClient
Imports System.IO

Public Class frmQueueSharpExpress
    Dim sqlstr As String = ""
    Dim IPserver As String
    Dim DBName As String
    Dim Username As String
    Dim Password As String
    Dim iniConfig As New IniReader(Application.StartupPath & "\config.ini")
    Dim SelectRoWindex As Long
    Dim SwarpCount As Long
    Dim FlagSwarp As Boolean
    Dim LastMiss As String
    Dim LongWait As String
    Dim Medicpack As String
    Dim FlashContent As String
    Dim QueueSharpFor As String
    Dim CallCounter As String   'ห้องยาเฝือก
    Dim CallCounter1 As String   'ห้องยาสูติ
    Dim CallCounter2 As String  'ห้องยาสูติ
    Public Counter As String
    Dim QinfoMisscall As String
    Dim DTV As New DataView
    Dim DVRoom As New DataView
    Dim SwarpMisscall As Long = 0
    Dim FlagSwarpMisscall As Long = 0
    Private Sub Form1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        IPserver = iniConfig.ReadString("Setting", "IPServer", "")
        DBName = iniConfig.ReadString("Setting", "DBname", "")
        Username = iniConfig.ReadString("Setting", "Username", "")
        Password = iniConfig.ReadString("Setting", "Password", "")
        Medicpack = iniConfig.ReadString("Setting", "Medicpack", "")
        FlashContent = iniConfig.ReadString("Setting", "FlashContent", "")
        QueueSharpFor = iniConfig.ReadString("Setting", "QueueSharpFor", "")
        CallCounter = iniConfig.ReadString("Setting", "CallCounter", "")
        Counter = iniConfig.ReadString("Setting", "Counter", "")
        CallCounter1 = iniConfig.ReadString("Setting", "CallCounter1", "")
        CallCounter2 = iniConfig.ReadString("Setting", "CallCounter2", "")
        ComboBox1.SelectedIndex = Counter
        AxShockwaveFlash1.LoadMovie(0, Application.StartupPath & "\" & FlashContent)
        AxShockwaveFlash1.Play()
    End Sub
    Private Sub Setgrid()
        With DataGridView1
            .Columns(0).Width = 50
            .Columns(1).Width = 135
            .Columns(2).Visible = False
            .Columns(3).Visible = False
            .Columns(4).Width = 100
            .ColumnHeadersVisible = True
            .Columns(0).HeaderText = "คิว"
            .Columns(1).HeaderText = "เวลาลงทะเบียน"
            .Columns(4).HeaderText = "สถานะ"
            .Columns(0).HeaderCell.Style.Font = New Font("Tahoma", 12, FontStyle.Regular)
            .Columns(1).HeaderCell.Style.Font = New Font("Tahoma", 12, FontStyle.Regular)
            .Columns(4).HeaderCell.Style.Font = New Font("Tahoma", 12, FontStyle.Regular)
            '.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(0).DefaultCellStyle.Font = New Font("Tahoma", 10, FontStyle.Regular)
            .Columns(1).DefaultCellStyle.Font = New Font("Tahoma", 10, FontStyle.Regular)
            .Columns(4).DefaultCellStyle.Font = New Font("Tahoma", 10, FontStyle.Regular)
            .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        'DataGridView1.Columns(4).SortMode = DataGridViewColumnSortMode.Programmatic

    End Sub
    Function WriteLogFile(ByVal txt As String, Optional ByVal lfileName As String = "QLog_") As String
        Dim ret As String = ""
        If Len(txt) > 0 Then
            Dim path As String = Application.StartupPath & "\"
            Try
                If Dir(path) = "" Then
                    MkDir(path)
                End If
                Dim fName As String = path & lfileName & (Now.Date) & "-" & Guid.NewGuid.ToString & ".txt"
                Dim f As New FileStream(fName, FileMode.Create, FileAccess.Write)
                f.Write(System.Text.ASCIIEncoding.ASCII.GetBytes(txt), 0, Len(txt))
                f.Close()
                ret = "*** LogFile writen Successfully! ***"
            Catch ex As Exception
                ret = "[WRITELOG] - " & ex.Message
            End Try
        Else
            ret = ""
        End If
        Return ret
    End Function
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        iniConfig.Write("Setting", "Counter", ComboBox1.SelectedIndex)
        Counter = iniConfig.ReadString("Setting", "Counter", "")
    End Sub
    Public Function formatConnectionString() As String
        'Return "Data Source=THOON-PC;Initial Catalog=" & DBName & ";User ID=sa; Password=1234;"
        Return "Data Source=" & IPserver & ";Initial Catalog=" & DBName & ";User ID=" & Username & "; Password=" & Password & ";"
        'Return "Data Source=" & IPserver & ";Initial Catalog=" & DBName & ";Persist Security Info=True;User ID=" & Username & ";Password=" & Password 
    End Function
    Enum CustomerStatus
        Printed = 40
        Packed = 70
        Paid = 80
        PaidMoney = 90
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
    Private Sub CallQueue()
        Dim dialg As New Dialog1
        Dim rowindex As Integer = DataGridView1.CurrentRow.Index
        Dim QueueNo As String = DataGridView1.Item(0, rowindex).Value
        Dim Result As Integer

        sqlstr = "SELECT  count(*) as count" & _
              " FROM ImportData WHERE   Queue_no ='" & QueueNo & "' and Other_Status = '" & QueueStatus.Waiting & "' AND (DATEDIFF(D,  Update_Time, GETDATE()) = 0)"
        Dim conn As New SqlConnection(formatConnectionString)

        Dim cmd As New SqlCommand(sqlstr, conn)
        Try
            conn.Open()
            Result = cmd.ExecuteScalar
            conn.Close()
            If Result = 1 Then
                sqlstr = "UPDATE    ImportData SET   Calling_Time = GETDATE(), Other_Status = '4', Counter = '" & ComboBox1.SelectedItem & "' WHERE Queue_no='" & QueueNo & "'  AND (DATEDIFF(D,  Insert_Time, GETDATE()) = 0)"
                cmd = New SqlCommand(sqlstr, conn)
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()

                sqlstr = " INSERT  INTO TB_Speaker(Queue_no, Counter_id, Counter_name, Nationality, Call_date, Status) VALUES ('" & QueueNo & "', 1, '" & ComboBox1.SelectedIndex + 1 & "', 'THAI', GETDATE(), 0)"
                cmd = New SqlCommand(sqlstr, conn)

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    conn.Close()
                    DataGridView1.Rows.RemoveAt(rowindex)
                Catch ex As Exception

                End Try
                Label2.Text = QueueNo
                'dialg.lblQueue.Text = QueueNo
                'dialg.ShowDialog()
            ElseIf Result = 0 Then


            End If
        Catch ex As Exception
            WriteLogFile(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        CallQueue()
    End Sub

    Private Sub TimerWaitingQueue_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerWaitingQueue.Tick
        sqlstr = "SELECT   Queue_no ,  CONVERT(varchar, Update_Time, 108) AS RegiserTime,Status,PaidStatus "
        sqlstr += " FROM ImportData "
        sqlstr += " WHERE Status <> '" & CustomerStatus.Paid & "' "
        sqlstr += " AND Status <>'" & CustomerStatus.Cancel & "' "
        sqlstr += " AND Other_Status = '" & QueueStatus.Waiting & "' "
        sqlstr += " AND DATEDIFF(D,  Update_Time, GETDATE()) = 0"
        sqlstr += "  ORDER BY Insert_Time ASC "
        Dim conn As New SqlConnection(formatConnectionString)
        Dim da As New SqlDataAdapter(sqlstr, conn)
        Dim dtq As New DataTable
        Try
            dtq.Clear()
            conn.Open()
            da.Fill(dtq)
            dtq.Columns.Add(New DataColumn("ColStatus", GetType(System.String)))
            For i As Integer = 0 To dtq.Rows.Count - 1
                If dtq.Rows(i).Item("Status") = "40" And dtq.Rows(i).Item("PaidStatus") <> "1" Then
                    dtq.Rows(i).Item("ColStatus") = "รอตรวจสอบ"
                ElseIf dtq.Rows(i).Item("Status") = "70" And dtq.Rows(i).Item("PaidStatus") <> "1" Then
                    dtq.Rows(i).Item("ColStatus") = "รอจ่ายยา"
                ElseIf dtq.Rows(i).Item("Status") = "90" Then
                    dtq.Rows(i).Item("ColStatus") = "ชำระเงินแล้ว"
                ElseIf dtq.Rows(i).Item("Status") = "70" And dtq.Rows(i).Item("PaidStatus") = "1" Then
                    dtq.Rows(i).Item("ColStatus") = "รอชำระเงิน"
                Else
                    'DTQ.Rows.Remove()
                End If
            Next

            dtq.AcceptChanges()
            DTV = dtq.DefaultView

            DTV.RowFilter = "ColStatus<>''"
            DTV.Sort = "ColStatus ASC"

            DataGridView1.DataSource = DTV
            Console.WriteLine(dtq.Rows.Count)
            Setgrid()
            If DTV.Count > 0 Then
                DataGridView1.Rows(SelectRoWindex).Selected = True
            End If
        Catch ex As Exception
            WriteLogFile(ex.Message)
        End Try
        dtq = Nothing
        conn.Close()
        TimerWaitingQueue.Interval = 10000
    End Sub

    Private Function GetCallingQueue() As DataTable
        Dim Conn As New SqlConnection(formatConnectionString)
        Dim SQL As String
        Dim Sqlstr As String = ""
        Dim CounterPam As String = ""
        Dim DTCounter As New DataTable

        Sqlstr = "SELECT  Counter  "
        Sqlstr += " FROM ImportData "
        Sqlstr += " WHERE Other_Status = '4' "
        Sqlstr += " AND DATEDIFF(D, Update_Time, GETDATE()) = 0 "
        Sqlstr += " GROUP BY Counter"
        Dim daCounter As New SqlDataAdapter(Sqlstr, Conn)
        daCounter.Fill(DTCounter)
        If DTCounter.Rows.Count > 0 Then
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
        daCounter.Dispose()
        DTCounter.Dispose()

        If CounterPam = "" Then
            CounterPam = "'0','0','0','0','0'"
        End If
        'Show Current Calling
        SQL = "EXEC dbo.ShowQueue " & CounterPam

        Dim da As New SqlDataAdapter(SQL, Conn)
        Dim DTQ As New DataTable
        Try
            Conn.Open()
            da.Fill(DTQ)
            Conn.Close()
        Catch ex As Exception
            DTQ = New DataTable
            Conn.Close()
        End Try
        da.Dispose()

        Return DTQ
    End Function

    Private Function ShowDisplayPetcharat() As String
        Dim Qinfo As String = ""
        Try
            Dim DTQ As DataTable = GetCallingQueue()

            DTV = DTQ.DefaultView
            DVRoom = DTQ.DefaultView
            DTV.Sort = "Counter_name"
            DVRoom.Sort = "Counter_name"

            Dim LastCounter As String = ""
            Dim CounterList As New ArrayList
            Try
                Dim start As Integer = 0
                If DTQ.Rows.Count > 0 Then
                    For Each dtrow As DataRow In DTQ.Rows
                        If LastCounter <> dtrow.Item("Counter_name") Then
                            CounterList.Add(dtrow.Item("Counter_name"))
                            LastCounter = dtrow.Item("Counter_name")
                        End If
                    Next

                    Dim TmpCounter As String = ""
                    Dim TmpQueue As String = ""
                    For i As Integer = 1 To 5
                        If i <= CounterList.Count Then
                            DVRoom.RowFilter = "Counter_name='" & CounterList(i - 1) & "'"
                        Else
                            DVRoom.RowFilter = "Counter_name=''"
                        End If

                        If i <= CounterList.Count Then
                            TmpCounter &= "&Counter_no" & i & "=" & Trim(CounterList(i - 1))
                        Else
                            TmpCounter &= "&Counter_no" & i & "="
                        End If

                        Dim rowindex As Integer = 0
                        For j As Integer = start To start + 4
                            If rowindex < DVRoom.Count And DVRoom.Count <> 0 Then
                                TmpQueue &= "&Queue_no" & (j + i).ToString.PadLeft(2, "0") & "=" & DVRoom(rowindex).Item("Queue_no") & "$"
                            Else
                                TmpQueue &= "&Queue_no" & (j + i).ToString.PadLeft(2, "0") & "="
                            End If
                            rowindex += 1
                        Next
                        TmpQueue += Chr(13)
                        start = start + 4
                        DVRoom.RowFilter = "1=1"
                    Next
                    Qinfo += TmpCounter & Chr(13) & TmpQueue
                ElseIf DTQ.Rows.Count = 0 Then
                    Qinfo &= "&Counter_no1=&Counter_no2=&Counter_no3=&Counter_no4=&Counter_no5="
                    Qinfo += "&Queue_no01=&Queue_no02=&Queue_no03=&Queue_no04=&Queue_no05="
                    Qinfo += "&Queue_no06=&Queue_no07=&Queue_no08=&Queue_no09=&Queue_no10="
                    Qinfo += "&Queue_no11=&Queue_no12=&Queue_no13=&Queue_no14=&Queue_no15="
                    Qinfo += "&Queue_no16=&Queue_no17=&Queue_no18=&Queue_no19=&Queue_no20="
                    Qinfo += "&Queue_no21=&Queue_no22=&Queue_no23=&Queue_no24=&Queue_no25="
                End If
                DTQ.Clear()


                'ตามหาคิวที่ Misscall
                Dim Sql As String = "SELECT TOP(8)  Queue_no, Other_Status, Status   "
                Sql += " FROM ImportData "
                Sql += " WHERE Other_Status = '99' "
                'Sql += " AND (Status IN('70')) "
                Sql += " AND (DATEDIFF(D,  Insert_Time, GETDATE()) = 0) "
                Sql += " Order by Queue_no DESC"
                '        da1 = New SqlDataAdapter(Sql.Replace("[%TMPSQL%]", ReplaceStr), Conn)
                Dim conn As New SqlConnection(formatConnectionString)
                Dim da1 As New SqlDataAdapter(Sql, conn)
                Try
                    conn.Open()
                    da1.Fill(DTQ)
                    Dim j As Integer = 26
                    If DTQ.Rows.Count > 0 Then
                        For Each dtrow As DataRow In DTQ.Rows
                            Qinfo &= "&Queue_no" & j.ToString & "=" & dtrow.Item("Queue_no")
                            j += 1
                        Next
                    End If
                    If j < 33 Then
                        For j = j To 33
                            Qinfo &= "&Queue_no" & j.ToString & "="
                        Next
                    End If

                    DTQ.Clear()
                    conn.Close()
                Catch ex As Exception
                    conn.Close()
                End Try

                DTQ.Dispose()
            Catch ex As Exception

            End Try
        Catch
        End Try

        'Qinfo &= "&Queue_no26=" & QinfoMisscall '& "&Queue_no28= **กรุณาติดต่อเจ้าหน้าที่**"
        Return Qinfo
    End Function

    Private Function ShowDisplayOrtho() As String
        Dim Qinfo As String = ""
        Try
            Dim DTQ As DataTable = GetCallingQueue()

            DTV = DTQ.DefaultView
            DVRoom = DTQ.DefaultView
            DTV.Sort = "Counter_name"
            DVRoom.Sort = "Counter_name"
            Qinfo = ""
            Dim LastCounter As String = ""
            Dim CounterList As New ArrayList

            Try
                Dim CallCounterParm As String = "&CallCounter"
                Dim CallingQueueParm As String = "&Queue_no011"
                Dim start As Integer = 0
                If DTQ.Rows.Count > 0 Then
                    'ช่องบริการ 2,3 Hardcode
                    'ถ้าเรียกพร้อมกัน 2 Counter ให้แสดงคิวที่เรียกทีหลัง
                    For rowindex As Integer = 0 To DTQ.Rows.Count - 1
                        Dim drq As DataRow = DTQ.Rows(rowindex)

                        Qinfo &= CallCounterParm & (rowindex + 1) & "=" & CallCounter
                        'Qinfo &= "&CallQueue_no" & (rowindex + 1).ToString.PadLeft(2, "0") & "=" & drq("Queue_no") & "$"
                        Qinfo += CallingQueueParm & "=" & drq("Queue_no") & "$"
                    Next
                ElseIf DTQ.Rows.Count = 0 Then
                    Qinfo &= CallCounterParm & "1=" & CallCounter & CallingQueueParm & "="
                End If
                DTQ.Dispose()
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try

        Qinfo += GetQueueMisscall()
        Return Qinfo
    End Function

    Private Function GetQueueMisscall() As String
        Dim Qinfo As String = ""
        'ตามหาคิวที่ Miss Call
        Dim Sql As String = "SELECT TOP 16  Queue_no, Other_Status, Status   "
        Sql += " FROM ImportData "
        Sql += " WHERE Other_Status = '99' "
        'Sql += " AND (Status IN('70')) "
        Sql += " AND (DATEDIFF(D,  Insert_Time, GETDATE()) = 0) "
        Sql += " Order by Queue_no DESC"

        Dim Conn As New SqlConnection(formatConnectionString)
        Dim da1 As SqlDataAdapter
        da1 = New SqlDataAdapter(Sql, Conn)
        Try
            Conn.Open()
            Dim DTQ As New DataTable
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
            Conn.Close()
            'Qinfo &= "&Queue_no26=" & QinfoMisscall '& "&Queue_no28= **กรุณาติดต่อเจ้าหน้าที่**&text22=คิวที่อาจรอนาน"
        Catch ex As Exception

        End Try
        Return Qinfo
    End Function

    Private Function ShowDisplayObste() As String
        Dim Qinfo As String = ""
        Try
            Dim DTQ As DataTable = GetCallingQueue()
            'DTV = DTQ.DefaultView
            'DVRoom = DTQ.DefaultView
            'DTV.Sort = "Counter_name"
            'DVRoom.Sort = "Counter_name"
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
                DTQ.Dispose()
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try

        Qinfo += GetQueueMisscall()
        Return Qinfo
    End Function

    Private Sub TimerShowDisplay_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerShowDisplay.Tick
        Dim Qinfo As String = ""
        Select Case QueueSharpFor
            Case "ห้องยาตึกเพชรรัตน์ 1 2"
                Qinfo = ShowDisplayPetcharat()
            Case "ห้องยาเฝือก"
                Qinfo = ShowDisplayOrtho()
            Case "ห้องยาสูติ"
                Qinfo = ShowDisplayObste()
        End Select

        SaveTextToFile(Qinfo, Application.StartupPath & "\queueVJH_info.txt")
        'Timer2.Interval = 10000
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

    Private Sub btnMissCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMissCall.Click
        Dim conn As New SqlConnection(formatConnectionString)
        Dim cmd As New SqlCommand
        sqlstr = "UPDATE    ImportData "
        sqlstr += " SET Other_Status = '99' "
        sqlstr += " WHERE Queue_no='" & Label2.Text & "' AND (DATEDIFF(D,  Insert_Time, GETDATE()) = 0)"
        cmd = New SqlCommand(sqlstr, conn)
        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
        Catch ex As Exception

        End Try
        Label2.Text = ""
    End Sub

    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        SelectRoWindex = DataGridView1.CurrentRow.Index
    End Sub

    'Private Sub TimerMissCall_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerMissCall.Tick
    '    Dim da1 As SqlDataAdapter
    '    Dim Sql As String = ""
    '    Dim ReplaceStr As String = ""
    '    Dim DTQ As New DataTable
    '    Dim Conn As New SqlConnection(formatConnectionString)

    '    If LastMiss <> "" Then
    '        ReplaceStr = " AND Queue_no not in (" & LastMiss & ")"
    '    End If

    '    Sql = "SELECT TOP(8)  Queue_no, Other_Status, Status   "
    '    Sql += " FROM ImportData WHERE (Other_Status = '99') "
    '    Sql += " [%TMPSQL%] AND (DATEDIFF(D,  Update_Time, GETDATE()) = 0) "
    '    Sql += " Order by Queue_no ASC"
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
    '    TimerMissCall.Interval = 30000
    '    'QinfoMisscall &= "&Queue_no28= **กรุณาติดต่อเจ้าหน้าที่**"
    '    DTQ = Nothing
    '    Conn = Nothing
    '    da1 = Nothing
    'End Sub


    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then

            CallQueue()
        End If
    End Sub


End Class
