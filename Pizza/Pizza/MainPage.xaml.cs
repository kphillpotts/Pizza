using SkiaSharp;
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
            this.SizeChanged += MainPage_SizeChanged;
        }

        bool hasSizeBeenCalculated = false;

        private void MainPage_SizeChanged(object sender, EventArgs e)
        {
            SetupAnimationStates();
            hasSizeBeenCalculated = true;
            animState.Go(State.Start, false);
            animState.Go(State.Entrance, true);
        }


        Animation pulse;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (hasSizeBeenCalculated)
            {
                // need to resetup animations here. 
                // because the animation helper has weak references
                SetupAnimationStates();
                animState.Go(State.Start, false);
                animState.Go(State.Entrance, true);
            }

            // create continuous animation for thumb
            pulse = new Animation();
            pulse.Add(0, .5, new Animation(a => PizzaThumbLabel.TranslationX = a, 0, 5, Easing.SinInOut));
            pulse.Add(.5, 1, new Animation(a => PizzaThumbLabel.TranslationX = a, 5, 0, Easing.SinInOut));
            // start the animation continuously
            pulse.Commit(this, "pulse", length:500, repeat: () => true);

        }

        enum State
        {
            Start,
            Entrance,
            Small,
            Medium,
            Large
        }

        private void SetupAnimationStates()
        {
            // calculate our size for our images
            var imageSize = Width * 1.5;

            // work out the position of the starting pizza
            var left = (imageSize - Width) / 2;
            var top = imageSize * .5;
            var startRect = new Rectangle(-left, -top, imageSize, imageSize);

            var headerTranslationY = PizzaDescription.Bounds.Top -
                (PizzaTitle.Bounds.Height + PizzaTitle.Bounds.Top + 20);

            animState = new AnimationStateMachine();

            // starting 
            animState.Add(State.Start, new ViewTransition[]
            {
                new ViewTransition(Pizza, AnimationType.Layout, startRect),
                new ViewTransition(Pizza, AnimationType.Rotation, 0),
                new ViewTransition(FlyingPizza, AnimationType.Layout, startRect),
                new ViewTransition(FlyingPizza, AnimationType.Rotation, 0),
                new ViewTransition(SizeLabel, AnimationType.Opacity, 0, 0),
                new ViewTransition(PizzaDescription, AnimationType.Opacity, 1),
                new ViewTransition(PizzaDescription, AnimationType.TranslationY, 0),
                new ViewTransition(PizzaTitle, AnimationType.TranslationY, headerTranslationY),
                new ViewTransition(QuantitySelect, AnimationType.Opacity, 0),
                new ViewTransition(PizzaRuler, AnimationType.Opacity, 0),
                new ViewTransition(PizzaRulerThumb, AnimationType.Opacity, 0),
                new ViewTransition(PlaceOrderButton, AnimationType.Opacity,0),
                new ViewTransition(AddToCartButton, AnimationType.Opacity, 1),
                new ViewTransition(PlaceOrder, AnimationType.Opacity, 0),
                new ViewTransition(BackButton, AnimationType.Opacity, 0),
                new ViewTransition(RatingBar, AnimationType.Opacity, 0),
                new ViewTransition(RatingBar, AnimationType.TranslationY, 90),
                new ViewTransition(BasilLeaf, AnimationType.TranslationX, 200),
            });

            // entrance pizza position is calculated 
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
                new ViewTransition(QuantitySelect, AnimationType.Opacity, 0),
                new ViewTransition(PizzaRuler, AnimationType.Opacity, 0),
                new ViewTransition(PizzaRulerThumb, AnimationType.Opacity, 0),
                new ViewTransition(PlaceOrderButton, AnimationType.Opacity,0),
                new ViewTransition(AddToCartButton, AnimationType.Opacity, 1),
                new ViewTransition(PlaceOrder, AnimationType.Opacity, 0),
                new ViewTransition(BackButton, AnimationType.Opacity, 0),
                new ViewTransition(RatingBar, AnimationType.Opacity, 0),
                new ViewTransition(RatingBar, AnimationType.TranslationY, 90),
                new ViewTransition(BasilLeaf, AnimationType.TranslationX, 200),
            });

            // small 
            var yPos = Height * 0.4;

            var pizzaImageSize = Width * .5;
            var smallRect = new Rectangle(20, yPos - (pizzaImageSize / 2), pizzaImageSize, pizzaImageSize);
            var smallLabelRect = new Rectangle(smallRect.Left, smallRect.Bottom + 10, pizzaImageSize, SizeLabel.Height);

            var rulerRect = new Rectangle(
                x: smallRect.Left + (smallRect.Width / 2),
                y: smallRect.Top + (smallRect.Height / 2),
                width: Width - (20 /* margin */) - (smallRect.Left + (smallRect.Width / 2)),
                height: 10);

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
                new ViewTransition(QuantitySelect, AnimationType.Opacity, 1),
                new ViewTransition(PizzaRuler, AnimationType.Opacity, 1),
                new ViewTransition(PizzaRuler, AnimationType.Layout, rulerRect),
                new ViewTransition(PizzaRulerThumb, AnimationType.Opacity, 1),
                new ViewTransition(PizzaRulerThumb, AnimationType.Layout, CalculateThumbPosition(smallRect) ),
                new ViewTransition(PizzaRulerThumb, AnimationType.Rotation, 0),
                new ViewTransition(PlaceOrderButton, AnimationType.Opacity,1),
                new ViewTransition(AddToCartButton, AnimationType.Opacity, 0),
                new ViewTransition(PlaceOrder, AnimationType.Opacity, 1),
                new ViewTransition(BackButton, AnimationType.Opacity, 1),
                new ViewTransition(RatingBar, AnimationType.Opacity, 1),
                new ViewTransition(RatingBar, AnimationType.TranslationY, 0),
                new ViewTransition(BasilLeaf, AnimationType.TranslationX, 100),

            }); ;

            // medium 
            pizzaImageSize = Width * .7;
            var mediumRect = new Rectangle(20, yPos - (pizzaImageSize / 2), pizzaImageSize, pizzaImageSize);
            var mediumLabelRect = new Rectangle(mediumRect.Left, mediumRect.Bottom + 10, pizzaImageSize, SizeLabel.Height);
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
                new ViewTransition(QuantitySelect, AnimationType.Opacity, 1),
                new ViewTransition(PizzaRuler, AnimationType.Opacity, 1),
                new ViewTransition(PizzaRuler, AnimationType.Layout, rulerRect),
                new ViewTransition(PizzaRulerThumb, AnimationType.Opacity, 1),
                new ViewTransition(PizzaRulerThumb, AnimationType.Layout, CalculateThumbPosition(mediumRect) ),
                new ViewTransition(PizzaRulerThumb, AnimationType.Rotation, 0),
                new ViewTransition(PlaceOrderButton, AnimationType.Opacity,1),
                new ViewTransition(AddToCartButton, AnimationType.Opacity, 0),
                new ViewTransition(PlaceOrder, AnimationType.Opacity, 1),
                new ViewTransition(BackButton, AnimationType.Opacity, 1),
                new ViewTransition(RatingBar, AnimationType.Opacity, 1),
                new ViewTransition(RatingBar, AnimationType.TranslationY, 0),
                new ViewTransition(BasilLeaf, AnimationType.TranslationX, 100),

            });

            // large 
            pizzaImageSize = Width * .9;
            var largeRect = new Rectangle(20, yPos - (pizzaImageSize / 2), pizzaImageSize, pizzaImageSize);
            var largeLabelRect = new Rectangle(largeRect.Left, largeRect.Bottom + 10, pizzaImageSize, SizeLabel.Height);
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
                new ViewTransition(QuantitySelect, AnimationType.Opacity, 1),
                new ViewTransition(PizzaRuler, AnimationType.Opacity, 1),
                new ViewTransition(PizzaRuler, AnimationType.Layout, rulerRect),
                new ViewTransition(PizzaRulerThumb, AnimationType.Opacity, 1),
                new ViewTransition(PizzaRulerThumb, AnimationType.Layout, CalculateThumbPosition(largeRect) ),
                new ViewTransition(PizzaRulerThumb, AnimationType.Rotation, -180),
                new ViewTransition(PlaceOrderButton, AnimationType.Opacity,1),
                new ViewTransition(AddToCartButton, AnimationType.Opacity, 0),
                new ViewTransition(PlaceOrder, AnimationType.Opacity, 1),
                new ViewTransition(BackButton, AnimationType.Opacity, 1),
                new ViewTransition(RatingBar, AnimationType.Opacity, 1),
                new ViewTransition(RatingBar, AnimationType.TranslationY, 0),
                new ViewTransition(BasilLeaf, AnimationType.TranslationX, 100),
            });
        }

        private Rectangle CalculateThumbPosition(Rectangle pizzaSize)
        {
            return new Rectangle(
                x: pizzaSize.Right - 15,
                y: pizzaSize.Top + (pizzaSize.Height / 2) - 15,
                width: 30,
                height: 30);
                
        }

        //private void Pizza_Tapped(object sender, EventArgs e)
        //{
        //    switch (animState.CurrentState)
        //    {
        //        case State.Start:
        //            animState.Go(State.Entrance);
        //            break;
        //        case State.Entrance:
        //            SizeLabel.Text = "Small";
        //            animState.Go(State.Small);
        //            break;
        //        case State.Small:
        //            SizeLabel.Text = "Medium";
        //            animState.Go(State.Medium);
        //            break;
        //        case State.Medium:
        //            SizeLabel.Text = "Large";
        //            animState.Go(State.Large);
        //            break;
        //        case State.Large:
        //            animState.Go(State.Start);
        //            break;

        //    }
        //}

        private void AddToCartButton_Clicked(object sender, EventArgs e)
        {
            //await PizzaFly();
            animState.Go(State.Small);

        }

        private async Task PizzaFly()
        {
            // check if the pizza is already flying
            if (FlyingPizza.IsVisible) return;

            // eat the pizza
            FlyingPizza.IsVisible = true;

            // position pizza at the bottom
            AbsoluteLayout.SetLayoutBounds(FlyingPizza, Pizza.Bounds);

            var buttonBounds = PlaceOrderButton.Bounds;

            // work out where it needs to fly to?
            var size = new Size(buttonBounds.Height, buttonBounds.Height);
            var location = new Point(buttonBounds.Right - size.Width, buttonBounds.Top);
            var chompBounds = new Rectangle(location, size);

            // animate the pizza down
            _ = FlyingPizza.LayoutTo(chompBounds, 500, Easing.SinInOut);
            _ = FlyingPizza.RelRotateTo(90, 500, Easing.SinInOut);

            // do the button chomp
            var buttonChompBounds = new Rectangle(PlaceOrderButton.Bounds.Location,
                new Size(PlaceOrderButton.Width - buttonBounds.Height, buttonBounds.Height));
            await PlaceOrderButton.LayoutTo(buttonChompBounds, 500, Easing.SinInOut);

            _ = FlyingPizza.RelRotateTo(-90, 500, Easing.SinInOut);

            // close the button chomp
            await PlaceOrderButton.LayoutTo(buttonBounds, 500, Easing.SinInOut);

            FlyingPizza.IsVisible = false;
        }


        private async Task RegurgitatePizza()
        {
            // check if the pizza is already flying
            if (FlyingPizza.IsVisible) return;


            var buttonBounds = PlaceOrderButton.Bounds;

            // work out where it needs to fly from?
            var size = new Size(buttonBounds.Height, buttonBounds.Height);
            var location = new Point(buttonBounds.Right - size.Width, buttonBounds.Top);
            var chompBounds = new Rectangle(location, size);

            // position pizza at the bottom
            AbsoluteLayout.SetLayoutBounds(FlyingPizza, chompBounds);

            // eat the pizza
            FlyingPizza.IsVisible = true;

            _ = FlyingPizza.RelRotateTo(90, 500, Easing.SinInOut);

            // open the button
            var buttonChompBounds = new Rectangle(PlaceOrderButton.Bounds.Location,
                new Size(PlaceOrderButton.Width - buttonBounds.Height, buttonBounds.Height));
            await PlaceOrderButton.LayoutTo(buttonChompBounds, 500, Easing.SinInOut);


            // work out where it needs to fly to - pizza is
            var destRect = Pizza.Bounds;

            // animate the pizza up
            _ = FlyingPizza.LayoutTo(destRect, 500, Easing.SinInOut);
            _ = FlyingPizza.RotateTo(Pizza.Rotation, 500, Easing.SinInOut);

            // close the button chomp
            await PlaceOrderButton.LayoutTo(buttonBounds, 500, Easing.SinInOut);

            FlyingPizza.IsVisible = false;
        }



        int currentQuantity = 1;

        private async void DecreaseButton_Clicked(object sender, EventArgs e)
        {
            if (currentQuantity == 1)
                return;
            else
                currentQuantity--;

            QuantityLabel.AnimationOffset = new Point(0, -20);
            QuantityLabel.Text = currentQuantity.ToString();
            UpdatePrice(currentQuantity);
            await RegurgitatePizza();

        }

        private async void IncreaseButton_Clicked(object sender, EventArgs e)
        {
            currentQuantity++;
            QuantityLabel.AnimationOffset = new Point(0, 20);
            QuantityLabel.Text = currentQuantity.ToString();
            UpdatePrice(currentQuantity);
            await PizzaFly();

        }

        private void UpdatePrice(int quantity)
        {
            OrderTotal.AnimationOffset = new Point(0,20);
            OrderTotal.Text = $"{(13.99 * quantity):C}";
        }

        SKPaint rulerPaint = new SKPaint()
        {
            Color = SKColors.White,
            StrokeWidth = 2,
            Style = SKPaintStyle.Stroke
        };

        private void PizzaRuler_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;

            // draw the main ruler line
            canvas.DrawLine(new SKPoint(0, 0), new SKPoint(e.Info.Width, 0), rulerPaint);

            // draw the ticks
            var numberOfTicks = 30;
            var distanceBetweenTicks = e.Info.Width / numberOfTicks;

            for (int i = 0; i <= numberOfTicks; i++)
            {
                // every 5th tick is full height
                float tickHeight = (i % 5) == 0 ? e.Info.Height : (float)(e.Info.Height / 2);
                
                canvas.DrawLine(
                    new SKPoint(i * distanceBetweenTicks, 0),
                    new SKPoint(i * distanceBetweenTicks, tickHeight),
                    rulerPaint);
            }

        }

        private void PizzaRulerThumb_Tapped(object sender, EventArgs e)
        {
            // navigate to the next state
            switch (animState.CurrentState)
            {
                case State.Small:
                    SizeLabel.Text = "Medium";
                    animState.Go(State.Medium);
                    break;
                case State.Medium:
                    SizeLabel.Text = "Large";
                    animState.Go(State.Large);
                    break;
                case State.Large:
                    SizeLabel.Text = "Small";
                    animState.Go(State.Small);
                    break;
            }
        }

        private void PlaceOrderButton_Clicked(object sender, EventArgs e)
        {

        }

        private void BackButton_Tapped(object sender, EventArgs e)
        {
            animState.Go(State.Entrance);
        }
    }
}
