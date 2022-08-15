namespace _Net_API_Web.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class UserAccount
{
    [Column("user_account")]
    public string? Account { get; set; }
    [Column("user_password")]
    public string? Password { get; set; }


}