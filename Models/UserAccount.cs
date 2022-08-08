namespace _Net_API_Web.Models;


public class UserAccount{
    public UserAccount(string a , string b){
        this.user_account = a;
        this.user_password=b;
    }
    public string? user_account{get;set;}
    public string? user_password{get;set;}


}