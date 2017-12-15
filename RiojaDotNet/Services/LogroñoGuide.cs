using RiojaDotNet.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiojaDotNet.Services.Model;

namespace RiojaDotNet.Services
{
    public class LogroñoGuide : ILogroñoGuide
    {
        private static ConcurrentBag<PlaceOfInterest> placesOfInterest = new ConcurrentBag<PlaceOfInterest>(
            new List<PlaceOfInterest>()
            {
                PlaceOfInterest.Create("La calle Laurel, (la ruta de los elefantes)", Address.Create("C:/ Laurel")),
                PlaceOfInterest.Create("Concatedral de Santa María la Redonda", Address.Create("C:/ Portales, 14")),
                PlaceOfInterest.Create("Cafe Moderno", Address.Create("Plaza Martínez Zaporta, 7")),
                PlaceOfInterest.Create("El espolón", Address.Create("Calle Miguel Villanueva")),
                PlaceOfInterest.Create("Calados de vino en Rua Vieja", Address.Create("Casco antiguo")),
                PlaceOfInterest.Create("El parlamento", Address.Create("C:/ Barriocepo, 49")),
                PlaceOfInterest.Create("Iglesia imperial de Santa María de Palacio", Address.Create("Calle Marqués de San Nicolás, 36")),
                PlaceOfInterest.Create("Iglesia de Santiago", Address.Create("Calle Barriocepo, 8"))
            }
        );

        public IList<PlaceOfInterest> GetPlacesOfInterest()
        {
            return placesOfInterest.ToList();
        }

        public void AddPlaceOfInterest(PlaceOfInterest place)
        {
            placesOfInterest.Add(place);
        }
    }
}
