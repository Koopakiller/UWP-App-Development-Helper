using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.UI
{
    public class AutoTypeTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item)
        {
            return this.SelectTemplateCore(item, null);
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item == null)
            {
                return null;
            }
            var typeName = item.GetType().FullName;
            typeName = typeName.Replace("ViewModel", "View");
            var type = Type.GetType(typeName);
            if (type == null)
            {
                return null;
            }

            var s = $@"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                                     xmlns:view=""using:{type.Namespace}"">                           
                           <Frame SourcePageType=""view:{type.Name}""/>
                       </DataTemplate>";
            
            return (DataTemplate)XamlReader.Load(s);
        }
    }
}
