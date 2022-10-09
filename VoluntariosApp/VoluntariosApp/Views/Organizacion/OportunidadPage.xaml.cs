
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VoluntariosApp.Models;
using VoluntariosApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VoluntariosApp.Views.Organizacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OportunidadPage : ContentPage
    {

        readonly int VolID = 1;
        OportunidadViewModel oportunidadItem;
        List<PostulacionViewModel> listadoPostulaciones;

        public OportunidadPage()
        {
            InitializeComponent();
            
        }

        protected async override void OnAppearing()
        {
            oportunidadItem = (OportunidadViewModel)BindingContext;
            await RefreshPostulaciones(oportunidadItem.OportunidadID);

            bool postulado = false;

            foreach (PostulacionViewModel postulacion in listadoPostulaciones)
            {
                if (postulacion.VoluntarioID == VolID)
                {
                    postulado = true;
                }
            }

            if (postulado)
            {
                buttonPostularse.IsEnabled = false;                
            }
        }

        async Task RefreshPostulaciones(int id)
        {
            listadoPostulaciones = await App.VoluntarioManager.GetPostulacionByOportunidadIDAllAsync(id);
        }


        async void OnApplyButtonClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Alerta", "¿Está seguro de que quiere postularse?", "Si", "No");

            if (answer)
            {
                oportunidadItem = (OportunidadViewModel)BindingContext;

                Postulacion postulacionItem = new Postulacion();

                postulacionItem.OportunidadID = oportunidadItem.OportunidadID;
                postulacionItem.VoluntarioID = VolID;
                postulacionItem.FechaPostulacion = DateTime.Today;
                postulacionItem.EstadoPostulacionID = 1;

                await App.VoluntarioManager.SavePostulacionAsync(postulacionItem, true);
                
                await Navigation.PopAsync();
            }

        }


    }
}