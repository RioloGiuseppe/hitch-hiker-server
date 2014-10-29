Imports System.Net
Imports System.Web.Http
Imports ServerAutostoppista.InnerFunctions
Public Class autostopController
    Inherits ApiController

    <HttpPost()> _
    Public Function MiServeUnPassaggio(<FromBody()> value As InputMain)
        Return myauth(Of InputMain).auth(Me.Request, AddressOf InnerFunctions.MiServeUnPassaggio, PASSENGER, value)
    End Function

    <HttpPost()> _
    Public Function Accetto(<FromBody()> value As InputMain)
        Return myauth(Of InputMain).auth(Me.Request, AddressOf InnerFunctions.Accetto, PASSENGER, value)
    End Function

    <HttpPost()> _
    Public Function getPassengersList(<FromBody()> value As InputMain)
        Return myauth(Of InputMain).auth(Me.Request, AddressOf InnerFunctions.getPassengersList, DRIVER, value)
    End Function

    'Feedback functions
    <HttpPost()> _
    Public Function GetFeedbackListPassenger(<FromBody()> value As InputFeedback)
        Return myauth(Of InputFeedback).auth(Me.Request, AddressOf InnerFunctions.GetFeedbackListPassenger, DRIVER, Value)
    End Function

    <HttpPost()> _
    Public Function GetFeedbackListDriver(<FromBody()> value As InputFeedback)
        Return myauth(Of InputFeedback).auth(Me.Request, AddressOf InnerFunctions.GetFeedbackListDriver, PASSENGER, value)
    End Function

    <HttpPost()> _
    Public Function getFeedbackToEditPassenger()
        Return myauth.auth(Me.Request, AddressOf InnerFunctions.getFeedbackToEditPassenger, PASSENGER)
    End Function

    <HttpPost()> _
    Public Function getFeedbackToEditDriver()
        Return myauth.auth(Me.Request, AddressOf InnerFunctions.getFeedbackToEditDriver, DRIVER)
    End Function

    <HttpPost()> _
    Public Function DriverSavePassengerFeedback(<FromBody()> value As InputFeedback)
        Return myauth(Of InputFeedback).auth(Me.Request, AddressOf InnerFunctions.DriverSavePassengerFeedback, DRIVER, value)
    End Function

    <HttpPost()> _
    Public Function PassengerSaveDriverFeedback(<FromBody()> value As InputFeedback)
        Return myauth(Of InputFeedback).auth(Me.Request, AddressOf InnerFunctions.PassengerSaveDriverFeedback, PASSENGER, value)
    End Function
    'GCM getter disposer functions
    <HttpPost()> _
    Public Function miSonoConnesso(<FromBody()> value As InputMain)
        Return myauth(Of InputMain).auth(Me.Request, AddressOf InnerFunctions.miSonoConnesso, "", value)
    End Function
    <HttpPost()> _
    Public Function miSonoDisconnesso()
        Return myauth.auth(Me.Request, AddressOf InnerFunctions.miSonoDisconnesso, "")
    End Function


End Class
