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
    /// <summary> 
    /// ����ͨѶ�¼�ģ��ί�� 
    /// </summary> 
    public delegate void NetEvent(object sender, NetEventArgs e); 

    /// <summary> 
    /// ������������¼�����,�����˼������¼��ĻỰ���� 
    /// </summary> 
    public class NetEventArgs : EventArgs
    {
        /// <summary> 
        /// �ͻ����������֮��ĻỰ 
        /// </summary> 
        private Session _client;

        /// <summary> 
        /// ���캯�� 
        /// </summary> 
        /// <param name="client">�ͻ��˻Ự</param> 
        public NetEventArgs(Session client)
        {
            _client = client;
        }

        /// <summary> 
        /// ��ü������¼��ĻỰ���� 
        /// </summary> 
        public Session Client
        {
            get
            {
                return _client;
            }
        }
    } 
}
