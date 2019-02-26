using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImService.Help;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ImServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public ImSerectInfo ImSercectInfo;

        public ValuesController(IOptions<ImSerectInfo> settings)
        {
            ImSercectInfo = settings.Value;
        }
        /// <summary>
        /// 获取Value
        /// </summary>
        /// <param name="testId">测试的ID</param>
        /// <param name="testCode">测试的Code</param>
        /// <returns></returns>
        [HttpGet]
        public AopResult<string[]> Get(string testId, string testCode)
        {
            String url = "https://api.netease.im/nimserver/user/create.action";
            var randomStr = Sha1SecretHelp.GetRandomStr();
            var curTimeStamp = Sha1SecretHelp.GetTimeStamp(DateTime.Now.Date).ToString();
            var checkSum = Sha1SecretHelp.GetCheckSum(ImSercectInfo.AppSecret, randomStr, curTimeStamp);
            var headdict = new Dictionary<string, string>();
            headdict.Add("AppKey", ImSercectInfo.AppId);
            headdict.Add("Nonce", randomStr);
            headdict.Add("CurTime", curTimeStamp);
            headdict.Add("CheckSum", checkSum);
            headdict.Add("Content-Type", "application/x-www-form-urlencoded;charset=utf-8");
            var str = HttpHelp.Post(url, "accid=helloworld", headdict);
            return AopResult.Success(new string[] { checkSum, str });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
