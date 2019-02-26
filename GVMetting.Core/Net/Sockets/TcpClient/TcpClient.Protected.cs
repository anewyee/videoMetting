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
        /// ���ݷ�����ɴ����� 
        /// </summary> 
        /// <param name="iar"></param> 
        protected virtual void SendDataEnd(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int sent = remote.EndSend(iar);
        }

        /// <summary> 
        /// ����Tcp���Ӻ������ 
        /// </summary> 
        /// <param name="iar">�첽Socket</param> 
        protected virtual void Connected(IAsyncResult iar)
        {
            Socket socket = (Socket)iar.AsyncState;

            socket.EndConnect(iar);

            //�����µĻỰ 
            _session = new Session(socket);

            _isConnected = true;

            //�������ӽ����¼� 
            if (ConnectedServer != null)
                ConnectedServer(this, new NetEventArgs(_session));

            ReceiveData();
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void ReceiveData()
        {
            _recvDataBuffer = new Byte[DefaultBufferSize];
            //������������ 
            _session.ClientSocket.BeginReceive(_recvDataBuffer
                , 0
                , DefaultBufferSize
                , SocketFlags.None
                , new AsyncCallback(RecvData)
                , _session.ClientSocket);
        }

        /// <summary> 
        /// ���ݽ��մ����� 
        /// </summary> 
        /// <param name="iar">�첽Socket</param> 
        protected virtual void RecvData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            try
            {
                int recv = remote.EndReceive(iar);

                _session.Datagram = new Data(_recvDataBuffer);
                //�������˳� 
                if (recv == 0)
                {
                    _session.TypeOfExit = Session.ExitType.NormalExit;

                    if (DisConnectedServer != null)
                        DisConnectedServer(this, new NetEventArgs(_session));
                }
                else
                {
                    //ͨ���¼������յ��ı��� 
                    if (ReceivedDatagram != null)
                        ReceivedDatagram(this, new NetEventArgs(_session));

                    ReceiveData();
                }
            }
            catch (SocketException ex)
            {
                //�ͻ����˳� 
                if (10054 == ex.ErrorCode)
                {
                    //������ǿ�ƵĹر����ӣ�ǿ���˳� 
                    _session.TypeOfExit = Session.ExitType.ExceptionExit;

                    if (DisConnectedServer != null)
                        DisConnectedServer(this, new NetEventArgs(_session));
                }
            }
            catch (Exception eex)
            {
                Console.WriteLine(eex.ToString());
            }
        }
    }
}