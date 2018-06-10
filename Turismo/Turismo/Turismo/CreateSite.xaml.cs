using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
	public partial class CreateSite : ContentPage
	{

        private const string UrlRoot = "https://apiturismow.herokuapp.com/";
        private const string UrlCreateSite = UrlRoot + "sites";
        private readonly HttpClient client = new HttpClient();

        public CreateSite ()
		{
			InitializeComponent ();
		}

        public void ClickButtonCreateSite(object sender, EventArgs e)
        {
            CreateNewSite();
        }

        async public void CreateNewSite()
        {
            Site contact = new Site()
            {
                Name = entryNameS.Text,
                Description = entryDescription.Text,
                Food = entryFood.Text

            };

            var json = JsonConvert.SerializeObject(contact);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(UrlCreateSite, content);
            string message = await response.Content.ReadAsStringAsync();

            await Navigation.PushModalAsync(new ListSites());
        }

    }
}