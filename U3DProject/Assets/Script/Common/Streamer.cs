using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Streamer : MemoryStream
{
    public Streamer(int cap)
        : base(cap)
    {

    }
    public Streamer(byte[] buffer)
        : base(buffer, 0, buffer.Length, true, true)
    {

    }

    public void Read(ref int data)
    {
        byte[] bf = this.GetBuffer();
        data = BitConverter.ToInt32(this.GetBuffer(), (int)this.Position);
        this.Seek(4, SeekOrigin.Current);
    }
    public void Read(ref short data)
    {
        data = BitConverter.ToInt16(this.GetBuffer(), (int)this.Position);
        this.Seek(2, SeekOrigin.Current);
    }
    public void Read(ref long data)
    {
        data = BitConverter.ToInt32(this.GetBuffer(), (int)this.Position);
        this.Seek(8, SeekOrigin.Current);
    }
    public void Read(ref float data)
    {
        data = BitConverter.ToSingle(this.GetBuffer(), (int)this.Position);
        this.Seek(4, SeekOrigin.Current);
    }
    public void Read(ref double data)
    {
        data = BitConverter.ToDouble(this.GetBuffer(), (int)this.Position);
        this.Seek(8, SeekOrigin.Current);
    }
    public void Read(ref char data)
    {
        data = BitConverter.ToChar(this.GetBuffer(), (int)this.Position);
        this.Seek(1, SeekOrigin.Current);
    }
    public void Read(ref bool data)
    {
        data = BitConverter.ToBoolean(this.GetBuffer(), (int)this.Position);
        this.Seek(1, SeekOrigin.Current);
    }

    public void Write(int data)
    {
        byte[] bt = BitConverter.GetBytes(data);
        this.Write(bt, 0, bt.Length);
    }
    public void Write(short data)
    {
        byte[] bt = BitConverter.GetBytes(data);
        this.Write(bt, 0, bt.Length);
    }
    public void Write(long data)
    {
        byte[] bt = BitConverter.GetBytes(data);
        this.Write(bt, 0, bt.Length);
    }
    public void Write(float data)
    {
        byte[] bt = BitConverter.GetBytes(data);
        this.Write(bt, 0, bt.Length);
    }
    public void Write(double data)
    {
        byte[] bt = BitConverter.GetBytes(data);
        this.Write(bt, 0, bt.Length);
    }
    public void Write(char data)
    {
        byte[] bt = BitConverter.GetBytes(data);
        this.Write(bt, 0, bt.Length);
    }
    public void Write(bool data)
    {
        byte[] bt = BitConverter.GetBytes(data);
        this.Write(bt, 0, bt.Length);
    }

    public void Write(CmdString data)
    {
        byte[] bt = data.ToArray();
        this.Write(bt, 0, bt.Length);
    }
    public bool Read(ref object data)
    {
        bool res = true;

        Type type = data.GetType();

        if (type == typeof(int))
        {
            int d = 0;
            Read(ref d);
            data = d;
        }
        else if (type == typeof(long))
        {
            long d = 0;
            Read(ref d);
            data = d;
        }
        else if (type == typeof(short))
        {
            short d = 0;
            Read(ref d);
            data = d;
        }
        else if (type == typeof(char))
        {
            char d = ' ';
            Read(ref d);
            data = d;
        }
        else if (type == typeof(double))
        {
            double d = 0;
            Read(ref d);
            data = d;
        }
        else if (type == typeof(float))
        {
            float d = 0;
            Read(ref d);
            data = d;
        }
        else if (type == typeof(bool))
        {
            bool d = false;
            Read(ref d);
            data = d;
        }
        else
        {
            res = false;
            Log.Error("Unknown Type");
        }
        return res;
    }
    public bool Write(object data)
    {
        bool res = true;

        Type type = data.GetType();
        if (type == typeof(int))
        {
            Write((int)data);
        }
        else if (type == typeof(long))
        {
            Write((long)data);
        }
        else if (type == typeof(short))
        {
            Write((short)data);
        }
        else if (type == typeof(char))
        {
            Write((char)data);
        }
        else if (type == typeof(double))
        {
            Write((double)data);
        }
        else if (type == typeof(float))
        {
            Write((float)data);
        }
        else if (type == typeof(bool))
        {
            Write((bool)data);
        }
        else
        {
            res = false;
        }
        return res;
    }

}
