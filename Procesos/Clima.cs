
using System.Text.Json;
using System.Text.RegularExpressions;


public class clima{

    public class coordenada{
        public double longitud{get;set;}=0;
        public double latitud{get;set;}=0;
    }
    
    public static async Task<ServerResult> obtenerclima(coordenada coor)
{
    try
    {
        coor.latitud = Math.Round(coor.latitud, 2);
        coor.longitud = Math.Round(coor.longitud, 2);

        if (!EsCoordenadaValida(coor))
        {
            return new ServerResult(false, "Coordenadas inválidas");
        }

        var url = $"http://api.weatherunlocked.com/api/current/{coor.latitud},{coor.longitud}?app_id=be08696d&app_key=7c8197ba308d64be6204b9caf57e6493";
        using (var cliente = new HttpClient())
        {
            var respuesta = await cliente.GetAsync(url);
            respuesta.EnsureSuccessStatusCode();
            var json = await respuesta.Content.ReadAsStringAsync();

            var datosClima = JsonSerializer.Deserialize<DatosClima>(json);
            var resultadoFormateado = FormatearResultado(datosClima);

            return new ServerResult(true, resultadoFormateado);
        }
    }
    catch (Exception ex)
    {
        return new ServerResult(false, $"Error: {ex.Message}");
    }
}

private static bool EsCoordenadaValida(coordenada coor)
{
    return coor.latitud >= -90 && coor.latitud <= 90 && coor.longitud >= -180 && coor.longitud <= 180;
}

private static string FormatearResultado(DatosClima datos)
{
    return $"Latitud: {datos.lat} " +
           $"Longitud: {datos.lon} " +
           $"Altitud: {datos.alt_m} m ({datos.alt_ft} ft) " +
           $"Descripción del clima: {datos.wx_desc} " +
           $"Temperatura: {datos.temp_c} °C ({datos.temp_f} °F) " +
           $"Sensación térmica: {datos.feelslike_c} °C ({datos.feelslike_f} °F) " +
           $"Humedad: {datos.humid_pct}% " +
           $"Velocidad del viento: {datos.windspd_mph} mph ({datos.windspd_kmh} km/h) " +
           $"Dirección del viento: {datos.winddir_compass} ({datos.winddir_deg}°) " +
           $"Cobertura de nubes: {datos.cloudtotal_pct}% " +
           $"Visibilidad: {datos.vis_km} km ({datos.vis_mi} mi) " +
           $"Presión: {datos.slp_mb} mb ({datos.slp_in} inHg) " +
           $"Punto de rocío: {datos.dewpoint_c} °C ({datos.dewpoint_f} °F)";
}

public class DatosClima
{
    public double lat { get; set; }
    public double lon { get; set; }
    public double alt_m { get; set; }
    public double alt_ft { get; set; }
    public string wx_desc { get; set; }
    public int wx_code { get; set; }
    public string wx_icon { get; set; }
    public double temp_c { get; set; }
    public double temp_f { get; set; }
    public double feelslike_c { get; set; }
    public double feelslike_f { get; set; }
    public double humid_pct { get; set; }
    public double windspd_mph { get; set; }
    public double windspd_kmh { get; set; }
    public double windspd_kts { get; set; }
    public double windspd_ms { get; set; }
    public double winddir_deg { get; set; }
    public string winddir_compass { get; set; }
    public double cloudtotal_pct { get; set; }
    public double vis_km { get; set; }
    public double vis_mi { get; set; }
    public string vis_desc { get; set; }
    public double slp_mb { get; set; }
    public double slp_in { get; set; }
    public double dewpoint_c { get; set; }
    public double dewpoint_f { get; set; }
}


}