namespace DanceConnect.Server.Response.Dtos
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public int IdentityId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Gender { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? Token { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsProfileCompleted { get; set; } = true;
    }
}
