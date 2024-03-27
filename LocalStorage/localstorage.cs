using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;

namespace localstorage
{

    public class localMemory<T>{
    
        public localMemory(){}
        public async Task AddLocalStorageAsync(LocalStorageOfUser<T> memory){

            using FileStream fs = new FileStream($"/home/user/Source/test_drive/Application2/LocalStorage/localstorage"+memory.key+".json", FileMode.OpenOrCreate);
            string json =JsonSerializer.Serialize(memory);
            byte[] buffer = Encoding.Default.GetBytes(json);
            await fs.WriteAsync(buffer, 0, buffer.Length);

            await fs.DisposeAsync();
            fs.Close();
            
        }
        public async Task<string> TakeValue(string KEY){
            
        
            try{

                using(FileStream fs = new FileStream($"/home/user/Source/test_drive/Application2/LocalStorage/localstorage"+KEY+".json", FileMode.Open)){
                    LocalStorageOfUser<T>? lm = await JsonSerializer.DeserializeAsync<LocalStorageOfUser<T>>(fs);
                    fs?.DisposeAsync();
                    fs?.Close();
                    return lm==null?"0":Convert.ToString(lm.value);
                }
                
                


            }
            catch{
                return "0";

                }
            
        
            
        }

        public async Task<string> DeleteDatajson(string memory){
            using FileStream fs = new FileStream($"/home/user/Source/test_drive/Application2/LocalStorage/localstorage"+memory+".json", FileMode.Create);
            return "0";
        }

       

    }
    public class LocalStorageOfUser<T>{

        [JsonInclude]
        public string key;
        [JsonInclude]
        public T value;

        public LocalStorageOfUser(string KEY, T VALUE){
            key =KEY;
            value = VALUE;

        }

    }
}