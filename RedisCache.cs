using CSRedis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dreamless.Core
{
    /// <summary>
    /// Redis缓存类
    /// </summary>
    public class RedisCache
    {
        #region 初始化
        public static void RedisInitialization(string str)
        {
            RedisHelper.Initialization(new CSRedisClient(str));
        }

        #endregion

        /// <summary>
        /// 尝试获取缓存
        /// </summary>
        public static bool TryGetCache<T>(string key, out T model)
        {
            if (RedisHelper.Exists(key))
                model = RedisHelper.Get<T>(key);
            else
            {
                model = default(T);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 添加或更新缓存
        /// </summary>
        public static void AddOrUpdateCache<T>(string key, T model, int expireSeconds = -1) => RedisHelper.Set(key, model, expireSeconds);
    }
}
