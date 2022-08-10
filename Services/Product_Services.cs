using Microsoft.AspNetCore.Mvc;
using _Net_API_Web.Models;

namespace _Net_API_Web.Services;


using Microsoft.EntityFrameworkCore;


public class ProductContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder
        .Entity<Product>(builder =>
    {
        builder.HasKey(x => x.id_product);
        builder.ToTable("product");
    });
}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(@"Host=localhost; Database=test_data; Username=postgres; Password=khoa123");
}




public static class Product_Services {
    static List<Product> List_of_product = new List<Product>();
    static Product_Services()
    {     
    }

    //public static List<MyData> GetList(){
        //var context = new BlogContext();
       // var fBlogs = context.MyDatas.ToList();
      //  return fBlogs;
   // }
  
    public static List<Product> GetAll() {
        var context = new ProductContext();
        var fBlogs = context.Products.ToList();
        return fBlogs;
    }

    public static Product? Get(string id_product) {
        var context = new ProductContext();
        var blog = context.Products
        .Single(b => b.id_product == id_product);
        return blog;
    }

    public static List<Product>? GetbyCategory (string category_product){
        var context = new ProductContext();
        var fBlogs = context.Products.Where(b => b.category_product == category_product).ToList();
        return fBlogs;

    }


   
    public static void Add(Product data)
    {
        if (data is null )
            return;
        data.id_product = data.id_product;
        data.name_product = data.name_product;
        data.price_product = data.price_product;
        data.description_product = data.description_product;
        data.category_product = data.category_product;
        data.imagedata = data.imagedata;


        List_of_product.Add(data);
     

        using (var db = new ProductContext())
        {
            var add_data = data;
            db.Products.Add(data);
            db.SaveChanges();
        }
    }
    public static void Delete(string id_product)
    {
        var data = Get(id_product);
        if(data is null)
            return;
        using (var db = new ProductContext())
        {
            db.Products.Attach(data);
            db.Entry(data).State = EntityState.Deleted;
            db.SaveChanges();
        }
    }
    public static void Update(Product data)
    {   
        var my_data = data;
        if (data is null)
            return;
        using (var db = new ProductContext())
        {
            db.Products.Attach(data);
            db.Entry(data).Property(x => x.name_product).IsModified = true;
            db.Entry(data).Property(x => x.price_product).IsModified = true;
            db.Entry(data).Property(x => x.description_product).IsModified = true;
            db.Entry(data).Property(x => x.category_product).IsModified = true;
            db.Entry(data).Property(x => x.imagedata).IsModified = true;
            db.SaveChanges();
        }
        return;
    }
}