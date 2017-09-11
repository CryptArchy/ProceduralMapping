using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Library.Collections
{
    public class Configuration : Dictionary<string, object>
    {
        public T Extract<T>(string key, T defaultValue)
        {
            object rawval = null;
            if (this.TryGetValue(key, out rawval))
                return (T)rawval;
            else
                return defaultValue;
        }
    }
}
