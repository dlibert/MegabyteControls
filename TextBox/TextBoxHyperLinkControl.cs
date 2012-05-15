// -----------------------------------------------------------------------
// <copyright file="MenuControl.cs" company="HP">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Megabyte.Web.Controls.TextBoxes {
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
    using System.Drawing;

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
        ToolboxData("<{0}:TextBoxHyperLinkControl runat=server></{0}:TextBoxHyperLinkControl>")]
    public class TextBoxHyperLinkControl : WebControl {

        [Category("Megabyte Properties")]
        public TextBoxHyperLinkTarget Target { get; set; }
        [Category("Megabyte Properties"), UrlProperty()]
        public string URL { get; set; }
        [Category("Megabyte Properties")]
        public bool ReadOnly { get; set; }
        [Category("Megabyte Properties")]
        public string Text { get; set; }
        [Category("Megabyte Properties")]
        public string BackgroundColorReadOnly { get; set; }

        public TextBoxHyperLinkControl()
            : base(HtmlTextWriterTag.Input) {
                this.BackgroundColorReadOnly = "#DDDDDD";
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void AddAttributesToRender(HtmlTextWriter writer) {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "input");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, this.Text);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "blue");
            writer.AddStyleAttribute(HtmlTextWriterStyle.TextDecoration, "underline");

            if (this.ReadOnly) {
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, GetJSURL());
                writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly");
                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, this.BackgroundColorReadOnly);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "pointer");
            } else writer.AddAttribute("ondblclick", GetJSURL());

            base.AddAttributesToRender(writer);
            
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void RenderContents(HtmlTextWriter writer) {
        }

        private string GetJSURL() {
            switch (this.Target) {
                case TextBoxHyperLinkTarget.New:
                    return "window.location=\"" + this.ResolveUrl(this.URL) + "\";";
                case TextBoxHyperLinkTarget.Self:
                    return "window.open(\"" + this.ResolveUrl(this.URL) + "\");";
                default:
                    break;
            }

            return String.Empty;
        }     
    }

    public enum TextBoxHyperLinkTarget {
        New,
        Self
    }
}
