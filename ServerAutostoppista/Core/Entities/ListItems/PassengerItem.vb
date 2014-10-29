Imports System.Math
Public Class PassengerItem
    Private _date As Date
    Public Property UserName As String
    Public Property Latitude As Single
    Public Property Longitude As Single
    Public Property [Date] As String
        Set(value As String)
            _date = Convert.ToDateTime(value)
        End Set
        Get
            Return _date.ToString("dd/MM/yyyy")
        End Get
    End Property
    Public Property Text As String

    Public Function InRangeOf(Lat As Single, Lon As Single, Range As Single) As Boolean
        If Sqrt(Pow(Me.Latitude - Lat, 2.0) + Pow(Me.Longitude - Lon, 2.0)) < Range Then
            Return True
        End If
        Return False
    End Function
End Class