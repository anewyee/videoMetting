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
        /// ��������������,��ʼ�����ͻ������� 
        /// </summary> 
        public virtual void Start()
        {
            if (!_isRun)
            {

                _sessionTable = new Hashtable(53);

                _recvDataBuffer = new byte[DefaultBufferSize];

                //��ʼ��socket 
                _svrSock = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                //�󶨶˿� 
                IPEndPoint iep = new IPEndPoint(_serverIP, _port);
                _svrSock.Bind(iep);

                //��ʼ���� 
                _svrSock.Listen(5);

                //�����첽�������ܿͻ������� 
                _svrSock.BeginAccept(new AsyncCallback(AcceptConn), _svrSock);

                _isRun = true;
            }
        }

        /// <summary> 
        /// ֹͣ����������,������ͻ��˵����ӽ��ر� 
        /// </summary> 
        public virtual void Stop()
        {
            if (_isRun)
            {

                //���������䣬һ��Ҫ�ڹر����пͻ�����ǰ���� 
                //������EndConn����ִ��� 
                _isRun = false;

                //�ر���������,����ͻ��˻���Ϊ��ǿ�ƹر����� 
                if (_svrSock.Connected)
                {
                    _svrSock.Shutdown(SocketShutdown.Both);
                }

                CloseAllClient();

                //������Դ 
                _svrSock.Close();

                _sessionTable = null;
            }
        }


        /// <summary> 
        /// �ر����еĿͻ��˻Ự,�����еĿͻ������ӻ�Ͽ� 
        /// </summary> 
        public virtual void CloseAllClient()
        {
            foreach (Session client in _sessionTable.Values)
            {
                client.Close();
            }

            _sessionTable.Clear();
        }


        /// <summary> 
        /// �ر�һ����ͻ���֮��ĻỰ 
        /// </summary> 
        /// <param name="closeClient">��Ҫ�رյĿͻ��˻Ự����</param> 
        public virtual void CloseSession(Session closeClient)
        {
            Debug.Assert(closeClient != null);

            if (closeClient != null)
            {
                closeClient.Datagram = null;

                _sessionTable.Remove(closeClient.ID);

                //�ͻ���ǿ�ƹر����� 
                if (ClientClose != null)
                    ClientClose(this, new NetEventArgs(closeClient));

                closeClient.Close();
            }
        }


        /// <summary> 
        /// �������� 
        /// </summary> 
        /// <param name="recvDataClient">�������ݵĿͻ��˻Ự</param> 
        /// <param name="datagram">���ݱ���</param> 
        public virtual void SendText(Session recvDataClient, Data datagram)
        {
            //������ݱ��� 
            byte[] data = datagram.ToByte();

            recvDataClient.ClientSocket.BeginSend(data
                , 0
                , data.Length
                , SocketFlags.None
                , new AsyncCallback(SendDataEnd)
                , recvDataClient.ClientSocket);
        }
    } 
}
