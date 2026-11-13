using System.Net;

namespace froldanTalleS6.Views;

public partial class vAgregarEstudiante : ContentPage
{
    public vAgregarEstudiante()
    {
        InitializeComponent();
    }

    // POST: envía los datos al servicio PHP
    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("nombre", txtNombre.Text);
            parametros.Add("apellido", txtApellido.Text);
            parametros.Add("edad", txtEdad.Text);
            cliente.UploadValues("http://192.168.2.108/moviles/post.php", "POST", parametros);

            await DisplayAlertAsync("Mensaje", "Estudiante agregado correctamente", "OK");
            await Navigation.PopAsync();   // regresa a la lista
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Alerta", "Error: " + ex.Message, "OK");
        }
    }

    private async void btnCancelar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}