using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;
using Moonflow.Core;


public class MoonflowPackageUpdater : Editor
{
        public class Package
    {
        public string version;
    }

    private const string PackageName = "com.moonflow-studio.core";
    private const string PackageURL = "https://gitee.com/reguluz/moonflow-core/raw/master/package.json";
    private const string CanUpdateKey = "Moonflow.Core.CanUpdate";
    private const string CheckForUpdateText = "Moonflow/Core/检查更新";
    private const string UpdateText = "Moonflow/Core/更新";
    private const string ForceUpdateText = "Moonflow/Core/强制最新";
    private const string GitURL = "https://gitee.com/reguluz/moonflow-core.git";
    private static Version remoteVersion;

    private static async Task<bool> IsUpdateAvailable()
    {
        remoteVersion = await GetRemoteVersion();
        Version currentVersion = await GetLocalVersion();

        if (currentVersion != null)
        {
            bool updateAvailable = currentVersion.CompareTo(remoteVersion) < 0;
            if(updateAvailable)MFDebug.Log($"最新版{remoteVersion}可以使用");
            return updateAvailable;
        }
        else
        {   
            MFDebug.Log("核心已经是最新版");
            return false;
        }
    }

    private static async Task<Version> GetRemoteVersion()
    {
        WebClient wc = new WebClient();
        string json = await wc.DownloadStringTaskAsync(PackageURL);
        Regex regex = new Regex("\"version\": \"(.+)\"");
        Match match = regex.Match(json);
        string versionText = match.Groups[1].Value;
        Version version = Version.Parse(versionText);
        return version;
    }

    private static async Task<Version> GetLocalVersion()
    {
        ListRequest listRequest = Client.List(true);
        while (!listRequest.IsCompleted)
        {
            await Task.Delay(1);
        }

        foreach (PackageInfo pack in listRequest.Result)
        {
            if (pack.name == PackageName)
            {
                if (pack.source == PackageSource.Local) continue;

                Version localVersion = Version.Parse(pack.version);
                return localVersion;
            }
        }

        return null;
    }

    [MenuItem(CheckForUpdateText, false, 0)]
    [DidReloadScripts]
    private static async void CheckForUpdates()
    {
        //check for updates
        bool canUpdate = await IsUpdateAvailable();
        EditorPrefs.SetBool(CanUpdateKey, canUpdate);
    }

    [MenuItem(UpdateText, false, 0)]
    public static void Update()
    {
        string path = Application.dataPath;
        path = Directory.GetParent(path).FullName;
        path = Path.Combine(path, "Packages", "manifest.json");
        if (File.Exists(path))
        {
            string text = File.ReadAllText(path);
            Regex regex = new Regex($"\"{PackageName}\": \"(.+)\"");
            Match match = regex.Match(text);
            string versionText = match.Groups[1].Value;
            // text = regex.Replace(text, , 1);
            // Version version = Version.Parse(versionText);

            text = text.Replace(versionText, $"{GitURL}#{remoteVersion}");
            File.WriteAllText(path, text);
            AssetDatabase.Refresh();
            MFDebug.Log($"Moonflow Core已升级到{remoteVersion}版");
        }
    }

    [MenuItem(UpdateText, true)]
    private static bool CanUpdate()
    {
        return EditorPrefs.GetBool(CanUpdateKey);
    }
    
    [MenuItem(ForceUpdateText, false, 0)]
    public static void ForceUpdate()
    {
        string path = Application.dataPath;
        path = Directory.GetParent(path).FullName;
        path = Path.Combine(path, "Packages", "manifest.json");
        if (File.Exists(path))
        {
            string text = File.ReadAllText(path);
            Regex regex = new Regex($"\"{PackageName}\": \"(.+)\"");
            Match match = regex.Match(text);
            string versionText = match.Groups[1].Value;
            // text = regex.Replace(text, , 1);
            // Version version = Version.Parse(versionText);
            if (versionText.Contains("master"))
            {
                text = text.Replace(versionText, $"{GitURL}");
            }
            else
            {
                text = text.Replace(versionText, $"{GitURL}#master");
            }
            File.WriteAllText(path, text);
            AssetDatabase.Refresh();
            MFDebug.Log($"Moonflow Core已升级到最新版");
        }
    }
}
