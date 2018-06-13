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
	public partial class UpdateSite : ContentPage
	{

        private const string UrlRoot = "https://apiturismow.herokuapp.com/";
        private const string UrlUpdateSite = UrlRoot + "sites";
        private readonly HttpClient client = new HttpClient();
        private Site site;

        public UpdateSite ()
		{
			InitializeComponent ();
		}

        public UpdateSite(Site site)
        {
            InitializeComponent();
            this.site = site;
            BindingContext = site;
        }


        async public void ClickButtonUpdateSite(object sender, EventArgs e)
        {
            site.Name = entryNameUs.Text;
            site.Description = entryDescriptionUs.Text;
            site.Food = entryFoodUs.Text;

            var json = JsonConvert.SerializeObject(site);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PutAsync(UrlUpdateSite + "/" + site.Id, content);

            await Navigation.PushModalAsync(new ListSites());
        }

    }
}