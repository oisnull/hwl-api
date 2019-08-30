﻿using HWL.Entity;
using HWL.Entity.Extends;
using System.Collections.Generic;

namespace HWL.Service.Circle.Body
{
    public class GetCircleInfosRequestBody
    {
        /// <summary>
        /// 当前登录的用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 查看的用户id
        /// </summary>
        public int ViewUserId { get; set; }
        /// <summary>
        /// 如果有值，则获取比这个值小的数据列表
        /// </summary>
        public int MinCircleId { get; set; }

        public int PageIndex { get; set; }

        public int Count { get; set; }
        public List<CircleMatchInfo> CircleMatchInfos { get; set; }
    }

    public class GetCircleInfosResponseBody
    {
        public int ViewUserId { get; set; }
        public string ViewUserName { get; set; }
        public string ViewUserImage { get; set; }
        public List<CircleInfo> CircleInfos { get; set; }
    }
}
