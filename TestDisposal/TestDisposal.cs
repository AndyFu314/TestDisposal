using System;
using System.Collections.Generic;

namespace TestDisposal
{
    public class TestDisposal
    {
        public static void Main()
        {
            List<object> tracking = new List<object>();

            //Dispose is not called, Finalize will be called later.
            using (null)
            {
                Console.WriteLine("\nDisposal Scenario: #1\n");
                Derived d1 = new Derived("d1", tracking);
            }

            // Dispose is implicitly called in the scope of the using statement.
            using(Derived d2 = new Derived("d2", tracking))
            {
                Console.WriteLine("\nDisposal Scenario: #2\n");
            }

            // Dispose is explicitly called.
            using (null)
            {
                Console.WriteLine("\nDisposal Scenario: #3\n");
                Derived d3 = new Derived("d3", tracking);
                d3.Dispose();
            }

            // Again, Dispose is not called, Finalize will be called later.
            using (null)
            {
                Console.WriteLine("\nDisposal Scenario: #4\n");
                Derived d4 = new Derived("d4", tracking);
            }

            // List the objects remaining to dispose.
            Console.WriteLine("\nObjects remaining to dispose = {0:d}", tracking.Count);
            foreach (Derived dd in tracking)
            {
                Console.WriteLine("    Reference Object: {0:s}, {1:x16}",
                    dd.InstanceName, dd.GetHashCode());
            }

            // Queued finalizers will be exeucted when Main() goes out of scope.
            Console.WriteLine("\nDequeueing finalizers...");

            Console.ReadKey();

        }
    }
}
