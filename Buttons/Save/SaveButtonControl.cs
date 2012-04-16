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
    using Megabyte.Web.Controls.Helper;
using System.Drawing;

    [
    AspNetHostingPermission(SecurityAction.Demand,
    Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand,
    Level = AspNetHostingPermissionLevel.Minimal),
    DefaultProperty("Tooltip"),
    ParseChildren(true, "Tooltip"),   
    ToolboxBitmap("save_16x16.bmp"),
    ToolboxData("<{0}:SaveButtonControl runat=server></{0}:SaveButtonControl>")]
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SaveButtonControl : WebControl, IPostBackEventHandler, ICallbackEventHandler {
        
        public override string ToolTip {
            get {
                return base.ToolTip;
            }
            set {
                base.ToolTip = value;
            }
        }
        public bool AutoPostBack { get; set; }
        public string CommandArgument { get; set; }
        public bool UseCallBack { get; set; }
        public string EndCallback { get; set; }
        public bool DisplayCallbackProgressBar { get; set; }
        public delegate void OnSaveEventHandler(object sender, SaveEventArgs e);        
        public event OnSaveEventHandler Save;        

        public SaveButtonControl()
            : base("div") {
                DefaultValues();
        }

        public void RaisePostBackEvent(string eventArgument) {
            if(Save != null) Save(this, new SaveEventArgs(eventArgument));

        }

        public string GetCallbackResult() {
            return _callbackResult;
        }

        public void RaiseCallbackEvent(string eventArgument) {
            if (Save != null)
            {
                string result = String.Empty;
                SaveEventArgs e = new SaveEventArgs(eventArgument, result);
                Save(this, e);
                _callbackResult = e.Result;
            }
            else
                _callbackResult = String.Empty;
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void AddAttributesToRender(HtmlTextWriter writer) {
            base.AddAttributesToRender(writer);
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void RenderContents(HtmlTextWriter writer) {            
            CreateSaveButton(writer);
        }

        private string GetScript() {
            string script = String.Empty;
            string cbref = this.Page.ClientScript.GetCallbackEventReference(this, "arg", "EndCallBackSaved_" + this.ID, "context");
            script = @"function "+ _callbackFunctionName + @"(arg,context){
                    PleaseWaitRT();
                    __theFormPostData = '';
                    WebForm_InitCallback();
                    " + cbref + @";
                }";

            script += "function EndCallBackSaved_" + this.ID + "(result,context){ UnPleaseWaitRT(); " + this.EndCallback + "}";

            return script;
        }

        private void CreateSaveButton(HtmlTextWriter writer) { 
           
            writer.WriteBeginTag("a");
            if (this.AutoPostBack)
                writer.WriteAttribute("href", "javascript:"+ this.Page.ClientScript.GetPostBackEventReference(this, this.CommandArgument));
            if (this.UseCallBack) {                
                writer.WriteAttribute("href", "javascript:" + _callbackFunctionName + "('"+ this.CommandArgument +"');");
            }

            writer.WriteLine(">");         
            writer.WriteBeginTag("img");
            writer.WriteAttribute("alt", this.ToolTip);
            writer.WriteAttribute("border", "0");
            writer.WriteAttribute("src", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Megabyte.Web.Controls.Images.save_24x24.png"));
            writer.WriteLine(" />");
            writer.WriteEndTag("a");            
        }

        private void DefaultValues() {
            this.AutoPostBack = true;
        }

        protected override void OnLoad(EventArgs e) {        
            if (this.Page.Header.FindControl("GLOBALMODALCSS") == null)
                Page.Header.Controls.Add(Helper.MegabyteHelper.GetGenericControl(this.Page, this.GetType(), "link", "GLOBALMODALCSS", "Megabyte.Web.Controls.CSS.modal.css"));
            if (this.Page.Header.FindControl("GLOBALMODALJS") == null)
                Page.Header.Controls.Add(Helper.MegabyteHelper.GetGenericControl(this.Page, this.GetType(), "script", "GLOBALMODALJS", "Megabyte.Web.Controls.JScript.Modal.js"));
        }

        protected override void OnPreRender(EventArgs e) {
            this._callbackFunctionName = "UseButtonSaveCallback_" + this.ID;
            this.script = GetScript();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ButtonSaveCallbackScript_" + this.ID, this.script, true);
        }

        private string _callbackFunctionName = String.Empty;
        private string script = String.Empty;
        private static string _callbackResult = null;        
    }

    public class SaveEventArgs : EventArgs {
        public string Argument { get; set; }
        public string Result { get; set; }

        public SaveEventArgs(string arg) {
            this.Argument = arg;
            this.Result = String.Empty;
        }

        public SaveEventArgs(string arg, string result) {
            this.Argument = arg;
            this.Result = result;
        }
    }
}