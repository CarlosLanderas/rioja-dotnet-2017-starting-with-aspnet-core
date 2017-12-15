using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiojaDotNet.Infractructure.Filters
{
    public class ShortCircuitResourceFilter :  Attribute, IResourceFilter
    {        
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.Result = new ContentResult()
            {
                Content = "The site is under maintenance"
            };
        }
        public void OnResourceExecuted(ResourceExecutedContext context) { }
        
    }
}
