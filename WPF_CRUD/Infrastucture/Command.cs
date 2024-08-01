using System.Windows.Input;

namespace WPF_CRUD.Infrastucture;

internal class Command : ICommand
{
    private readonly Action<object> _execute;
    private readonly Func<object, bool> _canExecute;

    public Command(Action<object> execute, Func<object, bool> canExecute = null) => (_execute, _canExecute) = (execute, canExecute);

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

    public void Execute(object parameter) => _execute(parameter);
}
