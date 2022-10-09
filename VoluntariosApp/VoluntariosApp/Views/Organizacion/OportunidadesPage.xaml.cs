using System;
using System.Collections.Generic;
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
    public partial class OportunidadesPage : ContentPage    {

        
        public OportunidadesPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            await RefreshOportunidades();
            
            EntornoPicker.ItemsSource = App.EntornoList;
            IntensidadPicker.ItemsSource = App.IntensidadList;
            SocialPicker.ItemsSource = App.SocialList;
            PaisPicker.ItemsSource = App.PaisList;        
        }

        async Task RefreshOportunidades()
        {
            listaOportunidadesActivas.ItemsSource = await App.VoluntarioManager.GetOportunidadAllAsync(App.EntornoFilter, App.IntensidadFilter, App.SocialFilter, App.PaisFilter, App.ProvinciaFilter, App.LocalidadFilter);
        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new OportunidadPage
            {
                BindingContext = e.CurrentSelection.FirstOrDefault() as OportunidadViewModel
            });
        }

        async void OnApplyButtonClicked(object sender, EventArgs e)
        {
            if (EntornoPicker.SelectedIndex != -1)
            {
                App.EntornoFilter = ((Entorno)EntornoPicker.SelectedItem).EntornoID;
            }
            else
            {
                App.EntornoFilter = -1;
            }

            if (IntensidadPicker.SelectedIndex != -1)
            {
                App.IntensidadFilter = ((Intensidad)IntensidadPicker.SelectedItem).IntensidadID;
            }
            else
            {
                App.IntensidadFilter = -1;
            }

            if (SocialPicker.SelectedIndex != -1)
            {
                App.SocialFilter = ((Social)SocialPicker.SelectedItem).SocialID;
            }
            else
            {
                App.SocialFilter = -1;
            }

            if (PaisPicker.SelectedIndex != -1)
            {
                App.PaisFilter = ((Pais)PaisPicker.SelectedItem).PaisID;
            }
            else
            {
                App.PaisFilter = -1;
            }

            if (ProvinciaPicker.SelectedIndex != -1)
            {
                App.ProvinciaFilter = ((Provincia)ProvinciaPicker.SelectedItem).ProvinciaID;
            }
            else
            {
                App.ProvinciaFilter = -1;
            }

            if (LocalidadPicker.SelectedIndex != -1)
            {
                App.LocalidadFilter = ((Localidad)LocalidadPicker.SelectedItem).LocalidadID;
            } else
            {
                App.LocalidadFilter = -1;
            }

            await RefreshOportunidades();
            await Navigation.PopAsync();
        }

        void OnClearButtonClicked(object sender, EventArgs e)
        {              
            EntornoPicker.SelectedIndex = -1;            
            IntensidadPicker.SelectedIndex = -1;
            SocialPicker.SelectedIndex = -1;
            PaisPicker.SelectedIndex = -1;
            ProvinciaPicker.SelectedIndex = -1;
            LocalidadPicker.SelectedIndex = -1;            
        }

        void OnSelectedPais(object sender, EventArgs e)
        {
            if (PaisPicker.SelectedIndex != -1)
            {
                int id = ((Pais)PaisPicker.SelectedItem).PaisID;
                ProvinciaPicker.ItemsSource = App.GetProvinciaByPaisID(id);
            }
        }

        void OnSelectedProvincia(object sender, EventArgs e)
        {
            if (ProvinciaPicker.SelectedIndex != -1)
            {
                int id = ((Provincia)ProvinciaPicker.SelectedItem).ProvinciaID;
                LocalidadPicker.ItemsSource = App.GetLocalidadByProvinciaID(id);                
            }            
        }

        private void OnFilterItemClicked(object sender, Xamarin.CommunityToolkit.UI.Views.TabTappedEventArgs e)
        {
            PopulatePickers();
        }

        void PopulatePickers()
        {            
            EntornoPicker.ItemsSource = App.EntornoList;            

            foreach (var entorno in App.EntornoList.Select((value, i) => new { i, value }))
            {
                var value = entorno.value;
                var index = entorno.i;

                if (value.EntornoID == App.EntornoFilter)
                {
                    EntornoPicker.SelectedIndex = index;
                }
            }

            IntensidadPicker.ItemsSource = App.IntensidadList;

            foreach (var intensidad in App.IntensidadList.Select((value, i) => new { i, value }))
            {
                var value = intensidad.value;
                var index = intensidad.i;

                if (value.IntensidadID == App.IntensidadFilter)
                {
                    IntensidadPicker.SelectedIndex = index;
                }
            }

            SocialPicker.ItemsSource = App.SocialList;

            foreach (var social in App.SocialList.Select((value, i) => new { i, value }))
            {
                var value = social.value;
                var index = social.i;

                if (value.SocialID == App.SocialFilter)
                {
                    SocialPicker.SelectedIndex = index;
                }
            }

            PaisPicker.ItemsSource = App.PaisList;

            foreach (var pais in App.PaisList.Select((value, i) => new { i, value }))
            {
                var value = pais.value;
                var index = pais.i;

                if (value.PaisID == App.PaisFilter)
                {
                    PaisPicker.SelectedIndex = index;
                }
            }

            var provincias = App.GetProvinciaByPaisID(App.PaisFilter);
            //ProvinciaPicker.ItemsSource = provincias;

            foreach (var provincia in provincias.Select((value, i) => new { i, value }))
            {
                var value = provincia.value;
                var index = provincia.i;

                if (value.ProvinciaID == App.ProvinciaFilter)
                {
                    ProvinciaPicker.SelectedIndex = index;
                }
            }

            var localidades = App.GetLocalidadByProvinciaID(App.LocalidadFilter);
            //LocalidadPicker.ItemsSource = localidades;

            foreach (var localidad in localidades.Select((value, i) => new { i, value }))
            {
                var value = localidad.value;
                var index = localidad.i;

                if (value.LocalidadID == App.LocalidadFilter)
                {
                    LocalidadPicker.SelectedIndex = index;
                }
            }

        }


    }
}