using System;


namespace RiojaDotNet.Services.Model
{
    public class PlaceOfInterest
    {
        internal PlaceOfInterest(string name, Address address)
        {
            Address = address;
            Name = name;
        }
        public string Name { get; }
        public Address Address { get; }

        public static PlaceOfInterest Create(string description, Address address)
        {
            if (string.IsNullOrEmpty(description)) throw new ArgumentNullException(nameof(description));

            return new PlaceOfInterest(
                  description,
                  address ?? throw new ArgumentNullException(nameof(address)));
        }
    }
}
