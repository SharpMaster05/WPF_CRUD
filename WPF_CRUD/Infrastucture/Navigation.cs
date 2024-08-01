using System.Windows.Controls;

namespace WPF_CRUD.Infrastucture;

internal class Navigation
{
    public Frame Frame { get; set; } = new Frame();

    public void ChangePage(Page page)
    {
        Animation.ChangePageAnimation(Frame, page);
    }
}
