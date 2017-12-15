
using FluentValidation;

namespace RiojaDotNet.Api.Cities.Requests
{
    public class CreatePlaceRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class CreatePlaceValidation : AbstractValidator<CreatePlaceRequest>
    {
        public CreatePlaceValidation()
        {
            RuleFor(x => x.Name)
                   .NotEmpty()
                   .MinimumLength(3)
                   .MaximumLength(100);

            
            RuleFor(x => x.Address)
                .NotEmpty()
                .Must(HaveValidAddressDescriptor);
        }

        private bool HaveValidAddressDescriptor(string address = "")
        {
            return !string.IsNullOrEmpty(address) &&
                 address.Contains("C:/") ||
                 address.Contains("Avda") ||
                 address.Contains("Pza");
        }
       
    }
}
