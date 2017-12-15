using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RiojaDotNet.Api.Cities.Requests;
using RiojaDotNet.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiojaDotNet.Infractructure.ModelBinders
{
    public class HumanSearchBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {

            var request = bindingContext.HttpContext.Request;

            //Allow rewinding for this request because we are gonna read the payload
            request.EnableRewind();

            using (var reader = new StreamReader(request.Body))
            {
                var bodyContent = reader.ReadToEnd();              
                bindingContext.Result = ModelBindingResult.Success(new HumanSearchRequest()
                {
                    Mood = GetMoodFromPayLoad(bodyContent),
                    Language = GetLanguageFromHeaders(request),
                    Search = bodyContent,

                });

                request.Body.Position = 0;

                return Task.CompletedTask;
            }          
        }

        private Language GetLanguageFromHeaders(HttpRequest request)
        {
            bool languageHeaderPresent = request.Headers.TryGetValue("language", out var stringValue);
            if(languageHeaderPresent && 
                Enum.TryParse<Language>(stringValue.FirstOrDefault(), true, out var headerValue))
            {
                return headerValue;
            }

            return Language.Spanish;
        }

        private MoodStatus GetMoodFromPayLoad(string content)
        {
            return content.Contains(":)")
                    ? MoodStatus.Happy :
                    content.Contains(":(") ? 
                                            MoodStatus.Unhappy : 
                                            MoodStatus.NotDefined ;
        }
        
    }
}
