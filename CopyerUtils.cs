using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Dreamless.Core
{
    public static class CopyerUtils
    {
        /// <summary>
        /// 反射
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="result">返回结果</param>
        /// <returns></returns>
        public static TResult CopyerTo<TSource, TResult>(this TSource source, TResult result)
        {
            var ps = typeof(TSource).GetProperties();
            var pr = typeof(TResult).GetProperties();
            foreach (var pItem in ps)
            {
                var obj = pItem.GetValue(source);
                foreach (var rItem in pr)
                {
                    if (pItem.Name == rItem.Name)
                    {
                        rItem.SetValue(result, obj);
                    }
                }
            }
            return result;
        }
    }
}
