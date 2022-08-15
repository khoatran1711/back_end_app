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
            builder.HasKey(x => x.IdProduct);
            builder.ToTable("product");
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(@"Host=localhost; Database=test_data; Username=postgres; Password=khoa123");
}




public static class Product_Services
{
    static List<Product> List_of_product = new List<Product>();
    static Product_Services()
    {
    }

    //public static List<MyData> GetList(){
    //var context = new BlogContext();
    // var fBlogs = context.MyDatas.ToList();
    //  return fBlogs;
    // }

    public static List<Product> GetAll()
    {
        var context = new ProductContext();
        var fBlogs = context.Products.ToList();
        return fBlogs;
    }

    public static Product? Get(string idProduct)
    {
        var context = new ProductContext();
        var blog = context.Products
        .Single(b => b.IdProduct == idProduct);
        return blog;
    }

    public static List<Product>? GetbyCategory(string categoryProduct)
    {
        var context = new ProductContext();
        var fBlogs = context.Products.Where(b => b.CategoryProduct == categoryProduct).ToList();
        return fBlogs;

    }



    public static void Add(Product data)
    {
        if (data is null)
            return;
        data.IdProduct = data.IdProduct;
        data.NameProduct = data.NameProduct;
        data.PriceProduct = data.PriceProduct;
        data.DescriptionProduct = data.DescriptionProduct;
        data.CategoryProduct = data.CategoryProduct;
        data.Imagedata = data.Imagedata;


        List_of_product.Add(data);


        using (var db = new ProductContext())
        {
            var add_data = data;
            db.Products.Add(data);
            db.SaveChanges();
        }
    }
    public static void Delete(string idProduct)
    {
        var data = Get(idProduct);
        if (data is null)
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
            db.Entry(data).Property(x => x.NameProduct).IsModified = true;
            db.Entry(data).Property(x => x.PriceProduct).IsModified = true;
            db.Entry(data).Property(x => x.DescriptionProduct).IsModified = true;
            db.Entry(data).Property(x => x.CategoryProduct).IsModified = true;
            db.Entry(data).Property(x => x.Imagedata).IsModified = true;
            db.SaveChanges();
        }
        return;
    }
}