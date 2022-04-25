using System;
using System.Runtime.InteropServices;

namespace MoonflowCore.Editor.Tool
{
    public class MFMessageBox
    {
        #if UNITY_EDITOR_WIN
        
        [DllImport("User32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr handle, string message, string title, int type);
        
        #endif
        
    }
}