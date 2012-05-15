// -----------------------------------------------------------------------
// <copyright file="DeleteButtonControl.cs" company="HP">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

internal sealed class ResourceFinder { };

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
    //ToolboxBitmap(typeof(ResourceFinder), "Megabyte.Web.Controls.Buttons.SaveButtonControl.bmp"),
    ToolboxBitmap(@"C:\Users\dl\Documents\Visual Studio 2010\Projects\Megabyte.Web.Controls\Megabyte.Web.Controls\Buttons\Save\SaveButtonControl.bmp"),
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
        [Category("Megabyte Properties")]
        public bool AutoPostBack { get; set; }
        [Category("Megabyte Properties")]
        public string CommandArgument { get; set; }
        [Category("Megabyte Properties")]
        public bool UseCallBack { get; set; }
        [Category("Megabyte Properties")]
        public string BeforeCallback { get; set; }
        [Category("Megabyte Properties")]
        public string EndCallback { get; set; }
        [Category("Megabyte Properties")]
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
            this._callbackObjectName = this.ClientID + "_CALLBACK";
            string bfcallback = "function (){ PleaseWaitRT(); " + this.BeforeCallback + "}";
            string endcallback = "function (result,context){ UnPleaseWaitRT(); " + this.EndCallback + "}";
            string cbref = "function() { " + this.Page.ClientScript.GetCallbackEventReference(this, "'"+ this.CommandArgument +"'",endcallback, "null") + "}";
            StringBuilder script = new StringBuilder();

            script.AppendFormat("var {0} = new Callback({1},{2},{3});", this._callbackObjectName, bfcallback, endcallback,cbref);

            return script.ToString();
        }

        private void CreateSaveButton(HtmlTextWriter writer) { 
           
            writer.WriteBeginTag("a");
            if (this.AutoPostBack)
                writer.WriteAttribute("href", "javascript:"+ this.Page.ClientScript.GetPostBackEventReference(this, this.CommandArgument));
            else if (this.UseCallBack) {
                writer.WriteAttribute("href", "javascript:" + _callbackObjectName + ".PerformCallback();");
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
            if (this.Page.Header.FindControl("GLOBALCALLBACKJS") == null && this.UseCallBack)
                Page.Header.Controls.Add(Helper.MegabyteHelper.GetGenericControl(this.Page, this.GetType(), "script", "GLOBALCALLBACKJS", "Megabyte.Web.Controls.JScript.Callback.js"));
        }

        protected override void OnPreRender(EventArgs e) {
            this.script = GetScript();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ButtonSaveCallbackScript_" + this.ID, this.script, true);
        }

        private string _callbackObjectName = String.Empty;
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