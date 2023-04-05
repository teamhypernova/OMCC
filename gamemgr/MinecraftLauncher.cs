using EDGW.Logging;
using OMCCore.Core.Game;
using OMCCore.Core.Tasks;
using OMCCore.Core.User;
using OMCCore.Cryptography;
using OMCCore.Globalization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace OMCC.Plugins.GameManager
{
    public class MinecraftLauncher : ILauncher
    {
        public static Logger logger = new Logger("Minecraft Launcher", nameof(MinecraftLauncher));
        public MinecraftVersion Version { get; set; }
        public MinecraftLauncher(MinecraftVersion version)
        {
            Version = version;
        }

        public void CompleteFiles()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            OTaskView task = new OTaskView(new Text("plugin.official.gamemgr.start.title", Version.Name));
            OTaskItem login = new OTaskItem(task, new Text("plugin.official.gamemgr.start.login"), true, 3);
            OTaskItem checkfiles = new OTaskItem(task, new Text("plugin.official.gamemgr.start.checkfiles"), 3);
            OTaskItem genargs = new OTaskItem(task, new Text("plugin.official.gamemgr.start.genargs"), true, 3);
            OTaskItem wait = new OTaskItem(task, new Text("plugin.official.gamemgr.start.wait"), true, 1);
            task.AddItems(login, checkfiles, genargs, wait);
            task.Start();
            //login
            var info = GetUserInfo();
            login.Complete();

            //start
            var prof = MinecraftProfile.Parse(Version);
            var res = Version.Resolution;
            var f = new MinecraftFeatures(res.HasCustomResolution, Version.IsDemoUser);
            CheckFiles(checkfiles, prof, f);
            checkfiles.Complete();
            var paths = new List<string>();
            paths.AddRange(Artifact); paths.AddRange(Classifiers);
            var settings = MinecraftSettings.Create(f, info, prof, paths.ToArray(), res);
            var args = GenArgs(prof, settings);
            genargs.Complete();
            Debug.WriteLine(args);
            Process.Start("java.exe", args);
            //TODO:Select correct java
        }
        public string GenArgs(MinecraftProfile prof, MinecraftSettings settings)
        {
            StringBuilder sb = new StringBuilder();
            List<IArgument> args = new List<IArgument>();
            args.AddRange(prof.Arguments.Java);
            args.Add(new StringArgument(prof.MainClass));
            if (prof.Arguments.Game != null)
            {
                args.AddRange(prof.Arguments.Game);
            }
            if (prof.Arguments.Additional != null)
            {
                args.Add(prof.Arguments.Additional);
            }
            foreach(var a in args)
            {
                foreach(var s in a.ToString(settings))
                {
                    var m = s;
                    if(m.Contains(" "))
                    {
                        m = $"\"{m}\"";
                    }
                    sb.Append(m).Append(" ");
                }
            }
            return sb.ToString();
        }
        public List<string> Artifact { get; } = new List<string>();
        public List<string> Classifiers { get; } = new List<string>();
        public void CheckFiles(OTaskItem taskItem, MinecraftProfile profile, MinecraftFeatures features)
        {
            List<string> dlfiles = new List<string>();
            Artifact.Clear();
            Classifiers.Clear();
            taskItem.MaxProgress = profile.Libraries.Count + 1;
            var enume =GetNecessaryLibraries(profile, features);
            while (enume.MoveNext())
            {
                var lib = enume.Current;
                if (lib.Artifact != null)
                {
                    string path = Path.Combine(profile.Version.Directory.LibrariesPath, lib.Artifact.Path);
                    Artifact.Add(path);
                    if (!CheckFile(path, lib.Artifact.Sha1))
                    {
                        dlfiles.Add(path);
                    }
                }
                if (lib.Classifiers != null)
                {
                    var w = lib.Classifiers.GetWindows();
                    if (w != null)
                    {
                        string path = Path.Combine(profile.Version.Directory.LibrariesPath, w.Path);
                        Classifiers.Add(path);
                        if (!CheckFile(path, w.Sha1))
                        {
                            dlfiles.Add(path);
                        }
                    }
                }
                taskItem.Progress++;
            }
            {
                var info = profile.Client;
                string path = profile.Version.JarPath;
                Artifact.Add(path);
                if (!CheckFile(path, info.Sha1))
                {
                    dlfiles.Add(path);
                }
            }
            //TODO:Download Unexisted files
        }
        public bool CheckFile(string path,string? sha1)
        {
            if (!File.Exists(path))
            {
                logger.error($"Incorrect file ({path}): File does not exist.");
                return false;
            }
            else
            {
                if (sha1 == null) return true;
                else
                {
                    var sha = MD5.GetSHA1(path).ToUpper();
                    if (sha != sha1.ToUpper())
                    {
                        logger.error($"Incorrect file ({path}): Expected to have sha1 value {sha1.ToLower()}, but actually had sha1 value {sha.ToLower()}.");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        public IEnumerator<Library> GetNecessaryLibraries(MinecraftProfile profile, MinecraftFeatures features)
        {
            foreach(var lib in profile.Libraries)
            {
                if (lib.Rules != null)
                {
                    if (lib.Rules.IsAllow(features))
                    {
                        yield return lib;
                    }
                }
                else
                {
                    yield return lib;
                }
            }
            yield break;
        }
        public static UserInfo GetUserInfo()
        {
            return UserRegistry.Current.CreateUserInfo();
        }
    }
}
