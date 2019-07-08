using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Dreamless.Core
{
    public static class CopyerUtils
    {
        public static TResult Copyer<TSource, TResult>(this TSource source, TResult result)
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
