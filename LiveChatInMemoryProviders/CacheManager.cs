using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using LiveChat.Entities;

namespace LiveChat.InMemoryProvider
{
    public static class CacheManager
    {
        private static IQueryable<T> GetMemoryObject<T>(string key)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx != null)
            {
                return (IQueryable<T>)ctx.Cache[key];
            }
            return null;
        }

        private static void SetMemoryObject<T>(string key, IQueryable<T> list)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (GetMemoryObject<T>(key) != null)
                    ctx.Cache[key] = list;
                else
                    ctx.Cache.Add(key, list, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30), CacheItemPriority.Normal, null);
            }
        }
        
        public static IQueryable<OperatorEntity> Operators
        {
            get { return GetMemoryObject<OperatorEntity>("op"); }
            set { SetMemoryObject<OperatorEntity>("op", value); }
        }

        public static IQueryable<DepartmentEntity> Departments
        {
            get { return GetMemoryObject<DepartmentEntity>("dep"); }
            set { SetMemoryObject<DepartmentEntity>("dep", value); }
        }

        public static IQueryable<PageRequestEntity> PageRequests
        {
            get { return GetMemoryObject<PageRequestEntity>("pages"); }
            set { SetMemoryObject<PageRequestEntity>("pages", value); }
        }

        public static IQueryable<ChatRequestEntity> ChatRequests
        {
            get { return GetMemoryObject<ChatRequestEntity>("chatReq"); }
            set { SetMemoryObject<ChatRequestEntity>("chatReq", value); }
        }

        public static IQueryable<ChannelEntity> ChatChannels
        {
            get { return GetMemoryObject<ChannelEntity>("chat"); }
            set { SetMemoryObject<ChannelEntity>("chat", value); }
        }

        public static IQueryable<MessageEntity> Messages
        {
            get { return GetMemoryObject<MessageEntity>("msg"); }
            set { SetMemoryObject<MessageEntity>("msg", value); }
        }


    }
}
