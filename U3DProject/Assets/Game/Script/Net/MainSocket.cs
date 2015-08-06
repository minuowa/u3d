//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;
//using System.Security.Cryptography;
//using System;
//using System.IO;
//using System.Threading;
//using System.Net;
//using System.Net.Sockets;




//public class MainSocket
//{
//    private const uint COMPRESS = 0x40000000;



//    private Boolean UseComPress = true;



//    private uint _packLen = 0;

//    private uint _packHead = 0;


//    private int _counterSend;

//    private Boolean _isMainSocket;

//    public static Socket ClientSocket = null;

//    public UInt64 _dwUserID;
//    public uint _loginTempID;
//    public byte[] _domain = new byte[stNullUserCmd.MAX_NAMESIZE + 1];
//    public ushort _port;
//    public uint _mGameTime;

//    private Queue<byte[]> mDataQueue = new Queue<byte[]>();
//    private object mQueueLock = new object();
//    private List<byte[]> mCommandList = new List<byte[]>();


//    public MainSocket()
//    {
//        Closed();
//    }
//    void OnDestroy()
//    {
//        Closed();
//    }

//    void Update()
//    {
//        UpdateMessageQueue();
//    }
//    //连接平台服务器，短连接

//    public void connectLoginServer(string host, int port, string user, string psd)
//    {
//        Closed();
//        ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//        IPAddress ipa = IPAddress.Parse(host);
//        IPEndPoint iep = new IPEndPoint(ipa, port);
//        try
//        {
//            ClientSocket.Connect(iep);//连接到服务器   
//            loginPlant(user, psd);
//            read();
//            UpdateMessageQueue();
//        }
//        catch (Exception ex)
//        {
//            Closed();
//            Debug.Log(ex.Message);
//        }
//    }
//    public void connectServer(string host, int port, Boolean isMainSocket = false)
//    {
//        ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//        IPAddress ipa = IPAddress.Parse(host);
//        IPEndPoint iep = new IPEndPoint(ipa, port);
//        try
//        {
//            ClientSocket.Connect(iep);//连接到服务器 
//            loginGame();
//            Thread thread = new Thread(ClientReceive);
//            //thread.IsBackground = true;
//            thread.Start();
//        }
//        catch (Exception ex)
//        {
//            Debug.Log(ex.Message);
//        }

//        _isMainSocket = isMainSocket;

//    }

//    void ClientReceive()
//    {
//        while (true)
//        {
//            if (ClientSocket != null && ClientSocket.Connected)
//            {
//                if (ClientSocket.Available <= 0)
//                    Thread.Sleep(5);
//                else
//                    read();
//            }
//            else
//            {
//                Closed();
//                break;
//            }
//            Thread.Sleep(50);
//        }
//    }
//    void read()
//    {
//        if (ClientSocket.Available < 0)
//            return;
//        try
//        {
//            if (_packLen == 0)
//            {

//                //_packHead = _socket.readUnsignedInt();
//                byte[] headbytes = new byte[4];
//                int ret = ClientSocket.Receive(headbytes, headbytes.Length, 0);//将数据从连接的   Socket   接收到接收缓冲区的特定位置。 
//                if (ret == 0)
//                {
//                    //socket连接已断开,调用处理方法
//                    Debug.Log("read 0");
//                    Closed();
//                    return;
//                }
//                _packHead = BitConverter.ToUInt32(headbytes, 0);


//                _packLen = (_packHead & 0x0000FFFF);
//                //	Debug.Log("read _packHead="+_packLen);
//                //					Boolean hasCompress  = (Boolean)((_packHead & COMPRESS));
//                //var hasEncrypt : Boolean = ((_packHead & ENCRYPT) != 0);

//                //	if(hasCompress)
//                //		_packLen = (_packHead ^ COMPRESS);			
//                //if(hasEncrypt)
//                //	_packLen = (_packHead ^ ENCRYPT);

//            }
//            //ClientSocket.Available
//            //if(_packLen > _socket.bytesAvailable)
//            //	return;

//            byte[] bytesArray = new byte[_packLen];
//            //_socket.readBytes(bytesArray,0,_packLen);
//            ClientSocket.Receive(bytesArray, (int)_packLen, 0);
//            //	if((_packHead & COMPRESS) && UseComPress)
//            //		bytesArray.uncompress();	

//            //if(hasEncrypt){
//            //	RC4.encrypt(decryptKey, bytesArray);				
//            //}
//            //dispathBytes(bytesArray);
//            lock (mQueueLock)
//            {
//                //	Debug.Log("ClientSocket receive: " + bytesArray.Length);
//                mDataQueue.Enqueue(bytesArray);
//            }
//            //UpdateMessageQueue();
//            _packLen = 0;
//        }
//        catch (Exception ex)
//        {
//            Debug.Log(ex.Message);
//            Closed();
//        }


//    }

//    //关闭Socket
//    public void OnClosed()
//    {
//        Closed();
//    }

//    public void Closed()
//    {
//        if (ClientSocket != null)
//        {
//            try
//            {
//                ClientSocket.Shutdown(SocketShutdown.Both);
//                ClientSocket.Close();
//            }
//            catch (Exception ex)
//            {
//                Debug.LogError(ex.Message);
//            }

//            Debug.Log("Main socket Closed ");
//        }
//        ClientSocket = null;
//    }
//    public void loginPlant(string user, string psd)
//    {
//        //stUserVerifyVerCmd versioncmd = new stUserVerifyVerCmd();
//        //versioncmd.version = 1999;
//        //MemoryStream streamversion = new MemoryStream();
//        //versioncmd.serialize(streamversion);
//        //byte[] dataversion = streamversion.ToArray();
//        //send(dataversion);

//        //stGMRequestLoginCmd logincmd = new stGMRequestLoginCmd();
//        //string msg = "sid=" + "9" + "&account=" + user + "&pwd=" + psd + "&time=100000&type=0";
//        //logincmd.sid = new byte[msg.Length];
//        //logincmd.sid = System.Text.Encoding.Default.GetBytes(msg);

//        //logincmd.wdSize = (ushort)msg.Length;
//        //MemoryStream stream = new MemoryStream();
//        //logincmd.serialize(stream);
//        //byte[] data = stream.ToArray();


//        //stGMRequestLoginCmd logincmd1 = new stGMRequestLoginCmd();
//        //MemoryStream stream1 = new MemoryStream(data);
//        //logincmd1.unserialize(stream1);
//        //Debug.Log("send byCmd=" + logincmd1.byCmd + " byParam=" + logincmd1.byParam + " wdSize=" + logincmd1.wdSize + "msg.Length=" + msg.Length);
//        //send(data);

//    }

//    public void loginGame()
//    {
//        //stUserVerifyVerCmd versioncmd = new stUserVerifyVerCmd();
//        //versioncmd.version = 1999;
//        //MemoryStream streamversion = new MemoryStream();
//        //versioncmd.serialize(streamversion);
//        //byte[] dataversion = streamversion.ToArray();
//        //send(dataversion);

//        //stPasswdLogonUserCmd passwdcmd = new stPasswdLogonUserCmd();
//        //passwdcmd.dwUserID = _dwUserID;
//        //passwdcmd.loginTempID = _loginTempID;


//        //MemoryStream streampasswd = new MemoryStream();
//        //passwdcmd.serialize(streampasswd);
//        //send(streampasswd.ToArray());

//        //LogClass.LogSend(passwdcmd);
//    }

//    public void send(byte[] bytes)
//    {
//        if (!ClientSocket.Connected)//判断Socket是否已连接 
//            return;


//        byte[] head = new byte[4];
//        head = BitConverter.GetBytes(bytes.Length);

//        byte[] topData = new byte[bytes.Length + 4];
//        Array.Copy(head, 0, topData, 0, 4);
//        Array.Copy(bytes, 0, topData, 4, bytes.Length);
//        Debug.Log("send  length=" + bytes.Length + " topDatalength=" + topData.Length);
//        ClientSocket.Send(topData);

//    }



//    public void UpdateMessageQueue()
//    {
//        if (ClientSocket == null)
//            return;
//        mCommandList.Clear();
//        lock (mQueueLock)
//        {
//            while (mDataQueue.Count > 0)
//                mCommandList.Add(mDataQueue.Dequeue());
//        }
//        if (mCommandList.Count > 0)
//        {
//            for (int i = 0; i < mCommandList.Count; i++)
//            {
//                byte[] data = mCommandList[i];
//                dispathBytes(data);
//            }
//        }
//        mCommandList.Clear();
//    }

//    public void dispathBytes(byte[] byteArray)
//    {
//        stNullUserCmd cmd = new stNullUserCmd();
//        MemoryStream stream = new MemoryStream(byteArray);
//        cmd.unserialize(stream);
//        LogClass.LogMsg("dispathBytes byCmd=" + cmd.byCmd + " byParam=" + cmd.byParam, Color.blue);
//        Debug.Log("dispathBytes byCmd=" + cmd.byCmd + " byParam=" + cmd.byParam);
//        switch (cmd.byCmd)
//        {
//            case stNullUserCmd.LOGON_USERCMD:
//                {
//                    switch (cmd.byParam)
//                    {
//                        case stLogonUserCmd.SERVER_RETURN_LOGIN_OK:
//                            {
//                                stServerReturnLoginSuccessCmd rev = new stServerReturnLoginSuccessCmd();
//                                MemoryStream streamrev = new MemoryStream(byteArray);
//                                rev.unserialize(streamrev);
//                                string domain = System.Text.Encoding.Default.GetString(rev.domain);

//                                _dwUserID = rev.dwUserID;
//                                _loginTempID = rev.loginTempID;
//                                _port = rev.port;
//                                Array.Copy(rev.domain, 0, _domain, 0, _domain.Length);

//                                Debug.Log("Connect Login Server Successfully !");

//                                Closed();
//                                LogClass.LogReceive(rev);

//                                connectServer(domain, _port, true);
//                                Informations.Instance.statRole.id = _dwUserID;
//                            } break;
//                        case stLogonUserCmd.SERVER_RETURN_LOGIN_FAILED:
//                            {
//                                Debug.Log("Connect Game Server Successfully !");

//                                stServerReturnLoginFailedCmd rev = new stServerReturnLoginFailedCmd();
//                                MemoryStream streamrev = new MemoryStream(byteArray);
//                                rev.unserialize(streamrev);
//                                LogClass.LogReceive(rev);
//                                if (rev.sdReturnCode == 13)
//                                {
//                                    // 角色选择界面。
//                                    LoginScene.Instance.GoToCreateRole();
//                                }
//                                //Closed();
//                            } break;
//                        default: break;
//                    }
//                } break;
//            case stNullUserCmd.TIME_USERCMD://heard beat
//                {
//                    switch (cmd.byParam)
//                    {

//                        case stTimerUserCmd.GAMETIME_TIMER_USERCMD_PARA:
//                            {
//                                stGameTimeTimerUserCmd rev = new stGameTimeTimerUserCmd();
//                                MemoryStream streamrev = new MemoryStream(byteArray);
//                                rev.unserialize(streamrev);
//                                _mGameTime = rev.qwGameTime;
//                            }
//                            break;
//                        case stTimerUserCmd.REQUESTUSERGAMETIME_TIMER_USERCMD_PARA:
//                            {

//                            }
//                            break;
//                    }
//                }
//                break;
//            case stNullUserCmd.SELECT_USERCMD:
//                {

//                    switch (cmd.byParam)
//                    {
//                        case stSelectUserCmd.RT_NAME_BY_RAND_PARA:
//                            {
//                                rtNameByRandUserCmd recv = Net.ParseCmd<rtNameByRandUserCmd>(byteArray);
//                                string name = Fun.BytesToString(recv.name);
//                                Singleton<CreateRole>.Instance.SetName(name);
//                            }
//                            break;
//                        case stSelectUserCmd.USERINFO_SELECT_USERCMD_PARA:
//                            {
//                                t_stUserInfoUserCmdProto recv = Net.Parse<stUserInfoUserCmd, t_stUserInfoUserCmdProto>(byteArray);
//                                stLoginSelectUserCmd cmdTo=new stLoginSelectUserCmd();
//                                cmdTo.charNo = 0;
//                                Net.Send(cmdTo);

//                                SceneMgr.Instance.EnterScene(SceneType.Main);
//                            }
//                            break;
//                    }

//                }
//                break;
//            //case stNullUserCmd.CHAT_USERCMD:
//            //{

//            //}
//            //break;
//            case stNullUserCmd.SCRIPT_USERCMD:
//                {

//                }
//                break;
//            case stNullUserCmd.MAPSCREEN_USERCMD:
//                {
//                    switch (cmd.byParam)
//                    {

//                    }
//                }
//                break;
//            case stNullUserCmd.DATA_USERCMD:
//                {
//                    switch (cmd.byParam)
//                    {

//                    }

//                }
//                break;
//            case stNullUserCmd.MOBILEUNITY_USERCMD:
//                {
//                    switch (cmd.byParam)
//                    {
//                        case stMobileUnityUserCmd.SEND_UNITY_USERCMD:
//                            {
//                                stSendUnityUserCmd ucmd = new stSendUnityUserCmd();
//                                ucmd.unserialize(new MemoryStream(byteArray));
//                                Net.OnMsg(ucmd.data, ucmd.messageid);
//                            }
//                            break;
//                    }
//                }
//                break;
//        }


//    }

//}


