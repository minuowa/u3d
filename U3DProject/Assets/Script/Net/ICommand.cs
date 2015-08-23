using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.IO;

/// <summary>
/// the base command of all proto commands.
/// </summary>
public interface ICommand
{
}
public class ICmd
{
    public int id;
    public int param;
    public static int HeadLength = 8;
    public void Send()
    {
        MS<Net>.Instance.Send(SerilizeWithHeader());
    }
    public void Receive(byte[] msg)
    {
        Streamer streamer = new Streamer(msg);
        DeserilizeWithHeader(streamer);
    }
    public byte[] SerilizeWithHeader()
    {
        Streamer streamer = new Streamer(1024 * 8);
        streamer.Seek(HeadLength, SeekOrigin.Begin);
        Serilize(streamer);
        WriteLength(streamer);
        return streamer.ToArray();
    }
    void DeserilizeWithHeader(Streamer msg)
    {
        long len = 0;
        msg.Read(ref len);

        if (len != msg.Length)
            Log.Error("Error Msg!");

        Deserilize(msg);
    }

    void WriteLength(Streamer streamer)
    {
        long len = streamer.Position;
        streamer.Seek(0, SeekOrigin.Begin);
        streamer.Write(len);
        streamer.Position = len;
    }

    void Serilize(Streamer buffer)
    {
        SerilizeObj(this, buffer);
    }
    void Deserilize(Streamer buffer)
    {
        DeserilizeObj(this, buffer);
    }

    public static void SerilizeObj(object obj, Streamer buffer)
    {
        Type thisType = obj.GetType();
        FieldInfo[] fields = thisType.GetFields();

        foreach (var field in fields)
        {
            if (!field.IsStatic)
            {
                object val = field.GetValue(obj);
                if (!buffer.Write(val))
                    SerilizeObj(val, buffer);
            }
        }
    }
    public static void DeserilizeObj(object obj, Streamer buffer)
    {
        Type thisType = obj.GetType();
        FieldInfo[] fields = thisType.GetFields();

        foreach (var field in fields)
        {
            if (!field.IsStatic)
            {
                object val = field.GetValue(obj);
                if (buffer.Read(ref val))
                {
                    field.SetValue(obj, val);
                }
                else
                {
                    DeserilizeObj(val, buffer);
                }
            }
        }
    }
}