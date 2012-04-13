// -----------------------------------------------------------------------
// <copyright file="DeleteButtonControl.cs" company="HP">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Megabyte.Web.Controls.Buttons {
    using System.Web.UI.WebControls;
    using System.Web.UI;
    using System.Collections.Generic;
    using System;
    using System.Web.UI.HtmlControls;
    using System.ComponentModel;
    using System.Web;
    using System.Security.Permissions;
    using System.Linq;
    using System.Text;

    [
    AspNetHostingPermission(SecurityAction.Demand,
    Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand,
    Level = AspNetHostingPermissionLevel.Minimal),
    DefaultProperty("Text"),
    ParseChildren(true, "Text"),
    ToolboxData("<{0}:DeleteButtonControl runat=server></{0}:DeleteButtonControl>")]
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DeleteButtonControl : WebControl {

        public string Text { get; set; }
        public string CallBackControlId { get; set; }

        public DeleteButtonControl()
            : base("div") {
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void AddAttributesToRender(HtmlTextWriter writer) {
            base.AddAttributesToRender(writer);
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void RenderContents(HtmlTextWriter writer) {            
            CreateDeleteButton(writer);
        }

        private void CreateDeleteButton(HtmlTextWriter writer) {
            DevExpress.Web.ASPxCallback.ASPxCallback cb = GetCallBack();
            writer.WriteBeginTag("a");            
            writer.WriteAttribute("href", "javascript:var c = confirm('"+ this.Text.Replace("'","\'") +"'); if(c){ " + cb.ClientInstanceName + ".PerformCallback();}");
            writer.WriteLine(">");         
            writer.WriteBeginTag("img");
            writer.WriteAttribute("src", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Megabyte.Web.Controls.Images.delete_24x24.png"));
            writer.WriteLine(" />");
            writer.WriteEndTag("a");            
        }

        private DevExpress.Web.ASPxCallback.ASPxCallback GetCallBack(){
            return Helper.MegabyteHelper.GetControl(this.Page,this.CallBackControlId) as DevExpress.Web.ASPxCallback.ASPxCallback;
        }
    }
}