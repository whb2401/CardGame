using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace FLH.Editor.Config
{
    public class BuildConfig
    {
        [MenuItem("Tools/Config/Open")]
        public static void OpenConfigExcel()
        {
            var path = Application.dataPath.Replace("/Assets", "") + "/ConfigExport/BallBall.xlsx";
            UnityEngine.Debug.Log($"config excel path:{path}");
            Process.Start(path);
        }

        [MenuItem("Tools/Config/Export")]
        public static void ExportConfig()
        {
            var path = Application.dataPath.Replace("/Assets", "") + "/ConfigExport/";
            var proc = new Process();
            proc.StartInfo.WorkingDirectory = path;
            proc.StartInfo.FileName = "__export.bat";
            proc.Start();
            proc.WaitForExit();
        }

        [MenuItem("Tools/Config/Copy")]
        public static void CopyConfig()
        {
            var sourceDirPath = Application.dataPath.Replace("/Assets", "") + "/ConfigExport/config_server/protos";
            var saveDirPath = Application.dataPath + "/Data/Config/Addressable";
            try
            {
                var files = Directory.GetFiles(sourceDirPath);
                foreach (string file in files)
                {
                    var pFilePath = saveDirPath + "\\" + Path.GetFileName(file);
                    File.Copy(file, pFilePath, true);
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError(ex);
            }
        }
    }
}
