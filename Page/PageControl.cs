﻿// -----------------------------------------------------------------------
// <copyright file="PageControl.cs" company="HP">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Megabyte.Web.Controls.Page {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using log4net;
    using System.Web.UI;
    using System.Reflection;
    using System.Web.Security;
    using System.Web;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PageControl : System.Web.UI.Page, Interfaces.IWebControl {
        public T GetDB<T>() { //plop
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

        public Control GetControl(Control parent, string id) { // plop2
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

        protected void Page_Error(object sender, EventArgs e) {
            string AppPath = System.IO.Path.Combine(HttpContext.Current.Request.Url.GetLeftPart(System.UriPartial.Authority), HttpContext.Current.Request.ApplicationPath.TrimEnd(new char[] { '/' }));
            string url = (Page.Request.UrlReferrer != null) ? Page.Request.UrlReferrer.ToString() : (AppPath + System.Web.Security.FormsAuthentication.DefaultUrl);
            Helper.MegabyteHelper.PrintErrorPage("", url); 
        }

        public DateTime? SetDate(DateTime dte) {
            return Helper.MegabyteHelper.SetDate(dte);
        }

        public DateTime GetDate(DateTime? dte) {
            return Helper.MegabyteHelper.GetDate(dte);
        }
    }
}
