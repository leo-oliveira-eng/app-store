using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Catalog.Api.Filters
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class RequiredIdAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
            => new RequiredIdFilter();
    }
}
