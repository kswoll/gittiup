using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Gittiup.Utils
{
    public static class VisualTreeExtensions
    {
        public static IEnumerable<T> FindVisualDescendants<T>(this DependencyObject dependencyObject)
        {
            if (dependencyObject != null)
            {
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
                {
                    var child = VisualTreeHelper.GetChild(dependencyObject, i);
                    if (child is T)
                    {
                        yield return (T)(object)child;
                    }

                    foreach (T childOfChild in FindVisualDescendants<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}