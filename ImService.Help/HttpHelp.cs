using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ImService.Help
{
    public static class HttpHelp
    {
        public static string Get(string url, IDictionary<string, string> heads = null, string charset = "utf-8", IDictionary<string, string> parameters = null)
        {
            try
            {
                var parameterstring = new StringBuilder();
                if (parameters != null)
                {
                    foreach (var key in parameters.Keys)
                    {
                        parameterstring.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    parameterstring.Remove(0, 1);
                    parameterstring.Insert(0, "?");
                }
                var request = HttpWebRequest.Create(url + parameterstring) as HttpWebRequest;
                request.Method = "GET";

                //添加http标头 用于验证
                if (heads != null)
                {
                    foreach (var key in heads.Keys)
                    {
                        request.Headers.Add(key, heads[key]);
                    }
                }

                // 设置请求的参数形式
                request.ContentType = string.Format("application/json; charset={0}", charset);


                var response = (HttpWebResponse)request.GetResponse();
                var st = response.GetResponseStream();


                var reader = new StreamReader(st, Encoding.GetEncoding(charset));
                var resultVal = reader.ReadToEnd();
                reader.Close();
                response.Close();
                return resultVal.Trim('\ufeff');
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// Http Post
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="heads">Heads</param>
        /// <param name="json">发送内容，一般使用json序列化</param>
        /// <param name="charset">编码，默认“utf-8”</param>
        /// <returns></returns>
        public static string Post(string url, string json, IDictionary<string, string> heads = null, string charset = "utf-8")
        {
            try
            {
                var request = System.Net.HttpWebRequest.Create(url) as System.Net.HttpWebRequest;
                request.Method = "POST";

                //添加http标头 用于验证
                if (heads != null)
                {
                    foreach (var key in heads.Keys)
                    {
                        request.Headers.Add(key, heads[key]);
                    }
                }
                // 设置请求的参数形式
                request.ContentType = string.Format("application/json; charset={0}", charset);

                var encoding = new UTF8Encoding();
                var byte1 = encoding.GetBytes(json);
                // 设置请求参数的长度.
                request.ContentLength = byte1.Length;
                // 取得发向服务器的流
                var newStream = request.GetRequestStream();
                // 使用 POST 方法请求的时候，实际的参数通过请求的 Body 部分以流的形式传送
                newStream.Write(byte1, 0, byte1.Length);
                // 完成后，关闭请求流.
                newStream.Close();
                // GetResponse 方法才真的发送请求，等待服务器返回
                var response = (System.Net.HttpWebResponse)request.GetResponse();

                // 然后可以得到以流的形式表示的回应内容
                var receiveStream = response.GetResponseStream();
                // 还可以将字节流包装为高级的字符流，以便于读取文本内容 
                // 需要注意编码
                var readStream = new System.IO.StreamReader(receiveStream, Encoding.UTF8);

                var resultVal = readStream.ReadToEnd();
                //完成后要关闭字符流，字符流底层的字节流将会自动关闭
                response.Close();
                readStream.Close();
                return resultVal;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
