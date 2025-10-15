using SQLiteTutorials3.Models;
using Contact = SQLiteTutorials3.Models.Contact;

namespace SQLiteTutorials3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadContacts();
        }

        private async Task LoadContacts()
        {
            ContactsList.ItemsSource = await App.Database.GetContactAsync();
        }

        private async void OnSaveContactClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NameEntry.Text) && !string.IsNullOrWhiteSpace(PhoneEntry.Text))
            {
                var contact = new Contact
                {
                    Name = NameEntry.Text,
                    Phone = PhoneEntry.Text
                };

                await App.Database.SaveContactAsync(contact);

                NameEntry.Text = string.Empty;
                PhoneEntry.Text = string.Empty;

                await LoadContacts();
            }
            else
            {
                await DisplayAlert("Error", "Please fill in both fields.", "OK");
            }
        }

        private async void OnDeleteContactClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var contact = button?.CommandParameter as Contact;

            if (contact != null)
            {
                bool confirm = await DisplayAlert(
                    "Delete Contact",
                    $"Are you sure you want to delete {contact.Name}?",
                    "Yes",
                    "No"
                );

                if (confirm)
                {
                    await App.Database.DeleteContactAsync(contact);
                    await LoadContacts();
                }
            }
        }
    }
}

