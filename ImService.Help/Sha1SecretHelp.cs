using System;
using System.Security.Cryptography;
using System.Text;

namespace ImService.Help
{
    public static class Sha1SecretHelp
    {

        /// <summary>
        /// Get TimeStamp
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static double GetTimeStamp(DateTime d)
        {
            TimeSpan ts = d - new DateTime(1970, 1, 1);
            return ts.TotalMilliseconds;
        }

        /// <summary>
        /// 获取长度为15的随机字符串
        /// </summary>
        /// <returns></returns>
        public static string GetRandomStr()
        {
            return System.Guid.NewGuid().ToString().Replace("-", "").Substring(0, 15);
        }
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="appSecret"></param>
        /// <param name="randomStr"></param>
        /// <param name="curTime"></param>
        /// <returns></returns>
        public static string GetSha1SercretStr(string appSecret, string randomStr, string curTime)
        {
            using (SHA1 sh1 = new SHA1CryptoServiceProvider())
            {
                var byte_in = Encoding.UTF8.GetBytes(appSecret + randomStr + curTime);
                var byte_out = sh1.ComputeHash(byte_in);
                string result = BitConverter.ToString(byte_out);
                return result.Replace("-", "");
            }
        }
    }
}
