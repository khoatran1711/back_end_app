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
        builder.HasKey(x => x.user_account);
        builder.ToTable("userinfo");
    });
}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(@"Host=localhost; Database=test_data; Username=postgres; Password=khoa123");
}




public static class UserInfo_Services {
    static List<User> List_of_user = new List<User>();
    static UserInfo_Services()
    {     
    }

    //public static List<MyData> GetList(){
        //var context = new BlogContext();
       // var fBlogs = context.MyDatas.ToList();
      //  return fBlogs;
   // }
  
    public static List<User> GetAll() {
        var context = new UserInfoContext();
        var fBlogs = context.Users.ToList();
        return fBlogs;
    }

    public static User? Get(string user_account) {
        var context = new UserInfoContext();
        var blog = context.Users
        .Single(b => b.user_account == user_account);
        return blog;
    }

    // public static List<Product>? GetbyCategory (string category_product){
    //     var context = new ProductContext();
    //     var fBlogs = context.Products.Where(b => b.category_product == category_product).ToList();
    //     return fBlogs;

    // }


   
    public static void Add(User data)
    {
        if (data is null )
            return;
        data.user_account = data.user_account;
        data.user_name = data.user_name;
        data.user_mail = data.user_mail;
        data.user_phone = data.user_phone;



        List_of_user.Add(data);
     

        using (var db = new UserInfoContext())
        {
            var add_data = data;
            db.Users.Add(data);
            db.SaveChanges();
        }
    }
    public static void Delete(string user_account)
    {
        var data = Get(user_account);
        if(data is null)
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
            db.Entry(data).Property(x => x.user_name).IsModified = true;
            db.Entry(data).Property(x => x.user_phone).IsModified = true;
            db.Entry(data).Property(x => x.user_mail).IsModified = true;
            db.SaveChanges();
        }
        return;
    }
}