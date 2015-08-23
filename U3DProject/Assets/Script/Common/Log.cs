using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEditor;

    public class Log
    {
        public static void Warning(string format, params object[] args)
        {
            string str = string.Format(format, args);
            UnityEngine.Debug.LogWarning(str);
        }
        public static void Error(string format, params object[] args)
        {
            string str = string.Format(format, args);
            UnityEngine.Debug.LogError(str);
        }
        public static void Info(string format, params object[] args)
        {
            string str = string.Format(format, args);
            UnityEngine.Debug.Log(str);
        }
        public static void Debug(string str)
        {
            UnityEngine.Debug.Log(str);
        }
        public static void Except(Exception str)
        {
            UnityEngine.Debug.LogException(str);
        }
        public static void Assert(bool b, string msg = "")
        {
            if (!b)
            {
                Info(msg);
                EditorUtility.DisplayDialog("Y_Y", msg, "OK");
            }
        }
    }
