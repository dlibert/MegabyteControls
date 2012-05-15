// -----------------------------------------------------------------------
// <copyright file="MenuControl.cs" company="HP">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Megabyte.Web.Controls.Modal {
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
        ToolboxData("<{0}:ModalControl runat=server></{0}:ModalControl>")]
    public class ModalControl : WebControl {
        [Category("Megabyte Properties")]
        public string Text { get; set; }

        public ModalControl()
            : base("div") {            
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void AddAttributesToRender(HtmlTextWriter writer) {            
            //base.AddAttributesToRender(writer);
            CreateModal(writer);
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void RenderContents(HtmlTextWriter writer) {
            //base.RenderContents(writer);
        }

        private void CreateModal(HtmlTextWriter writer) {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "modalBackground");         
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "grayScreen");
        }

        protected override void OnInit(EventArgs e) {
            Page.Header.Controls.Add(Helper.MegabyteHelper.GetGenericControl(this.Page, this.GetType(), "link", "GLOBALMODALCSS", "Megabyte.Web.Controls.CSS.modal.css"));        
            Page.Header.Controls.Add(Helper.MegabyteHelper.GetGenericControl(this.Page, this.GetType(), "script", "GLOBALMODALJS", "Megabyte.Web.Controls.JScript.Modal.js"));
            
            base.OnInit(e);            
        }

        
    }
}
