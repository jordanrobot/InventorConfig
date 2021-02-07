using System;
using Inventor;
//using Autodesk.iLogic.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace ConfigLoader
{
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
        public bool CleanExternalRuleDirectories;
        private Application app;
        private dynamic iLogicAuto;

        public void LoadConfigurationIntoInventor(Application application)
        {
            app = application;
            iLogicAuto = GetiLogicAddIn(app);


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

            EmptyExternalRuleDirectories();
            SetExternalRuleDirectories();

        }

        private void SetStringOption(string prop, Action<string> appOption)
        {
            if (String.IsNullOrEmpty(prop))
                return;

            try
            {
                appOption(prop);
            }
            catch (Exception e)
            {
                throw new SystemException("The value of the " + nameof(appOption) + " setting in the json configuration file was invalid.", e);
            }
        }

        private void SetCCAccessOption(string access, string path)
        {
            if (string.IsNullOrEmpty(access))
                return;

            try
            {
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
            catch (Exception e)
            {
                throw new SystemException("The value of the CCLibraryPath setting in the json configuration file was invalid.", e);
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

        /// <summary>
        /// Set the External iLogic Rule directories
        /// </summary>
//        internal IiLogicAutomation iLogicAutomation { get; private set; }
        internal void SetExternalRuleDirectories()
        {
            if (ExternalRuleDirectories.Length == 0)
                return;

            //Add iLogic Directories
            string[] oldDirectories = iLogicAuto.FileOptions.ExternalRuleDirectories;
            List<string> newDirectories = oldDirectories.OfType<string>().ToList();

            //if it isn't already in there, add it!
            foreach (string i in ExternalRuleDirectories)
            {
                if (!newDirectories.Contains(i))
                {
                    newDirectories.Add(i);
                }
            }

            try
            {
                if (newDirectories.Count > oldDirectories.Length)
                {
                    iLogicAuto.FileOptions.ExternalRuleDirectories = newDirectories.ToArray();
                }

            }
            catch (Exception e)
            {
                throw new SystemException("An error occurred while updating the External iLogic Rule Directories", e);
            }
        }

        private void EmptyExternalRuleDirectories()
        {
            if (CleanExternalRuleDirectories)
            {
                string[] temp = { };
                iLogicAuto.FileOptions.ExternalRuleDirectories = temp;
            }
        }

        internal object GetiLogicAddIn(Inventor.Application _app)
        {
            ApplicationAddIns appAddIns = _app.ApplicationAddIns;
            ApplicationAddIn addIn = appAddIns.ItemById["{3bdd8d79-2179-4b11-8a5a-257b1c0263ac}"];

            if (addIn == null)
                throw new SystemException("The iLogic add-in could not be found by ID {3bdd8d79-2179-4b11-8a5a-257b1c0263ac}.");

            try
            {
                addIn.Activate();
                return addIn.Automation;
            }
            catch (Exception e)
            {
                throw new SystemException("The iLogic add-in could not be accessed.", e);
            }
        }


    }
}
