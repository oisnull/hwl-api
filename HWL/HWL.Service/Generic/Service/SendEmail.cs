﻿using GMSF;
using HWL.Entity;
using HWL.Entity.Models;
using HWL.Service.Generic.Body;
using HWL.Tools;
using System;
using System.Net.Mail;

namespace HWL.Service.Generic.Service
{
    public class SendEmail : ServiceHandler<SendEmailRequestBody, SendEmailResponseBody>
    {
        private readonly HWLEntities db;
        public SendEmail(HWLEntities dbContext, SendEmailRequestBody request) : base(request)
        {
            this.db = dbContext;
        }

        protected override void ValidateRequestParams()
        {
            base.ValidateRequestParams();
            if (string.IsNullOrEmpty(this.request.Email))
            {
                throw new Exception("Email can't be empty.");
            }

            if (!GenericUtility.IsValidMail(this.request.Email))
            {
                throw new Exception("The current format of email is invaild.");
            }
        }

        public override SendEmailResponseBody ExecuteCore()
        {
            string randText = RandomText.GetNum(6);//生成6位验证码

            var emailInfo = SendContentConfig.EmailRegisterDesc(randText);//组织发送内容

            string error = "";
            bool succ = EmailAction.SendEmail(emailInfo.Item1, emailInfo.Item2, this.request.Email, out error);//开始发送
            if (!succ) throw new Exception(error);

            int codeId = User.UserUtility.AddCode(db, CodeType.Register, randText, emailInfo.Item2, this.request.Email); //发送成功后记录验证码
            if (codeId <= 0)
            {
                throw new Exception("Check code send failed.");
            }

            return new SendEmailResponseBody()
            {
                CurrentEmail = this.request.Email,
                Status = ResultStatus.Success,
                //CheckCode = randText
            };
        }
    }
}
