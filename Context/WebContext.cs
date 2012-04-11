// -----------------------------------------------------------------------
// <copyright file="WebContext.cs" company="HP">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Megabyte.Web.Controls.Context {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class WebContext {

        public string Log4NetPath { get; set; }

        private WebContext() {

        }

        public static WebContext Instance {
            get {
                if (_instance == null) {
                    _instance = new WebContext();
                }

                return _instance;
            }
        }

        private static WebContext _instance = null;
        private bool _ismodified = false;
    }
}
