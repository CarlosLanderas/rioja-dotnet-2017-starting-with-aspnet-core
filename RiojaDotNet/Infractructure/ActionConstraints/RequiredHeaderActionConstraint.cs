using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace RiojaDotNet.Infractructure.ActionConstraints
{
    public class RequiredHeaderActionConstraint: IActionConstraint
    {
        private readonly string header;

        public RequiredHeaderActionConstraint(string header)
        {
            this.header = header;
        }
        public bool Accept(ActionConstraintContext context)
        {
            return context.RouteContext.HttpContext.Request.Headers.ContainsKey(header);
        }

        public int Order { get; } = 300;
    }
}
