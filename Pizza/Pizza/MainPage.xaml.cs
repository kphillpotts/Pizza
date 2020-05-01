using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pizza
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        AnimationStateMachine animState;

        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            this.SizeChanged += MainPage_SizeChanged;
            base.OnAppearing();
            
            if (animState != null)
            {
                animState.Go(State.Start, false);
                animState.Go(State.Entrance, true);
            }
        }

        enum State
        {
            Start,
            Entrance,
            Small,
            Medium,
            Large
        }


        private void MainPage_SizeChanged(object sender, EventArgs e)
        {
            if (animState != null)
                return;

            // calculate our size for our images
            var imageSize = Width * 1.5;

            var left = (imageSize - Width) / 2;
            var top = imageSize * .5;

            var startRect = new Rectangle(0, 0, imageSize, imageSize);
            startRect.Location = new Point(-left, -top);

            AbsoluteLayout.SetLayoutBounds(Pizza, startRect);

            animState = new AnimationStateMachine();

            var headerTranslationY = PizzaDescription.Bounds.Top - 
                (PizzaTitle.Bounds.Height + PizzaTitle.Bounds.Top + 20);


            // starting 
            animState.Add(State.Start, new ViewTransition[]
            {
                new ViewTransition(Pizza, AnimationType.Layout, startRect),
                new ViewTransition(Pizza, AnimationType.Rotation, 0),
                new ViewTransition(SizeLabel, AnimationType.Opacity, 0, 0),
                new ViewTransition(PizzaDescription, AnimationType.Opacity, 1),
                new ViewTransition(PizzaDescription, AnimationType.TranslationY, 0),
                new ViewTransition(PizzaTitle, AnimationType.TranslationY, headerTranslationY),
                new ViewTransition(QuantitySelect, AnimationType.Opacity, 0)
            }) ;

            // entrance 
            var entranceRect = startRect;
            entranceRect.Location = new Point(-left, -top + 50);

            animState.Add(State.Entrance, new ViewTransition[]
            {
                new ViewTransition(Pizza, AnimationType.Layout, entranceRect),
                new ViewTransition(Pizza, AnimationType.Rotation, 20),
                new ViewTransition(SizeLabel, AnimationType.Opacity, 0, 0),
                new ViewTransition(PizzaDescription, AnimationType.Opacity, 1),
                new ViewTransition(PizzaDescription, AnimationType.TranslationY, 0),
                new ViewTransition(PizzaTitle, AnimationType.TranslationY, headerTranslationY),
                new ViewTransition(QuantitySelect, AnimationType.Opacity, 0)
            });

            // small 
            var yPos = Height * 0.4;

            var pizzaImageSize = Width * .5;
            var smallRect = new Rectangle(20, yPos - (pizzaImageSize/2), pizzaImageSize, pizzaImageSize);
            var smallLabelRect = new Rectangle(smallRect.Left, smallRect.Bottom + 20, pizzaImageSize, SizeLabel.Height);

            animState.Add(State.Small, new ViewTransition[]
            {
                new ViewTransition(Pizza, AnimationType.Layout, smallRect),
                new ViewTransition(Pizza, AnimationType.Rotation, 45),
                new ViewTransition(FlyingPizza, AnimationType.Layout, smallRect),
                new ViewTransition(FlyingPizza, AnimationType.Rotation, 45),
                new ViewTransition(SizeLabel, AnimationType.Opacity, 1, 0),
                new ViewTransition(SizeLabel, AnimationType.Layout, smallLabelRect),
                new ViewTransition(PizzaDescription, AnimationType.Opacity, 0),
                new ViewTransition(PizzaDescription, AnimationType.TranslationY, 300),
                new ViewTransition(PizzaTitle, AnimationType.TranslationY, 0),
                new ViewTransition(QuantitySelect, AnimationType.Opacity, 1)

            }); ;

            // medium 
            pizzaImageSize = Width * .7;
            var mediumRect = new Rectangle(20, yPos - (pizzaImageSize / 2), pizzaImageSize, pizzaImageSize);
            var mediumLabelRect = new Rectangle(mediumRect.Left, mediumRect.Bottom + 20, pizzaImageSize, SizeLabel.Height);
            animState.Add(State.Medium, new ViewTransition[]
            {
                new ViewTransition(Pizza, AnimationType.Layout, mediumRect),
                new ViewTransition(Pizza, AnimationType.Rotation, 90),
                new ViewTransition(FlyingPizza, AnimationType.Layout, mediumRect),
                new ViewTransition(FlyingPizza, AnimationType.Rotation, 90),
                new ViewTransition(SizeLabel, AnimationType.Opacity, 1, 0),
                new ViewTransition(SizeLabel, AnimationType.Layout, mediumLabelRect),
                new ViewTransition(PizzaDescription, AnimationType.Opacity, 0),
                new ViewTransition(PizzaDescription, AnimationType.TranslationY, 300),
                new ViewTransition(PizzaTitle, AnimationType.TranslationY, 0),
                new ViewTransition(QuantitySelect, AnimationType.Opacity, 1)
            });

            // large 
            pizzaImageSize = Width * .9;
            var largeRect = new Rectangle(20, yPos - (pizzaImageSize / 2), pizzaImageSize, pizzaImageSize);
            var largeLabelRect = new Rectangle(largeRect.Left, largeRect.Bottom + 20, pizzaImageSize, SizeLabel.Height);
            animState.Add(State.Large, new ViewTransition[]
            {
                new ViewTransition(Pizza, AnimationType.Layout, largeRect),
                new ViewTransition(Pizza, AnimationType.Rotation, 135),
                new ViewTransition(FlyingPizza, AnimationType.Layout, largeRect),
                new ViewTransition(FlyingPizza, AnimationType.Rotation, 135),
                new ViewTransition(SizeLabel, AnimationType.Opacity, 1, 0),
                new ViewTransition(SizeLabel, AnimationType.Layout, largeLabelRect),
                new ViewTransition(PizzaDescription, AnimationType.Opacity, 0),
                new ViewTransition(PizzaDescription, AnimationType.TranslationY, 300),
                new ViewTransition(PizzaTitle, AnimationType.TranslationY, 0),
                new ViewTransition(QuantitySelect, AnimationType.Opacity, 1)
            });


            // go to our starting state
            animState.Go(State.Start, false);
            animState.Go(State.Entrance);
        }
        
        private void Pizza_Tapped(object sender, EventArgs e)
        {
            switch (animState.CurrentState)
            {
                case State.Start:
                    animState.Go(State.Entrance);
                    break;
                case State.Entrance:
                    SizeLabel.Text = "Small";
                    animState.Go(State.Small);
                    break;
                case State.Small:
                    SizeLabel.Text = "Medium";
                    animState.Go(State.Medium);
                    break;
                case State.Medium:
                    SizeLabel.Text = "Large";
                    animState.Go(State.Large);
                    break;
                case State.Large:
                    animState.Go(State.Start);
                    break;

            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            // eat the pizza
            FlyingPizza.IsVisible = true;

            var buttonBounds = AddToCartButton.Bounds;

            // work out where it needs to fly to?
            var size = new Size(buttonBounds.Height, buttonBounds.Height);
            var location = new Point(buttonBounds.Right - size.Width, buttonBounds.Top);
            var chompBounds = new Rectangle(location, size);

            // animate the pizza down
            _ = FlyingPizza.LayoutTo(chompBounds, 500, Easing.SinInOut);
            _ = FlyingPizza.RelRotateTo(90, 500, Easing.SinInOut);

            // do the button chomp
            var buttonChompBounds = new Rectangle(AddToCartButton.Bounds.Location,
                new Size(AddToCartButton.Width - buttonBounds.Height, buttonBounds.Height));
            await AddToCartButton.LayoutTo(buttonChompBounds, 500, Easing.SinInOut);
            
            _ = FlyingPizza.RelRotateTo(-90, 500, Easing.SinInOut);
            
            // close the button chomp
            await AddToCartButton.LayoutTo(buttonBounds, 500, Easing.SinInOut);
            
            FlyingPizza.IsVisible = false;

        }

        int currentQuantity = 1;

        private void DecreaseButton_Clicked(object sender, EventArgs e)
        {
            currentQuantity--;
            QuantityLabel.AnimationOffset = new Point(0, -20);
            QuantityLabel.Text = currentQuantity.ToString();

        }

        private void IncreaseButton_Clicked(object sender, EventArgs e)
        {
            currentQuantity++;
            QuantityLabel.AnimationOffset = new Point(0, 20);
            QuantityLabel.Text = currentQuantity.ToString();

        }
    }
}
