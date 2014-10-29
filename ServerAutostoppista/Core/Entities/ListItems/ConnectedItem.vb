Public Class ConnectedItem
    Private _date As Date
    Private _username As String
    Private _gcm_id As String
    Public Sub New()
        _date = Date.Now
    End Sub
    Public Property UserName As String
        Set(value As String)
            _username = value
            _date = Date.Now
        End Set
        Get
            _date = Date.Now
            Return _username
        End Get
    End Property
    Public Property GCM_ID As String
        Set(value As String)
            _gcm_id = value
            _date = Date.Now
        End Set
        Get
            _date = Date.Now
            Return _gcm_id
        End Get
    End Property
    Public ReadOnly Property LastDate As Date
        Get
            Return _date
        End Get
    End Property
End Class

