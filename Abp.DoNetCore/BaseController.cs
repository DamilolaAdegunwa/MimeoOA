using Abp.DoNetCore.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.DoNetCore
{
    [ServiceFilter(typeof(ExceptionFilter))]
    public class BaseController : Controller
    {
        protected string ClientIP { get; }
    }
}
