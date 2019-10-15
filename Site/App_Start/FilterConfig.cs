﻿using Site.Filters;
using System.Web;
using System.Web.Mvc;

namespace Site
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CultureActionFilter());
            filters.Add(new AjaxAuthorizeAttribute());
            filters.Add(new PermissionAttribute());
            filters.Add(new PageHeaderAttribute());
        }
    }
}
