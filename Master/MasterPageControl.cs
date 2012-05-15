// -----------------------------------------------------------------------
// <copyright file="MasterPageControl.cs" company="HP">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Megabyte.Web.Controls.Master
{
    using System;
    using System.Web.UI.HtmlControls;
    using log4net;
    using System.Web.UI;
    using System.Web.Security;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MasterPageControl : System.Web.UI.MasterPage, Interfaces.IWebControl
    {   
        protected override void  OnInit(EventArgs e)
        {
            Page.Header.Controls.Add(Helper.MegabyteHelper.GetGenericControl(this.Page, typeof(MasterPageControl), "link", "GLOBALBASE", "Megabyte.Web.Controls.CSS.base.css"));
            Page.Header.Controls.Add(Helper.MegabyteHelper.GetGenericControl(this.Page, typeof(MasterPageControl), "link", "GLOBALSTYLE", "Megabyte.Web.Controls.CSS.style.css"));
            Page.Header.Controls.Add(Helper.MegabyteHelper.GetGenericControl(this.Page, typeof(MasterPageControl), "link", "MSGBOXSTYLE", "Megabyte.Web.Controls.CSS.msgbox.css"));
            Page.Header.Controls.Add(Helper.MegabyteHelper.GetGenericControl(this.Page, typeof(MasterPageControl), "link", "BUTTONSTYLE", "Megabyte.Web.Controls.CSS.buttons.css"));          

            base.OnInit(e);  
        }

        public T GetDB<T>() {
            return Megabyte.Web.Controls.Helper.MegabyteHelper.GetDB<T>();
        }

        public ILog Log {
            get {
                return Web.Controls.Log.Log4NetConfigurator.log;
            }
        }

        public object RunMethodFromControl(Control c, string method, object[] parametters) {
            return Megabyte.Web.Controls.Helper.MegabyteHelper.RunMethodFromControl(c, method, parametters);
        }

        public Control GetControl(Control parent, string id) {
            return Megabyte.Web.Controls.Helper.MegabyteHelper.GetControl(parent, id);
        }

        public string UserName {
            get { return Helper.MegabyteHelper.UserName; }
        }

        public string UserID {
            get { return Helper.MegabyteHelper.UserID; }
        }

        public Guid UserGUID {
            get { return Helper.MegabyteHelper.UserGUID; }
        }

        public MembershipUser User {
            get { return Helper.MegabyteHelper.User; }
        }

        public void SetPropertyForEntity(object entity, string property, object value) {
            Helper.MegabyteHelper.SetPropertyForEntity(entity, property, value);
        }

        public T GetPropertyFromEntity<T>(object entity, string property) {
            return (T) Helper.MegabyteHelper.GetPropertyFromEntity(entity, property);
        }

        public DateTime? SetDate(DateTime dte) {
            return Helper.MegabyteHelper.SetDate(dte);
        }

        public DateTime GetDate(DateTime? dte) {
            return Helper.MegabyteHelper.GetDate(dte);
        }
    }
}
