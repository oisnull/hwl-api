using HWL.Entity.Models;
using HWL.Service.User.Body;
using System;
using System.Linq;

namespace HWL.Service.User.Service
{
    public class GetUserRelationInfo : GMSF.ServiceHandler<GetUserRelationInfoRequestBody, GetUserRelationInfoResponseBody>
    {
        private readonly HWLEntities db;
        public GetUserRelationInfo(HWLEntities db, GetUserRelationInfoRequestBody request) : base(request)
        {
            this.db = db;
        }
        protected override void ValidateRequestParams()
        {
            base.ValidateRequestParams();
            if (this.request.UserId <= 0)
                throw new Exception("UserId参数值无效");
            if (this.request.RelationUserId <= 0)
                throw new ArgumentNullException("RelationUserId参数值无效");
            if (this.request.UserId == this.request.RelationUserId)
                throw new Exception("UserId和RelationUserId的值不能一样");
        }

        public override GetUserRelationInfoResponseBody ExecuteCore()
        {
            GetUserRelationInfoResponseBody res = new GetUserRelationInfoResponseBody();

            res.isFriend = db.t_user_friend.Count(u => u.user_id == this.request.UserId && u.friend_user_id == this.request.RelationUserId) > 0;
            res.isInBlackList = false;

            return res;
        }
    }
}
