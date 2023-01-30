using Firebase.Database;
using Newtonsoft.Json;

namespace ToDo.Models
{
    public class NoteRepository
 
    {

        FirebaseClient firebaseClient = new FirebaseClient("https://todo-702cd-default-rtdb.europe-west1.firebasedatabase.app/");
        public async Task<bool> Save(NoteModel note)
        {
            var data = await firebaseClient.Child(nameof(NoteModel)).PostAsync(JsonConvert.SerializeObject(note));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<List<NoteModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(NoteModel)).OnceAsync<NoteModel>()).Select(item => new NoteModel
            {
                Text = item.Object.Text,
                Name = item.Object.Name,
                Id = item.Key
            }).ToList();
        }

        public async Task<List<NoteModel>> GetAllByName(string name)
        {
            return (await firebaseClient.Child(nameof(NoteModel)).OnceAsync<NoteModel>()).Select(item => new NoteModel
            {
                Text = item.Object.Text,
                Name = item.Object.Name,
                Id = item.Key
            }).Where(c => c.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public async Task<NoteModel> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(NoteModel) + "/" + id).OnceSingleAsync<NoteModel>());
        }

        public async Task<bool> Update(NoteModel note)
        {
            await firebaseClient.Child(nameof(NoteModel) + "/" + note.Id).PutAsync(JsonConvert.SerializeObject(note));
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child(nameof(NoteModel) + "/" + id).DeleteAsync();
            return true;
        }

    }

}
