Imports System.io
Imports System.Data.OleDb
Imports System.Data
Imports Queue_VRH_Display.Org.Mentalis.Files

Module VRHModule
    Dim logPath As String = "Log"
    Dim iniFileName As String = Application.StartupPath & "\vrhDisplay.ini"
    Dim flashDataFile, importedFolder, importFileFolder As String
    Public config As New Dictionary(Of String, String)
    Public DayName() As String = {"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"}
    Public CurrentDay As String = ""
    Public IsVDO As Boolean = False

    Enum CustomerStatus
        Printed = 40
        Packed = 70
        Paid = 80
        Cancel = -10
    End Enum

    Enum QueueStatus
        Calling = 4
        Finish = 3
        Waiting = 1
        MissedCall = 99
        Expired = 100
    End Enum

    Public Function formatConnectionString(ByVal argIP As String, ByVal argDB As String, ByVal argUser As String, ByVal argPassword As String) As String
        Return "Data Source=" & argIP & ";Initial Catalog=" & argDB & ";Persist Security Info=True;User ID=" & argUser & ";Password=" & argPassword
    End Function

    Function WriteLogFile(ByVal txt As TextBox, Optional ByVal lfileName As String = "QLog_") As String
        Dim ret As String = ""
        If Len(txt.Text) > 0 Then
            Dim path As String = Application.StartupPath & "\" & logPath & "\"
            Try
                If Dir(path) = "" Then
                    MkDir(path)
                End If
                Dim fName As String = path & lfileName & fixDate(Now.Date) & "-" & Guid.NewGuid.ToString & ".txt"
                Dim f As New FileStream(fName, FileMode.Create, FileAccess.Write)
                f.Write(System.Text.ASCIIEncoding.ASCII.GetBytes(txt.Text), 0, Len(txt.Text))
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

    Public Structure EXESQL
        Dim STATUS As Boolean
        Dim EX As String
    End Structure

    Public Function executeSQL(ByVal sql As String) As EXESQL
        ' Try
        Dim Conn As New OleDbConnection(getConnectionString)
        Try
            Conn.Open()
        Catch ex As Exception
            executeSQL.EX = ex.Message
            executeSQL.STATUS = False
        End Try

        If sql.Trim <> "" Then
            Dim cmd As New OleDbCommand(sql)
            cmd.Connection = Conn
            Try
                cmd.ExecuteNonQuery()
                executeSQL.STATUS = True
                executeSQL.EX = ""
            Catch ex As Exception
                executeSQL.STATUS = False
                executeSQL.EX = ex.Message
            End Try
        Else
            executeSQL.STATUS = False
            executeSQL.EX = "EMPTY SQL!"
        End If
        Try
            Conn.Close()
            Conn.Dispose()
        Catch ex As Exception

        End Try

        'GC.Collect()
    End Function

    Public Function getVersion()
        Dim VersionNo As System.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
        Return VersionNo.Major & "." & VersionNo.Minor & "." & VersionNo.Build & "." & VersionNo.Revision
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

    Sub loadConfig()
        Dim ini As New IniReader(iniFileName)
        ini.Section = "SETTING"
        Dim keys(config.Count - 1) As String
        config.Keys.CopyTo(keys, 0)
        For Each key As String In keys
            config(key) = ini.ReadString(key)
        Next
    End Sub

    Sub saveConfig()
        Dim ini As New IniReader(iniFileName)
        ini.Section = "SETTING"
        For Each key As String In config.Keys
            ini.Write(key, config(key))
        Next
    End Sub

    Function getConnectionString() As String
        Dim templateFile As String = Application.StartupPath & "\template.mdb"
        Dim f As String = Application.StartupPath & "\DB\" & fixDate(Now.Date) & ".mdb"    'Create File วันต่อวัน
        Dim err As Boolean = False
        If Not File.Exists(f) Then  'ถ้ายังไม่มี File MDB
            If File.Exists(templateFile) Then   'หา template file
                Try
                    File.Copy(templateFile, f)  'Copy template file ไปเป็น file ของวันนี้
                Catch ex As Exception
                    err = True
                    frmDisplayAgent.uplog(ex.Message)
                End Try
            Else
                err = True
                frmDisplayAgent.uplog("Template File Not Found!!!")
            End If
        End If
        If err Then
            Return ""
        End If
        Return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " & f

    End Function


End Module
