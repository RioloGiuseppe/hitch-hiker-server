Imports System.Net
Imports System.IO

Friend Module GCM_functions
    Public Sub NotifyList()
        Try
            Dim result As String
            Dim Request As HttpWebRequest = WebRequest.Create(New Uri("https://android.googleapis.com/gcm/send"))
            Request.Method = "POST"
            Request.ContentType = "application/json"
            Request.Headers.Add("Authorization: key=AIzaSyCM1xZjqgAw8qrfmh-y6bfGzRUiCfGzr8Y")
            'Dim ids = ListManager.Update.IndexToJsonArray()

            'sender ID 131654239725
            Dim mmm = New GCM_Message("UPL", "")
            Dim aaa = From i In ListManager.Update Select i.GCM_ID
            mmm.registration_ids = aaa.ToArray
            Dim body As String = mmm.ToString()

            Dim bodydata As Byte() = UTF8Encoding.UTF8.GetBytes(body)
            Using a As Stream = Request.GetRequestStream
                a.Write(bodydata, 0, bodydata.Length)
            End Using
            Using a As HttpWebResponse = Request.GetResponse
                Dim reader = New StreamReader(a.GetResponseStream())
                result = reader.ReadToEnd()
            End Using
            'read result? 
        Catch e As Exception
        End Try
    End Sub

End Module
