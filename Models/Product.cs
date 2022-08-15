namespace _Net_API_Web.Models;
using System.ComponentModel.DataAnnotations.Schema;
public class Product
{
    [Column("id_product")]
    public string? IdProduct { get; set; }
    [Column("name_product")]
    public string? NameProduct { get; set; }
    [Column("price_product")]
    public int PriceProduct { get; set; }
    [Column("description_product")]
    public string? DescriptionProduct { get; set; }
    [Column("category_product")]
    public string? CategoryProduct { get; set; }
    [Column("imagedata")]
    public string? Imagedata { get; set; }

}