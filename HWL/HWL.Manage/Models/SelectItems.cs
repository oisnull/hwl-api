using HWL.Entity;
using HWL.PushStandard;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HWL.Manage
{
    public class SelectItems
    {
        public static List<SelectListItem> GetNoticeTypeList()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            SelectListItem item0 = new SelectListItem()
            {
                Text = "公告类型",
                Value = "0",
            };

            SelectListItem item1 = new SelectListItem()
            {
                Value = ((int)NoticeType.Register).ToString(),
                Text = CustomerEnumDesc.GetNoticeType(NoticeType.Register),
            };

            SelectListItem item2 = new SelectListItem()
            {
                Value = ((int)NoticeType.Update).ToString(),
                Text = CustomerEnumDesc.GetNoticeType(NoticeType.Update),
            };

            SelectListItem item3 = new SelectListItem()
            {
                Value = ((int)NoticeType.Other).ToString(),
                Text = CustomerEnumDesc.GetNoticeType(NoticeType.Other),
            };

            items.Add(item0);
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);

            return items;
        }

        public static List<SelectListItem> GetPushSourceTypeList()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value=((int)SourceType.SystemCollection).ToString(),
                    Text= SourceType.SystemCollection.ToString()
                },
                new SelectListItem()
                {
                    Value=((int)SourceType.TestCreate).ToString(),
                    Text= SourceType.TestCreate.ToString(),
                    Selected=true
                },
            };

            return items;
        }

        public static List<SelectListItem> GetPushMessageTypeList()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value=((int)PushMessageType.NearCirlce).ToString(),
                    Text= PushMessageType.NearCirlce.ToString(),
                    Selected=true
                },
                new SelectListItem()
                {
                    Value=((int)PushMessageType.ChatRecord).ToString(),
                    Text= PushMessageType.ChatRecord.ToString(),
                },
            };

            return items;
        }

        public static List<SelectListItem> GetPositionTypeList()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value=((int)PushPositionType.User).ToString(),
                    Text= PushPositionType.User.ToString(),
                },
                new SelectListItem()
                {
                    Value=((int)PushPositionType.Near).ToString(),
                    Text= PushPositionType.Near.ToString(),
                    Selected=true
                },
                new SelectListItem()
                {
                    Value=((int)PushPositionType.Area).ToString(),
                    Text= PushPositionType.Area.ToString(),
                },
            };

            return items;
        }
    }
}