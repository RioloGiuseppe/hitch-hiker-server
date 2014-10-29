Imports System.Net.Http
Imports WebMatrix.WebData
Imports System.Web.Http

Public Class AuthInfo
    Public Property Name As String = ""
    Public Property Password As String = ""
    Public Property Role As String = ""
End Class


Public Class myauth(Of T)
    Public Delegate Function actionMethod(a As AuthInfo, parameters As T) As Object



    Public Shared Function auth(req As HttpRequestMessage, fu As actionMethod, Optional Role As String = "", Optional Parameters As T = Nothing)
        Dim a As New AuthInfo
        If GetUserNameAndPassword(req, a.Name, a.Password) Then
            If WebSecurity.Login(a.Name, a.Password, False) Then
                Dim roles = DirectCast(System.Web.Security.Roles.Provider, SimpleRoleProvider)
                If String.IsNullOrEmpty(Role) OrElse roles.IsUserInRole(a.Name, Role) Then
                    If roles.GetRolesForUser(a.Name).Length > 0 Then
                        a.Role = roles.GetRolesForUser(a.Name)(0)
                    Else
                        a.Role = ""
                    End If
                    Return fu(a, Parameters)
                Else
                    Throw New HttpResponseException(New HttpResponseMessage(System.Net.HttpStatusCode.Forbidden))
                End If
            Else
                Throw New HttpResponseException(New HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized))
            End If
        Else
            Throw New HttpResponseException(New HttpResponseMessage(System.Net.HttpStatusCode.BadRequest))
        End If
    End Function

    Private Shared Function GetUserNameAndPassword(req As HttpRequestMessage, ByRef username As String, ByRef password As String) As Boolean
        username = String.Empty
        password = String.Empty
        Dim headerVals As IEnumerable(Of String) = Nothing
        If req.Headers.TryGetValues("Authorization", headerVals) Then
            Try
                Dim authHeader As String = headerVals.FirstOrDefault()
                Dim delims As Char() = {" "c}
                Dim authHeaderTokens As String() = authHeader.Split(New Char() {" "c})
                If authHeaderTokens(0).Contains("Basic") Then
                    Dim decodedStr As String = DecodeFrom64(authHeaderTokens(1))
                    Dim unpw As String() = decodedStr.Split(New Char() {":"c})
                    username = unpw(0)
                    password = unpw(1)
                    Return True
                Else
                    Return False
                End If
            Catch
                Return False
            End Try
        End If
        Return False
    End Function
    Private Shared Function DecodeFrom64(encodedData As String) As String
        Return System.Text.Encoding.ASCII.GetString(System.Convert.FromBase64String(encodedData))
    End Function


End Class
Public Class myauth
    Public Delegate Function actionMethod(a As AuthInfo) As Object
    Public Shared Function auth(req As HttpRequestMessage, fu As actionMethod, Optional Role As String = "")
        Dim a As New AuthInfo
        If GetUserNameAndPassword(req, a.Name, a.Password) Then
            If WebSecurity.Login(a.Name, a.Password, False) Then
                Dim roles = DirectCast(System.Web.Security.Roles.Provider, SimpleRoleProvider)
                If String.IsNullOrEmpty(Role) OrElse roles.IsUserInRole(a.Name, Role) Then
                    If roles.GetRolesForUser(a.Name).Length > 0 Then
                        a.Role = roles.GetRolesForUser(a.Name)(0)
                    Else
                        a.Role = ""
                    End If
                    Return fu(a)
                Else
                    Throw New HttpResponseException(New HttpResponseMessage(System.Net.HttpStatusCode.Forbidden))
                End If
            Else
                Throw New HttpResponseException(New HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized))
            End If
        Else
            Throw New HttpResponseException(New HttpResponseMessage(System.Net.HttpStatusCode.BadRequest))
        End If
    End Function

    Private Shared Function GetUserNameAndPassword(req As HttpRequestMessage, ByRef username As String, ByRef password As String) As Boolean
        username = String.Empty
        password = String.Empty
        Dim headerVals As IEnumerable(Of String) = Nothing
        If req.Headers.TryGetValues("Authorization", headerVals) Then
            Try
                Dim authHeader As String = headerVals.FirstOrDefault()
                Dim delims As Char() = {" "c}
                Dim authHeaderTokens As String() = authHeader.Split(New Char() {" "c})
                If authHeaderTokens(0).Contains("Basic") Then
                    Dim decodedStr As String = DecodeFrom64(authHeaderTokens(1))
                    Dim unpw As String() = decodedStr.Split(New Char() {":"c})
                    username = unpw(0)
                    password = unpw(1)
                    Return True
                Else
                    Return False
                End If
            Catch
                Return False
            End Try
        End If
        Return False
    End Function
    Private Shared Function DecodeFrom64(encodedData As String) As String
        Return System.Text.Encoding.ASCII.GetString(System.Convert.FromBase64String(encodedData))
    End Function


End Class