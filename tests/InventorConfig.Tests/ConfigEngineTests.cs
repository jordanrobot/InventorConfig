using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventorConfig;
using System.Windows;

namespace InventorConfig.Tests
{
    [TestClass]
    public class ConfigEngineTests
    {
        [TestMethod]
        public void GetFileContents_true()
        {
            ConfigEngine.GetConfigFileContents("c:\\Windows\\win.ini");
         }
    }
}
