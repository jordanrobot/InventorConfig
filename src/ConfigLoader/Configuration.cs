using System;
using Inventor;

namespace ConfigLoader
{
    //TODO: Set the ExternalRuleDirectories
    public class Configuration
    {
        public string ConfigName;
        public string UserName;
        public string DefaultVBAProjectFileFullFilename;
        public string TemplatesPath;
        public string DesignDataPath;
        public string PresetsPath;
        public string SymbolLibraryPath;
        public string SheetMetalPunchesRootPath;
        public string iFeatureOptionsRootPath;
        public string CCAccess;
        public string CCLibraryPath;
        public bool CCCustomFamilyAsStandard;
        public bool CCRefreshOutOfDateStandardParts;
        public bool SectionAllParts;
        public string DefaultDrawingFileType;
        public string[] ExternalRuleDirectories;
        private Application app;

        public void apply(Application application)
        {

            app = application;

            SetStringOption(UserName, app.GeneralOptions.UserName);
            SetStringOption(DefaultVBAProjectFileFullFilename, app.FileOptions.DefaultVBAProjectFileFullFilename);
            SetStringOption(TemplatesPath, app.FileOptions.TemplatesPath);
            SetStringOption(DesignDataPath, app.FileOptions.DesignDataPath);
            SetStringOption(PresetsPath, app.FileOptions.PresetsPath);
            SetStringOption(SymbolLibraryPath, app.FileOptions.SymbolLibraryPath);
            SetStringOption(SheetMetalPunchesRootPath, app.iFeatureOptions.SheetMetalPunchesRootPath);
            SetStringOption(iFeatureOptionsRootPath, app.iFeatureOptions.RootPath);

            SetCCAccessOption(CCAccess, CCLibraryPath);
            SetBoolOption(CCCustomFamilyAsStandard, app.ContentCenterOptions.CustomFamilyAsStandard);
            SetBoolOption(CCRefreshOutOfDateStandardParts, app.ContentCenterOptions.RefreshOutOfDateStandardParts);

            SetBoolOption(SectionAllParts, app.AssemblyOptions.SectionAllParts);

            SetDefaultDrawingOption(DefaultDrawingFileType, app.DrawingOptions.DefaultDrawingFileType);

        }

        private void SetStringOption(string prop, object appOption)
        {
            if (String.IsNullOrEmpty(prop))
                return;

            try
            {
                appOption = prop;
            }
            catch
            {
            }
        }

        private void SetCCAccessOption(string access, string path)
        {
            if (string.IsNullOrEmpty(access))
                return;


            switch (access.ToLower())
            {

                case "desktop":
                    if (string.IsNullOrEmpty(path))
                        return;

                    app.ContentCenterOptions.SetAccessOption(Inventor.ContentCenterAccessOptionEnum.kInventorDesktopAccess, path);
                    break;

                case "vault":
                    app.ContentCenterOptions.SetAccessOption(Inventor.ContentCenterAccessOptionEnum.kVaultOrProductstreamServerAccess);
                    break;
            }
        }

        private void SetBoolOption(bool prop, object appOption)
        {
            try
            {
                appOption = prop;
            }
            catch { }

        }

        private void SetDefaultDrawingOption(string prop, object appOption)
        {
            if (string.IsNullOrEmpty(prop))
                return;

            switch (prop.ToLower())
            {
                case "dwg":
                    appOption = 69633;
                    break;

                case "idw":
                    appOption = 69634;
                    break;
            }
        }
    }
}
