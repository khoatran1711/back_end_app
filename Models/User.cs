namespace _Net_API_Web.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Column("user_account")]
    public string? UserAccount { get; set; }
    [Column("user_name")]
    public string? UserName { get; set; }
    [Column("user_phone")]
    public string? UserPhone { get; set; }
    [Column("user_mail")]
    public string? UserMail { get; set; }

}