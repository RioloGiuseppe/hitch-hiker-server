Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Globalization
Imports System.Data.Entity

<Table("Feedbacks")> _
Public Class Feedback
    <Key()> _
    <DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)> _
    Public Property Id As Integer

    Public Property PassengerName As String
    Public Property DriverName As String
    Public Property [Date] As Date
    Public Property PassengerRating As Single
    Public Property PassengerText As String
    Public Property DriverRating As Single
    Public Property DriverText As String
End Class