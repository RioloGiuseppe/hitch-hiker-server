' Nota: per istruzioni su come abilitare la modalità classica di IIS6 o IIS7, 
' visitare il sito Web all'indirizzo http://go.microsoft.com/?LinkId=9394802
Imports System.Web.Http
Imports System.Web.Optimization
Imports System.Data.Entity.Infrastructure

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()

        ListManager.Init()
        WebApiConfig.Register(GlobalConfiguration.Configuration)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        AuthConfig.RegisterAuth()
        SimpleMembershipInitializer.Init()
        AsyncListCleaner.Start(30, 30, 120)
    End Sub



End Class
