namespace Contracts.Responses
{
    public class LoginResponse : Employee
    {
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}
