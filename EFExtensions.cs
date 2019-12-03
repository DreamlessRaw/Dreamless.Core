using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Dreamless.Core
{
    public static class EFExtensions
    {
        public static IQueryable<TDestination> SelectAndMapper<TDestination>(
            this IQueryable source,
            params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return AutoMapper.QueryableExtensions.Extensions.ProjectTo(
                source,
                MapperUtils.Mapper.ConfigurationProvider,
                null,
                membersToExpand
                );
        }
    }
}
