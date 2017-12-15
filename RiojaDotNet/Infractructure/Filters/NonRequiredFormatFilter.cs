using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace RiojaDotNet.Infractructure.FormatFilters
{
    public class NonRequiredFormatFilter: FormatFilter
    {
        public NonRequiredFormatFilter(IOptions<MvcOptions> options) : base(options) { }
        public override string GetFormat(ActionContext context)
        {            
            var format =  base.GetFormat(context);
            return format ?? "json";
        }
    }
}
