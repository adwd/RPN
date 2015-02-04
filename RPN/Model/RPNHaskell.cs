using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RPN.Model
{
    // http://stackoverflow.com/questions/16615641/calling-haskell-from-c-sharp
    class RPNHaskell : IDisposable
    {
        [DllImport("HSdll.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void hs_init(IntPtr argc, IntPtr argv);

        [DllImport("HSdll.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void hs_exit();

        [DllImport("HSdll.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 c_fibonacci(byte i);

        public RPNHaskell()
        {
            Debug.WriteLine("Initialising DLL...");
            hs_init(IntPtr.Zero, IntPtr.Zero);
        }

        public void Dispose()
        {
            Debug.WriteLine("Shutting down DLL...");
            hs_exit();
        }

        public UInt32 fibonacci(byte i)
        {
            Debug.WriteLine(string.Format("Calling c_fibonacci({0})...", i));
            var result = c_fibonacci(i);
            Debug.WriteLine(string.Format("Result = {0}", result));
            return result;
        }
    }
}
