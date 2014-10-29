Public Class FeedbackReturn
    Private _date As Date
    Public Property Writer As String
    Public Property Mark As Single
    Public Property Comment As String
    Public Property [Date] As String
        Set(value As String)
            _date = value
        End Set
        Get
            Return _date.ToString("dd/MM/yyyy")
        End Get
    End Property
    Public Property ID As Integer
End Class
