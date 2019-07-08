using Fissoft.EntitySearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dreamless.Core
{
    public static class SearchModelExtensions
    {
        public static SearchModel GetSearchModel(this SearchModel searchModel)
        {
            if (string.IsNullOrWhiteSpace(searchModel.SortName))
            {
                searchModel.SortName = "Id";
                searchModel.SortOrder = "Desc";
            }
            return searchModel;
        }
    }
}
