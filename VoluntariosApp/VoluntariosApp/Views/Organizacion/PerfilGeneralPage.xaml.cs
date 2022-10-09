using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VoluntariosApp.Models;
using VoluntariosApp.Services;
using VoluntariosApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VoluntariosApp.Views.Organizacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilGeneralPage : ContentPage
    {
        byte[] imageHolder;
        readonly int VolID = 1;
        public PerfilGeneralPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            VoluntarioViewModel voluntarioItem = await App.VoluntarioManager.GetVoluntarioByIDAsync(VolID);            

            BindingContext = voluntarioItem;

            //imageHolder = voluntarioItem.Imagen;

            PopulatePickers(voluntarioItem);
        }

        void PopulatePickers(VoluntarioViewModel voluntarioItem)
        {
                        
            PaisPicker.ItemsSource = App.PaisList;

            foreach (var pais in App.PaisList.Select((value, i) => new { i, value }))
            {
                var value = pais.value;
                var index = pais.i;

                if (value.PaisID == voluntarioItem.PaisID)
                {
                    PaisPicker.SelectedIndex = index;
                }
            }
                        
            var provincias = App.GetProvinciaByPaisID(voluntarioItem.PaisID);
            
            foreach (var provincia in provincias.Select((value, i) => new { i, value }))
            {
                var value = provincia.value;
                var index = provincia.i;

                if (value.ProvinciaID == voluntarioItem.ProvinciaID)
                {
                    ProvinciaPicker.SelectedIndex = index;
                }
            }

            var localidades = App.GetLocalidadByProvinciaID(voluntarioItem.ProvinciaID);
            
            foreach (var localidad in localidades.Select((value, i) => new { i, value }))
            {
                var value = localidad.value;
                var index = localidad.i;

                if (value.LocalidadID == voluntarioItem.LocalidadID)
                {
                    LocalidadPicker.SelectedIndex = index;
                }
            }
        }

        void OnSelectedPais(object sender, EventArgs e)
        {
            if (PaisPicker.SelectedIndex != -1)
            {
                int id = ((Pais)PaisPicker.SelectedItem).PaisID;
                ProvinciaPicker.ItemsSource = App.GetProvinciaByPaisID(id); //await App.VoluntarioManager.GetProvinciaByPaisAllAsync(id);
            }
        }

        void OnSelectedProvincia(object sender, EventArgs e)
        {
            if (ProvinciaPicker.SelectedIndex != -1)
            {
                int id = ((Provincia)ProvinciaPicker.SelectedItem).ProvinciaID;
                LocalidadPicker.ItemsSource = App.GetLocalidadByProvinciaID(id); //await App.VoluntarioManager.GetLocalidadByProvinciaAllAsync(id);
            }
        }

        async void OnImageTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;

            Stream imageStream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();

            if (imageStream != null)
            {
                imageHolder = ReadFully(imageStream);
                imagen.Source = ImageSource.FromStream(() => new MemoryStream(imageHolder));
            }

            (sender as Button).IsEnabled = true;
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }

        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            bool status;

            var currentContext = (VoluntarioViewModel)BindingContext;

            var voluntarioItem = new Models.Voluntario
            {
                VolutnarioID = currentContext.VolutnarioID,                
                Nombre = currentContext.Nombre,
                Apellido = currentContext.Apellido,                
                FechaNacimiento = currentContext.FechaNacimiento,
                DNI = currentContext.DNI,
                Direccion = currentContext.Direccion,                
                LocalidadID = currentContext.LocalidadID,
                ProvinciaID = currentContext.ProvinciaID,
                PaisID = currentContext.PaisID,
                EntornoID = currentContext.EntornoID,
                IntensidadID = currentContext.IntensidadID,
                SocialID = currentContext.SocialID,
                Mail = currentContext.Mail,
                PasswordHash = currentContext.PasswordHash,
                Telefono = currentContext.Telefono,                
                Verificacion = currentContext.Verificacion,
                FechaRegistro = currentContext.FechaRegistro,
                FechaVerificacion = currentContext.FechaVerificacion
            };

            if (imageHolder == null)
            {
                voluntarioItem.Imagen = null;
            }
            else
            {
                voluntarioItem.Imagen = imageHolder;
            }
            
            status = await App.VoluntarioManager.SaveVoluntarioAsync(voluntarioItem, false);

            if (status != true)
            {
                await DisplayAlert("Error", "Ocurrió un error al grabar", "OK");
            } else
            {
                await DisplayAlert("Exito", "Las modificaciones se grabaron con éxito", "OK");
            }            

            await Navigation.PopAsync();            
        }
               


        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void OnChangePasswordButtonClicked(object sender, EventArgs e)
        {
            var currentContext = (VoluntarioViewModel)BindingContext;

            string currentPassword = await DisplayPromptAsync("Cambiar password", "Ingrese su password actual");

            if (PasswordHasher.HashPassword(currentPassword) != currentContext.PasswordHash)
            {
                await DisplayAlert("Error", "El password no es correcto", "OK");
                return;
            }

            string newPassword = await DisplayPromptAsync("Cambiar password", "Ingrese su nuevo password");

            string newPasswordCheck = await DisplayPromptAsync("Cambiar password", "Ingrese nuevamente su nuevo password");

            if (newPassword != newPasswordCheck)
            {
                await DisplayAlert("Error", "Los passwords no coinciden", "OK");
                return;
            }

            currentContext.PasswordHash = PasswordHasher.HashPassword(newPassword);
        }
    }
}