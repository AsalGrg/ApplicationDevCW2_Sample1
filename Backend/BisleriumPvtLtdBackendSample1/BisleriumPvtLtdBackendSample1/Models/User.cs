namespace BisleriumPvtLtdBackendSample1.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }

        public Guid RolesId { get; set; }

        //navigation properties
        public Roles Roles { get; set; }
    }
}
