// -----------------------------------------------------------------------
// <copyright file="IWebControl.cs" company="HP">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Megabyte.Web.Controls.Interfaces {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using log4net;
    using System.Web.UI;
    using System.Web.Security;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IWebControl {
        
        T GetDB<T>();

        ILog Log {get;}

        object RunMethodFromControl(Control c, string method, object[] parametters);

        void SetPropertyForEntity(object entity, string property, object value);

        T GetPropertyFromEntity<T>(object entity, string property);

        Control GetControl(Control parent, string id);

        string UserName { get; }

        string UserID { get; }
        
        Guid UserGUID {get; }

        MembershipUser User { get; }

        DateTime? SetDate(DateTime dte);

        DateTime GetDate(DateTime? dte);
    }
}
