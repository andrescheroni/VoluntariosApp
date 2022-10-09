﻿using System.Collections.Generic;

namespace VoluntariosApp.Models
{
    public class EstadoPostulacion
    {
        public int EstadoPostulacionID { get; set; }
        public string Estado { get; set; }

        //Propiedades de navegación	
        public ICollection<Postulacion> Postulaciones { get; set; }
    }
}


