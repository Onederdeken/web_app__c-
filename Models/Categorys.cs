using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Application2.Models;

public class Category{
    [Key]
    [DataType(DataType.Text)]
    public string? NameCategory{get;set;}
    
    public List<Product>? Products{get;set;}

}