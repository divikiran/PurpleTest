using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PacManDiv
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainViewModel CurrentViewModel { get { return BindingContext as MainViewModel; } }

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            int rowMax = 6;
            int colMax = 6;

            for (int row = 0; row < rowMax; row++)
            {
                for (int col = 0; col < colMax; col++)
                {
                    PacManStackLayout pacStack = new PacManStackLayout()
                    {
                        HeightRequest = 10,
                        WidthRequest = 10,
                        BackgroundColor = Color.Green,
                        Row = row,
                        Col = col
                    };
                    TapGestureRecognizer stackTapGesture = new TapGestureRecognizer();
                    stackTapGesture.SetBinding(TapGestureRecognizer.CommandProperty, new Binding(nameof(CurrentViewModel.StackTapCommand)));

                    stackTapGesture.CommandParameter = pacStack;
                    pacStack.GestureRecognizers.Add(stackTapGesture);
                    PacManGrid.Children.Add(pacStack, col, row);
                    CurrentViewModel.AllStacks.Add(pacStack);

                }
            }
        }
    }
}
