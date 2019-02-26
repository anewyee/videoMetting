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
        /// Ĭ�Ϲ��캯��,ʹ��Ĭ�ϵı����ʽ 
        /// </summary> 
        public TcpClient() { }

        /// <summary> 
        /// ���ӷ����� 
        /// </summary> 
        /// <param name="ip">������IP��ַ</param> 
        /// <param name="port">�������˿�</param> 
        public virtual void Connect(string ip, int port)
        {
            if (IsConnected)
            {
                //�������� 
                Debug.Assert(_session != null);

                Close();
            }

            Socket newsock = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ip), port);
            newsock.BeginConnect(iep, new AsyncCallback(Connected), newsock);

        }

        /// <summary> 
        /// �������ݱ��� 
        /// </summary> 
        /// <param name="datagram"></param> 
        public virtual void SendText(Data datagram)
        {
            if (!_isConnected)
            {
                throw (new ApplicationException("û�����ӷ����������ܷ�������"));
            }

            //��ñ��ĵı����ֽ� 
            byte[] data = datagram.ToByte();

            _session.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None,
                new AsyncCallback(SendDataEnd), _session.ClientSocket);
        }

        /// <summary> 
        /// �ر����� 
        /// </summary> 
        public virtual void Close()
        {
            if (!_isConnected)
            {
                return;
            }

            _session.Close();

            _session = null;

            _isConnected = false;
        }
    }
}