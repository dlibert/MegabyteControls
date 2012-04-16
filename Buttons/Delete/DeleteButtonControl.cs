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
    public class DeleteButtonControl : WebControl, ICallbackEventHandler {

        public string Text { get; set; }
        public string CommandArgument { get; set; }
        public string EndCallback { get; set; }
        public string CallbackUrl { get; set; }
        public bool DisplayCallbackProgressBar { get; set; }
        public bool UseRedirect { get; set; }
        public delegate void OnDeleteEventHandler(object sender, DeleteEventArgs e);
        public event OnDeleteEventHandler Delete;        


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
            writer.WriteBeginTag("a");            
            writer.WriteAttribute("href", "javascript:var c = confirm('"+ this.Text.Replace("'","\'") +"'); if(c){ " + this._callbackFunctionName + "('"+ this.CommandArgument +"'); }");
            writer.WriteLine(">");         
            writer.WriteBeginTag("img");
            writer.WriteAttribute("alt", this.ToolTip);
            writer.WriteAttribute("src", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Megabyte.Web.Controls.Images.delete_24x24.png"));
            writer.WriteLine(" />");
            writer.WriteEndTag("a");            
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
        }

        private string GetScript() {
            StringBuilder sb = new StringBuilder();
            string cbref = this.Page.ClientScript.GetCallbackEventReference(this, "arg", "EndCallBackDeleted_" + this.ID, "context");
            sb.Append("function ");
            sb.Append(_callbackFunctionName);
            sb.Append("(arg,context){");
            if(DisplayCallbackProgressBar) sb.Append(" PleaseWaitRT(); ");
            sb.Append(cbref);
            sb.Append("}");
            sb.Append("function EndCallBackDeleted_");
            sb.Append(this.ID);
            sb.Append("(result,context){ ");
            if(DisplayCallbackProgressBar) sb.Append("UnPleaseWaitRT(); ");
            if (!this.UseRedirect) sb.Append(this.EndCallback);
            else { sb.Append("window.location='"); sb.Append(this.CallbackUrl); sb.Append("';"); }
            sb.Append(" }");

            return sb.ToString();
        }

        protected override void OnPreRender(EventArgs e) {
            this._callbackFunctionName = "UseButtonDeleteCallback_" + this.ID;
            this.script = GetScript();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ButtonDeleteCallbackScript_" + this.ID, this.script, true);
        }

        private string _callbackFunctionName = String.Empty;
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