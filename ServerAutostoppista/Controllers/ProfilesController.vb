Imports System.Net
Imports System.Web.Http
Imports WebMatrix.WebData
Imports System.Reflection
Imports System.Web.Http.Controllers
Imports System.Net.Http

Public Class ProfilesController
    Inherits ApiController

    'profile functions
    <HttpPost()> _
    Public Function getProfileInfo(auth As AuthInfo) As Object
        Return myauth.auth(Me.Request, AddressOf InnerFunctions.getProfileInfo)
    End Function

    <HttpPost()> _
    Public Function deleteUserAndAccount(auth As AuthInfo)
        Return myauth.auth(Me.Request, AddressOf InnerFunctions.deleteUserAndAccount)
    End Function

    <HttpPost()> _
    Public Function editAccount(Auth As AuthInfo, value As Profile)
        Return myauth(Of Profile).auth(Me.Request, AddressOf InnerFunctions.editAccount, Parameters:=value)
    End Function

    <HttpPost()> _
    Public Function createAccount(<FromBody()> ByVal value As Profile) As String
        Try
            If value.Role = "User" Then
                WebSecurity.CreateUserAndAccount(value.UserName, value.Password, New With {.Name = value.Name, .Surname = value.Surname})
                Roles.AddUserToRole(value.UserName, "User")
            ElseIf value.Role = "Driver" Then
                WebSecurity.CreateUserAndAccount(value.UserName, value.Password, New With {.Name = value.Name, .Surname = value.Surname})
                Roles.AddUserToRole(value.UserName, "Driver")
            Else
                Return "Invalid role!"
            End If
            Return "OK Done!"
        Catch e As MembershipCreateUserException
            Return "ERROR " + e.Message
        End Try
    End Function

End Class