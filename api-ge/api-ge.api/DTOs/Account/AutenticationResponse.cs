namespace api_gestiona.DTOs.Account
{
    public class AutenticationResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
