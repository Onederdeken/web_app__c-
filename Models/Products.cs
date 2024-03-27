using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Application2.Models;

public class Product{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id{get;set;}
    [Required(ErrorMessage ="Дайте название")]
    [DataType(DataType.Text)]
    public string? Name{get;set;}
    public string? DirectoryPhotoProduct{get;set;}
    [Required(ErrorMessage ="Опишите свой продукт")]
    [StringLength(100, ErrorMessage ="Максимум 100 символов")]
    [DataType(DataType.MultilineText)]
    public string? Description{get;set;}
    [Required]
    [DataType(DataType.MultilineText)]
    public string? CategoryNameCategory{get;set;}

    [ForeignKey(nameof(CategoryNameCategory))]
    [Required]
    public Category? Category{get;set;}
    public List<Person>? Person{get;set;}

    public Seller? Seller{get;set;}
    [ForeignKey(nameof(SellerId))]
    public Guid? SellerId{get;set;}

    [Required(ErrorMessage ="Введите стоимость продукции")]
    [DataType(DataType.Currency)]
    public int cost{get;set;}
    
   
}
