using System.Web;
using Microsoft.Practices.Unity;

namespace DI
{
    public class PerRequestLifetimeManager : LifetimeManager
    {
        private readonly object _key = new object();

        public override object GetValue()
        {
            if (HttpContext.Current != null &&
                HttpContext.Current.Items.Contains(_key))
                return HttpContext.Current.Items[_key];
            else
                return null;
        }

        public override void RemoveValue()
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Items.Remove(_key);
        }

        public override void SetValue(object newValue)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Items[_key] = newValue;
        }
    }
}