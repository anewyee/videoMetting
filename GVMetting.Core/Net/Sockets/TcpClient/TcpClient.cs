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
        /// �ͻ����������֮��ĻỰ�� 
        /// </summary> 
        private Session _session;

        /// <summary> 
        /// �ͻ����Ƿ��Ѿ����ӷ����� 
        /// </summary> 
        private bool _isConnected = false;

        /// <summary> 
        /// �������ݻ�������С 
        /// </summary> 
        public const int DefaultBufferSize = 1024 * 1024;

        /// <summary> 
        /// �������ݻ����� 
        /// </summary> 
        private byte[] _recvDataBuffer;

    } 
}
