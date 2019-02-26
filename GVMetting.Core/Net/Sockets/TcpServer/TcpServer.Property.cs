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
        /// ��������Socket���� 
        /// </summary> 
        public Socket ServerSocket
        {
            get
            {
                return _svrSock;
            }
        }

        /// <summary> 
        /// �ͻ��˻Ự����,�������еĿͻ���,������Ը���������ݽ����޸� 
        /// </summary> 
        public Hashtable SessionTable
        {
            get
            {
                return _sessionTable;
            }
        }

        /// <summary> 
        /// �������������ɿͻ��˵�������� 
        /// </summary> 
        public int Capacity
        {
            get
            {
                return _maxClient;
            }
        }

        /// <summary> 
        /// ����������״̬ 
        /// </summary> 
        public bool IsRun
        {
            get
            {
                return _isRun;
            }

        }
    } 
}
