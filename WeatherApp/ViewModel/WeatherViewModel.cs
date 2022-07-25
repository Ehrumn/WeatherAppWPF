namespace WeatherApp.ViewModel;

public class WeatherViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged( string propertyName)
    {
        PropertyChanged?.Invoke( this, new PropertyChangedEventArgs(propertyName ));
    }
}
