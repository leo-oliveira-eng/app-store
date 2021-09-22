namespace IdentityServer.Domain.Events.Models
{
    public class PasswordChangedEventModel : EventModel
    {
        public string Email { get; set; }

        public string Name { get; set; }
    }
}
