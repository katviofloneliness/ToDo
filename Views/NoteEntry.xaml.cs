using ToDo.Models;

namespace ToDo.Views;

public partial class NoteEntry : ContentPage
{
    NoteRepository repository = new NoteRepository();
    public NoteEntry()
    {
        InitializeComponent();
    }

    private async void ButtonSave_Clicked(object sender, EventArgs e)
    {
        string name = TxtName.Text;
        string text = TxtText.Text;
        if (string.IsNullOrEmpty(name))
        {
            await DisplayAlert("Warning", "Please enter your name", "Cancel");
        }
        if (string.IsNullOrEmpty(text))
        {
            await DisplayAlert("Warning", "Please enter your note", "Cancel");
        }

        NoteModel note = new NoteModel();
        note.Name = name;
        note.Text = text;

        var isSaved = await repository.Save(note);
        if (isSaved)
        {
            await DisplayAlert("Information", "Note saved.", "Ok");
            Clear();
        }
        else
        {
            await DisplayAlert("Error", "Note save failed.", "Ok");
        }

    }
    public void Clear()
    {
        TxtName.Text = string.Empty;
        TxtText.Text = string.Empty;
    }

    private async void ButtonBack_Clicked(object sender, EventArgs e)
    {
        //await Navigation.PushModalAsync(new NoteListPage());
        await Navigation.PopModalAsync();
    }

}