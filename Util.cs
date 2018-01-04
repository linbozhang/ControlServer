using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlServer
{
    /// <summary>
    /// 工具
    /// </summary>
    class Util
    {
        public static void LOG(string msg)
        {
            //var currentTime = new System.DateTime();
            var now = System.DateTime.Now;
            var s = "Info :" + now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + msg;
            Console.WriteLine(s);
        }
        public static void LOGDebug(string msg)
        {
            var now = System.DateTime.Now;
            var s = "Debug:" + now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + msg;
            Console.WriteLine(s);
        }

        public static void LOGError(string msg)
        {
            var now = System.DateTime.Now;
            var s = "Error:" + now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + msg;
            Console.WriteLine(s);
        }
    }
}
