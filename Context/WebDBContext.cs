// -----------------------------------------------------------------------
// <copyright file="WebContext.cs" company="HP">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Megabyte.Web.Controls.Context
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Reflection;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class WebDBContext<T>
    {
        private WebDBContext()
        {
            Type [] t = new Type[0];
            ConstructorInfo ci = typeof(T).GetConstructor(t);
            _db = (T) ci.Invoke(null);
        }

        public static WebDBContext<T> Instance
        {
            get
            {
                if (_instance == null)                
                    _instance = new WebDBContext<T>();                

                return _instance;
            }
        }

        public T DB
        {
            get
            {
                return _db;
            }
        }

        private T _db;
        private static WebDBContext<T> _instance = null;
    }
}
