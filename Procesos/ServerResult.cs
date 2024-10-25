public class ServerResult{

    public bool Exito {set; get;}
    public string Mensaje {set; get;}
    public object Datos {set; get;}

    public ServerResult(bool exito, string mensaje, object datos){
        
        this.Exito=exito;
        this.Mensaje=mensaje;
        this.Datos=datos;

    }

    public ServerResult(bool exito, string mensaje){
        
        this.Exito=exito;
        this.Mensaje=mensaje;
        
    }

    public ServerResult(bool exito){
        
        this.Exito=exito;
        
    }

    public ServerResult(){
        
        this.Exito=false;
        
    }

    



}