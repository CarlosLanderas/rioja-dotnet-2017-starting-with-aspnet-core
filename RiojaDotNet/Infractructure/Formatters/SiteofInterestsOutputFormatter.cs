using Microsoft.AspNetCore.Mvc.Formatters;
using RiojaDotNet.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace RiojaDotNet.Infractructure.Formatters
{
    public class SiteofInterestsOutputFormatter: TextOutputFormatter
    {
        public SiteofInterestsOutputFormatter()
        {                     
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/logronosites"));
            SupportedEncodings.Add(Encoding.UTF8);
        }
        protected override bool CanWriteType(Type type)
        {
            if (typeof(IList<PlaceOfInterest>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();
            buffer.AppendLine("Site of interest name ; Address line");
            
            if(context.Object is IList<PlaceOfInterest>)
            {
                foreach(var siteOfInterest in context.Object as IList<PlaceOfInterest>)
                {
                    buffer.AppendLine($"{siteOfInterest.Name} ; {siteOfInterest.Address.AddressLine}");
                }              
            }

            return response.WriteAsync(buffer.ToString());
        }
    }
}
