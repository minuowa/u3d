/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Tencent is pleased to support the open source community by making behaviac available.
//
// Copyright (C) 2015 THL A29 Limited, a Tencent company. All rights reserved.
//
// Licensed under the BSD 3-Clause License (the "License"); you may not use this file except in compliance with
// the License. You may obtain a copy of the License at http://opensource.org/licenses/BSD-3-Clause
//
// Unless required by applicable law or agreed to in writing, software distributed under the License is
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

#if !BEHAVIAC_RELEASE
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Reflection;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace behaviac
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct InitialSettingsPacket
    {
        public void Init()
        {
            messageSize = 0;
            command = (byte)CommandId.CMDID_INITIAL_SETTINGS;
            platform = (byte)Platform.WINDOWS;

            System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess();
            this.processId = process.Id;
        }

        public int PrepareToSend()
        {
            messageSize = (byte)(Marshal.SizeOf(typeof(InitialSettingsPacket)) - 1);
            return messageSize + 1;
        }

        public byte[] GetData()
        {
            int len = this.PrepareToSend();
            byte[] data = new byte[len];

            data[0] = messageSize;
            data[1] = command;
            data[2] = platform;
            byte[] iarray = BitConverter.GetBytes(this.processId);

            Array.Copy(iarray, 0, data, 3, sizeof(int));

            return data;
        }

        public byte messageSize;
        public byte command;
        public byte platform;
        public int processId;
    };

    class ConnectorImpl : ConnectorInterface
    {
        public ConnectorImpl()
        {
            m_workspaceSent = false;
            //don't handle message automatically
            m_bHandleMessage = false;
        }
        ~ConnectorImpl()
        {

        }

        private void SendInitialSettings()
        {
            InitialSettingsPacket initialPacket = new InitialSettingsPacket();
            initialPacket.Init();
            int bytesWritten = 0;

            if (!SocketBase.Write(m_writeSocket, initialPacket.GetData(), ref bytesWritten))
            {
                Log("behaviac: Couldn't send initial settings.\n");
            }

            gs_packetsStats.init++;
        }

        protected override void OnConnection()
        {
            Log("behaviac: sending initial settings.\n");

            this.SendInitialSettings();

            SocketUtils.SendWorkspaceSettings();

            this.SendInitialProperties();

            {
                Log("behaviac: sending packets before connecting.\n");

                this.SendExistingPackets();
            }

            SocketUtils.SendText("[connected]precached message done");

            //when '[connected]' is handled in the designer, it will send back all the breakpoints if any and '[breakcpp]' and '[start]'
            //here we block until all those messages have been received, otherwise, if we don't block here to wait for all those messages
            //the breakpoints checking might be wrong.
            bool bLoop = true;
            while (bLoop && m_isDisconnected.Get() == 0)
            {
                //sending packets if any
                if (m_packetsCount > 0)
                {
                    SendAllPackets();
                }

                string kStartMsg = "[start]";
                bool bFound = this.ReceivePackets(kStartMsg);

                if (bFound)
                {
                    bLoop = false;
                }
                else
                {
                    System.Threading.Thread.Sleep(1);
                }
            }

            //this.m_bHandleMessage = false;
        }

        private void SendInitialProperties()
        {
            Context.LogCurrentStates();
        }

        public bool IsWorkspaceSent()
        {
            return m_workspaceSent;
        }

        public void SetWorkspaceSent(bool bSent)
        {
            m_workspaceSent = bSent;
        }
        private bool m_workspaceSent;

        protected override void Clear()
        {
            base.Clear();

            m_workspaceSent = false;
        }
    };
}
#endif

namespace behaviac
{
    public static class SocketUtils
    {
#if !BEHAVIAC_RELEASE
		static ConnectorImpl s_tracer = new ConnectorImpl();
#endif
        public static bool SetupConnection(bool bBlocking, ushort port)
        {
#if !BEHAVIAC_RELEASE
            if (Config.IsSocketing)
            {
                if (!s_tracer.IsInited())
                {
                    const int kMaxThreads = 16;

                    if (!s_tracer.Init(kMaxThreads, port, bBlocking))
                    {
                        return false;
                    }
                }

                behaviac.Debug.Log("behaviac: SetupConnection successful\n");

                return true;
            }
#endif
            return false;
        }

        public static bool SetupConnection(bool bBlocking)
        {
#if !BEHAVIAC_RELEASE
            if (Config.IsSocketing)
            {
                ushort port = 60636;

                bool bResult = SocketUtils.SetupConnection(bBlocking, port);
                return bResult;
            }
#endif
            return false;
        }

        public static void ShutdownConnection()
        {
#if !BEHAVIAC_RELEASE
            if (Config.IsSocketing)
            {
                s_tracer.Close();

                behaviac.Debug.Log("behaviac: ShutdownConnection\n");
            }
#endif
        }

        public static void SendText(string text)
        {
#if !BEHAVIAC_RELEASE
            if (Config.IsSocketing)
            {
                s_tracer.SendText(text, (byte)CommandId.CMDID_TEXT);
            }
#endif
        }

        public static void SendWorkspace(string text)
        {
#if !BEHAVIAC_RELEASE
            if (Config.IsSocketing)
            {
                s_tracer.SendText(text, (byte)CommandId.CMDID_WORKSPACE);
            }
#endif
        }

        public static bool ReadText(ref string text)
        {
#if !BEHAVIAC_RELEASE
            if (Config.IsSocketing)
            {
                return s_tracer.ReadText(ref text);
            }
#endif
            return false;
        }

        public static void Flush()
        {
#if !BEHAVIAC_RELEASE
            if (Config.IsSocketing)
            {
                while (s_tracer.GetPacketsCount() > 0)
                {
                    System.Threading.Thread.Sleep(1);
                }
            }
#endif
        }

        public static void SendWorkspaceSettings()
        {
#if !BEHAVIAC_RELEASE
            if (Config.IsSocketing)
            {
                if (!s_tracer.IsWorkspaceSent() && s_tracer.IsConnected())
                {
                    string wksAbsPath = Workspace.GetWorkspaceAbsolutePath();

                    if (!string.IsNullOrEmpty(wksAbsPath))
                    {
                        Workspace.EFileFormat format = Workspace.FileFormat;
                        string formatString = (format == Workspace.EFileFormat.EFF_xml ? "xml" : "bson");

                        string msg = string.Format("[workspace] {0} \"{1}\"\n", formatString, wksAbsPath);
						behaviac.Debug.Log(msg);

                        //behaviac.Socket.SendText(msg);
                        LogManager.LogWorkspace(msg);

                        s_tracer.SetWorkspaceSent(true);
                    }
                }
            }
#endif
        }

        public static int GetMemoryOverhead()
        {
#if !BEHAVIAC_RELEASE
            if (Config.IsSocketing)
            {
                return s_tracer.GetMemoryOverhead();
            }
#endif
            return 0;
        }

        public static int GetNumTrackedThreads()
        {
#if !BEHAVIAC_RELEASE
            if (Config.IsSocketing)
            {
                return s_tracer.GetNumTrackedThreads();
            }
#endif
            return 0;
        }

        public static void UpdatePacketsStats()
        {
#if !BEHAVIAC_RELEASE
            if (Config.IsSocketing)
            {
                //uint overhead = (behaviac.GetMemoryOverhead());
                //BEHAVIAC_SETTRACEDVAR("Stats.Vars", gs_packetsStats.vars);
            }
#endif
        }
    }

} // behaviac
