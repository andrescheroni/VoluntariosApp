using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VoluntariosApp.Data;
using VoluntariosApp.Models;
using VoluntariosApp.Views.Common;
using Xamarin.Forms;

namespace VoluntariosApp
{
    public partial class App : Application
    {
        public static VoluntarioAppManager VoluntarioManager { get; private set; }
        private event EventHandler onStart = delegate { };
        public static List<Entorno> EntornoList { get; private set; }
        public static List<Intensidad> IntensidadList { get; private set; }
        public static List<Social> SocialList { get; private set; }
        public static List<Localidad> LocalidadList { get; private set; }
        public static List<Provincia> ProvinciaList { get; private set; }
        public static List<Pais> PaisList { get; private set; }
        public static List<EstadoPostulacion> EstadoPostulacionList { get; private set; }
        public static int EntornoFilter { get; set; }
        public static int IntensidadFilter { get; set; }
        public static int SocialFilter { get; set; }
        public static int PaisFilter { get; set; }
        public static int ProvinciaFilter { get; set; }
        public static int LocalidadFilter { get; set; }


        public App()
        {
            InitializeComponent();
            VoluntarioManager = new VoluntarioAppManager(new RestService());
            //MainPage = new LoadingPage();
        }

        protected override void OnStart()
        {
            onStart += HandleStart; //subscribe
            onStart(this, EventArgs.Empty); //raise event            
            MainPage = new AppShell();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void HandleStart(object sender, EventArgs args)
        {
            onStart -= HandleStart; //unsubscribe

            // Initialise the application
            Task.Run(async () => { await RefreshLists(); }).Wait();
            EntornoFilter = -1;
            IntensidadFilter = -1;
            SocialFilter = -1;
            PaisFilter = -1;
            ProvinciaFilter = -1;
            LocalidadFilter = -1;

            base.OnStart();
        }

        public async Task RefreshLists()
        {
            LocalidadList = await VoluntarioManager.GetLocalidadAllAsync();
            EntornoList = await VoluntarioManager.GetEntornoAllAsync();
            IntensidadList = await VoluntarioManager.GetIntensidadAllAsync();
            SocialList = await VoluntarioManager.GetSocialAllAsync();
            ProvinciaList = await VoluntarioManager.GetProvinciaAllAsync();
            PaisList = await VoluntarioManager.GetPaisAllAsync();
        }

        public static List<Provincia> GetProvinciaByPaisID(int id)
        {
            List<Provincia> ProvinciaListFiltrada = new List<Provincia>();

            foreach (Provincia provincia in ProvinciaList)
            {
                if (provincia.PaisID == id)
                {
                    ProvinciaListFiltrada.Add(provincia);
                }
            }

            return ProvinciaListFiltrada;
        }

        public static List<Localidad> GetLocalidadByProvinciaID(int id)
        {
            List<Localidad> LocalidadListFiltrada = new List<Localidad>();

            foreach (Localidad localidad in LocalidadList)
            {
                if (localidad.ProvinciaID == id)
                {
                    LocalidadListFiltrada.Add(localidad);
                }
            }

            return LocalidadListFiltrada;
        }

        public static string GetPaisByID(int id)
        {
            string nombrePais = null;

            foreach (var pais in PaisList)
            {
                if (pais.PaisID == id)
                {
                    nombrePais = pais.NombrePais;
                }
            }

            return nombrePais;
        }

        public static string GetLocalidadByID(int id)
        {
            string tipoLocalidad = null;

            foreach (var localidad in LocalidadList)
            {
                if (localidad.LocalidadID == id)
                {
                    tipoLocalidad = localidad.TipoLocalidad;
                }
            }

            return tipoLocalidad;
        }

        public static string GetProvinciaByID(int id)
        {
            string nombreProvincia = null;

            foreach (var provincia in ProvinciaList)
            {
                if (provincia.ProvinciaID == id)
                {
                    nombreProvincia = provincia.NombreProvincia;
                }
            }

            return nombreProvincia;
        }

        public static string GetEntornoByID(int id)
        {
            string tipoEntorno = null;

            foreach (var entorno in EntornoList)
            {
                if (entorno.EntornoID == id)
                {
                    tipoEntorno = entorno.TipoEntorno;
                }
            }

            return tipoEntorno;
        }

        public static string GetIntensidadByID(int id)
        {
            string tipoIntensidad = null;

            foreach (var intensidad in IntensidadList)
            {
                if (intensidad.IntensidadID == id)
                {
                    tipoIntensidad = intensidad.TipoIntensidad;
                }
            }

            return tipoIntensidad;
        }

        public static string GetSocialByID(int id)
        {
            string tipoSocial = null;

            foreach (var social in SocialList)
            {
                if (social.SocialID == id)
                {
                    tipoSocial = social.TipoSocial;
                }
            }

            return tipoSocial;
        }
    }
}
