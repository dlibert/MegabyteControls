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
    using System.Drawing;

    [
    AspNetHostingPermission(SecurityAction.Demand,
    Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand,
    Level = AspNetHostingPermissionLevel.Minimal),
    DefaultProperty("Text"),
    ParseChildren(true, "Text"),
    ToolboxBitmap(@"C:\Users\dl\Documents\Visual Studio 2010\Projects\Megabyte.Web.Controls\Megabyte.Web.Controls\Buttons\Delete\DeleteButtonControl.bmp"),
    ToolboxData("<{0}:DeleteButtonControl runat=server></{0}:DeleteButtonControl>")]
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DeleteButtonControl : WebControl, ICallbackEventHandler, IPostBackEventHandler {

        public override string ToolTip {
            get {
                return base.ToolTip;
            }
            set {
                base.ToolTip = value;
            }
        }
        [Category("Megabyte Properties")]
        public string Text { get; set; }
        [Category("Megabyte Properties")]
        public string CommandArgument { get; set; }
        [Category("Megabyte Properties")]
        public string BeforeCallback { get; set; }
        [Category("Megabyte Properties")]
        public string EndCallback { get; set; }
        [Category("Megabyte Properties")]
        public string CallbackUrl { get; set; }
        [Category("Megabyte Properties")]
        public bool AutoPostBack { get; set; }
        [Category("Megabyte Properties")]
        public bool UseCallBack { get; set; }
        [Category("Megabyte Properties")]
        public bool DisplayCallbackProgressBar { get; set; }
        [Category("Megabyte Properties")]
        public bool UseRedirect { get; set; }
        public delegate void OnDeleteEventHandler(object sender, DeleteEventArgs e);
        public event OnDeleteEventHandler Delete;        


        public DeleteButtonControl()
            : base("div") {
                DefaultValues();
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
            writer.WriteBeginTag("a");
            if(this.AutoPostBack)
                writer.WriteAttribute("href", "javascript:var c = confirm('" + this.Text.Replace("'", "\'") + "'); if(c){ " + this.Page.ClientScript.GetPostBackEventReference(this, this.CommandArgument) + "}");
            else if(this.UseCallBack)                
                writer.WriteAttribute("href", "javascript:var c = confirm('" + this.Text.Replace("'", "\'") + "'); if(c){ " + this._callbackObjectName + ".PerformCallback(); }");
            writer.WriteLine(">");         
            writer.WriteBeginTag("img");
            writer.WriteAttribute("alt", this.ToolTip);
            writer.WriteAttribute("src", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Megabyte.Web.Controls.Images.delete_24x24.png"));
            writer.WriteLine(" />");
            writer.WriteEndTag("a");            
        }

        public void RaisePostBackEvent(string eventArgument) {
            if (Delete != null) {
                string result = String.Empty;
                DeleteEventArgs e = new DeleteEventArgs(eventArgument, result);
                Delete(this, e);
            }
        }

        public string GetCallbackResult() {
            return _callbackResult;
        }

        public void RaiseCallbackEvent(string eventArgument) {
            if (Delete != null) {
                string result = String.Empty;
                DeleteEventArgs e = new DeleteEventArgs(eventArgument, result);
                Delete(this, e);
                _callbackResult = e.Result;
            } else
                _callbackResult = String.Empty;
        }

        protected override void OnLoad(EventArgs e) {
            if (this.Page.Header.FindControl("GLOBALMODALCSS") == null)
                Page.Header.Controls.Add(Helper.MegabyteHelper.GetGenericControl(this.Page, this.GetType(), "link", "GLOBALMODALCSS", "Megabyte.Web.Controls.CSS.modal.css"));
            if (this.Page.Header.FindControl("GLOBALMODALJS") == null)
                Page.Header.Controls.Add(Helper.MegabyteHelper.GetGenericControl(this.Page, this.GetType(), "script", "GLOBALMODALJS", "Megabyte.Web.Controls.JScript.Modal.js"));
            if (this.Page.Header.FindControl("GLOBALCALLBACKJS") == null && this.UseCallBack)
                Page.Header.Controls.Add(Helper.MegabyteHelper.GetGenericControl(this.Page, this.GetType(), "script", "GLOBALCALLBACKJS", "Megabyte.Web.Controls.JScript.Callback.js"));
        }

        private void DefaultValues() {
            this.AutoPostBack = true;
        }

        private string GetScript() {
            this._callbackObjectName = this.ClientID + "_CALLBACK";
            string bfcallback = "function (){ PleaseWaitRT(); " + this.BeforeCallback + "}";
            string endcallback = "function (result,context){ ";
            
            if (!this.UseRedirect) endcallback += "UnPleaseWaitRT(); " + this.EndCallback + "}";
            else endcallback += this.EndCallback + " window.location='" + this.CallbackUrl + "'; }";

            string cbref = "function() { " + this.Page.ClientScript.GetCallbackEventReference(this, "'" + this.CommandArgument + "'", endcallback, "null") + "}";
            StringBuilder script = new StringBuilder();

            script.AppendFormat("var {0} = new Callback({1},{2},{3});", this._callbackObjectName, bfcallback, endcallback, cbref);

            return script.ToString();
        }

        protected override void OnPreRender(EventArgs e) {
            this.script = GetScript();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ButtonDeleteCallbackScript_" + this.ID, this.script, true);
        }

        private string _callbackObjectName = String.Empty;
        private string script = String.Empty;
        private static string _callbackResult = null;
    }

    public class DeleteEventArgs : EventArgs {
        public string Argument { get; set; }
        public string Result { get; set; }

        public DeleteEventArgs(string arg) {
            this.Argument = arg;
            this.Result = String.Empty;
        }

        public DeleteEventArgs(string arg, string result) {
            this.Argument = arg;
            this.Result = result;
        }
    }
}