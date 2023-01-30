using Microsoft.VisualBasic;
using ToDo.Models;

namespace ToDo.Views;

public partial class NoteDetails : ContentPage
{
    NoteRepository noteRepository = new NoteRepository();
    public NoteDetails(NoteModel note)
    {
        InitializeComponent();
        LabelName.Text = note.Name;
        LabelText.Text = note.Text;
    }
    private async void ButtonBack_Clicked(object sender, EventArgs e)
    { 
        //await Navigation.PushModalAsync(new NoteList());
        await Navigation.PopModalAsync();
    }

}