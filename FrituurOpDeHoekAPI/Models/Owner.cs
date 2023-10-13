namespace FrituurOpDeHoekAPI.Models
{
    public class Owner : User
    {
        public string AdminName { get; set; } = "Admin";
        public string AdminPassword { get; set; } = "Admin";
    }
}
