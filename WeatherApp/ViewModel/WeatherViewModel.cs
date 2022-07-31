using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel;

public class WeatherViewModel : INotifyPropertyChanged
{
    private string query;

    public string Query
    {
        get { return query; }
        set
        {
            query = value;
            OnPropertyChanged("query");
        }
    }

    public ObservableCollection<City> Cities { get; set; }

    private CurrentConditions currentConditions;

    public CurrentConditions CurrentConditions
    {
        get { return currentConditions; }
        set
        {
            currentConditions = value;
            OnPropertyChanged("CurrentConditions");
        }
    }

    private City selectecCity;

    public City SelectecCity
    {
        get { return selectecCity; }
        set
        {
            selectecCity = value;
            OnPropertyChanged("SelectecCity");
            GetCurrentCondintions();
        }
    }

    public SearchCommand SearchCommand { get; set; }


    public WeatherViewModel()
    {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
        {
            selectecCity=new()
            {
                LocalizedName="Curitiba"
            };
            currentConditions = new()
            {
                WeatherText = "Partly cloudy",
                Temperature = new()
                {
                    Metric = new() { Value = "21" }
                }
            };
        }

        SearchCommand = new(this);
        Cities = new ObservableCollection<City>();
    }

    private async void GetCurrentCondintions()
    {
        Query = String.Empty;
        Cities.Clear();
        CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectecCity.Key);
    }

    public async void MakeQuery()
    {
        var cities = await AccuWeatherHelper.GetCities(Query);

        Cities.Clear();
        foreach (City city in cities)
            Cities.Add(city);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
