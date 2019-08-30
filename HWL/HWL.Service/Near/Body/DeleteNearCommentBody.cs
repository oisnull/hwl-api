﻿using HWL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.Service.Near.Body
{
    public class DeleteNearCommentRequestBody
    {
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public string NearCircleUpdateTime { get; set; }
    }
    public class DeleteNearCommentResponseBody
    {
        public ResultStatus Status { get; set; }
        public string NearCircleLastUpdateTime { get; set; }
    }
}
