using DeclaimerCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WaitingGodotDeclaimer
{
    public interface IDeclaimer
    {


        /// <summary>
        /// 初始化读取器
        /// </summary>
        /// <param name="strIP"></param>
        /// <param name="strPort"></param>
        /// <param name="connTimeOut"></param>
        /// <param name="readerPowerList"></param>
        /// <param name="readerAntennasList"></param>
        /// <returns></returns>
        ReturnMessage InitReader(string strIP, string strPort, string connTimeOut, List<string> readerPowerList, List<string> readerAntennasList);

        /// <summary>
        /// 开启读取器连接
        /// </summary>
        /// <returns></returns>
        ReturnMessage OpenReaderConnection();

        /// <summary>
        /// 关闭读取器连接
        /// </summary>
        /// <returns></returns>
        ReturnMessage CloseReaderConnection();

        /// <summary>
        /// 开启读取
        /// </summary>
        /// <param name="readingType">读取类型：1.连续读取;2.一次读取</param>
        /// <returns></returns>
        ReturnMessage StartReading(ReadingType readingType, int aPollInterval, int aReadInterval);

        /// <summary>
        /// 暂停读取
        /// </summary>
        /// <returns></returns>
        ReturnMessage StopReading();

        /// <summary>
        /// 显示声音与报警灯
        /// </summary>
        /// <param name="GPOIndex"></param>
        void ShowAlarmLamp_Sound(int[] GPOIndex);
    }
}
