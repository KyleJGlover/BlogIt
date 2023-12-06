namespace BlogIt.Web.Models.ViewModels
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool AdminRole { get; set; }
        public bool AdminRoleCheckbox { get; set; }
    }
}
