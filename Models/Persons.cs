using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application2.Models;
public class Person{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id{get;set;}

    
    [MaxLength(16, ErrorMessage ="максимальная длина никнейма 16 символов")]
    [MinLength(6,ErrorMessage ="Минимальная длина 6 символов")]
    [Required(ErrorMessage ="Вы не ввели свой ник")]
    [UIHint("String")]
    [DataType(DataType.Text)]
    [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage ="принимаются любые латинские символы")]
    public string? NickName{get;set;}


    [Required(ErrorMessage ="ВЫ не ввели пароль")]
    [StringLength(25, ErrorMessage ="максимальная длина пароля 25 символов")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$", ErrorMessage ="Первый сивол атинский верхнего регистра, остальные символы латинские любого регистра, а так же допускается ввод специальных знаков")]
    [UIHint("Password")]
    public string? Password{get;set;}



    public string? DirectoryPhotoUser{get;set;}

    [Required(ErrorMessage ="Вы не ввели почту")]
    [DataType(DataType.EmailAddress)]
    public string? Email{get;set;}

    public List<Product>? Products{get;set;}
    
    [Required(ErrorMessage ="Вы не ввели номер телефона")]
    [DataType(DataType.PhoneNumber)]
    //[DisplayFormat(DataFormatString ="{+7-yyy-nnn-mm-hh}")]
    [UIHint("String")]
    public string? Telephone{get;set;}
    [DataType(DataType.Currency)]
    public int CMoney{get;set;}


    


}

