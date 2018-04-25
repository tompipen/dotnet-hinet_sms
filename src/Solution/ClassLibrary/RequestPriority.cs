using System;
using System.Collections.Generic;
using System.Text;

namespace HinetSms
{
    /// <summary>
    /// 訊息優先權
    /// </summary>
    public enum RequestPriority:byte
    {
        /// <summary>
        /// 最低優先權
        /// </summary>
        P0 = 0,

        P1 = 1,
        P2 = 2,
        P3 = 3,
        P4 = 4,
        P5 = 5,
        P6 = 6,
        P7 = 7,
        P8 = 8,

        /// <summary>
        /// 最高優先權
        /// </summary>
        P9 = 9
    }
}
