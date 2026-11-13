using froldanTalleS6.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace froldanTalleS6.Views;

public partial class vEstudiante : ContentPage
{
	private const string Url = "http://192.168.2.108/moviles/post.php";
	private readonly HttpClient _cliente = new HttpClient();
	private ObservableCollection<Estudiante> _estudiante = new();

    public vEstudiante()
	{
		InitializeComponent();
    }

    // Se ejecuta cada vez que la ventana aparece:
    // al abrir la app y al regresar de "Agregar"
    protected override void OnAppearing()
    {
        base.OnAppearing();
        MostrarEstudiante();
    }

    // GET: trae la lista de estudiantes del servicio
    public async void MostrarEstudiante()
    {
        try
        {
            var content = await _cliente.GetStringAsync(Url);
            List<Estudiante> _mostrar = JsonConvert.DeserializeObject<List<Estudiante>>(content) ?? new();
            _estudiante = new ObservableCollection<Estudiante>(_mostrar);
            listaEstudiante.ItemsSource = _estudiante;
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error al listar", ex.Message, "OK");
        }
    }

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new vAgregarEstudiante());
    }

    // Al tocar un estudiante, abre la ventana de actualizar
    private async void listaEstudiante_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null) return;
        var objEstudiante = (Estudiante)e.SelectedItem;
        listaEstudiante.SelectedItem = null;
        await Navigation.PushAsync(new vActualizarEliminar(objEstudiante));
    }
}