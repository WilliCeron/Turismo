using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Turismo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Galeria : ContentPage
	{
		public Galeria ()
		{
			InitializeComponent ();
		}

        async public void regresarlista()
        {
            await Navigation.PushModalAsync(new ListSites());
        }
    }
}