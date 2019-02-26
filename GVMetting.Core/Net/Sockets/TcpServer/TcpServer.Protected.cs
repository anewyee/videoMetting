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
        /// �ر�һ���ͻ���Socket,������Ҫ�ر�Session 
        /// </summary> 
        /// <param name="client">Ŀ��Socket����</param> 
        /// <param name="exitType">�ͻ����˳�������</param> 
        protected virtual void CloseClient(Socket client, Session.ExitType exitType)
        {
            //���Ҹÿͻ����Ƿ����,���������,�׳��쳣 
            Session closeClient = FindSession(client);

            closeClient.TypeOfExit = exitType;

            if (closeClient != null)
                CloseSession(closeClient);
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void ReceiveData(Socket client)
        {
            _recvDataBuffer = new Byte[DefaultBufferSize];
            //������������ 
            client.BeginReceive(_recvDataBuffer
                , 0
                , DefaultBufferSize
                , SocketFlags.None
                , new AsyncCallback(ReceiveData)
                , client);
        }

        /// <summary> 
        /// �ͻ������Ӵ����� 
        /// </summary> 
        /// <param name="iar">���������������ӵ�Socket����</param> 
        protected virtual void AcceptConn(IAsyncResult iar)
        {
            //�������ܿͻ��� 
            _svrSock.BeginAccept(new AsyncCallback(AcceptConn), _svrSock);

            //���������ֹͣ�˷���,�Ͳ����ٽ����µĿͻ��� 
            if (!_isRun) return;

            //����һ���ͻ��˵��������� 
            Socket client = _svrSock.EndAccept(iar);

            Session newSession = new Session(client);
            _sessionTable.Add(newSession.ID, newSession);

            //��ʼ�������Ըÿͻ��˵����� 
            ReceiveData(client);

            //�µĿͻ�������,����֪ͨ 
            if (ClientConn != null)
                ClientConn(this, new NetEventArgs(newSession));
        }

        /// <summary> 
        /// ͨ��Socket�������Session���� 
        /// </summary> 
        /// <param name="client"></param> 
        /// <returns>�ҵ���Session����,���Ϊnull,˵���������ڸûػ�</returns> 
        private Session FindSession(Socket client)
        {
            SessionId id = new SessionId((int)client.Handle);

            return (Session)_sessionTable[id];
        }
        
        /// <summary> 
        /// ����������ɴ��������첽�����Ծ���������������У� 
        /// �յ����ݺ󣬻��Զ�����Ϊ�ַ������� 
        /// </summary> 
        /// <param name="iar">Ŀ��ͻ���Socket</param> 
        protected virtual void ReceiveData(IAsyncResult iar)
        {
            Socket client = (Socket)iar.AsyncState;
            try
            {
                int recv = client.EndReceive(iar);

                if (recv == 0)
                {
                    //�����Ĺر� 
                    CloseClient(client, Session.ExitType.NormalExit);
                }
                else
                {
                    Session session = FindSession(client);
                    session.Datagram = new Data(_recvDataBuffer);
                    //�����յ����ݵ��¼� 
                    if (RecvData != null)
                        RecvData(this, new NetEventArgs(session));

                    // ������������
                    ReceiveData(client);
                }
            }
            catch (SocketException ex)
            {
                //�ͻ����˳� 
                if (10054 == ex.ErrorCode)
                {
                    //�ͻ���ǿ�ƹر� 
                    CloseClient(client, Session.ExitType.ExceptionExit);
                }
            }
        }

        /// <summary> 
        /// ����������ɴ����� 
        /// </summary> 
        /// <param name="iar">Ŀ��ͻ���Socket</param> 
        protected virtual void SendDataEnd(IAsyncResult iar)
        {
            Socket client = (Socket)iar.AsyncState;

            int sent = client.EndSend(iar);
        }
    } 
}
