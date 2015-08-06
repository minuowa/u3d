//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Reflection;
//using System.Text;
//using UnityEngine;
//public enum CLIENT_MESSAGE
//{
//    Login,
//}
//public enum SERVER_MESSAGE
//{
//    Login,
//}
//public class stNullUserCmd
//{
//    public static int MAX_NAMESIZE = 64;
//}
//public class Net : MonoBehaviour
//{
//    public MainSocket socket;
//    public string loginServer = "169.254.1.200";
//    public int loginPort = 15299;

//    void Start()
//    {
//        UnityEngine.Object.DontDestroyOnLoad(gameObject);
//        if (Application.isPlaying)
//            socket = new MainSocket();
//    }

//    public void ConnectLoginServer(string user, string psd)
//    {
//        if (socket != null)
//            socket.connectLoginServer(loginServer, loginPort, user, psd);
//    }

//    void Update()
//    {
//        if (socket != null)
//            socket.UpdateMessageQueue();
//    }

//    public static void Send(SERVER_MESSAGE msgid)
//    {
//        if (!isConnected)
//            return;
//    }
//    public static bool isConnected
//    {
//        get
//        {
//            if (MY<Net>.Instance == null)
//                return false;
//            if (MY<Net>.Instance.socket == null)
//                return false;
//            return true;
//        }
//    }
//    public static void Send(CLIENT_MESSAGE msgid)
//    {
//        Send((SERVER_MESSAGE)msgid);
//    }

//    //public static void Send(stNullUserCmd cmd)
//    //{
//    //    if (!isConnected)
//    //        return;
//    //    MemoryStream stream = new MemoryStream();
//    //    cmd.serialize(stream);
//    //    MY<Net>.Instance.socket.send(stream.ToArray());
//    //}

//    //public static void Send<T>(SERVER_MESSAGE msgid, T protodata) where T : ICommand
//    //{
//    //    if (!isConnected)
//    //        return;
//    //    MemoryStream stream = new MemoryStream();
//    //    ProtoBuf.Serializer.Serialize<T>(stream, protodata);

//    //    stClientUnityUserCmd sendcmd = new stClientUnityUserCmd();
//    //    sendcmd.messageid = (uint)msgid;
//    //    sendcmd.size = (uint)stream.ToArray().Length;
//    //    sendcmd.data = new byte[sendcmd.size];
//    //    Array.Copy(stream.GetBuffer(), sendcmd.data, sendcmd.size);

//    //    MemoryStream streamcmd = new MemoryStream();
//    //    sendcmd.serialize(streamcmd);

//    //    MY<Net>.Instance.socket.send(streamcmd.ToArray());

//    //    LogClass.LogSend(protodata);
//    //}

//    //public static void Send<CMD, T>(T protodata) where CMD : stNullUserCmd
//    //{
//    //    if (!isConnected)
//    //        return;
//    //    MemoryStream stream = new MemoryStream();
//    //    ProtoBuf.Serializer.Serialize<T>(stream, protodata);

//    //    Type tp = typeof(CMD);
//    //    CMD sendcmd = (CMD)tp.GetConstructor(Type.EmptyTypes).Invoke(null);
//    //    FieldInfo[] fields = tp.GetFields();
//    //    FieldInfo fiSize = Array.Find(fields, (FieldInfo ifo) => { return ifo.Name == "size"; });
//    //    FieldInfo fiData = Array.Find(fields, (FieldInfo ifo) => { return ifo.Name == "data"; });

//    //    if (fiSize != null && fiData != null)
//    //    {
//    //        int len = stream.ToArray().Length;
//    //        fiSize.SetValue(sendcmd, (uint)stream.ToArray().Length);
//    //        byte[] arr = new byte[len];
//    //        Array.Copy(stream.GetBuffer(), arr, len);
//    //        fiData.SetValue(sendcmd, arr);
//    //    }
//    //    MemoryStream streamcmd = new MemoryStream();
//    //    sendcmd.serialize(streamcmd);

//    //    MY<Net>.Instance.socket.send(streamcmd.ToArray());

//    //    LogClass.LogSend(protodata);
//    //}
//    //public static T Parse<CMD, T>(byte[] msg) where CMD : stNullUserCmd
//    //{
//    //    MemoryStream stream = new MemoryStream(msg);
//    //    Type tp = typeof(CMD);
//    //    CMD recv = (CMD)tp.GetConstructor(Type.EmptyTypes).Invoke(null);
//    //    recv.unserialize(stream);

//    //    FieldInfo[] fields = tp.GetFields();
//    //    FieldInfo fiSize = Array.Find(fields, (FieldInfo ifo) => { return ifo.Name == "size"; });
//    //    FieldInfo fiData = Array.Find(fields, (FieldInfo ifo) => { return ifo.Name == "data"; });

//    //    if (fiSize != null && fiData != null)
//    //    {
//    //        byte[] arr = (byte[])fiData.GetValue(recv);
//    //        MemoryStream proArray = new MemoryStream(arr);

//    //        T protoBuffer = ProtoBuf.Serializer.Deserialize<T>(proArray);
//    //        return protoBuffer;
//    //    }
//    //    return default(T);
//    //}
//    //public static T Parse<T>(byte[] msg) where T : ICommand
//    //{
//    //    MemoryStream stream = new MemoryStream(msg);
//    //    T protoBuffer = ProtoBuf.Serializer.Deserialize<T>(stream);
//    //    LogClass.LogReceive(protoBuffer);
//    //    return protoBuffer;
//    //}

//    //public static T ParseCmd<T>(byte[] msg) where T : stNullUserCmd
//    //{
//    //    Type tp = typeof(T);
//    //    T cmd = (T)tp.GetConstructor(Type.EmptyTypes).Invoke(null);
//    //    cmd.unserialize(new MemoryStream(msg));
//    //    return cmd;
//    //}

//    //public static void OnMsg(byte[] msg, uint msgId)
//    //{
//    //    switch ((SERVER_MESSAGE)msgId)
//    //    {
//    //        case SERVER_MESSAGE.SERVER_MESSAGE_USERDATA:
//    //            {
//    //                MainAttrib cmd = Parse<MainAttrib>(msg);
//    //                StatRole role = Informations.Instance.statRole;
//    //                role.id = cmd.id;
//    //                role.name = cmd.name;
//    //                role.exp = (uint)cmd.exp;
//    //                role.totleExp = (uint)cmd.totleExp;
//    //                role.title = cmd.title;
//    //                role.iconEdage = cmd.iconEdage;
//    //                role.lv.value = cmd.lv;
//    //                role.heroLvLimit.value = cmd.heroLvLimit;
//    //                role.energyLimit.value = (int)cmd.energyLimit;

//    //                Role roleWin = UIManager.Get<Role>();
//    //                if (roleWin != null)
//    //                    roleWin.UpdateWindow();

//    //                MainUI mainui = UIManager.Get<MainUI>();
//    //                if (mainui)
//    //                    mainui.UpdateWindow();

//    //            }
//    //            break;
//    //        case SERVER_MESSAGE.SERVER_MESSAGE_OBJS:
//    //            {
//    //                var cmd = Parse<t_stAddObjectListUnityUserCmdProto>(msg);

//    //                Informations.Instance.ItemMgr.OnNet(cmd);
//    //            }
//    //            break;
//    //        case SERVER_MESSAGE.SERVER_MESSAGE_CHAT:
//    //            {
//    //                var cmd = Parse<t_stMobileChannelChatUserCmdProto>(msg);
//    //            }
//    //            break;
//    //        case SERVER_MESSAGE.SERVER_MESSAGE_DAYSIGN:
//    //            {
//    //                var cmd = Parse<t_DaySignCmdProto>(msg);
//    //                StatSign statSign = Informations.Instance.statSign;
//    //                if (cmd.signs != null)
//    //                {
//    //                    foreach (var s in cmd.signs)
//    //                    {
//    //                        statSign.signStats[(int)s.id - 1].state = (CommonState)s.state;
//    //                    }
//    //                    UIManager.Get<Sign>().UpdateWindow();
//    //                }
//    //            }
//    //            break;
//    //    }
//    //}

//}
