Imports System.ComponentModel.DataAnnotations.Schema
Imports System.ComponentModel.DataAnnotations

<Table("Users")> _
Public Class User
    <Key()> _
    <DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)> _
    Public Property UserId As Integer
    Public Property UserName As String
    Public Property Name As String
    Public Property Surname As String
End Class