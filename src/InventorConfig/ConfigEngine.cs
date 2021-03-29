using System;

namespace InventorConfig
{
    public class ConfigEngine
    {
        protected bool _closeInventorAfterCompletion;
        protected Configuration Config { get; set; }
        protected Inventor.Application App { get; set; }

        protected Inventor.Application GetInventorInstance()
        {
            try
            {
                Inventor.Application i = InventorInstance.GetInventorAppReference();
                return i;
            }
            catch (Exception e)
            {
                throw new SystemException("The Inventor application could not be started on this computer.  Is it installed?  Process aborted, press any key to continue...", e);
            }
        }

        protected void DecideIfWeShouldCloseInventorAfterCompletion()
        {
            if (InventorInstance.NumberOfRunningInventorInstances() == 0)
            { _closeInventorAfterCompletion = true; }
        }

        protected void CloseInventorIfRequired()
        {
            if (_closeInventorAfterCompletion)
            {
                App.Quit();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}