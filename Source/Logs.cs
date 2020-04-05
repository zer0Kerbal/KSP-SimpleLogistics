using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

namespace SimpleLogistics
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    class Logs : MonoBehaviour
    {
        internal static void msg(string msg, params object[] @params)
        {
            if (0 != @params.Length) msg = string.Format(msg, @params);

            ScreenMessages.PostScreenMessage(msg, 2.5f, ScreenMessageStyle.UPPER_CENTER, true);
            dbg(msg, @params);
        }
        /// <summary>
        /// sends the specific message to ingame mail and screen if Debug is defined
        /// For debugging use only.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="params">The parameters.</param>
        [ConditionalAttribute("DEBUG")]
        internal static void dbg(string msg, params object[] @params)
        {
            DebugLog(msg);
           // Log(msg);
            // UnityEngine.Debug.Log("[SimpleLogistics] " + msg);
            //UnityEngine.Debug.Log("[" + SimpleLogistics.MODNAME  + "] " + msg);
            
        }

        internal enum LogType
        {
            INFO,
            WARNING,
            ERROR,
            EXCEPTION
        }

        internal class Timer : IDisposable
        {
            private string _msg = string.Empty;
            System.Diagnostics.Stopwatch _watch = null;
            public Timer(string message)
            {
                _msg = message;
                _watch = System.Diagnostics.Stopwatch.StartNew();
            }

            public void Dispose()
            {
                _watch.Stop();
                DebugLog($"{_msg}: {_watch.ElapsedMilliseconds}ms");
            }

            internal static Timer StartNew(string message)
            {
                return new Timer(message);
            }

            internal TimeSpan Elapsed()
            {
                return _watch.Elapsed;
            }
        }

        /// <summary>
        /// Logs the provided message only if built in Debug mode
        /// </summary>
        /// <param name="msg">The object to log</param>
        /// <param name="type">The type of message being logged (severity)</param>
        internal static void DebugLog(object msg, LogType type = LogType.INFO)
        {
            bool shouldLog = HighLogic.CurrentGame.Parameters.CustomParams<OptionsA>().DebugLogging;
#if DEBUG
            shouldLog = true;
#endif
            if (shouldLog)
            {
                Log(msg, type);
            }
        }

        /// <summary>
        /// Logs the provided message
        /// </summary>
        /// <param name="msg">The object to log</param>
        /// <param name="type">The type of message being logged (severity)</param>
        internal static void Log(object msg, LogType type = LogType.INFO)
        {
            string final = SimpleLogistics.MODNAME + msg?.ToString();
            if (type == LogType.INFO)
            {
                UnityEngine.Debug.Log(final);
            }
            else if (type == LogType.WARNING)
            {
                UnityEngine.Debug.LogWarning(final);
            }
            else if (type == LogType.ERROR)
            {
                UnityEngine.Debug.LogError(final);
            }
            else if (type == LogType.EXCEPTION)
            {
                Exception ex;
                if ((ex = msg as Exception) != null)
                {
                    LogException(ex, final);
                }
                else
                {
                    UnityEngine.Debug.LogError(msg);
                }
            }
        }

        /// <summary>
        /// Logs the provided Exception
        /// </summary>
        /// <param name="ex">The exception to log</param>
        internal static void LogException(Exception ex)
        {
            UnityEngine.Debug.LogException(ex);
        }
        internal static void LogException(Exception ex, string message)
        {
            Log(message);
            UnityEngine.Debug.Log(SimpleLogistics.MODNAME + ": " + message);
            UnityEngine.Debug.LogException(ex);
        }
    }

    [KSPAddon(KSPAddon.Startup.Flight, false)]
    class Logger : MonoBehaviour
    {
        public List<string> logs = new List<string>();
        public static Logger instance;
        string directory;
        public void Awake()
        {
            // logs.Add("Using SimpleLogistics! " + Version.Number);
            logs.Add("Using SimpleLogistics! " + Version.Text);
            instance = this;
            directory = KSPUtil.ApplicationRootPath + "/GameData/SimpleLogistics/Plugins/Logs/";
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            DirectoryInfo source = new DirectoryInfo(directory);
            foreach (FileInfo fi in source.GetFiles())
            {
                var creationTime = fi.CreationTime;
                if (creationTime < (DateTime.Now - new TimeSpan(1, 0, 0, 0)))
                {
                    fi.Delete();
                }
            }
        }

        public void Log(string s)
        {
            logs.Add(s);
            UnityEngine.Debug.Log(SimpleLogistics.MODNAME + ": " + s);
        }

        public void OnDisable()
        {
            if (logs.Count() == 1) return;
            string path = directory + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".txt";
            using (StreamWriter writer = File.AppendText(path))
            {
                foreach (string s in logs)
                {
                    writer.WriteLine(s);
                }
            }
        }
    }
}
