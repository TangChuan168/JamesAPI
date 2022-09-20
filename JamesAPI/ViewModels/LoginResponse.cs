namespace JamesAPI.ViewModels
{
    public class LoginResponse
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }   
        public string Token { get; set; }   
    }
}
