using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using RiojaDotNet.Infractructure.ActionConstraints;
using RiojaDotNet.Infractructure.Attributes;

namespace RiojaDotNet.Infractructure.Conventions
{
    public class RequiresHeaderApplicationConvention: IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            var applicableActions = GetConventionApplicationActions(application);

            foreach (var action in applicableActions)
            {
                foreach (var selector in action.Selectors)
                {
                    var requiredAttribute = action.ActionMethod.GetCustomAttribute<RequiredHeaderAttribute>();
                    selector.ActionConstraints.Add(new RequiredHeaderActionConstraint(requiredAttribute.Header));
                }
            }
        }

        public IEnumerable<ActionModel> GetConventionApplicationActions(ApplicationModel application)
        {            
            return application.Controllers.SelectMany(c => c.Actions)
                .Where(a => a.ActionMethod.GetCustomAttribute<RequiredHeaderAttribute>() != null)
                .ToList(); 
        }
    }
}
