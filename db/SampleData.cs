using System.Linq;
using Microsoft.EntityFrameworkCore;
using Application2.Models;
namespace db;

public static  class SampleData{
    public static void InitData(Context context){
        if(!context.Categories.Any()){
            context.Categories.Add(new Category(){NameCategory="Одежда", Products= new List<Product>()});
            context.Categories.Add(new Category(){NameCategory="Электроника", Products= new List<Product>()});
            context.Categories.Add(new Category(){NameCategory="Бытовая техника", Products= new List<Product>()});
            context.Categories.Add(new Category(){NameCategory="Товары для дома", Products= new List<Product>()});
            context.SaveChanges();
        }
    }
}