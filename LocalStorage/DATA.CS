using localstorage;
using System;
using System.IO;
using System.Text.Json;
namespace Application2.DATA;

static public class data{
    static public string ID {get;set;}= "0";

    static  public string role{get;set;}
    /*

    static public async Task inizialisation_user_id(string key){
        localMemory<Guid> storage = new localMemory<Guid>();
        ID =   await storage.TakeValue(key);   
        
    }
    static public async Task inizialisation_user_role(string key){
        localMemory<string> storage = new localMemory<string>();
        role =   await storage.TakeValue(key);   
        
    }
    static public async Task delete_user_id(string key){
        localMemory<Guid> storage = new localMemory<Guid>();
        ID = await storage.DeleteDatajson(key);
    }
    static public async Task delete_user_role(string key){
        localMemory<string> storage = new localMemory<string>();
        role = await storage.DeleteDatajson(key);
    }*/

}