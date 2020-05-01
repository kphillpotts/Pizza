using System;
using Xamarin.Forms;
using Pizza.Controls;


namespace Pizza.Controls
{
    public partial class PillLabel : Frame
    {
        public static readonly BindableProperty LabelStyleProperty 
            = BindableProperty.Create(nameof(LabelStyle),
                typeof(Style), 
                typeof(PillLabel), default, propertyChanged: OnLabelStyleChanged);
        public Style LabelStyle
        {
            get
            {
                return (Style)GetValue(LabelStyleProperty);
            }

            set
            {
                SetValue(LabelStyleProperty, value);
            }
        }

        static void OnLabelStyleChanged(BindableObject bindable, 
            object oldValue, 
            object newValue)
        {
            try
            {
                var control = (PillLabel)bindable;

                var value = newValue;

                control.ApplyLabelStyle(value);
            }
            catch (Exception ex)
            {
                // TODO: Handle exception.
            }
        }

        void ApplyLabelStyle(object value)
        {
            PillText.Style = ((Style)value);
        }

        public static readonly BindableProperty FrameStyleProperty = 
            BindableProperty.Create(nameof(FrameStyle), typeof(Style), typeof(PillLabel), default, propertyChanged: OnFrameStyleChanged);
        public Style FrameStyle
        {
            get
            {
                return (Style)GetValue(FrameStyleProperty);
            }

            set
            {
                SetValue(FrameStyleProperty, value);
            }
        }

        static void OnFrameStyleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                var control = (PillLabel)bindable;

                var value = newValue;

                control.ApplyFrameStyle((Style)value);
            }
            catch (Exception ex)
            {
                // TODO: Handle exception. 
            }
        }

        void ApplyFrameStyle(Style value)
        {
            PillFrame.Style = value;
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(PillLabel), default(string), propertyChanged: OnTextChanged);
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

        static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                var control = (PillLabel)bindable;

                var value = (string)newValue;

                control.ApplyText(value);
            }
            catch (Exception ex)
            {
                // TODO: Handle exception.
            }
        }

        void ApplyText(string value)
        {
            PillText.Text = value;
        }

        public PillLabel()
        {
            InitializeComponent();
        }
    }
}