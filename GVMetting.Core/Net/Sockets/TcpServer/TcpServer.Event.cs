///////////////////////////////////////////////////////
//NSTCPFramework
//�汾��1.0.0.1
//////////////////////////////////////////////////////
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.IO;
using System.Reflection;

namespace GVMetting.Core.Net.Sockets
{
    public partial class TcpServer
    {
        /// <summary> 
        /// �ͻ��˽��������¼� 
        /// </summary> 
        public event NetEvent ClientConn;

        /// <summary> 
        /// �ͻ��˹ر��¼� 
        /// </summary> 
        public event NetEvent ClientClose;

        /// <summary> 
        /// �������Ѿ����¼� 
        /// </summary> 
        public event NetEvent ServerFull;

        /// <summary> 
        /// ���������յ������¼� 
        /// </summary> 
        public event NetEvent RecvData;

    } 
}
