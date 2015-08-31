using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
public class ItemCount
{
    public int id;
    public int count;
}
public class Converter
{
    static Dictionary<Type, MethodInfo> mValueParsers;
    static Dictionary<Type, MethodInfo> mStringParsers;
    static Type mType = typeof(Converter);

    public static MethodInfo GetStringParser(Type type)
    {
        return GetParser(mStringParsers, "GetString_", type);
    }

    public static MethodInfo GetValueParser(Type type)
    {
        return GetParser(mValueParsers, "GetValue_", type);
    }
    static MethodInfo GetParser(Dictionary<Type, MethodInfo> maps,string tag, Type type)
    {
        if (type == null)
            return null;
        if (maps == null)
            maps = new Dictionary<Type, MethodInfo>();

        MethodInfo parser = null;

        Type key = type;
        if (type.BaseType == typeof(Enum))
            key = type.BaseType;

        string methodname = tag + key.Name;

        if (!maps.TryGetValue(key, out parser))
        {
            MethodInfo[] methods = mType.GetMethods();
            foreach (var m in methods)
            {
                if (m.Name == methodname)
                {
                    parser = m;
                    break;
                }
            }
            //Log.Info("GetString.Method : =>" + methodname);

            maps.Add(key, parser);
        }
        return parser;
    }
    public static object GetValue(Type type,String value)
    {
        //Log.Info("GetValue.String : =>" + value);

        MethodInfo parser = GetValueParser(type);

        if (parser != null)
        {
            object[] paras=new object[2];
            paras[0] = type;
            paras[1] = value;
            //Log.Info("GetValue.Invoke : =>" + parser.Name);
            try
            {
                return parser.Invoke(null, paras);
            }
            catch(Exception)
            {
                Log.Error(string.Format("Error Attribute => {0}:{1}", type.Name, value));
                return null;
            }
        }
        return null;
    }


    public static String GetString(Type type, object value)
    {
        //Log.Info("GetString.String : =>" + value);
        MethodInfo parser = GetStringParser(type);

        if (parser != null)
        {
            object[] paras = new object[2];
            paras[0] = type;
            paras[1] = value;
            //Log.Info("GetString.Invoke : =>" + parser.Name);
            return (string)parser.Invoke(null, paras);
        }
        return string.Empty;
    }

    public static object GetValue_String(Type type, string value)
    {
        return value;
    }

    public static object GetValue_Int32(Type type, String value)
    {
        return Int32.Parse(value);
    }

    public static object GetValue_float(Type type, String value)
    {
        return float.Parse(value);
    }
    public static object GetValue_Single(Type type, String value)
    {
        return float.Parse(value);
    }
    public static object GetValue_byte(Type type, String value)
    {
        return byte.Parse(value);

    }

    public static object GetValue_sbyte(Type type, String value)
    {
        return sbyte.Parse(value);
    }

    public static object GetValue_UInt32(Type type, String value)
    {
        return UInt32.Parse(value);

    }

    public static object GetValue_Int16(Type type, String value)
    {
        return Int16.Parse(value);
    }

    public static object GetValue_Int64(Type type, String value)
    {
        return Int64.Parse(value);

    }

    public static object GetValue_UInt16(Type type, String value)
    {
        return UInt16.Parse(value);

    }

    public static object GetValue_UInt64(Type type, String value)
    {
        return UInt64.Parse(value);

    }

    public static object GetValue_double(Type type, String value)
    {
        return double.Parse(value);

    }

    public static object GetValue_bool(Type type, String value)
    {
        return bool.Parse(value);
    }

    public static object GetValue_Enum(Type type, String value)
    {
        return Enum.Parse(type, value);

    }
    public static object GetValue_Vector3(Type type, String value)
    {
        Vector3 v = Vector3.zero;
        string[] vals = value.Split(',');
        if (vals.Length > 0)
            v.x = float.Parse(vals[0]);
        if (vals.Length > 1)
            v.y = float.Parse(vals[1]);
        if (vals.Length > 2)
            v.z = float.Parse(vals[2]);
        return v;
    }
    public static object GetValue_ItemCount(Type type, String value)
    {
        ItemCount cnt = new ItemCount();
        string[] vals = value.Split(':');
        if (vals.Length > 0)
            cnt.id = int.Parse(vals[0]);
        if (vals.Length > 1)
            cnt.count = int.Parse(vals[1]);
        return cnt;
    }
    public static object GetValue_Color(Type type, String value)
    {
        string[] vals = value.Split(',');
        Color32 color32 = new Color32();
        if (vals.Length > 0)
            color32.a = byte.Parse(vals[0]);
        if (vals.Length > 1)
            color32.r = byte.Parse(vals[1]);
        if (vals.Length > 2)
            color32.g = byte.Parse(vals[2]);
        if (vals.Length > 3)
            color32.b = byte.Parse(vals[3]);
        Color c = color32;
        return c;
    }

    public static string GetString_String(Type type, object value)
    {
        return (string)value;
    }

    public static string GetString_Int32(Type type, object value)
    {
        return value.ToString();
    }

    public static string GetString_float(Type type, object value)
    {
        return value.ToString();
    }

    public static string GetString_byte(Type type, object value)
    {
        return value.ToString();
    }

    public static string GetString_sbyte(Type type, object value)
    {
        return value.ToString();
    }

    public static string GetString_UInt32(Type type, object value)
    {
        return value.ToString();
    }

    public static string GetString_Int16(Type type, object value)
    {
        return value.ToString();
    }

    public static string GetString_Int64(Type type, object value)
    {
        return value.ToString();
    }

    public static string GetString_UInt16(Type type, object value)
    {
        return value.ToString();
    }

    public static string GetString_UInt64(Type type, object value)
    {
        return value.ToString();
    }

    public static string GetString_double(Type type, object value)
    {
        return value.ToString();
    }

    public static string GetString_bool(Type type, object value)
    {
        return value.ToString();
    }

    public static string GetString_Enum(Type type, object value)
    {
        return value.ToString();
    }
    public static string GetString_Vector3(Type type, object value)
    {
        Vector3 v = (Vector3)value;
        return string.Format("{0},{1},{2}", v.x, v.y, v.z);
    }
    public static string GetString_ItemCount(Type type, object value)
    {
         ItemCount v = (ItemCount)value;
         return string.Format("{0},{1},{2}", v.id, v.count);
    }
    public static string GetString_Color(Type type, object value)
    {
        Color color = (Color)value;
        Color32 v = color;
        return v.ToString("{0},{1},{2},{3}");
    }
}
