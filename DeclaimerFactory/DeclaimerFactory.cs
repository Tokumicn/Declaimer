using DeclaimerCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaitingGodotDeclaimer;

namespace DeclaimerFactory
{
    public class DeclaimerFactory
    {
        /// <summary>
        /// 创建读取器
        /// </summary>
        /// <param name="hardwareType"></param>
        /// <returns></returns>
        public static IDeclaimer CreateDeclaimer(DeclaimerHardwareType hardwareType)
        {
            IDeclaimer declarmer = null;
            switch (hardwareType)
            {
                //斑马（摩托罗拉）  固定式读取器
                case DeclaimerHardwareType.Fixed_ZebraFX9500:
                    declarmer = new ZebraRFIDFixedReader();
                    break;
                //易迈腾（霍尼韦尔） 固定式读取器
                case DeclaimerHardwareType.Fixed_IntermecIV7:
                case DeclaimerHardwareType.Fixed_IntermecIF2:
                    declarmer = new IntermecRFIDFixedReader();
                    break;
                //斑马 （摩托罗拉）  手持读取器
                case DeclaimerHardwareType.Handeld_ZebraMC9190Z:
                    break;
                //易迈腾（霍尼韦尔） 手持读取器
                case DeclaimerHardwareType.Handeld_IntermecIP30:
                    break;
            }
            return declarmer;
        }
    }
}
