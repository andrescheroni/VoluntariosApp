using MvvmHelpers;
using System;
using VoluntariosApp.Models;

namespace VoluntariosApp.ViewModels
{
    public class PostulacionViewModel : BaseViewModel
    {
        public int PostulacionID { get; set; }
        public int OportunidadID { get; set; }
        public Oportunidad Oportunidad { get; set; }
        public string OportunidadActividad { get; set; }
        public string OportunidadDescripcion { get; set; }
        public string OportunidadRol { get; set; }
        public int OportunidadEntornoID { get; set; }
        public string OportunidadEntornoTipoEntorno
        { get { return App.GetEntornoByID(OportunidadEntornoID); } }
        public int OportunidadIntensidadID { get; set; }
        public string OportunidadIntensidadTipoIntensidad
        { get { return App.GetIntensidadByID(OportunidadIntensidadID); } }
        public int OportunidadSocialID { get; set; }
        public string OportunidadSocialTipoSocial
        { get { return App.GetSocialByID(OportunidadSocialID); } }
        public DateTime OportunidadFechaInicio { get; set; }
        public DateTime OportunidadFechaFin { get; set; }
        public int OportunidadLocalidadID { get; set; }
        public string OportunidadLocalidadTipoLocalidad
        { get { return App.GetLocalidadByID(OportunidadLocalidadID); } }
        public int OportunidadProvinciaID { get; set; }
        public string OportunidadProvinciaNombreProvincia 
        { get { return App.GetProvinciaByID(OportunidadProvinciaID); } }
        public int OportunidadPaisID { get; set; }
        public string OportunidadPaisNombrePais
        { get { return App.GetPaisByID(OportunidadPaisID); } }
        public bool OportunidadBaja { get; set; }
        public int? VoluntarioID { get; set; }
        public string VoluntarioNombre { get; set; }
        public string VoluntarioApellido { get; set; }        
        public byte[] VoluntarioImagen { get; set; }
        public string VoluntarioNombreCompleto => $"{VoluntarioNombre} {VoluntarioApellido}";
        public int VoluntarioLocalidadID { get; set; }
        public string VoluntarioLocalidadTipoLocalidad
        { get { return App.GetLocalidadByID(VoluntarioLocalidadID); } }
        public int VoluntarioProvinciaID { get; set; }
        public string VoluntarioProvinciaNombreProvincia 
        { get { return App.GetProvinciaByID(VoluntarioProvinciaID); } }
        public int VoluntarioPaisID { get; set; }
        public string VoluntarioPaisNombrePais
        { get { return App.GetPaisByID(VoluntarioPaisID); } }
        public int VoluntarioEdad
        { get { return CalcularEdad(VoluntarioFechaNacimiento); } }
        public string VoluntarioMail { get; set; }
        public string VoluntarioTelefono { get; set; }        
        public DateTime VoluntarioFechaNacimiento { get; set; }
        public DateTime FechaPostulacion { get; set; }
        public int EstadopostulacionID { get; set; }
        public string EstadopostulacionEstado { get; set; }        
        public string Mensaje { get; set; }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            var hoy = DateTime.Today;
            int edad = hoy.Year - fechaNacimiento.Year;

            // Año bisiesto
            if (fechaNacimiento > hoy.AddYears(-edad))
                edad--;

            return edad;
        }

    }
}
