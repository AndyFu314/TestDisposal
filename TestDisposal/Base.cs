using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDisposal
{
    public abstract class Base : IDisposable
    {
        private bool disposed = false;
        private string instanceName;
        private List<object> trackingList;

        public Base(string instanceName, List<object> tracking)
        {
            this.instanceName = instanceName;
            trackingList = tracking;
            trackingList.Add(this);
        }

        public string InstanceName
        {
            get { return instanceName; }
        }

        //Implement IDisposable.
        public void Dispose()
        {
            Console.WriteLine("\n[{0}].Base.Dispose()", instanceName);
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //Free other state(managed objects).
                    Console.WriteLine("[{0}].Base.Dispose(true)", instanceName);
                    trackingList.Remove(this);
                    Console.WriteLine("[{0}] Removed from tracking list: {1:x16}",
                    instanceName, this.GetHashCode());
                }
                else
                {
                    Console.WriteLine("[{0}].Base.Dispose(false)", instanceName);
                }
                disposed = true;
            }
        }

        // Use C# destructor syntax for finalization code.
        ~Base()
        {
            // Simply call Dispose(false).
            Console.WriteLine("\n[{0}].Base.Finalize()", instanceName);
            Dispose(false);
        }
    }
}
