using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
public class RecordBase
{
    public int id;

    protected static Dictionary<int, T> GetDataMap<T>()
    {
        Dictionary<int, T> recordMap;
        Stopwatch sw = new Stopwatch();
        sw.Start();
        var type = typeof(T);
        var fileNameField = type.GetField("filename");
        if (fileNameField != null)
        {
            var fileName = fileNameField.GetValue(null) as String;
            fileName = MS<Setting>.Instance.ResourcePath + fileName;
            var result = FormatXMLData(fileName, typeof(Dictionary<int, T>), type);
            recordMap = result as Dictionary<int, T>;
        }
        else
        {
            recordMap = new Dictionary<int, T>();
        }
        sw.Stop();
        Log.Info(String.Concat(type, " time: ", sw.ElapsedMilliseconds));
        return recordMap;
    }
    private static object FormatXMLData(string fileName, Type dicType, Type type)
    {
        object result = null;
        try
        {
            //Log.Debug(fileName);
            //var dicType = dicProp.PropertyType;
            result = dicType.GetConstructor(Type.EmptyTypes).Invoke(null);
            Dictionary<Int32, SecurityElement> map;//int32 为 id, string 为 属性名, string 为 属性值
            if (AResource.LoadIntMap(fileName, out map))
            {
                foreach (var item in map)
                {
                    object obj;
                    AResource.ParseFromXML(item.Value, type, out obj);
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
}
public abstract class Record<T> : RecordBase where T : Record<T>
{
    public static implicit operator bool(Record<T> c)
    {
        return c != null;
    }

    private static Dictionary<int, T> mRecordMap;

    public static Dictionary<int, T> recordMap
    {
        get
        {
            if (mRecordMap == null)
                mRecordMap = GetDataMap<T>();
            return mRecordMap;
        }
        set { mRecordMap = value; }
    }
    public static T Get(int id)
    {
        if (recordMap != null && recordMap.ContainsKey(id))
            return recordMap[id];
        else
        {
            var fileNameField = typeof(T).GetField("fileName");
            if (fileNameField != null)
            {
                var fileName = fileNameField.GetValue(null) as String;
                string error = string.Format("Config=> 文件名：" + fileName + " error id: " + id.ToString());
                Log.Warning(error);
            }
        }
        return null;
    }
}