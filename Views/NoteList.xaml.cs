using ToDo.Models;

namespace ToDo.Views;

public partial class NoteList : ContentPage
{
    NoteRepository noteRepository = new NoteRepository();
    public NoteList()
    {
        InitializeComponent();

        NoteListView.RefreshCommand = new Command(() =>
        {
            OnAppearing();
        });
    }

    protected override async void OnAppearing()
    {
        var notes = await noteRepository.GetAll();
        NoteListView.ItemsSource = null;
        NoteListView.ItemsSource = notes;
        NoteListView.IsRefreshing = false;

    }

    private void AddToolBarItem_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new NoteEntry());
    }

#if WINDOWS
    private async void NoteListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item == null)
        {
            return;
        }
        var note = e.Item as NoteModel;
        await Navigation.PushModalAsync(new NoteDetails(note));
        ((ListView)sender).SelectedItem = null;

    }
#else
    private async void NoteListView_ItemTapped(object sender, EventArgs e)
    {

        string id = ((TappedEventArgs)e).Parameter.ToString();
        var note = await noteRepository.GetById(id);
        if (note == null)
        {
            return;
        }
        note.Id = id;
        await Navigation.PushModalAsync(new NoteDetails(note));
    }
#endif



    private async void ViewTap_ItemTapped(object sender, EventArgs e)
    {
        string id = ((TappedEventArgs)e).Parameter.ToString();
        var note = await noteRepository.GetById(id);
        if (note == null)
        {
            await DisplayAlert("Warning", "Data not found.", "Ok");
        }
        note.Id = id;
        await Navigation.PushModalAsync(new NoteDetails(note));
    }

    private async void DeleteTapp_Tapped(object sender, EventArgs e)
    {

        var response = await DisplayAlert("Delete", "Do you want to delete?", "Yes", "No");
        if (response)
        {
            string id = ((TappedEventArgs)e).Parameter.ToString();
            bool isDelete = await noteRepository.Delete(id);
            if (isDelete)
            {
                await DisplayAlert("Information", "Note has been deleted.", "Ok");
                OnAppearing();
            }
            else
            {
                await DisplayAlert("Error", "Note delete failed.", "Ok");
            }
        }
    }

    private async void EditTap_Tapped(object sender, EventArgs e)
    {
        //DisplayAlert("Edit", "Do you want to Edit?", "Ok");

        string id = ((TappedEventArgs)e).Parameter.ToString();
        var note = await noteRepository.GetById(id);
        if (note == null)
        {
            await DisplayAlert("Warning", "Data not found.", "Ok");
        }
        note.Id = id;
        await Navigation.PushModalAsync(new NoteEdit(note));

    }


    private async void TxtSearch_SearchButtonPressed(object sender, EventArgs e)
    {
        string searchValue = TxtSearch.Text;
        if (!String.IsNullOrEmpty(searchValue))
        {
            var notes = await noteRepository.GetAllByName(searchValue);
            NoteListView.ItemsSource = null;
            NoteListView.ItemsSource = notes;
        }
        else
        {
            OnAppearing();
        }
    }

    private async void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        string searchValue = TxtSearch.Text;
        if (!String.IsNullOrEmpty(searchValue))
        {
            var notes = await noteRepository.GetAllByName(searchValue);
            NoteListView.ItemsSource = null;
            NoteListView.ItemsSource = notes;
        }
        else
        {
            OnAppearing();
        }
    }

    private async void EditMenuItem_Clicked(object sender, EventArgs e)
    {
        string id = ((MenuItem)sender).CommandParameter.ToString();
        var note = await noteRepository.GetById(id);
        if (note == null)
        {
            await DisplayAlert("Warning", "Data not found.", "Ok");
        }
        note.Id = id;
        await Navigation.PushModalAsync(new NoteEdit(note));
    }

    private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
    {
        var response = await DisplayAlert("Delete", "Do you want to delete?", "Yes", "No");
        if (response)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            bool isDelete = await noteRepository.Delete(id);
            if (isDelete)
            {
                await DisplayAlert("Information", "Note deleted", "Ok");
                OnAppearing();
            }
            else
            {
                await DisplayAlert("Error", "Note delete failed.", "Ok");
            }
        }
    }

    private async void EditSwipeItem_Invoked(object sender, EventArgs e)
    {
        string id = ((MenuItem)sender).CommandParameter.ToString();
        var note = await noteRepository.GetById(id);
        if (note == null)
        {
            await DisplayAlert("Warning", "Data not found.", "Ok");
        }
        note.Id = id;
        await Navigation.PushModalAsync(new NoteEdit(note));
    }

}