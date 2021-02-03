using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using Inventor;

namespace ConfigLoader
{
    //var invApp = InventorInstance.GetInventorAppReference();
    static class InventorInstance
    {
        public static Inventor.Application GetInventorAppReference(bool forceNewInstance = false)
        {
            Inventor.Application returnInventorReference;

            if ((forceNewInstance) || (NumberOfRunningInventorInstances() == 0))
            {
                //returnInventorReference = MakeNewInventorInstance();
                return null;
            }

            // Warn user if we can't attach because too many instances found
            if ((NumberOfRunningInventorInstances() > 1))
            {
                throw new Exception("There is more than one inventor instance detected. Cannot attach.");
            }

            // Otherwise:
            returnInventorReference = (Inventor.Application)MarshalForCore.GetActiveObject("Inventor.Application");

            return returnInventorReference;
        }

        public static int NumberOfRunningInventorInstances()
        {
            return Process.GetProcessesByName("Inventor").Count();
        }

        private static Inventor.Application MakeNewInventorInstance()
        {
            var inventorAppType = Type.GetTypeFromProgID("Inventor.Application");

            var inventorInstance =
                (Inventor.Application)Activator.CreateInstance(inventorAppType);

            inventorInstance.Visible = true;

            return inventorInstance;
        }
    }

    public static class MarshalForCore
    {
        internal const String OLEAUT32 = "oleaut32.dll";
        internal const String OLE32 = "ole32.dll";

        [System.Security.SecurityCritical]  // auto-generated_required
        public static Object GetActiveObject(String progID)
        {
            Object obj = null;
            Guid clsid;

            // Call CLSIDFromProgIDEx first then fall back on CLSIDFromProgID if
            // CLSIDFromProgIDEx doesn't exist.
            try
            {
                CLSIDFromProgIDEx(progID, out clsid);
            }
            //            catch
            catch (Exception)
            {
                CLSIDFromProgID(progID, out clsid);
            }

            GetActiveObject(ref clsid, IntPtr.Zero, out obj);
            return obj;
        }

        //[DllImport(Microsoft.Win32.Win32Native.OLE32, PreserveSig = false)]
        [DllImport(OLE32, PreserveSig = false)]
        [ResourceExposure(ResourceScope.None)]
        [SuppressUnmanagedCodeSecurity]
        [System.Security.SecurityCritical]  // auto-generated
        private static extern void CLSIDFromProgIDEx([MarshalAs(UnmanagedType.LPWStr)] String progId, out Guid clsid);

        //[DllImport(Microsoft.Win32.Win32Native.OLE32, PreserveSig = false)]
        [DllImport(OLE32, PreserveSig = false)]
        [ResourceExposure(ResourceScope.None)]
        [SuppressUnmanagedCodeSecurity]
        [System.Security.SecurityCritical]  // auto-generated
        private static extern void CLSIDFromProgID([MarshalAs(UnmanagedType.LPWStr)] String progId, out Guid clsid);

        //[DllImport(Microsoft.Win32.Win32Native.OLEAUT32, PreserveSig = false)]
        [DllImport(OLEAUT32, PreserveSig = false)]
        [ResourceExposure(ResourceScope.None)]
        [SuppressUnmanagedCodeSecurity]
        [System.Security.SecurityCritical]  // auto-generated
        private static extern void GetActiveObject(ref Guid rclsid, IntPtr reserved, [MarshalAs(UnmanagedType.Interface)] out Object ppunk);
    }
}