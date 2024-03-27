using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application2.Models;

public class Cart{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public Guid Id{get;set;}
    
    public Guid PersonId{get;set;}
    

    [ForeignKey(nameof(PersonId))]

    
    public Person? Person{get;set;}


   
    

   
    public List<Product>? Products{get;set;}

}