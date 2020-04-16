﻿using CodeChallenge.Entities;
using System.Web;
using System.Web.Mvc;

namespace CodeChallenge
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CCErrorFilter());
        }
    }
}