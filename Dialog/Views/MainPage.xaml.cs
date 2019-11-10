using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dialog.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public async void OnShowDialogButtonClicked(object sender, EventArgs arg)
        {
            Debug.WriteLine("OnShowDialogButtonClicked");

            //var result = await  DependencyService.Get<ICustomDialogService>().Show(
            //      "Prain text", "Please enter text.", "OK", "Cancel");

            var result = await DependencyService.Get<ICustomDialogService>().Show3();

            Debug.WriteLine(result);
        }
    }
}
