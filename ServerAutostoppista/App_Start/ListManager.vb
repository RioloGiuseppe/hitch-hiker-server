Imports System.Threading

Public Class ListManager
    Private Shared _listPassenger As List(Of PassengerItem)
    Private Shared _listupdate As List(Of ConnectedItem)

    Public Shared Sub Init()
        _listupdate = New List(Of ConnectedItem)
        _listPassenger = New List(Of PassengerItem)
    End Sub

    Public Shared ReadOnly Property User As List(Of PassengerItem)
        Get
            SyncLock (_listPassenger)
                Return _listPassenger
            End SyncLock
        End Get
    End Property

    Public Shared ReadOnly Property Update As List(Of ConnectedItem)
        Get
            SyncLock (_listupdate)
                Return _listupdate
            End SyncLock
        End Get
    End Property
End Class
