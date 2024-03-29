﻿using Fissoft.EntitySearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dreamless.Core
{
    public static class SearchModelExtensions
    {
        /// <summary>
        /// 补全SearchModel无默认排序【Id,Desc】,
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public static SearchModel GetSearchModel(this SearchModel searchModel, string sortName = "Id", string sortOrder = "Desc")
        {
            searchModel.SortName = sortName;
            searchModel.SortOrder = sortOrder;
            return searchModel;
        }
    }
}
