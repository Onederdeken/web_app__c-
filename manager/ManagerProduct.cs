using Application2.Models;

namespace Application2.Manager;
public class ManagerProduct{
    
    public Guid Id{get;set;}
   
    public string? Name{get;set;}
    public string? DirectoryPhotoProduct{get;set;}
    
    public string? Description{get;set;}
    
    public string? CategoryNameCategory{get;set;}

    
    public Category? Category{get;set;}
    public Guid CartId{get;set;}

    
    public Cart? Cart{get;set;}
    
    public List<Person>? Person{get;set;}

    public Seller? Seller{get;set;}
    public Guid SellerId{get;set;}
    public int? cost{get;set;}

}
