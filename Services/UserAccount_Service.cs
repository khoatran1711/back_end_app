
using Microsoft.AspNetCore.Mvc;
using _Net_API_Web.Models;

namespace _Net_API_Web.Services;


using Microsoft.EntityFrameworkCore;

public class UserAccountContext : DbContext
{
    public DbSet<UserAccount> MyDatas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder
        .Entity<UserAccount>(builder =>
    {
        builder.HasKey(x => x.user_account);
        builder.ToTable("useraccount");
    });
}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(@"Host=localhost; Database=test_data; Username=postgres; Password=khoa123");
}



public static class UserAccount_Services {
    static List<UserAccount> List_of_account { get; }
    static UserAccount_Services()
    {     
    }

    //public static List<MyData> GetList(){
        //var context = new BlogContext();
       // var fBlogs = context.MyDatas.ToList();
      //  return fBlogs;
   // }
  
    public static List<UserAccount> GetAll() {
        var context = new UserAccountContext();
        var fBlogs = context.MyDatas.ToList();
        return fBlogs;
    }

    public static UserAccount? Get(string account) {
        var context = new UserAccountContext();
        var blog = context.MyDatas
        .Single(b => b.user_account == account);
        return blog;
    }
    public static void Add(UserAccount data)
    {
        if (data is null )
            return;
        
        List_of_account.Add(data);

        using (var db = new UserAccountContext())
        {
            var add_data = data;
            db.MyDatas.Add(data);
            db.SaveChanges();
        }
    }
    public static void Delete(string account)
    {
        var data = Get(account);
        if(data is null)
            return;
        using (var db = new UserAccountContext())
        {
            db.MyDatas.Attach(data);
            db.Entry(data).State = EntityState.Deleted;
            db.SaveChanges();
        }
    }
    public static void Update(UserAccount data)
    {   
        var my_data = data;
        if (data is null)
            return;
        using (var db = new UserAccountContext())
        {
            db.MyDatas.Attach(data);
            db.Entry(data).Property(x => x.user_account).IsModified = true;
            db.SaveChanges();
        }
        return;
    }
}