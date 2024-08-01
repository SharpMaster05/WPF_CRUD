using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_CRUD.Infrastucture;

internal class Notifier : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
