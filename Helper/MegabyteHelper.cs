// -----------------------------------------------------------------------
// <copyright file="MegabyteHelper.cs" company="HP">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Megabyte.Web.Controls.Helper {
    using System;
    using log4net;
    using System.Reflection;
    using System.Web.UI;
    using System.Web.Security;
    using System.Web.UI.HtmlControls;
    using System.Web;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class MegabyteHelper {
        public static T GetDB<T>() {
            return (T)Megabyte.Web.Controls.Context.WebDBContext<T>.Instance.DB;
        }

        public static object RunMethodFromControl(Control c, string method, object[] parametters) {
            Type t = c.GetType();
            MethodInfo mi = t.GetMethod(method);
            return mi.Invoke(c, parametters);
        }

        public static void SetPropertyForEntity(object entity, string property, object value) {
            Type t = entity.GetType();
            PropertyInfo pi = t.GetProperty(property);
            pi.SetValue(entity, value, null);
        }

        public static object GetPropertyFromEntity(object entity, string property) {
            Type t = entity.GetType();
            PropertyInfo pi = t.GetProperty(property);
            return pi.GetValue(entity, null);
        }

        public static Control GetControl(Control Root, string Id) {
            if (Root.ID == Id)
                return Root;

            foreach (Control Ctl in Root.Controls) {
                Control FoundCtl = GetControl(Ctl, Id);
                if (FoundCtl != null)
                    return FoundCtl;
            }
            return null;
        }

        public static string UserName {
            get { return (System.Web.HttpContext.Current.Handler as Page).User.Identity.Name; }
        }

        public static string UserID {
            get { return User.ProviderUserKey.ToString(); }
        }

        public static Guid UserGUID {
            get {                
                return (Guid)User.ProviderUserKey;
            }
        }

        public static MembershipUser User {
            get { return System.Web.Security.Membership.GetUser(UserName); }
        }

        public static HtmlGenericControl GetGenericControl(Page page,Type controltype ,string type, string id, string href) {
            HtmlGenericControl csslink = new HtmlGenericControl(type);
            csslink.ID = id;
            switch (type.ToLower()) {
                case "link":
                    csslink = GetGenericControlCss(page, controltype, csslink, href);
                    break;
                case "script":
                    csslink = GetGenericControlJs(page, controltype, csslink, href);
                    break;
            }

            return csslink;
        }

        private static HtmlGenericControl GetGenericControlCss(Page page,Type type, HtmlGenericControl csslink, string href) {
            csslink.Attributes.Add("href", page.ClientScript.GetWebResourceUrl(type, href));
            csslink.Attributes.Add("type", "text/css");
            csslink.Attributes.Add("rel", "stylesheet");

            return csslink;
        }

        private static HtmlGenericControl GetGenericControlJs(Page page, Type type, HtmlGenericControl csslink, string src) {
            csslink.Attributes.Add("src", page.ClientScript.GetWebResourceUrl(type, src));
            csslink.Attributes.Add("type", "text/javascript");

            return csslink;
        }

        public static void PrintErrorPage(string txt, string PreviousPage) {
            // At this point we have information about the error
            HttpContext ctx = HttpContext.Current;
            Page page = ctx.Handler as Page;
            Exception exception = ctx.Server.GetLastError();

            string errorInfo = // Template Error
                @"<head><link rel='stylesheet' type='text/css' href='" + page.ClientScript.GetWebResourceUrl(typeof(Megabyte.Web.Controls.Page.PageControl), "Megabyte.Web.Controls.CSS.error.css") + @"' /><script>
                function swap(sender,id){
                    if(document.getElementById(id).style.display == 'none'){
                        document.getElementById(id).style.display = '';
                        sender.innerText = 'Less Detail';
                    }
                    else{
                        document.getElementById(id).style.display = 'none';
                        sender.innerText = 'More Detail';
                    }
                }
                </script></head><body>
                <div>An error has occurred</div>
                <div>" + txt + @"</div>                
               <div id='details' style='display:none;'>
               <strong>Offending URL:</strong> " + ctx.Request.Url.ToString() + @"
               <br><strong>Source:</strong> " + exception.Source + @"
               <br><strong>Message:</strong> " + exception.Message + @"
               <br><strong>Stack trace:</strong> " + exception.StackTrace + @"
               </div>";
            errorInfo += "<a style='text-decoration:underline;cursor:pointer;color:#0000FF' onclick='swap(this," + "" + "\"details\");'>More details</a><br>";
            errorInfo += "<a href='" + PreviousPage + "'>Go to Application</a>";

            // Write Response
            ctx.Response.Write(errorInfo);

            // To let the page finish running we clear the error            
            ctx.Server.ClearError();
        }

        public static DateTime? SetDate(DateTime dte) {
            if (dte != null && dte != DateTime.MinValue) {
                return dte.ToUniversalTime();
            }

            return null;
        }

        public static DateTime GetDate(DateTime? dte) {
            return dte.HasValue ? dte.Value.ToLocalTime() : DateTime.MinValue;
        }
    }
}
