using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoluntariosApp.Models;
using VoluntariosApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VoluntariosApp.Views.Organizacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostulacionPage : ContentPage
    {       

        public PostulacionPage()
        {
            InitializeComponent();
        }        

        protected override void OnAppearing()
        {            
            base.OnAppearing();
            var postulacionItem = (PostulacionViewModel)BindingContext;
            if (postulacionItem.EstadopostulacionID != 1)
            {
                buttonCancelar.IsEnabled = false;                
            }                      
        }


        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            var postulacionItem = (PostulacionViewModel)BindingContext;

            bool answer = await DisplayAlert("Alerta", "¿Está seguro de que quiere cancelar esta Postulación? (Esta acción no se puede deshacer)", "Si", "No");

            if (answer)
            {
                await App.VoluntarioManager.DeletePostulacionAsync(postulacionItem.PostulacionID);                    
                await Navigation.PopAsync();
            }
        }

    }
}