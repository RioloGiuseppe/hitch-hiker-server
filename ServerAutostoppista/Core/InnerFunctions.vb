Imports WebMatrix.WebData
Imports System.Runtime.CompilerServices
Imports System.Web.Script.Serialization

Public Module InnerFunctions
    Public Const PASSENGER As String = "PASSENGER"
    Public Const DRIVER As String = "DRIVER"
    Public Const ADMIN As String = "ADMIN"
    'Main functions
    Public Function MiServeUnPassaggio(auth As AuthInfo, value As InputMain)
        Try
            Dim a As PassengerItem = Nothing
            Dim ind As Integer
            Try
                a = (From i In ListManager.User Where i.UserName = auth.Name Select i Take 1)(0)
                ind = ListManager.User.IndexOf(a)
            Catch ex As Exception
                a = Nothing
            End Try
            If a Is Nothing Then
                ListManager.User.Add(New PassengerItem With {.UserName = auth.Name, .Date = Date.Now, .Latitude = value.Latitude, .Longitude = value.Longitude, .Text = value.Text})
            Else
                ListManager.User(ind).Latitude = value.Latitude
                ListManager.User(ind).Longitude = value.Longitude
                ListManager.User(ind).Text = value.Text
                ListManager.User(ind).Date = Date.Now
            End If
            GCM_functions.NotifyList()
            Return "OK "
        Catch e As Exception
            Return "ERROR " + e.Message
        End Try
    End Function
    Public Function Accetto(auth As AuthInfo, value As InputMain)
        Dim ff As New IO.StreamWriter(HttpContext.Current.Request.PhysicalApplicationPath + "\file.txt")

        Try
            Dim d As Date
            Dim a = (From i In ListManager.User Where i.UserName = auth.Name Select i).ToArray
            If a IsNot Nothing AndAlso a.Length > 0 Then
                Try
                    ff.WriteLine(a(0).Date)
                    ff.Close()
                    ListManager.User.Remove(a(0))
                Catch ex As Exception
                End Try
                Dim d_r = a(0).Date.Split("/")
                d = New Date(Integer.Parse(d_r(2)), Integer.Parse(d_r(1)), Integer.Parse(d_r(0)))
            Else
                d = Date.Now
            End If
            Using db As New UsersContext
                db.Feedbacks.Add(New Feedback With {.Date = d,
                                                    .PassengerName = auth.Name,
                                                    .DriverName = value.UserName,
                                                    .DriverRating = -1,
                                                    .DriverText = "",
                                                    .PassengerRating = -1,
                                                    .PassengerText = ""})
                db.SaveChanges()
            End Using
            GCM_functions.NotifyList()
            Return "OK"
        Catch e As Exception
            Return "ERROR " + e.Message + e.StackTrace
        End Try
    End Function
    Public Function getPassengersList(Auth As AuthInfo, value As InputMain)
        Dim Lat = value.Latitude
        Dim Lon = value.Longitude
        Try
            Using db As New UsersContext
                Dim a = From e In ListManager.User
                        Join i In ListManager.Update
                        On i.UserName Equals e.UserName
                        Join u In db.Users On u.UserName Equals e.UserName
                        Select New PassengerReturn With {.UserName = e.UserName,
                                                         .Latitude = e.Latitude,
                                                         .Longitude = e.Longitude,
                                                         .[Date] = e.Date,
                                                         .Text = e.Text,
                                                         .Name = u.Name,
                                                         .Surname = u.Surname,
                                                         .GCM_ID = i.GCM_ID}
                Return a.ToArray()
            End Using
            'Return ListManager.User
        Catch e As Exception
            Return "ERROR " + e.Message
        End Try
    End Function
    'Feedback functions
    Public Function GetFeedbackListPassenger(auth As AuthInfo, Value As InputFeedback)
        Try
            Using db As New UsersContext
                Dim a = From f In db.Feedbacks Where f.PassengerName Is Value.Username And f.PassengerRating >= 0
                        Select New FeedbackReturn With {.Comment = f.PassengerText,
                                                        .Date = f.Date,
                                                        .Mark = f.PassengerRating,
                                                        .ID = f.Id,
                                                        .Writer = f.DriverName}

                Dim g = a.ToArray
                Dim tot = g.Length
                Dim p As Double = 0.0
                For i As Integer = 0 To g.Length - 1
                    p += g(i).Mark
                Next
                If tot > 0 Then
                    p = p / tot
                End If
                Return New With {.ComplessiveMark = p, .Voters = tot, .List = g}
            End Using
        Catch ex As Exception
            Return "ERROR " + ex.Message
        End Try
    End Function
    Public Function GetFeedbackListDriver(auth As AuthInfo, value As InputFeedback)
        Try
            Using db As New UsersContext
                Dim a = From f In db.Feedbacks Where f.DriverName Is value.Username And f.DriverRating >= 0
                        Select New FeedbackReturn With {.Comment = f.DriverText,
                                                        .Date = f.Date,
                                                        .Mark = f.DriverRating,
                                                        .ID = f.Id,
                                                        .Writer = f.PassengerName}
                Dim g = a.ToArray
                Dim tot = g.Length
                Dim p As Double = 0.0
                For i As Integer = 0 To g.Length - 1
                    p += g(i).Mark
                Next
                If tot > 0 Then
                    p = p / tot
                End If
                Return New With {.ComplessiveMark = p, .Voters = tot, .List = g}
            End Using
        Catch ex As Exception
            Return "ERROR " + ex.Message
        End Try
    End Function
    Public Function getFeedbackToEditPassenger(auth As AuthInfo)
        Try
            Using db As New UsersContext
                Dim a = From f In db.Feedbacks Where f.PassengerName Is auth.Name And f.DriverRating < 0
                        Select New FeedbackReturn With {.Date = f.Date,
                                                        .ID = f.Id,
                                                        .Writer = f.DriverName}
                Dim g = a.ToArray
                Dim tot = g.Length
                Dim p As Single = 0
                For i As Integer = 0 To g.Length - 1
                    p += g(i).Mark
                Next
                If tot > 0 Then
                    p = p / tot
                End If
                Return New With {.ComplessiveMark = p, .Voters = tot, .List = g}
            End Using
        Catch ex As Exception
            Return "ERROR " + ex.Message + Chr(13) + ex.ToString
        End Try
    End Function
    Public Function getFeedbackToEditDriver(auth As AuthInfo)
        Try
            Using db As New UsersContext
                Dim a = From f In db.Feedbacks Where f.DriverName Is auth.Name And f.PassengerRating < 0
                        Select New FeedbackReturn With {.Date = f.Date,
                                                        .ID = f.Id,
                                                        .Writer = f.PassengerName}
                Dim g = a.ToArray
                Dim tot = g.Length
                Dim p As Double = 0.0
                For i As Integer = 0 To g.Length - 1
                    p += g(i).Mark
                Next
                If tot > 0 Then
                    p = p / tot
                End If
                Return New With {.ComplessiveMark = p, .Voters = tot, .List = g}
            End Using
        Catch ex As Exception
            Return "ERROR " + ex.Message
        End Try
    End Function
    Public Function DriverSavePassengerFeedback(auth As AuthInfo, value As InputFeedback)
        Try
            Using db As New UsersContext
                Dim a = From f In db.Feedbacks Where f.Id = value.FeedbackID
                        Select f

                Dim b = a.ToArray()(0)
                b.PassengerText = value.Comment
                b.PassengerRating = value.Rate
                db.SaveChanges()
                Return "OK"
            End Using
        Catch ex As Exception
            Return "ERROR " + ex.Message
        End Try
    End Function
    Public Function PassengerSaveDriverFeedback(auth As AuthInfo, value As InputFeedback)
        Try
            Using db As New UsersContext
                Dim a = From f In db.Feedbacks Where f.Id = value.FeedbackID
                        Select f

                Dim b = a.ToArray()(0)
                b.DriverText = value.Comment
                b.DriverRating = value.Rate
                db.SaveChanges()
                Return "OK"
            End Using
        Catch ex As Exception
            Return "ERROR " + ex.Message
        End Try
    End Function
    'GCM getter disposer functions
    Public Function miSonoConnesso(auth As AuthInfo, value As InputMain)
        Try
            Dim a As ConnectedItem = Nothing
            Try
                a = (From i In ListManager.Update Where i.UserName = auth.Name Select i)(0)
            Catch ex As Exception
                a = Nothing
            End Try
            If a Is Nothing Then
                ListManager.Update.Add(New ConnectedItem With {.UserName = auth.Name, .GCM_ID = value.GCM_ID})
                Return "OK Added!"
            Else
                a.GCM_ID = value.GCM_ID
                Return "OK Updated!"
            End If
        Catch e As Exception
            Return "ERROR " + e.Message
        End Try
    End Function
    Public Function miSonoDisconnesso(auth As AuthInfo)
        Try
            ListManager.Update.RemoveAll(Function(o) o.UserName = auth.Name)
            Return "OK Deleted"
        Catch ex As Exception
            Return "ERROR " + ex.Message
        End Try
    End Function
    'profile functions
    Public Function getProfileInfo(auth As AuthInfo) As Object
        Try
            Using db As New UsersContext
                Dim a = From u In db.Users Where u.UserName = auth.Name
                        Select New With {.UserName = u.UserName, .Name = u.Name, .Surname = u.Surname, .Role = auth.Role}
                Return a.ToArray()(0)
            End Using
        Catch ex As Exception
            Return "ERROR " + ex.Message + Chr(10) + ex.InnerException.Message
        End Try
    End Function
    Public Function deleteUserAndAccount(auth As AuthInfo)
        Try
            DirectCast(Membership.Provider, SimpleMembershipProvider).DeleteAccount(auth.Name)
            DirectCast(Membership.Provider, SimpleMembershipProvider).DeleteUser(auth.Name, True)
            Return "OK"
        Catch e As Exception
            Return "ERROR " + e.Message
        End Try
    End Function
    Public Function editAccount(Auth As AuthInfo, value As Profile)
        Try
            If String.IsNullOrEmpty(value.Password) OrElse value.Password = Auth.Password Then
                Try
                    Using db As New UsersContext
                        Dim a = (From u In db.Users Where u.UserName = Auth.Name Select u).ToArray()(0)
                        a.Name = value.Name
                        a.Surname = value.Surname
                        db.SaveChanges()
                    End Using
                    Return "OK Account information have been successfully updated!"
                Catch e As Exception
                    Return "ERROR " + e.Message
                End Try
            Else
                Try
                    WebSecurity.ChangePassword(Auth.Name, Auth.Password, value.Password)
                    Return "The new password has been set successfully!"
                Catch ex As Exception
                    Return "There was an error while trying to change the password: The current password is incorrect or the new password is invalid."
                End Try
            End If
        Catch e As MembershipCreateUserException
            Return e.Message
        End Try
    End Function

    <Extension()> _
    Public Function IndexToJsonArray(l As List(Of ConnectedItem))
        Return (New JavaScriptSerializer).Serialize(From i In l Select i.GCM_ID)
    End Function

End Module