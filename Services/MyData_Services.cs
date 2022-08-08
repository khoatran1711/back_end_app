


using Microsoft.AspNetCore.Mvc;

namespace _Net_API_Web.Services;


using Microsoft.EntityFrameworkCore;

public class BlogContext : DbContext
{
    public DbSet<MyData> MyDatas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder
        .Entity<MyData>(builder =>
    {
        builder.HasKey(x => x.myid);
        builder.ToTable("data_values");
    });
}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(@"Host=localhost; Database=test_data; Username=postgres; Password=khoa123");
}



public static class MyData_Services {
    static List<MyData> List_of_data { get; }
    static int nextId = 3;
    static MyData_Services()
    {
        List_of_data = new List<MyData>
        {
            new MyData { myid = 1, my_name = "Classic Italian" },
            new MyData { myid = 2, my_name = "Veggie" }
        };

        
    }

    //public static List<MyData> GetList(){
        //var context = new BlogContext();
       // var fBlogs = context.MyDatas.ToList();
      //  return fBlogs;
   // }
  
    public static List<MyData> GetAll() {
        var context = new BlogContext();
        var fBlogs = context.MyDatas.ToList();
        return fBlogs;
    }

    public static MyData? Get(int id) {
        var context = new BlogContext();
        var blog = context.MyDatas
        .Single(b => b.myid == id);
        return blog;
    }
    public static void Add(MyData data)
    {
        if (data is null )
            return;
        data.myid = data.myid;
        List_of_data.Add(data);

        using (var db = new BlogContext())
        {
            var add_data = data;
            db.MyDatas.Add(data);
            db.SaveChanges();
        }
    }
    public static void Delete(int id)
    {
        var data = Get(id);
        if(data is null)
            return;
        using (var db = new BlogContext())
        {
            db.MyDatas.Attach(data);
            db.Entry(data).State = EntityState.Deleted;
            db.SaveChanges();
        }
    }
    public static void Update(MyData data)
    {   

        var my_data = data;
        if (data is null)
            return;
        using (var db = new BlogContext())
        {
            db.MyDatas.Attach(data);
            db.Entry(data).Property(x => x.my_name).IsModified = true;
            db.SaveChanges();
        }
        return;
    }
}