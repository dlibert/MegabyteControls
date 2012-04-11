// -----------------------------------------------------------------------
// <copyright file="Log4NetConfigurator.cs" company="HP">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Megabyte.Web.Controls.Log {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using log4net;
    using log4net.Config;
    using System.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class Log4NetConfigurator {

        public static readonly ILog log = LogManager.GetLogger("InfoLogger");
        public static readonly ILog logError = LogManager.GetLogger("ErrorLogger");

        public static void Configure()
        {
            XmlConfigurator.Configure(new FileInfo(Web.Controls.Context.WebContext.Instance.Log4NetPath));
        }        
    }
}
