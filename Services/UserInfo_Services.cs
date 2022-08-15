using Microsoft.AspNetCore.Mvc;
using _Net_API_Web.Models;

namespace _Net_API_Web.Services;


using Microsoft.EntityFrameworkCore;


public class UserInfoContext : DbContext
{
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User>(builder =>
        {
            builder.HasKey(x => x.UserAccount);
            builder.ToTable("userinfo");
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(@"Host=localhost; Database=test_data; Username=postgres; Password=khoa123");
}




public static class UserInfo_Services
{
    static List<User> List_of_user = new List<User>();
    static UserInfo_Services()
    {
    }

    //public static List<MyData> GetList(){
    //var context = new BlogContext();
    // var fBlogs = context.MyDatas.ToList();
    //  return fBlogs;
    // }

    public static List<User> GetAll()
    {
        var context = new UserInfoContext();
        var fBlogs = context.Users.ToList();
        return fBlogs;
    }

    public static User? Get(string userAccount)
    {
        var context = new UserInfoContext();
        var blog = context.Users
        .Single(b => b.UserAccount == userAccount);
        return blog;
    }

    // public static List<Product>? GetbyCategory (string category_product){
    //     var context = new ProductContext();
    //     var fBlogs = context.Products.Where(b => b.category_product == category_product).ToList();
    //     return fBlogs;

    // }



    public static void Add(User data)
    {
        if (data is null)
            return;
        data.UserAccount = data.UserAccount;
        data.UserName = data.UserName;
        data.UserMail = data.UserMail;
        data.UserPhone = data.UserPhone;

        List_of_user.Add(data);


        using (var db = new UserInfoContext())
        {
            var add_data = data;
            db.Users.Add(data);
            db.SaveChanges();
        }
    }
    public static void Delete(string userAccount)
    {
        var data = Get(userAccount);
        if (data is null)
            return;
        using (var db = new UserInfoContext())
        {
            db.Users.Attach(data);
            db.Entry(data).State = EntityState.Deleted;
            db.SaveChanges();
        }
    }
    public static void Update(User data)
    {
        var my_data = data;
        if (data is null)
            return;
        using (var db = new UserInfoContext())
        {
            db.Users.Attach(data);
            db.Entry(data).Property(x => x.UserName).IsModified = true;
            db.Entry(data).Property(x => x.UserPhone).IsModified = true;
            db.Entry(data).Property(x => x.UserMail).IsModified = true;
            db.SaveChanges();
        }
        return;
    }
}