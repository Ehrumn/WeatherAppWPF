namespace WeatherApp.ViewModel.Commands;

public class SearchCommand : ICommand
{
    public WeatherViewModel VM { get; set; }


    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested+=value; }
        remove { CommandManager.RequerySuggested -= value; }
    }


    public SearchCommand(WeatherViewModel vm)
    {
        VM = vm;
    }

    public bool CanExecute(object? parameter)
    {
        string query = parameter as string;
        if (string.IsNullOrWhiteSpace(query))
            return false;

        return true;
    }

    public void Execute(object? parameter)
    {
        VM.MakeQuery();
    }
}
