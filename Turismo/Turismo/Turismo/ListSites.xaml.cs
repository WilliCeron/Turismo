using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Turismo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Turismo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListSites : ContentPage
	{

        private const string UrlRoot = "https://apiturismow.herokuapp.com/";
        private const string UrlListContact = UrlRoot + "sites";
        private const string UrlDeleteContact = UrlRoot + "sites";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Site> _sites;


        public ListSites ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.Properties.ContainsKey("id_user"))
            {
                ListDataContacts();
            }
            else
            {
                showWindowMainPage();
            }
        }

        // Metodo para listar todos los contactos
        async public void ListDataContacts()
        {
            string content = await client.GetStringAsync(UrlListContact);
            List<Site> contacts = JsonConvert.DeserializeObject<List<Site>>(content);
            _sites = new ObservableCollection<Site>(contacts);
            listViewContacts.ItemsSource = _sites;
        }

        public void ClickUpdateContact(object sender, EventArgs e)
        {
            var mi = sender as MenuItem;
            var item = mi.BindingContext as Site;

            Site contact = new Site()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Food = item.Food,
                Image = item.Image
            };

            showWindowUpdateContact(contact);
        }

        async public void showWindowUpdateContact(Site contact)
        {
            await Navigation.PushModalAsync(new UpdateSite(contact));
        }
        // Metodo para visitar sitio
        public void ClickVisitSite(object sender, EventArgs e)
        {
            var mi = sender as MenuItem;
            var item = mi.BindingContext as Site;

            Site visit = new Site()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Food = item.Food,
                Image = item.Image
            };

            showWindowVisitSite(visit);
        }

        async public void showWindowVisitSite(Site visit)
        {
            await Navigation.PushModalAsync(new VisiteSite(visit));
        }

        public void ClickDeleteContact(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DeleteContact(mi.CommandParameter.ToString());
        }

        // Metodo para eliminar un contacto
        async public void DeleteContact(string position)
        {
            HttpResponseMessage response = null;
            response = await client.DeleteAsync(UrlDeleteContact + "/" + position);

            ListDataContacts();
        }

        async public void ClickButtonShowWindowNewContact(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CreateSite());
        }

        public void ClickButtonSignOff(object sender, EventArgs e)
        {
            Application.Current.Properties.Clear();
            showWindowMainPage();
        }

        async public void showWindowMainPage()
        {
            await Navigation.PushModalAsync(new MainPage());
        }
    }
}