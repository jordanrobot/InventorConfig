'</CollectorHeader>
Imports System.IO

Module Program '<CollectorPrepend>'</CollectorPrepend>
    Public Sub Main()

        Dim configPath As String = GetConfigFile()

        Dim configRaw As String = GetConfigRaw(configPath)

        Dim configuration As Configuration = DeserializeConfiguration(configRaw)

        Dim closeApp As Boolean
        Dim app As Inventor.Application = GetInventorInstance(closeApp)

        configuration.Apply(app)

        Close(app, closeApp)

        Console.WriteLine("The options have been set!  Press any key to exit.")
        Console.ReadLine()
    End Sub

    Private Function GetInventorInstance(ByRef closeApp As Boolean) As Inventor.Application
        Try
            Dim appType As Type = Type.GetTypeFromProgID("Inventor.Application")
            Return System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application")
        Catch ex As Exception
            closeApp = True
            Return CreateObject("Inventor.Application", "")
        End Try
    End Function

    Private Function GetConfigFile() As String

        Dim pathToExecutable As String
        pathToExecutable = Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().Location)

        Return pathToExecutable + Path.DirectorySeparatorChar + "config.json"

    End Function

    Private Function GetConfigRaw(ByRef configPath As String) As String

        Try
            Return File.ReadAllText(configPath)
        Catch ex As Exception
            Throw New SystemException("The config file could not be found.")
        End Try

    End Function

    Private Function DeserializeConfiguration(configRaw As String) As Configuration
        Try
            Return Newtonsoft.Json.JsonConvert.DeserializeObject(Of Configuration)(configRaw)
        Catch ex As Exception
            Console.WriteLine("The configuration was invalid, process aborted.  Press any key to exit...")
            Throw New SystemException("The configuration was invalid, process aborted.", ex)
        End Try

    End Function

    Private Sub Close(ByRef app As Inventor.Application, closeApp As Boolean)
        If (closeApp = True) Then
            app.Quit()
            GC.WaitForPendingFinalizers()
        End If
    End Sub

End Module