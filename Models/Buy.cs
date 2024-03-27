using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application2.Models;

public class Buy{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id{get;set;}
    
    public Guid IdPerson{get;set;}
      
    public Guid IdSeller{get;set;}

    public Guid IdProduct{get;set;}

    public int cost{get;set;}

}