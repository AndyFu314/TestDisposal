using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestDisposal
{
    public class Derived: Base
    {
        private bool disposed = false;
        private IntPtr umResource;

        public Derived(string instanceName, List<object> tracking):
            base(instanceName, tracking)
        {
            umResource = Marshal.StringToCoTaskMemAuto(instanceName);
        }


        protected override void Dispose(bool disposing)
        //   disposing:
        //     true to release both managed and unmanaged resources; false to release only unmanaged
        //     resources.
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Console.WriteLine("[{0}].Derived.Dispose(true)", InstanceName);
                    // Release managed resources.
                }
                else
                {
                    Console.WriteLine("[{0}].Derived.Dispose(false)", InstanceName);
                }
                // Release unmanaged resources.
                if(umResource != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(umResource);
                    Console.WriteLine("[{0}] Unmanaged memory freed at {1:x16}",
                    InstanceName, umResource.ToInt64());
                    umResource = IntPtr.Zero;
                }
                disposed = true;
            }

            // Call Dispose in the base class.
            base.Dispose(disposing);
        }
        // The derived class does not have a Finalize method
        // or a Dispose method without parameters because it inherits
        // them from the base class.
    }
}
