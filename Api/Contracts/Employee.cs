using Contracts.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Contracts
{
    public class Employee
    {
        public long Id { get; set; }
        public long? LeadId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public UserRole Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
