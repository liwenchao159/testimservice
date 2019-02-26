using System;
using System.Collections.Generic;
using System.Text;

namespace ImService.Help
{
    public class AopResult<T>
    {
        /// <summary>
        /// 返回代码
        /// </summary>
        public ReturnCode RtnCode { get; set; }                 //返回代码
        /// <summary>
        /// 返回消息
        /// </summary>
        public string RtnMsg { get; set; }                      //返回消息
        /// <summary>
        /// 菜单数据
        /// </summary>
        public string Action { get; set; }                        //菜单数据
        /// <summary>
        /// 返回内容数据
        /// </summary>
        public T content { get; set; }                          //返回内容数据
        /// <summary>
        /// 分页参数
        /// </summary>
        public PageAttribute Page { get; set; }

        public string RequestId { get; set; }
        //页面数据

        public AopResult()
        {
            this.Action = "";
            this.RtnCode = ReturnCode.Successful;
            this.RtnMsg = string.Empty;
            this.Page = new PageAttribute();
        }
    }
    /// <summary>
    /// 分页对象
    /// </summary>
    public class PageAttribute
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }  //当前页码
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }   //每页行数 
        /// <summary>
        /// 总行数据，返回数据时使用
        /// </summary>
        public int Rows { get; set; }       //总行数据，返回数据时使用
        /// <summary>
        /// 查询消耗的总时间(单位 毫秒)
        /// </summary>
        public long QueryTime { get; set; }  //查询消耗的总时间(单位 毫秒)

        public PageAttribute()
        {
            this.PageIndex = 1;
            this.PageSize = 20;
            this.Rows = 0;
            this.QueryTime = 0;
        }

        public static PageAttribute Init(int rows, int page = 1, long queryTime = 0, int pagesize = 20)
        {
            return new PageAttribute
            {
                PageIndex = page,
                PageSize = pagesize,
                QueryTime = queryTime,
                Rows = rows
            };
        }
    }

    public enum ReturnCode
    {
        Successful = 200,           //成功
        Error = 201,                //失败
        Unauthorized = 401,         //没有登录 或 登录超时
        VerifyError = 402, //验证失败
        QRCodeScanOK = 600,         //登录二维码扫描成功
        QRCodeScanError = 601,       //登录二维码扫描失败
        QRCodeAuthOK = 700,         //二维码登录授权成功
        QRCodeAuthError = 701,       //二维码登录授权失败
        FYQLeave = 403,             //离职用户强制退出
        /// <summary>
        /// 未注册认证
        /// </summary>
        NotRegisterAuth = 405,
        /// <summary>
        /// 注册未完成（需上传身份证）
        /// </summary>
        RegisterToCard = 406
    }

    public static class AopResult
    {
        public static AopResult<T> Success<T>(T t, PageAttribute page = null, ReturnCode code = ReturnCode.Successful, string msg = null, string requestId = null)
        {
            var result = new AopResult<T>
            {
                content = t,
                RtnCode = code,
                RtnMsg = msg,
                Page = page
            };
            result.RequestId = result.RequestId ?? requestId;
            return result;
        }

        public static AopResult<T> Fail<T>(string msg, ReturnCode code = ReturnCode.Error, T t = default(T), PageAttribute page = null, string requestId = null)
        {
            var result = new AopResult<T>
            {
                content = t,
                RtnCode = code,
                RtnMsg = msg,
                Page = page
            };
            result.RequestId = result.RequestId ?? requestId;
            return result;
        }
    }
}
