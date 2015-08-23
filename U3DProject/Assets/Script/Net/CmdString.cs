using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class CmdString : Streamer
{
    public CmdString(int cap)
        : base(cap)
    {
    }

    public string content
    {
        get
        {
            return Encoding.UTF8.GetString(ToArray(), 0, (int)Position);
        }
        set
        {
            int len = value.Length;
            if (len > value.Length)
                Log.Error("Buffer is to small!");

            byte[] bf = Encoding.UTF8.GetBytes(value);
            Seek(0, SeekOrigin.Begin);
            Write(bf);
        }
    }
}
