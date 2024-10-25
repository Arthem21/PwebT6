
using System.Text.Json;
using System.Text.RegularExpressions;

class Noticia{

      public class Excerpt
    {
        public string rendered { get; set; }
    }

    public class Reportaje
    {
        public Title title { get; set; }
        public Excerpt excerpt { get; set; }
    }

    public class Title
    {
        public string rendered { get; set; }
    }

    public static async Task<ServerResult> Ejecutar(){
        try{
        var url="https://remolacha.net/wp-json/wp/v2/posts?search=migraci%C3%B3n&_fields=title,excerpt";
        var cliente= new HttpClient();
        var respuesta= await cliente.GetAsync(url);
        var json= await respuesta.Content.ReadAsStringAsync();
        var Reportajes= JsonSerializer.Deserialize<List<Reportaje>>(json);

        var resultado= new List<Dictionary< string , string>>();
        
        foreach (var Reportaje in Reportajes){

            var Titulo = Reportaje.title.rendered;
            var resumen = Reportaje.excerpt.rendered;
            resumen= Regex.Replace(resumen,"<.*?>", string.Empty);
            var dic = new Dictionary<string,string>();
            dic.Add("Titulo", Titulo);
            dic.Add("Resumen", resumen);
            resultado.Add(dic);
        }
        return new ServerResult(true, "Noticias cargadas", resultado);
        }
        catch(Exception ex){
            return new ServerResult(false, ex.Message);
        }
    }
}