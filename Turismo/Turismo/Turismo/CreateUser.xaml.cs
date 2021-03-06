﻿using Newtonsoft.Json;
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
	public partial class CreateUser : ContentPage
	{
        private const string UrlRoot = "https://apiturismow.herokuapp.com/";
        private const string UrlCreateUser = UrlRoot + "createUser";
        private readonly HttpClient client = new HttpClient();

        public CreateUser ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.Properties.ContainsKey("id_user"))
            {
                ShowWindowListSites();
            }
        }

        public void ClickButtonCreateUser(object sender, EventArgs e)
        {
            CreateAccount();
        }

        async public void CreateAccount()
        {
            User user = new User()
            {
                Username = entryUsernameCu.Text,
                Password = entryPasswordCu.Text,
                Name = entryNameCu.Text,
                Email = entryEmailCu.Text
            };

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(UrlCreateUser, content);
            string message = await response.Content.ReadAsStringAsync();
            List<User> users = JsonConvert.DeserializeObject<List<User>>(message);

            Application.Current.Properties["id_user"] = users[0].Id;

            await Navigation.PushModalAsync(new ListSites());
        }

        async private void ShowWindowListSites()
        {
            await Navigation.PushModalAsync(new ListSites());
        }
        async public void ClickButtonHome()
        {
            await Navigation.PushModalAsync(new MainPage());
        }
    }
}