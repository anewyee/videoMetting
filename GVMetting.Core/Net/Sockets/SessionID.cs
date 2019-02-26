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
    /// Ψһ�ı�־һ��Session,����Session������Hash��������ض����� 
    /// </summary> 
    public class SessionId
    {
        /// <summary> 
        /// ��Session�����Socket�����Handleֵ��ͬ,���������ֵ����ʼ���� 
        /// </summary> 
        private int _id;

        /// <summary> 
        /// ����IDֵ 
        /// </summary> 
        public int ID
        {
            get
            {
                return _id;
            }
        }

        /// <summary> 
        /// ���캯�� 
        /// </summary> 
        /// <param name="id">Socket��Handleֵ</param> 
        public SessionId(int id)
        {
            _id = id;
        }

        /// <summary> 
        /// ����.Ϊ�˷���Hashtable��ֵ���� 
        /// </summary> 
        /// <param name="obj"></param> 
        /// <returns></returns> 
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                SessionId right = (SessionId)obj;

                return _id == right._id;
            }
            else if (this == null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary> 
        /// ����.Ϊ�˷���Hashtable��ֵ���� 
        /// </summary> 
        /// <returns></returns> 
        public override int GetHashCode()
        {
            return _id;
        }

        /// <summary> 
        /// ����,Ϊ�˷�����ʾ��� 
        /// </summary> 
        /// <returns></returns> 
        public override string ToString()
        {
            return _id.ToString();
        }

    }

}
