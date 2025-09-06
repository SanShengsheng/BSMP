using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.RedisHelpers
{
    public class CustomRedisHelper
    {
        private IDistributedCache _memoryCache;

        public CustomRedisHelper(IDistributedCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            string value = string.Empty;

            if (!string.IsNullOrEmpty(key))
            {
                if (Exists(key))
                {
                    value = Encoding.UTF8.GetString(_memoryCache.Get(key));
                }
            }
            return value;
        }

        public object GetObj(string key)
        {
            object resultObj = new object();

            if (!string.IsNullOrEmpty(key))
            {
                if (Exists(key))
                {

                    using (MemoryStream ms = new MemoryStream())
                    {
                        IFormatter formatter = new BinaryFormatter();

                        formatter.Serialize(ms, resultObj);
                    }
                }
            }

            return resultObj;
        }

        /// <summary>
        /// 设置或添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddObj(string key, object value)
        {
            byte[] val = null;

            if (value.ToString() != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    IFormatter formatter = new BinaryFormatter();

                    formatter.Serialize(ms, value);

                    val = ms.GetBuffer();
                }
            }

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();

            //设置绝对过期时间
            options.AbsoluteExpiration = DateTime.Now.AddMinutes(30);
            //options.SlidingExpiration = TimeSpan.FromMinutes(30);
            _memoryCache.Set(key, val, options);
            //刷新缓存
            _memoryCache.Refresh(key);

            return Exists(key);
        }

        /// <summary>
        /// 异步获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<object> GetAsync(string key)
        {
            string value = string.Empty;

            var tempValue = await _memoryCache.GetAsync(key);

            if (tempValue != null)
            {
                value = Encoding.UTF8.GetString(tempValue);
            }

            return value;
        }

        /// <summary>
        /// 设置或添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Add(string key, object value)
        {
            byte[] val = null;

            if (value.ToString() != null)
            {
                val = Encoding.UTF8.GetBytes(value.ToString());
            }

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();

            //设置绝对过期时间
            options.AbsoluteExpiration = DateTime.Now.AddMinutes(30);
            //options.SlidingExpiration = TimeSpan.FromMinutes(30);
            _memoryCache.Set(key, val, options);
            //刷新缓存
            _memoryCache.Refresh(key);

            return Exists(key);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            bool isRemove = false;

            if (key != "" || key == null)
            {
                _memoryCache.Remove(key);

                if (Exists(key) == false)
                {
                    isRemove = true;
                }
            }

            return isRemove;
        }

        /// <summary>
        /// 验证缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            bool isExist = true;

            byte[] val = _memoryCache.Get(key);

            if (val == null || val.Length == 0)
            {
                isExist = false;
            }

            return isExist;
        }
    }
}
