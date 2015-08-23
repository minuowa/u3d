#region 模块信息
/*----------------------------------------------------------------
// 模块名：GameData
// 模块描述：配置数据抽象类。
//----------------------------------------------------------------*/
#endregion

using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Util;
using System.Diagnostics;
using System.Security;
public class ItemCount
{
    public int id;
    public int count;
    public int Parse(string str)
    {
        return 0;
    }
}
namespace GameData
{
    public abstract class GameData
    {
        public int id;

        protected static Dictionary<int, T> GetDataMap<T>()
        {
            Dictionary<int, T> dataMap;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var type = typeof(T);
            var fileNameField = type.GetField("fileName");
            if (fileNameField != null)
            {
                var fileName = fileNameField.GetValue(null) as String;
                var result = GameDataControler.Instance.FormatData(fileName, typeof(Dictionary<int, T>), type);
                dataMap = result as Dictionary<int, T>;
            }
            else
            {
                dataMap = new Dictionary<int, T>();
            }
            sw.Stop();
            Log.Info(String.Concat(type, " time: ", sw.ElapsedMilliseconds));
            return dataMap;
        }
    }
    public abstract class GameData<T> : GameData where T : GameData<T>
    {
        public static implicit operator bool(GameData<T> c)
        {
            return c != null;
        }
        
        private static Dictionary<int, T> m_dataMap;

        public static Dictionary<int, T> dataMap
        {
            get
            {
                if (m_dataMap == null)
                    m_dataMap = GetDataMap<T>();
                return m_dataMap;
            }
            set { m_dataMap = value; }
        }
        public static T Get(int id)
        {
            if (dataMap != null && dataMap.ContainsKey(id))
                return dataMap[id];
            else
            {
                var fileNameField = typeof(T).GetField("fileName");
                if (fileNameField != null)
                {
                    var fileName = fileNameField.GetValue(null) as String;
                    string error = string.Format("Config=> 文件名：" + fileName + " error id: " + id.ToString());
                    UnityEngine.Debug.LogWarning(error);
                }
            }
            return null;
        }
    }

    public class GameDataControler : DataLoader
    {
        private List<Type> m_defaultData = new List<Type>();

        private static GameDataControler m_instance;

        public static GameDataControler Instance
        {
            get { return m_instance; }
        }

        static GameDataControler()
        {
            m_instance = new GameDataControler();
        }

        public static void Init(Action<int, int> progress = null, Action finished = null)
        {
            {
                m_instance.LoadData(m_instance.m_defaultData, m_instance.FormatXMLData, null);
                if (m_isPreloadData)
                {
                    Action action = () => { m_instance.InitAsynData(m_instance.FormatXMLData, progress, finished); };
                    if (GameMode.ReleaseMode)
                        action.BeginInvoke(null, null);
                    else
                        action();
                }
                else
                {
                    finished();
                }
            }
        }

        /// <summary>
        /// 进行读取数据准备工作和调用处理方法
        /// </summary>
        /// <param name="formatData">格式化数据方法</param>
        /// <param name="progress">处理进度回调</param>
        /// <param name="finished">处理完成回调</param>
        private void InitAsynData(Func<string, Type, Type, object> formatData, Action<int, int> progress, Action finished)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                List<Type> gameDataType = new List<Type>();
                Assembly ass = typeof(GameDataControler).Assembly;
                var types = ass.GetTypes();
                foreach (var item in types)
                {
                    if (item.Namespace == "GameData")
                    {
                        var type = item.BaseType;
                        while (type != null)
                        {
                            //#if UNITY_IPHONE
                            if (type == typeof(GameData) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(GameData<>)))//type == typeof(GameData) || 
                            //#else
                            //                                if ((type.IsGenericType && type.GetGenericTypeDefinition() == typeof(GameData<>)))
                            //#endif
                            {
                                if (!m_defaultData.Contains(item))
                                    gameDataType.Add(item);
                                break;
                            }
                            else
                            {
                                type = type.BaseType;
                            }
                        }
                    }
                }
                LoadData(gameDataType, formatData, progress);
                sw.Stop();
                Log.Debug("Asyn GameData init time: " + sw.ElapsedMilliseconds);
                GC.Collect();
                if (finished != null)
                    finished();
            }
            catch (Exception ex)
            {
                Log.Error("InitData Error: " + ex.Message);
            }
        }

        /// <summary>
        /// 加载数据逻辑
        /// </summary>
        /// <param name="gameDataType">加载数据列表</param>
        /// <param name="formatData">处理数据方法</param>
        /// <param name="progress">数据处理进度</param>
        private void LoadData(List<Type> gameDataType, Func<string, Type, Type, object> formatData, Action<int, int> progress)
        {
            List<string> nameList = new List<string>();

            var count = gameDataType.Count;
            var i = 1;
            foreach (var item in gameDataType)
            {
                var p = item.GetProperty("dataMap", ~BindingFlags.DeclaredOnly);
                var fileNameField = item.GetField("fileName");
                if (p != null && fileNameField != null)
                {
                    var fileName = fileNameField.GetValue(null) as String;
                    if (nameList.Contains(fileName))
                    {
                        UnityEngine.Debug.LogError("Repeated File=> " + fileName);
                    }
                    else
                    {
                        nameList.Add(fileName);
                        var result = formatData(String.Concat(m_resourcePath, fileName, m_fileExtention), p.PropertyType, item);
                        p.GetSetMethod().Invoke(null, new object[] { result });
                    }
                }
                if (progress != null)
                    progress(i, count);
                i++;
            }
        }

        public object FormatData(string fileName, Type dicType, Type type)
        {
            return FormatXMLData(String.Concat(m_resourcePath, fileName, m_fileExtention), dicType, type);
        }

        #region xml
        public static object ParseValue(String value, Type type)
        {
            if (type == null)
                return null;
            else if (type == typeof(string))
                return value;
            else if (type == typeof(Int32))
                return Convert.ToInt32(Convert.ToDouble(value));
            else if (type == typeof(float))
                return float.Parse(value);
            else if (type == typeof(byte))
                return Convert.ToByte(Convert.ToDouble(value));
            else if (type == typeof(sbyte))
                return Convert.ToSByte(Convert.ToDouble(value));
            else if (type == typeof(UInt32))
                return Convert.ToUInt32(Convert.ToDouble(value));
            else if (type == typeof(Int16))
                return Convert.ToInt16(Convert.ToDouble(value));
            else if (type == typeof(Int64))
                return Convert.ToInt64(Convert.ToDouble(value));
            else if (type == typeof(UInt16))
                return Convert.ToUInt16(Convert.ToDouble(value));
            else if (type == typeof(UInt64))
                return Convert.ToUInt64(Convert.ToDouble(value));
            else if (type == typeof(double))
                return double.Parse(value);
            else if (type == typeof(bool))
            {
                if (value == "0")
                    return false;
                else if (value == "1")
                    return true;
                else
                    return bool.Parse(value);
            }
            else if (type.BaseType == typeof(Enum))
            {
                return Enum.Parse(type, value);
            }
            //return ParseValue(value, Enum.GetUnderlyingType(type));
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                Type[] types = type.GetGenericArguments();
                var map = Fun.ParseMap(value);
                var result = type.GetConstructor(Type.EmptyTypes).Invoke(null);
                foreach (var item in map)
                {
                    var key = ParseValue(item.Key, types[0]);
                    var v = ParseValue(item.Value, types[1]);
                    type.GetMethod("Add").Invoke(result, new object[] { key, v });
                }
                return result;
            }
            else if (type == typeof(ItemCount))
            {
                string[] vals = value.Split(':');
                ItemCount cnt = new ItemCount();
                cnt.id = int.Parse(vals[0]);
                cnt.count = int.Parse(vals[1]);
                return cnt;
            }
            else if (type == typeof(List<ItemCount>))
            {
                string[] kinds = value.Split(',');
                List<ItemCount> l = new List<ItemCount>();
                foreach (string sk in kinds)
                {
                    ItemCount ic = new ItemCount();
                    ic.Parse(sk);
                    l.Add(ic);
                }
                return l;
            }
            else if (type == typeof(List<int>))
            {
                string[] kinds = value.Split(',');
                List<int> l = new List<int>();
                foreach (string sk in kinds)
                {
                    if(sk.Length>0)
                        l.Add(int.Parse(sk));
                }
                return l;
            }
            else if (type == typeof(Color))
            {
                string[] vals = value.Split(',');
                const float colorbase = 255f;
                Color color = new Color(
                    int.Parse(vals[0]) / colorbase
                    , int.Parse(vals[1]) / colorbase
                    , int.Parse(vals[2]) / colorbase
                    , int.Parse(vals[3]) / colorbase
                    );
                return color;
            }
            else
            {
                return null;
            }
        }
        public static string GetTag(string node)
        {
            string tag;
            if (node.Length < 3)
            {
                tag = node;
            }
            else
            {
                var tagTial = node.Substring(node.Length - 2, 2);
                if (tagTial == "_i" || tagTial == "_s" || tagTial == "_f" || tagTial == "_l" || tagTial == "_k" || tagTial == "_m")
                    tag = node.Substring(0, node.Length - 2);
                else
                    tag = node;
            }
            return tag;
        }
        public static void ParseFromXML(SecurityElement node, Type type, out object obj)
        {
            object pval = ParseValue(node.Text, type);
            if (pval != null)
            {
                obj = pval;
                return;
            }
            try
            {
                var _contor = type.GetConstructor(Type.EmptyTypes).Invoke(null);
            }
            catch (Exception)
            {
                Log.Error("FormatData Error: " + type.Name + " doesn't have an empty param ctor!");
            }


            var t = type.GetConstructor(Type.EmptyTypes).Invoke(null);


            FieldInfo[] props = type.GetFields();

            foreach (FieldInfo prop in props)
            {
                if (prop.MemberType == MemberTypes.Property || prop.MemberType == MemberTypes.Field)
                {
                    Type proptype = prop.FieldType;

                    if (proptype.IsGenericType && proptype.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var propValue = proptype.GetConstructor(Type.EmptyTypes).Invoke(null);

                        if (node.Attributes != null)
                        {
                            string _tagname=GetTag(prop.Name);

                            if (node.Attributes.ContainsKey(_tagname))
                            {
                                string str = node.Attributes[_tagname] as string;
                                object listVal = ParseValue(str, proptype);
                                if (listVal != null)
                                    proptype.GetMethod("AddRange").Invoke(propValue, new object[] { listVal });
                            }
                        }
                        if (node.Children != null)
                        {
                            foreach (var item in node.Children)
                            {
                                var child = (SecurityElement)item;
                                string tag = GetTag(child.Tag);
                                if (tag == prop.Name)
                                {
                                    foreach (var cchild in child.Children)
                                    {
                                        Type listParamType = proptype.GetGenericArguments()[0];
                                        var listItemValue = listParamType.GetConstructor(Type.EmptyTypes).Invoke(null);
                                        object listobj;
                                        ParseFromXML((SecurityElement)cchild, listParamType, out listobj);

                                        if (listobj != null)
                                        {
                                            proptype.GetMethod("Add").Invoke(propValue, new object[] { listobj });
                                        }
                                    }
                                }
                            }
                        }
                        prop.SetValue(t, propValue);
                    }
                    else
                    {
                        if (node.Attributes != null)
                        {
                            foreach (var key in node.Attributes.Keys)
                            {
                                string tag = GetTag((string)key);
                                if (tag == prop.Name)
                                {
                                    object pattr = ParseValue((string)node.Attributes[tag], proptype);
                                    if (pattr != null)
                                        prop.SetValue(t, pattr);
                                }
                            }
                        }
                        if (node.Children != null)
                        {
                            foreach (var item in node.Children)
                            {
                                var child = (SecurityElement)item;
                                string tag = GetTag(child.Tag);
                                if (tag == prop.Name)
                                {
                                    object baseobj;
                                    ParseFromXML(child, proptype, out baseobj);
                                    prop.SetValue(t, baseobj);
                                }
                            }
                        }
                    }
                }
            }


            obj = t;
        }

        private object FormatXMLData(string fileName, Type dicType, Type type)
        {
            object result = null;
            try
            {
                //Log.Debug(fileName);
                //var dicType = dicProp.PropertyType;
                result = dicType.GetConstructor(Type.EmptyTypes).Invoke(null);
                Dictionary<Int32, SecurityElement> map;//int32 为 id, string 为 属性名, string 为 属性值
                if (XMLParser.LoadIntMap(fileName, m_isUseOutterConfig, out map))
                {
                    foreach (var item in map)
                    {
                        object obj;
                        ParseFromXML(item.Value, type, out obj);
                        if (obj != null)
                            dicType.GetMethod("Add").Invoke(result, new object[] { item.Key, obj });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("FormatData Error: " + fileName + "  " + ex.Message);
            }

            return result;
        }

        #endregion

    }
}

public abstract class DataLoader
{
    protected static readonly bool m_isPreloadData = true;
    protected readonly String m_resourcePath;
    protected readonly String m_fileExtention;
    protected readonly bool m_isUseOutterConfig;
    protected Action<int, int> m_progress;
    protected Action m_finished;

    protected DataLoader()
    {
        m_isUseOutterConfig = GameMode.IsUseOutterConfig;
        if (m_isUseOutterConfig)
        {
            m_resourcePath = String.Concat(GameMode.OutterPath, GameMode.CONFIG_SUB_FOLDER);
            m_fileExtention = GameMode.XML;
        }
        else
        {
            m_resourcePath = GameMode.CONFIG_SUB_FOLDER;//兼容文件模块
            m_fileExtention = GameMode.CONFIG_FILE_EXTENSION;
        }
    }
}
