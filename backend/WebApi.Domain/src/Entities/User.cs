using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Domain.src.Entities
{
        public class User : BaseEntity
    {
        [Required] 
        public string Username { get; set; }
        [Required] 
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [Required] 
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Postcode { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public UserRole UserRole { get; set; }

        [JsonIgnore]
        public List<Order> Orders { get; set; } = new List<Order>();

    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        Admin = 1,
        User = 2
    }
}