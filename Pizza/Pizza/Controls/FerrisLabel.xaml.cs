using System;
using Xamarin.Forms;
using Pizza.Controls;


namespace Pizza.Controls
{
    public partial class FerrisLabel : Grid
    {
        public static readonly BindableProperty TextStyleProperty = 
            BindableProperty.Create(nameof(TextStyle), 
                typeof(Style), 
                typeof(FerrisLabel), 
                default, propertyChanged: OnTextStyleChanged);
        public Style TextStyle
        {
            get
            {
                return (Style)GetValue(TextStyleProperty);
            }

            set
            {
                SetValue(TextStyleProperty, value);
            }
        }

        static void OnTextStyleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                var control = (FerrisLabel)bindable;

                var value = (Style)newValue;

                control.ApplyTextStyle(value);
            }
            catch (Exception ex)
            {
                // TODO: Handle exception.
            }
        }

        void ApplyTextStyle(Style value)
        {
            CurrentLabel.Style = value;
            NextLabel.Style = value;
        }

        public Point AnimationOffset { get; set; }


        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(FerrisLabel), default(string), propertyChanged: OnTextChanged);
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }

            set
            {
                SetValue(TextProperty, value);
            }
        }

        public Label Current { get; set; }
        public Label Next { get; set; }

        static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                var control = (FerrisLabel)bindable;

                var value = (string)newValue;

                control.ApplyText((string)oldValue, value);
            }
            catch (Exception ex)
            {
                // TODO: Handle exception.
            }
        }
        double distance = 20;

        async void ApplyText(string oldValue, string newValue)
        {
            // update the labels
            Current.Text = oldValue;
            Current.TranslationY = 0;
            Current.TranslationX = 0;
            Current.Opacity = 1;

            // set the starting positions
            Current.TranslationY = 0;
            _ = Current.TranslateTo(-AnimationOffset.X, -AnimationOffset.Y);
            _ = Current.FadeTo(0);

            // animate in the next label
            Next.Text = newValue;
            Next.TranslationY = AnimationOffset.Y;
            Next.TranslationX = AnimationOffset.X;
            Next.Opacity = 0;
            _ = Next.TranslateTo(0, 0);
            await Next.FadeTo(1);

            // recycle the views
            Current = NextLabel;
            Next = CurrentLabel;
        }

        public FerrisLabel()
        {
            InitializeComponent();
            Current = CurrentLabel;
            Next = NextLabel;

        }
    }
}