//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("VoluntariosApp.Views.Organizacion.PostulacionesPage.xaml", "Views/Organizacion/PostulacionesPage.xaml", typeof(global::VoluntariosApp.Views.Organizacion.PostulacionesPage))]

namespace VoluntariosApp.Views.Organizacion {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\Organizacion\\PostulacionesPage.xaml")]
    public partial class PostulacionesPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.CheckBox pendientesCheckBox;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.CollectionView listaPostulaciones;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(PostulacionesPage));
            pendientesCheckBox = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.CheckBox>(this, "pendientesCheckBox");
            listaPostulaciones = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.CollectionView>(this, "listaPostulaciones");
        }
    }
}