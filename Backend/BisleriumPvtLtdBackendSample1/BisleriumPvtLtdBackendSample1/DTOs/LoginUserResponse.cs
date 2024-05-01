namespace BisleriumPvtLtdBackendSample1.DTOs
{
    public class LoginUserResponse
    {
        public Guid Id { get; set; }
        public bool IsAdmin { get; set; }
        public string Username { get; set; }
        public string UserDp {  get; set; }

    }
}
