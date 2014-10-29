Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Threading
Imports WebMatrix.WebData


Public Class SimpleMembershipInitializer
    Public Shared Sub Init()
        Database.SetInitializer(Of UsersContext)(Nothing)

        Try
            Using context As New UsersContext()
                If Not context.Database.Exists() Then
                    ' Creare il database SimpleMembership senza lo schema di migrazione di Entity Framework
                    CType(context, IObjectContextAdapter).ObjectContext.CreateDatabase()
                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "UserId", "UserName", autoCreateTables:=True)
                    Roles.CreateRole(InnerFunctions.PASSENGER)
                    Roles.CreateRole(InnerFunctions.ADMIN)
                    Roles.CreateRole(InnerFunctions.DRIVER)
                    WebSecurity.CreateUserAndAccount("admin", "admin", New With {.Name = "Administrator", .Surname = "Administrator", .DateBirth = "1/1/1970"})
                    Roles.AddUserToRole("admin", InnerFunctions.PASSENGER)
                Else
                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "UserId", "UserName", autoCreateTables:=True)
                End If
            End Using
        Catch ex As Exception
            Throw New InvalidOperationException("Impossibile inizializzare il database di ASP.NET Simple Membership. Per ulteriori informazioni, visitare il sito Web all'indirizzo http://go.microsoft.com/fwlink/?LinkId=256588", ex)
        End Try
    End Sub
End Class
