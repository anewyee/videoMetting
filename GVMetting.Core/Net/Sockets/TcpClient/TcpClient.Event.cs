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
    /// �ṩTcp�������ӷ���Ŀͻ����� 
    /// 
    /// ԭ��: 
    /// 1.ʹ���첽SocketͨѶ�����������һ����ͨѶ��ʽͨѶ,��ע�����������ͨ 
    /// Ѷ��ʽһ��Ҫһ��,���������ɷ������������,��������û�п˷�,��ô��byte[] 
    /// �ж����ı����ʽ 
    /// 2.֧�ִ���ǵ����ݱ��ĸ�ʽ��ʶ��,����ɴ����ݱ��ĵĴ������Ӧ���ӵ��� 
    /// �绷��. 
    /// </summary> 
    public partial class TcpClient
    {
        /// <summary> 
        /// �Ѿ����ӷ������¼� 
        /// </summary> 
        public event NetEvent ConnectedServer;

        /// <summary> 
        /// ���յ����ݱ����¼� 
        /// </summary> 
        public event NetEvent ReceivedDatagram;

        /// <summary> 
        /// ���ӶϿ��¼� 
        /// </summary> 
        public event NetEvent DisConnectedServer;
    } 
}
