Imports System.Web.Script.Serialization

Public Class GCM_Message


    Public Sub New(type As String, mess As String)
        data = New Dictionary(Of String, String)
        data.Add("Type", type)
        data.Add("Message", mess)
    End Sub


    Public Property data As Dictionary(Of String, String)
    Public Property registration_ids As String()

    Public Overrides Function ToString() As String
        Return (New JavaScriptSerializer).Serialize(Me)
    End Function

End Class
