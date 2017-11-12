using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeclaimerCommon
{
    public static class DeclaimerCommon
    {
        private static List<string> tagTypeList = new List<string>();
        /// <summary>
        /// 读取当前标签类型
        /// </summary>
        public static List<string> TagTypeList
        {
            get { return DeclaimerCommon.tagTypeList; }
            set { DeclaimerCommon.tagTypeList = value; }
        }

        /// <summary>
        /// 字符对应ASCII码转换为对应的16进制数（1个字符对应2位16进制数[同为1个字节]）字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string String2HexStr(string str)
        {
            string strReturn = "";//  存储转换后的编码
            foreach (short shortx in str.ToCharArray())
            {
                strReturn += shortx.ToString("X");
            }
            return strReturn;
        }

        /// <summary>
        /// 16进制数字符转换为十进制表示的ASCII码对应的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HexStr2String(string str)
        {
            string sResult = "";
            for (int i = 0; i < str.Length / 2; i++)
            {
                sResult += (char)short.Parse(str.Substring(i * 2, 2), global::System.Globalization.NumberStyles.HexNumber);
            }
            return sResult;
        }
    }

    /// <summary>
    /// 方法调用返回信息
    /// </summary>
    public class ReturnMessage
    {
        /// <summary>
        /// 方法调用返回状态
        /// </summary>
        public bool CallStatus { get; set; }
        /// <summary>
        /// 方法调用返回状态信息
        /// </summary>
        public string CallMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ReturnMessage()
        {
            CallStatus = false;
            CallMessage = "未知";
        }
    }
}
