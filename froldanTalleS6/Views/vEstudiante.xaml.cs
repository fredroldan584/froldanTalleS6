using froldanTalleS6.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace froldanTalleS6.Views;

public partial class vEstudiante : ContentPage
{
	private const string Url = "http://192.168.2.108/moviles/post.php";
	private readonly HttpClient _cliente = new HttpClient();
	private ObservableCollection<Estudiante> _estudiante;

    public vEstudiante()
	{
		InitializeComponent();
        MostrarEstudiante();
    }

	public async void MostrarEstudiante()
    {
        var content = await _cliente.GetStringAsync(Url);
		List<Estudiante> _mostrar = JsonConvert.DeserializeObject<List<Estudiante>>(content);
        _estudiante = new ObservableCollection<Estudiante>(_mostrar);
		listaEstudiante.ItemsSource = _estudiante;
    }
}