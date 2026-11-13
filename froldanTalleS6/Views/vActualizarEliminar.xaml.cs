using System.Net;
using froldanTalleS6.Models;

namespace froldanTalleS6.Views;

public partial class vActualizarEliminar : ContentPage
{
    private const string Url = "http://192.168.2.108/moviles/post.php";

    public vActualizarEliminar(Estudiante datos)
    {
        InitializeComponent();
        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre;
        txtapellido.Text = datos.apellido;
        txtEdad.Text = datos.edad.ToString();
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            string urlPut = Url + "?codigo=" + txtCodigo.Text
                          + "&nombre=" + Uri.EscapeDataString(txtNombre.Text)
                          + "&apellido=" + Uri.EscapeDataString(txtapellido.Text)
                          + "&edad=" + txtEdad.Text;
            cliente.UploadValues(urlPut, "PUT", parametros);

            await DisplayAlertAsync("Mensaje", "Estudiante actualizado correctamente", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Alerta", "Error: " + ex.Message, "OK");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        // Pregunta antes de eliminar: devuelve true si presiona "Sí"
        bool confirmar = await DisplayAlertAsync("Confirmar",
            "¿Seguro que quieres eliminar el registro de " + txtNombre.Text + " " + txtapellido.Text + "?",
            "Sí", "No");

        if (!confirmar)
            return;
        
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            cliente.UploadValues(Url + "?codigo=" + txtCodigo.Text, "DELETE", parametros);

            await DisplayAlertAsync("Mensaje", "Estudiante eliminado correctamente", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Alerta", "Error: " + ex.Message, "OK");
        }
    }
}