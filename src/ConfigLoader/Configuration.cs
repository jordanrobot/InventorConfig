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

            SetStringOption(UserName, s => app.GeneralOptions.UserName = s);
            SetStringOption(DefaultVBAProjectFileFullFilename, s => app.FileOptions.DefaultVBAProjectFileFullFilename = s);
            SetStringOption(TemplatesPath, s => app.FileOptions.TemplatesPath = s);
            SetStringOption(DesignDataPath, s => app.FileOptions.DesignDataPath = s);
            SetStringOption(PresetsPath, s => app.FileOptions.PresetsPath = s);
            SetStringOption(SymbolLibraryPath, s => app.FileOptions.SymbolLibraryPath = s);
            SetStringOption(SheetMetalPunchesRootPath, s => app.iFeatureOptions.SheetMetalPunchesRootPath = s);
            SetStringOption(iFeatureOptionsRootPath, s => app.iFeatureOptions.RootPath = s);

            SetCCAccessOption(CCAccess, CCLibraryPath);
            SetBoolOption(CCCustomFamilyAsStandard, b => app.ContentCenterOptions.CustomFamilyAsStandard = b);
            SetBoolOption(CCRefreshOutOfDateStandardParts, b => app.ContentCenterOptions.RefreshOutOfDateStandardParts = b);

            SetBoolOption(SectionAllParts, b => app.AssemblyOptions.SectionAllParts = b);

            SetDefaultDrawingOption(DefaultDrawingFileType, l => app.DrawingOptions.DefaultDrawingFileType = (DefaultDrawingFileTypeEnum)l);
        }

        private void SetStringOption(string prop, Action<string> appOption)
        {
            if (String.IsNullOrEmpty(prop))
                return;

            try
            {
                appOption(prop);
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

        private void SetBoolOption(bool prop, Action<bool> appOption)
        {
            try
            {
                appOption(prop);
            }
            catch { }
        }

        private void SetDefaultDrawingOption(string prop, Action<int> appOption)
        {
            if (string.IsNullOrEmpty(prop))
                return;

            switch (prop.ToLower())
            {
                case "dwg":
                    appOption(69633);
                    break;

                case "idw":
                    appOption(69634);
                    break;
            }
        }
    }
}
