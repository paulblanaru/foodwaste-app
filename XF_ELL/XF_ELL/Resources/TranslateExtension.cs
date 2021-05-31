using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF_ELL.Resources
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private const string ResourceId = "XF_ELL.Resources.Resources";
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                if (string.IsNullOrEmpty(Text))
                {
                    return null;
                }

                var resourceManager = new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);
                return resourceManager.GetString(Text, CultureInfo.CurrentCulture);
                
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
