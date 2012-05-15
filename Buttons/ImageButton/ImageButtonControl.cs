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
    DefaultProperty("ImageURL"),
    ParseChildren(true, "ImageURL"),
    ToolboxBitmap(@"C:\Users\dl\Documents\Visual Studio 2010\Projects\Megabyte.Web.Controls\Megabyte.Web.Controls\Buttons\ImageButton\ImageButton.bmp"),
    //ToolboxItem(true),
    ToolboxData("<{0}:ImageButtonControl runat=server></{0}:ImageButtonControl>")]
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImageButtonControl : WebControl, IPostBackEventHandler, ICallbackEventHandler {
        
        public override string ToolTip {
            get {
                return base.ToolTip;
            }
            set {
                base.ToolTip = value;
            }
        }

        [Category("Megabyte Properties"), UrlProperty()]
        public string ImageURL { get; set; }
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
        /// <summary>
        /// Width in pixels
        /// </summary>
        //public int Width { get; set; }
        /// <summary>
        /// Height in pixels
        /// </summary>
        //public int Height { get; set; }
        public delegate void OnClickImageButtonEventHandler(object sender, ImageButtonEventArgs e);        
        public event OnClickImageButtonEventHandler Click;

        public ImageButtonControl()
            : base("div") {
                DefaultValues();
        }

        public void RaisePostBackEvent(string eventArgument) {
            if (Click != null) Click(this, new ImageButtonEventArgs(eventArgument));

        }

        public string GetCallbackResult() {
            return _callbackResult;
        }

        public void RaiseCallbackEvent(string eventArgument) {
            if (Click != null)
            {
                string result = String.Empty;
                ImageButtonEventArgs e = new ImageButtonEventArgs(eventArgument, result);
                Click(this, e);
                _callbackResult = e.Result;
            }
            else
                _callbackResult = String.Empty;
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void AddAttributesToRender(HtmlTextWriter writer) {
            //writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Width + "px");
            //writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height + "px");
            base.AddAttributesToRender(writer);
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void RenderContents(HtmlTextWriter writer) {
            CreateImageButton(writer);
        }

        private string GetScript() {
            this._callbackObjectName = this.ClientID + "_CALLBACK";
            string bfcallback = "function (){ PleaseWaitRT(); " + this.BeforeCallback + "}";
            string endcallback = "function (result,context){ UnPleaseWaitRT(); " + this.EndCallback + "}";
            string cbref = "function() { " + this.Page.ClientScript.GetCallbackEventReference(this, "'" + this.CommandArgument + "'", endcallback, "null") + "}";
            StringBuilder script = new StringBuilder();

            script.AppendFormat("var {0} = new Callback({1},{2},{3});", this._callbackObjectName, bfcallback, endcallback, cbref);

            return script.ToString();
        }

        private void CreateImageButton(HtmlTextWriter writer) { 
           
            writer.WriteBeginTag("a");
            if (this.AutoPostBack)
                writer.WriteAttribute("href", "javascript:"+ this.Page.ClientScript.GetPostBackEventReference(this, this.CommandArgument));
            else if (this.UseCallBack) {                
                writer.WriteAttribute("href", "javascript:" + _callbackObjectName + ".PerformCallback();");
            }

            writer.WriteLine(">");       
            writer.WriteBeginTag("img");
            writer.WriteAttribute("width", this.Width.ToString());
            writer.WriteAttribute("height", this.Height.ToString());
            writer.WriteAttribute("alt", this.ToolTip);
            writer.WriteAttribute("border", "0");
            writer.WriteAttribute("src", this.ResolveUrl(this.ImageURL));
            writer.WriteLine(" />");
            writer.WriteEndTag("a");            
        }

        private void DefaultValues() {
            this.AutoPostBack = true;
            this.UseCallBack = false;
            this.Width = Unit.Pixel(16);
            this.Height = Unit.Pixel(16);
        }

        protected override void OnPreRender(EventArgs e) {
            this.script = GetScript();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ImageButtonCallbackScript_" + this.ID, this.script, true);
        }

        private string _callbackObjectName = String.Empty;
        private string script = String.Empty;
        private static string _callbackResult = null;        
    }

    public class ImageButtonEventArgs : EventArgs {
        public string Argument { get; set; }
        public string Result { get; set; }

        public ImageButtonEventArgs(string arg) {
            this.Argument = arg;
            this.Result = String.Empty;
        }

        public ImageButtonEventArgs(string arg, string result) {
            this.Argument = arg;
            this.Result = result;
        }
    }
}