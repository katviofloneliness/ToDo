
using ToDo.Models;

namespace ToDo.Views;

public partial class NoteEdit : ContentPage
{

    NoteRepository noteRepository = new NoteRepository();
    public NoteEdit(NoteModel note)
    {
        InitializeComponent();
        TxtId.Text = note.Id;
        TxtName.Text = note.Name;
        TxtText.Text = note.Text;

    }

    private async void ButtonUpdate_Clicked(object sender, EventArgs e)
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
        note.Id = TxtId.Text;
        note.Name = name;
        note.Text = text;
        bool isUpdated = await noteRepository.Update(note);
        if (isUpdated)
        {
            await Navigation.PopModalAsync();
        }
        else
        {
            await DisplayAlert("Error", "Update failed.", "Cancel");
        }

    }
    private async void ButtonBack_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new NoteList());
        //await Navigation.PopModalAsync();
    }
}