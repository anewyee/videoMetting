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
    /// �ͻ����������֮��ĻỰ�� 
    /// 
    /// ˵��: 
    /// �Ự�����Զ��ͨѶ�˵�״̬,��Щ״̬����Socket,��������, 
    /// �ͻ����˳�������(�����ر�,ǿ���˳���������) 
    /// </summary> 
    public class Session : ICloneable
    {
        #region �ֶ�

        /// <summary> 
        /// �ỰID 
        /// </summary> 
        private SessionId _id;

        /// <summary> 
        /// �ͻ��˵�Socket 
        /// </summary> 
        private Socket _cliSock;

        /// <summary> 
        /// �˳�����ö�� 
        /// </summary> 
        public enum ExitType
        {
            NormalExit,
            ExceptionExit
        };

        #endregion

        #region ����

        /// <summary>
        /// ������ļ����·��
        /// </summary>
        public string ServerPath { get; set; }
        /// <summary>
        /// �ͻ����ļ����·��
        /// </summary>
        public string ClientPath { get; set; }


        /// <summary> 
        /// ���ػỰ��ID 
        /// </summary> 
        public SessionId ID
        {
            get
            {
                return _id;
            }
        }

        /// <summary> 
        /// ��ȡ�Ự�ı��� 
        /// </summary> 
        public Data Datagram{get;set; }

        /// <summary> 
        /// �����ͻ��˻Ự������Socket���� 
        /// </summary> 
        public Socket ClientSocket
        {
            get
            {
                return _cliSock;
            }
        }

        /// <summary> 
        /// ��ȡ�ͻ��˵��˳���ʽ 
        /// </summary> 
        public ExitType TypeOfExit{get;set; }

        #endregion

        #region ����

        /// <summary> 
        /// ʹ��Socket�����Handleֵ��ΪHashCode,���������õ���������. 
        /// </summary> 
        /// <returns></returns> 
        public override int GetHashCode()
        {
            return (int)_cliSock.Handle;
        }

        /// <summary> 
        /// ��������Session�Ƿ����ͬһ���ͻ��� 
        /// </summary> 
        /// <param name="obj"></param> 
        /// <returns></returns> 
        public override bool Equals(object obj)
        {
            Session rightObj = (Session)obj;

            return (int)_cliSock.Handle == (int)rightObj.ClientSocket.Handle;

        }

        /// <summary> 
        /// ����ToString()����,����Session��������� 
        /// </summary> 
        /// <returns></returns> 
        public override string ToString()
        {
            string result = string.Format("Session:{0},IP:{1}",
                _id, _cliSock.RemoteEndPoint.ToString());

            return result;
        }

        /// <summary> 
        /// ���캯�� 
        /// </summary> 
        /// <param name="cliSock">�Ựʹ�õ�Socket����</param> 
        public Session(Socket cliSock)
        {
            Debug.Assert(cliSock != null);

            _cliSock = cliSock;

            _id = new SessionId((int)cliSock.Handle);

            Datagram = new Data();
            Datagram.Command = Command.Null;
        }

        /// <summary> 
        /// �رջỰ 
        /// </summary> 
        public void Close()
        {
            Debug.Assert(_cliSock != null);

            //�ر����ݵĽ��ܺͷ��� 
            _cliSock.Shutdown(SocketShutdown.Both);

            //������Դ 
            _cliSock.Close();
        }

        #endregion

        #region ICloneable ��Ա

        object System.ICloneable.Clone()
        {
            Session newSession = new Session(_cliSock);

            newSession.Datagram = Datagram;
            newSession.TypeOfExit = TypeOfExit;
            newSession.ServerPath = ServerPath;
            newSession.ClientPath = ClientPath;

            return newSession;
        }

        #endregion
    }
}
