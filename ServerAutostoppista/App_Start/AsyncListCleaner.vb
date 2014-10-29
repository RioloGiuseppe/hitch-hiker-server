Imports System.Threading

Public Class AsyncListCleaner
    Private Class dat
        Public _userListTime As Double
        Public _updateLisTime As Double
        Public _clock As Integer
    End Class

    Private Shared Sub _clean(data As Object)
        While True
            Dim a = DirectCast(data, dat)
            ListManager.User.RemoveAll(Function(o) Date.Now.Subtract(o.Date).TotalMinutes >= a._userListTime)
            ListManager.Update.RemoveAll(Function(o) Date.Now.Subtract(o.LastDate).TotalMinutes >= a._userListTime)
            Thread.Sleep(a._clock * 1000)
        End While
    End Sub
    Private Shared a As New Thread(AddressOf _clean)

    Public Shared Sub Start(UserListItemDuration As Double, UpdateListItemDuration As Double, Clock As Integer)
        a.Start(DirectCast(New dat With {._userListTime = UserListItemDuration, ._updateLisTime = UpdateListItemDuration, ._clock = Clock}, Object))
    End Sub

End Class
