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
    /// �ṩTCP���ӷ���ķ������� 
    /// 
    /// �ص�: 
    /// 1.ʹ��hash�������������ӿͻ��˵�״̬���յ�����ʱ��ʵ�ֿ��ٲ���.ÿ�� 
    /// ��һ���µĿͻ������Ӿͻ����һ���µĻỰ(Session).��Session�����˿� 
    /// ���˶���. 
    /// 2.ʹ���첽��Socket�¼���Ϊ�������������ͨѶ����. 
    /// 3.֧�ִ���ǵ����ݱ��ĸ�ʽ��ʶ��,����ɴ����ݱ��ĵĴ������Ӧ���ӵ��� 
    /// �绷��.�����涨����֧�ֵ�������ݱ���Ϊ640K(��һ�����ݰ��Ĵ�С���ܴ��� 
    /// 640K,���������������Զ�ɾ����������,��Ϊ�ǷǷ�����),��ֹ��Ϊ���ݱ��� 
    /// �����Ƶ����������·��������� 
    /// 4.ͨѶ��ʽĬ��ʹ��Encoding.Default��ʽ�����Ϳ��Ժ���ǰ32λ����Ŀͻ��� 
    /// ͨѶ.Ҳ����ʹ��U-16��U-8�ĵ�ͨѶ��ʽ����.�����ڸ�DatagramResolver��� 
    /// �̳��������ر���ͽ��뺯��,�Զ�����ܸ�ʽ����ͨѶ.��֮ȷ���ͻ�������� 
    /// ����ʹ����ͬ��ͨѶ��ʽ 
    /// 5.ʹ��C# native code,��������Ч�ʵĿ��ǿ��Խ�C++����д�ɵ�32λdll������ 
    /// C#���Ĵ���, ��������ȱ������ֲ��,������Unsafe����(�����C++����Ҳ����) 
    /// 6.�������Ʒ�����������½�ͻ�����Ŀ 
    /// 7.��ʹ��TcpListener�ṩ���Ӿ�ϸ�Ŀ��ƺ͸���ǿ���첽���ݴ���Ĺ���,����Ϊ 
    /// TcpListener������� 
    /// 8.ʹ���첽ͨѶģʽ,��ȫ���õ���ͨѶ�������߳�����,���뿼��ͨѶ��ϸ�� 
    /// 
    /// </summary> 
    public partial class TcpServer
    {
        /// <summary> 
        /// Ĭ�ϵķ�����������ӿͻ��˶����� 
        /// </summary> 
        public const int DefaultMaxClient = 100;

        /// <summary> 
        /// �������ݻ�������С64K 
        /// </summary> 
        public const int DefaultBufferSize = 1024 * 1024;

        /// <summary>
        /// ���������������IP��ַ
        /// </summary>
        private IPAddress _serverIP;
        /// <summary> 
        /// ����������ʹ�õĶ˿� 
        /// </summary> 
        private ushort _port;

        /// <summary> 
        /// ������������������ͻ��������� 
        /// </summary> 
        private ushort _maxClient;

        /// <summary> 
        /// ������������״̬ 
        /// </summary> 
        private bool _isRun;

        /// <summary> 
        /// �������ݻ����� 
        /// </summary> 
        private byte[] _recvDataBuffer;

        /// <summary> 
        /// ������ʹ�õ��첽Socket��, 
        /// </summary> 
        private Socket _svrSock;

        /// <summary> 
        /// �������пͻ��˻Ự�Ĺ�ϣ�� 
        /// </summary> 
        private Hashtable _sessionTable;

        /// <summary> 
        /// ���캯�� 
        /// </summary> 
        /// <param name="port">�������˼����Ķ˿ں�</param> 
        /// <param name="maxClient">�����������ɿͻ��˵��������</param> 
        /// <param name="serverIP"></param>
        public TcpServer(IPAddress serverIP,ushort port, ushort maxClient)
        {
            _serverIP = serverIP;
            _port = port;
            _maxClient = maxClient;
        }
        public TcpServer(IPAddress serverIP, ushort port)
            : this(serverIP, port, 5)
        {
        }
    } 
}
