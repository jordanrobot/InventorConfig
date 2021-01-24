Imports Inventor

Public Class Configuration

    Public ConfigName As String
    Public UserName As String '
    Public DefaultVBAProjectFileFullFilename As String '
    Public TemplatesPath As String '
    Public DesignDataPath As String '
    Public PresetsPath As String '
    Public SymbolLibraryPath As String '
    Public SheetMetalPunchesRootPath As String '
    Public iFeatureOptionsRootPath As String '
    Public CCAccess As String '
    Public CCLibraryPath As String '
    Public CCCustomFamilyAsStandard As Boolean '
    Public CCRefreshOutOfDateStandardParts As Boolean '
    Public SectionAllParts As Boolean '
    Public DefaultDrawingFileType As String '
    Public ExternalRuleDirectories() As String 'TODO!

    Private Application As Inventor.Application

    Public Sub Apply(ByRef app As Inventor.Application)
        Application = app

        SetStringOption(UserName, Application.GeneralOptions.UserName)
        SetStringOption(DefaultVBAProjectFileFullFilename, Application.FileOptions.DefaultVBAProjectFileFullFilename)
        SetStringOption(TemplatesPath, Application.FileOptions.TemplatesPath)
        SetStringOption(DesignDataPath, Application.FileOptions.DesignDataPath)
        SetStringOption(PresetsPath, Application.FileOptions.PresetsPath)
        SetStringOption(SymbolLibraryPath, Application.FileOptions.SymbolLibraryPath)
        SetStringOption(SheetMetalPunchesRootPath, Application.iFeatureOptions.SheetMetalPunchesRootPath)
        SetStringOption(iFeatureOptionsRootPath, Application.iFeatureOptions.RootPath)

        SetCCAccessOption(CCAccess, CCLibraryPath)
        SetBoolOption(CCCustomFamilyAsStandard, Application.ContentCenterOptions.CustomFamilyAsStandard)
        SetBoolOption(CCRefreshOutOfDateStandardParts, Application.ContentCenterOptions.RefreshOutOfDateStandardParts)

        SetBoolOption(SectionAllParts, Application.AssemblyOptions.SectionAllParts)

        SetDefaultDrawingOption(DefaultDrawingFileType, Application.DrawingOptions.DefaultDrawingFileType)

    End Sub


    Private Sub SetStringOption(ByVal prop As String, ByRef appOption As Object)

        If String.IsNullOrEmpty(prop) Then
            Return
        End If

        Try
            appOption = prop
        Catch ex As Exception

        End Try

    End Sub


    Private Sub SetCCAccessOption(ByVal access As String, ByRef path As String)

        If String.IsNullOrEmpty(access) Then Return


        Select Case access.ToLower()
            Case "desktop"
                If String.IsNullOrEmpty(path) Then Return

                Application.ContentCenterOptions.SetAccessOption(Inventor.ContentCenterAccessOptionEnum.kInventorDesktopAccess, path)

            Case "vault"
                Application.ContentCenterOptions.SetAccessOption(Inventor.ContentCenterAccessOptionEnum.kVaultOrProductstreamServerAccess)

        End Select

    End Sub

    Private Sub SetBoolOption(ByVal prop As Boolean, ByRef appOption As Object)

        Try
            appOption = prop
        Catch ex As Exception

        End Try

    End Sub

    Private Sub SetDefaultDrawingOption(prop As String, appOption As Object)

        If String.IsNullOrEmpty(prop) Then Return

        Select Case prop.ToLower()
            Case "dwg"
                appOption = 69633

            Case "idw"
                appOption = 69634

        End Select

    End Sub


End Class
