
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoluntariosApp.Models;
using VoluntariosApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VoluntariosApp.Views.Organizacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostulacionesPage : ContentPage
    {
        readonly int VolID = 1;
        readonly OportunidadViewModel OportunidadIndividual;

        public PostulacionesPage()
        {
            InitializeComponent();
        }

        public PostulacionesPage(OportunidadViewModel oportunidad)
        {
            InitializeComponent();
            OportunidadIndividual = oportunidad;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //await RefreshOportunidades();            
            await RefreshPostulaciones(VolID);            
        }

        async Task RefreshPostulaciones(int id)
        {           
            if (id != -1) {
                
                List<PostulacionViewModel> listadoPostulaciones = await App.VoluntarioManager.GetPostulacionByVoluntarioIDAllAsync(id);
                List<PostulacionViewModel> listadoPostulacionesFiltrado = new List<PostulacionViewModel>();

                foreach (PostulacionViewModel postulacion in listadoPostulaciones)
                {               
                    switch (postulacion.EstadopostulacionID)
                    {
                        case 1:
                            postulacion.EstadopostulacionEstado = "Pendiente";                                                        
                            listadoPostulacionesFiltrado.Add(postulacion);                            
                            break;
                        case 2:
                            postulacion.EstadopostulacionEstado = "Aceptada";
                            if (!pendientesCheckBox.IsChecked)
                            {
                                listadoPostulacionesFiltrado.Add(postulacion);
                            }
                            break;
                        case 3:
                            postulacion.EstadopostulacionEstado = "Rechazada";
                            if (!pendientesCheckBox.IsChecked)
                            {
                                listadoPostulacionesFiltrado.Add(postulacion);
                            }
                            break;
                    }
                }
                    listaPostulaciones.ItemsSource = listadoPostulacionesFiltrado;
            }            
        }

        //async Task RefreshOportunidades()
        //{
        //    List<OportunidadViewModel> listadoOportunidades = new List<OportunidadViewModel>();

        //    if (OportunidadIndividual == null)
        //    {
        //        listadoOportunidades = await App.VoluntarioManager.GetOportunidadByOrganizacionIDAsync(OrgID, true);                
        //    }
        //    else
        //    {
        //        listadoOportunidades.Add(OportunidadIndividual);
        //    }
            
        //    oportunidadesPicker.ItemsSource = listadoOportunidades;
            
        //    if (listadoOportunidades.Count != 0)
        //    {
        //        oportunidadesPicker.SelectedIndex = 0;
        //    }

        //}

        private async void OnPendientesCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            await RefreshPostulaciones(VolID);            
        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new PostulacionPage
            {                
                BindingContext = e.CurrentSelection.SingleOrDefault() as PostulacionViewModel
            });
        }
    }
}