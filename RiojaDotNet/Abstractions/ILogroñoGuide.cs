using RiojaDotNet.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiojaDotNet.Abstractions
{
    public interface ILogroñoGuide
    {
        IList<PlaceOfInterest> GetPlacesOfInterest();
        void AddPlaceOfInterest(PlaceOfInterest place);
    }
}
