// -----------------------------------------------------------------------
// <copyright file="InfoBarControl.cs" company="HP">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Megabyte.Web.Controls.InfoBar {
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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [
        AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal),
        AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal),
        DefaultProperty("Text"),
        ParseChildren(true, "Text"),
        ToolboxData("<{0}:InfoBarControl runat=server></{0}:InfoBarControl>")]
    public class InfoBarControl : WebControl {
        [Category("Megabyte Properties")]
        public string Text { get; set; }
        [Category("Megabyte Properties")]
        private string LinkId { get { return this.ClientID + this.ClientIDSeparator + "infobarlink"; } }

        public InfoBarControl()
            : base("div") {
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void AddAttributesToRender(HtmlTextWriter writer) {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "success");
            base.AddAttributesToRender(writer);
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void RenderContents(HtmlTextWriter writer) {            
            CreateInfoBar(writer);
        }

        private void CreateInfoBar(HtmlTextWriter writer) {
            writer.WriteBeginTag("a");
            writer.WriteAttribute("id", this.LinkId);
            writer.WriteAttribute("onclick", "this.parentNode.style.display='none';");
            writer.WriteLine(">");         
            writer.WriteBeginTag("img");
            writer.WriteAttribute("src", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Megabyte.Web.Controls.Images.close.png"));
            writer.WriteLine(" />");
            writer.WriteEndTag("a");
            writer.WriteBeginTag("p");
            writer.WriteLine(">");            
            writer.Write(this.Text);
            writer.WriteEndTag("p");
        }

        protected override void OnInit(EventArgs e) {
            Page.Header.Controls.Add(Helper.MegabyteHelper.GetGenericControl(this.Page, this.GetType(), "link", "INFOBARCSS", "Megabyte.Web.Controls.CSS.msgbox.css"));
            Page.Header.Controls.Add(Helper.MegabyteHelper.GetGenericControl(this.Page, this.GetType(), "script", "INFOBARJS", "Megabyte.Web.Controls.JScript.InfoBar.js"));            
            base.OnInit(e);
        }
    }

    public enum InfoBarRenderType {
        info,
        success,
        validation,
        warning,
        Error
    }
}
