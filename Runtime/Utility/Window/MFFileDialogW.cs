using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Application = UnityEngine.Application;

namespace Moonflow.Utility
{
    /// <summary>
    /// 打开“选择文件”功能
    /// </summary>
    public class MFFileDialogW
    {
        #if UNITY_STANDALONE_WIN
        #region GetOpenFileName
        /// <summary>
        /// OpenFileName
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class OpenFileName
        {
            /// <summary>
            /// 
            /// </summary>
            public int structSize = 0;
            /// <summary>
            /// 
            /// </summary>
            public IntPtr dlgOwner = IntPtr.Zero;
            /// <summary>
            /// 
            /// </summary>
            public IntPtr instance = IntPtr.Zero;
            /// <summary>
            /// 
            /// </summary>
            public String filter = null;
            /// <summary>
            /// 
            /// </summary>
            public String customFilter = null;
            /// <summary>
            /// 
            /// </summary>
            public int maxCustFilter = 0;
            /// <summary>
            /// 
            /// </summary>
            public int filterIndex = 0;
            /// <summary>
            /// 
            /// </summary>
            public String file = null;
            /// <summary>
            /// 
            /// </summary>
            public int maxFile = 0;
            /// <summary>
            /// 
            /// </summary>
            public String fileTitle = null;
            /// <summary>
            /// 
            /// </summary>
            public int maxFileTitle = 0;
            /// <summary>
            /// 
            /// </summary>
            public String initialDir = null;
            /// <summary>
            /// 
            /// </summary>
            public String title = null;
            /// <summary>
            /// 
            /// </summary>
            public int flags = 0;
            /// <summary>
            /// 
            /// </summary>
            public short fileOffset = 0;
            /// <summary>
            /// 
            /// </summary>
            public short fileExtension = 0;
            /// <summary>
            /// 
            /// </summary>
            public String defExt = null;
            /// <summary>
            /// 
            /// </summary>
            public IntPtr custData = IntPtr.Zero;
            /// <summary>
            /// 
            /// </summary>
            public IntPtr hook = IntPtr.Zero;
            /// <summary>
            /// 
            /// </summary>
            public String templateName = null;
            /// <summary>
            /// 
            /// </summary>
            public IntPtr reservedPtr = IntPtr.Zero;
            /// <summary>
            /// 
            /// </summary>
            public int reservedInt = 0;
            /// <summary>
            /// 
            /// </summary>
            public int flagsEx = 0;
        }
        
        /// <summary>
        /// 获取被打开的文件名
        /// </summary>
        /// <param name="ofn"></param>
        /// <returns></returns>
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
        
        /// <summary>
        /// 保存文件窗口
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class SaveFileDlg: OpenFileName
        {

        }

        [DllImport("Comdlg32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool GetSaveFileName([In, Out] OpenFileName ofn);

        /// <summary>
        /// (预留)多格式浏览
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        static string Filter(params string[] filters)
        {
            return string.Join("\0", filters) + "\0";
        }
        /// <summary>
        /// 用资源管理器打开指定格式文件，返回文件名全路径
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="extensions">可供浏览的文件格式</param>
        /// <returns></returns>
        public static string FileDialog(string title, string extensions)
        {
            OpenFileName ofn = new OpenFileName();
            ofn.structSize = Marshal.SizeOf(ofn);

            var filters = new List<string>();
            filters.Add("All Files"); filters.Add("*.*");
            // foreach(var ext in extensions)
            // {
            //     filters.Add(ext); filters.Add("*" + ext);
            // }
            ofn.filter = Filter(extensions);
            ofn.filterIndex = 2;
            ofn.file = new string(new char[256]);
            ofn.maxFile = ofn.file.Length;
            ofn.fileTitle = new string(new char[64]);
            ofn.maxFileTitle = ofn.fileTitle.Length;
            ofn.initialDir = UnityEngine.Application.dataPath;
            ofn.title = title;
            ofn.defExt = extensions;
            ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR
            if (!GetOpenFileName(ofn))
            {
                return null;
            }

            return ofn.file;
        }
        
        /// <summary>
        /// 用资源管理器保存(选定)指定文件(的路径)，返回存储路径
        /// </summary>
        /// <param name="title">对话框开头</param>
        /// <param name="extensions">which format you want to save as</param>
        /// <returns></returns>
        public static string SaveDialog(string title, string extensions)
        {
            SaveFileDlg pth = new SaveFileDlg();
            pth.structSize = System.Runtime.InteropServices.Marshal.SizeOf(pth);
            
            var filters = new List<string>();
            filters.Add("All Files"); filters.Add("*.*");
            // foreach(var ext in extensions)
            // {
            //     filters.Add(ext); filters.Add("*" + ext);
            // }
            pth.filter = $"{extensions} (*.{extensions})";
            pth.file = new string(new char[256]);
            pth.maxFile = pth.file.Length;
            pth.fileTitle = title;
            pth.maxFileTitle = pth.fileTitle.Length;
            pth.initialDir = Application.dataPath;  // default path  
            pth.title = "保存";
            pth.defExt = extensions;
            pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
            if (GetSaveFileName(pth))
            {
                string filepath = pth.file;//选择的文件路径;  
                Debug.Log(filepath);
            }

            return pth.file;
        }
        #endregion
#endif
    }
}