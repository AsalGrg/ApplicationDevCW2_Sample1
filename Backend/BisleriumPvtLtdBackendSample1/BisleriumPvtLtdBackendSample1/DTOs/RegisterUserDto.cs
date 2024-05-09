namespace BisleriumPvtLtdBackendSample1.DTOs
{
    public class RegisterUserDto
    {
        //for editing profile
        public string? Id {  get; set; }
        public string? PhoneNumber {  get; set; }

        public string Email {get;set;}
        public string Password { get;set;}  
        public string Username { get;set;}
    }
}
