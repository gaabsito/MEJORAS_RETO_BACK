public static class DatosCines
{
    // Lista estática de cines predefinidos
    public static List<Cine> Cines { get; set; } = new List<Cine>
    {
        new Cine
        {
            CineId = 1, // Identificador único del cine
            Nombre = "MUELMO Cines Puerto Venecia", // Nombre del cine
            Ubicacion = "Zaragoza", // Ciudad donde se encuentra el cine
            Salas = new List<Sala>
            {
                new Sala
                {
                    SalaId = 1, // Identificador único de la sala
                    Nombre = "Sala 1", // Nombre de la sala
                    CineId = 1, // Relación con el cine al que pertenece
                    Sesiones = GenerarSesionesPorPelicula(1, 1, DateTime.Now.Date.AddDays(1)) // Sesiones programadas dinámicamente
                },
                new Sala
                {
                    SalaId = 2, // Identificador único de la sala
                    Nombre = "Sala 2",
                    CineId = 1,
                    Sesiones = GenerarSesionesPorPelicula(6, 2, DateTime.Now.Date.AddDays(2))
                },
                new Sala
                {
                    SalaId = 3,
                    Nombre = "Sala 3",
                    CineId = 1,
                    Sesiones = GenerarSesionesPorPelicula(11, 3, DateTime.Now.Date.AddDays(3))
                },
                new Sala
                {
                    SalaId = 4,
                    Nombre = "Sala 4",
                    CineId = 1,
                    Sesiones = GenerarSesionesPorPelicula(16, 4, DateTime.Now.Date.AddDays(4))
                }
            }
        }
    };

    // Método para generar una lista de sesiones por película de forma dinámica
    private static List<Sesion> GenerarSesionesPorPelicula(int inicioSesionId, int salaId, DateTime fechaInicio)
    {
        var sesiones = new List<Sesion>(); // Lista que contendrá las sesiones generadas

        // Genera un rango de películas para la sala
        var peliculas = Enumerable.Range(inicioSesionId, 5); // Suponiendo que hay 5 películas por sala

        // Horarios base para las sesiones: 16:00, 18:30, 21:00
        var horarios = new[] { 16, 18, 21 };

        // Itera sobre las películas generando una sesión para cada una
        foreach (var peliculaId in peliculas)
        {
            var indice = peliculaId - inicioSesionId; // Índice relativo para alternar horarios

            // Añade una nueva sesión a la lista
            sesiones.Add(new Sesion
            {
                SesionId = peliculaId, // El identificador de la sesión coincide con el de la película
                SalaId = salaId, // Relación con la sala correspondiente
                PeliculaId = peliculaId, // Identificador único de la película en la sesión
                FechaDeSesion = fechaInicio.AddDays(indice / 3), // La fecha incrementa cada 3 sesiones

                // Define la hora de inicio alternando entre los horarios base
                HoraDeInicio = new DateTime(
                    fechaInicio.Year,
                    fechaInicio.Month,
                    fechaInicio.Day,
                    horarios[indice % horarios.Length], // Selecciona un horario basado en el índice
                    indice % 2 == 0 ? 0 : 30, // Alterna los minutos entre 00 y 30
                    0
                ),

                // Genera las butacas para la sesión
                Butacas = Enumerable.Range(1, 150).Select(id => new Butaca
                {
                    ButacaId = id, // Identificador único de la butaca
                    SalaId = salaId, // Relación con la sala correspondiente
                    Estado = "Disponible", // Estado inicial de la butaca
                    PrecioButaca = 6.90 // Precio de la butaca
                }).ToList()
            });
        }

        return sesiones; // Devuelve la lista de sesiones generadas
    }
}
