

using System.Runtime.CompilerServices;
using System.IO;
using System.Text.Json;

class usuario{

    public string cédula{get;set;}= string.Empty;
    public string nombre{get;set;}=string.Empty;
    public string apellido{get;set;}=string.Empty;
    public string teléfono{get;set;}=string.Empty;
    public string correo{get;set;}=string.Empty;
    public string clave{get;set;}=string.Empty;
    
}

class incidencia{
    public string Pasaporte{get;set;}= string.Empty;
    public string nombre{get;set;}=string.Empty;
    public string apellido{get;set;}=string.Empty;
    public string WhatsApp{get;set;}=string.Empty;
    public string fecha{get;set;}=string.Empty;
    public double longitud{get;set;}=0;
    public double latitud{get;set;}=0;
    public string agenteID{get;set;}=string.Empty;
}

class credencial{
    public string cédula_o_correo{get;set;}= string.Empty;
    public string clave{get;set;}= string.Empty;
}

class gestion
{
    public static ServerResult reggistroIncidencia(incidencia inc){

        if(!Directory.Exists("Incidencias")){

            Directory.CreateDirectory("Incidencias");
        }

        var miid= Guid.NewGuid().ToString();

        var archivo= $"Incidencias/{miid}.json";

        var json = JsonSerializer.Serialize(inc);

        File.WriteAllText(archivo,json);

        return new ServerResult(true, "Incidenca registrada", miid);
    }

    public static ServerResult login(credencial cred)
{
    if (cred.cédula_o_correo.Length == 0)
    {
        return new ServerResult(false, "Debe ingresar su cédula o correo");
    }

    if (cred.clave.Length < 4)
    {
        return new ServerResult(false, "Clave inválida");
    }
    
    var archivos = Directory.GetFiles("usuarios", "*.json");
    usuario user = null;

    foreach (var archivo in archivos)
    {
        var usuarioExistente = JsonSerializer.Deserialize<usuario>(File.ReadAllText(archivo));
        if (usuarioExistente.cédula == cred.cédula_o_correo || usuarioExistente.correo == cred.cédula_o_correo)
        {
            user = usuarioExistente;
            break;
        }
    }

    if (user == null)
    {
        return new ServerResult(false, "Usuario no encontrado");
    }

    if (user.clave != cred.clave)
    {
        return new ServerResult(false, "Clave incorrecta");
    }

    user.clave = "****";

    return new ServerResult(true, "Sesión iniciada", user);
}


    public static ServerResult registro(usuario usuario)
    {
        List<string> errores = new List<string>();

        if (usuario.cédula.Length != 11)
        {
            errores.Add("La cédula debe tener 11 dígitos");
        }

        if (usuario.nombre.Length == 0)
        {
            errores.Add("El campo nombre es obligatorio");
        }

        if (usuario.clave.Length < 4)
        {
            errores.Add("La clave debe tener por lo menos 4 dígitos");
        }

        if (errores.Count > 0)
        {
            Console.WriteLine("Errores en el registro");
            foreach (var x in errores)
            {
                Console.WriteLine(x);
            }
            return new ServerResult(false, "Errores en el registro", errores);
        }


        if (!Directory.Exists("usuarios"))
        {
            Directory.CreateDirectory("usuarios");
        }
        
        var archivos = Directory.GetFiles("usuarios", "*.json");


        foreach (var archivo in archivos)
        {
            var usuarioExistente = JsonSerializer.Deserialize<usuario>(File.ReadAllText(archivo));
            if (usuarioExistente.cédula == usuario.cédula)
            {
                return new ServerResult(false, "El usuario ya existe");
            }
            if (usuarioExistente.correo == usuario.correo)
            {
                return new ServerResult(false, "El usuario ya existe");
            }
        }

        var archivoCed = $"usuarios/{usuario.cédula}.json";
        var json = JsonSerializer.Serialize(usuario);
        File.WriteAllText(archivoCed, json);

        return new ServerResult(true, "Usuario registrado con éxito");
    }
}
