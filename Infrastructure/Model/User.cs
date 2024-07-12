namespace Infrastructure.Model;

/**
 * User model class
 * <p>
 *   - This class is used to represent a user.
 *   - The user can create reports but a report can only be created by a user.
 * </p>
 * <remarks>
 *     - Author : LordMathi2741
 *     - Version 1.0
 * </remarks>
 */
public partial class User
{
    public long Id { get; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public string Role { get; set; }
    public string Email { get; set; }
    
    public string SocialReason { get; set; }
    
    public string Ruc { get; set; }
    
    public ICollection<Report> Reports { get; private set; }
}

public partial class User
{
    public User()
    {
        Username = string.Empty;
        Password = string.Empty;
        Email = string.Empty;
    }

    public User(string username, string password, string email)
    {
        Username = username;
        Password = password;
        Email = email;
        Role = ERoleTypes.Default.ToString();
        CreatedDate = DateTime.Now;
        UpdatedDate = null;
    }
    
}