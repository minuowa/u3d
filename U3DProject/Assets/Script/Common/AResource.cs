using UnityEngine;
using System.Collections;
using System.Security;
using System.Collections.Generic;
using System.Xml;
using System;
using System.Text;
using System.Reflection;
using Mono.Xml;
using System.IO;

public class XMLFile
{
}
public class AResource
{
    public static char ListSeparation=';';

    public static string GetResourcePath(string file)
    {
        if(string.IsNullOrEmpty(file))
            return string.Empty;
        file = file.ToLower();
        int pos = file.IndexOf(MS<Setting>.Instance.ResourcePath);
        if (pos == -1)
            return string.Empty;
        return file.Substring(pos + MS<Setting>.Instance.ResourcePath.Length);
    }

    public static T LoadResource<T>(string name) where T : UnityEngine.Object
    {
        return Resources.Load(name, typeof(T)) as T;
    }
    public static GameObject Instance(string prefab)
    {
        GameObject prefabobj = LoadResource<GameObject>(prefab);
        if (prefabobj)
            return GameObject.Instantiate(prefabobj) as GameObject;
        return null;
    }
    public static string LoadFile(string file)
    {
        if (file.IndexOf(MS<Setting>.Instance.XMLExtension) < 0)
            file += MS<Setting>.Instance.XMLExtension;
        if (File.Exists(file))
        {
            return File.ReadAllText(file);
        }
        return string.Empty;
    }
    public static SecurityElement LoadEles(String fileName)
    {
        SecurityParser securityParser = new SecurityParser();
        string xml = LoadFile(fileName);
        securityParser.LoadXml(xml);
        return securityParser.ToXml();
    }
    /// <summary>
    /// 加载一个xml文件填充到指定结构体       
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="file"></param>
    /// <returns></returns>
    ///  AMap amap = AResource.LoadXML<AMap>("data/xml/903");
    public static T LoadXML<T>(string file) where T : XMLFile
    {
        if (string.IsNullOrEmpty(file))
            return null;
        SecurityElement root = LoadEles(file);
        if (root == null)
            return null;

        object obj;
        ParseFromXML(root, typeof(T), out obj);
        if (obj != null)
            return (T)obj;
        return null;
    }

    ///  AResource.SaveXML(amap, "Assets/Resources/data/xml/905.xml");
    public static void SaveXML<T>(T obj, string file) where T : XMLFile
    {
        if (obj == null)
            return;

        XmlDocument doc = new XmlDocument();
        XmlDeclaration dlcl = doc.CreateXmlDeclaration("1.0", "utf-8", string.Empty);
        doc.AppendChild(dlcl);
        XmlElement root = doc.CreateElement("root");
        doc.AppendChild(root);
        SaveNode(root, string.Empty, doc, obj);
        if (file.IndexOf(MS<Setting>.Instance.XMLExtension) == -1)
            file += MS<Setting>.Instance.XMLExtension;
        doc.Save(file);
    }
    public static string GetTag(string node)
    {
        return node;
    }
    public static void SaveNode(XmlElement node, string propName,XmlDocument doc,object obj)
    {
        string res = Converter.GetString(obj.GetType(), obj);
        if (res.Length > 0)
        {
            node.SetAttribute(propName, res);
        }
        else
        {
            Type type = obj.GetType();

            FieldInfo[] props = type.GetFields();

            foreach (FieldInfo prop in props)
            {
                if (prop.IsStatic)
                    continue;
                if (prop.MemberType == MemberTypes.Property || prop.MemberType == MemberTypes.Field)
                {
                    System.Type proptype = prop.FieldType;
                    object childValue = prop.GetValue(obj);
                    if (childValue == null)
                        continue;
                    if (proptype.IsGenericType && proptype.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        XmlElement child = doc.CreateElement(prop.Name);
                        int cnt = (int)proptype.GetProperty("Count").GetValue(childValue, null);
                        StringBuilder liststr = new StringBuilder();

                        for (int i = 0; i < cnt; ++i)
                        {
                            XmlElement element = doc.CreateElement(prop.Name + "Item");
                            child.AppendChild(element);
                            object[] args=new object[1];
                            args[0] = i;

                            PropertyInfo propinfo = proptype.GetProperty("Item");

                            object item = propinfo.GetValue(childValue, args);
                            string itemstr = Converter.GetString(item.GetType(), item);
                            if (itemstr.Length > 0)
                            {
                                liststr.Append(itemstr);
                                liststr.Append(ListSeparation);
                            }
                            else
                            {
                                SaveNode(element, prop.Name, doc, item);
                            }
                        }
                        if (liststr.Length > 0)
                        {
                            liststr.Remove(liststr.Length - 1, 1);
                            node.SetAttribute(prop.Name, liststr.ToString());
                        }
                        else
                        {
                            node.AppendChild(child);
                        }
                    }
                    else
                    {
                        SaveNode(node, prop.Name, doc, childValue);
                    }
                }
            }
        }
    }

    public static Boolean LoadIntMap(String fileName, out Dictionary<Int32, SecurityElement> map)
    {
        SecurityElement xml = LoadEles(fileName);
        map = LoadIntMap(xml, fileName);
        return true;
    }
    public static Dictionary<Int32, SecurityElement> LoadIntMap(SecurityElement xml, string source)
    {
        var result = new Dictionary<Int32, SecurityElement>();

        var index = 0;
        foreach (SecurityElement subMap in xml.Children)
        {
            index++;
            if (subMap.Attributes == null || subMap.Attributes.Count == 0)
            {
                Log.Warning("empty row in row NO." + index + " of " + source);
                continue;
            }

            if (!subMap.Attributes.ContainsKey("id"))
            {
                Log.Warning("Invalid Record ID" + index + " of " + source);
                continue;
            }

            Int32 key = Int32.Parse(subMap.Attributes["id"] as string);
            if (result.ContainsKey(key))
            {
                Log.Warning(String.Format("Key {0} already exist, in {1}.", key, source));
                continue;
            }

            result.Add(key, subMap);
        }
        return result;
    }
    public static void ParseFromXML(SecurityElement node, Type type, out object obj)
    {
        object pval = Converter.GetValue(type, node.Text);
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
                    Type listParamType = proptype.GetGenericArguments()[0];

                    if (node.Attributes != null)
                    {
                        string _tagname = GetTag(prop.Name);

                        if (node.Attributes.ContainsKey(_tagname))
                        {
                            string str = node.Attributes[_tagname] as string;

                            string[] items = str.Split(ListSeparation);
                            foreach (var item in items)
                            {
                                object listobj = Converter.GetValue(listParamType, item);
                                if (listobj != null)
                                    proptype.GetMethod("Add").Invoke(propValue, new object[] { listobj });
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
                                foreach (var cchild in child.Children)
                                {
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
                                object pattr = Converter.GetValue(proptype, (string)node.Attributes[tag]);
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
}

