// -----------------------------------------------------------------------
// <copyright file="MenuControl.cs" company="HP">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Megabyte.Web.Controls.Menu
{
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

    public enum MenuOrientation
    {
        Horizontal,
        Vertical
    }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [
        AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal),
        AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal),
        DefaultProperty("MainItems"),
        ParseChildren(true, "MainItems"),
        ToolboxData("<{0}:MenuControl runat=server></{0}:MenuControl>")]
    public class MenuControl : WebControl
    {
        [Category("Megabyte Properties")]
        public MenuItemCollection MainItems { get; set; }
        [Category("Megabyte Properties")]
        public MenuItemCollection SecondItems { get; set; }
        [Category("Megabyte Properties")]
        public MenuOrientation Orientation { get; set; }
        [Category("Megabyte Properties")]
        public MenuItem SelectedItem
        {
            get
            {
                object o = Page.Session[_selectedItemKey] ?? null;
                return o as MenuItem;
            }
            set
            {
                Page.Session[_selectedItemKey] = value;
            }
        }

        public event MenuClickEventHandler OnMenuClick;

        public MenuControl()
            : base("div")
        {
            this._selectedItemKey = "Megabyte.Web.Controls.Menu.MenuControl";
            this.MainItems = new MenuItemCollection();
            this.SecondItems = new MenuItemCollection();
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void RenderContents(HtmlTextWriter writer)
        {
            CreateMenu(writer, this.MainItems.GetEnumerator(), "mbmenu", "");
            CreateMenu(writer, this.SecondItems.GetEnumerator(), "mbmenuadmin", "newRequest");

            base.RenderContents(writer);
        }

        private void CreateMenu(HtmlTextWriter writer, IEnumerator<MenuItem> items, string cssclass, string liId)
        {
            writer.WriteBeginTag("ul");
            writer.WriteAttribute("class", cssclass);
            writer.Write(">");

            string firstitemliid = "FirstItem";

            while (items.MoveNext())
            {
                writer.WriteBeginTag("li");

                if (!String.IsNullOrEmpty(liId))
                    writer.WriteAttribute("id", liId);
                else if (!String.IsNullOrEmpty(firstitemliid))
                {
                    writer.WriteAttribute("id", firstitemliid);
                    firstitemliid = String.Empty;
                }

                if (this.SelectedItem == items.Current)
                    writer.WriteAttribute("class", "selected");

                writer.Write(">");
                writer.WriteBeginTag("a");

                if (!String.IsNullOrEmpty(items.Current.CssClass))
                {
                    writer.WriteAttribute("class", items.Current.CssClass);
                }

                if (OnMenuClick != null)
                {
                    writer.WriteAttribute("href", "javascript:" + Page.ClientScript.GetPostBackEventReference(this, items.Current.Id.ToString()));
                }
                else if (items.Current.OnClientClick != String.Empty)
                {
                    writer.WriteAttribute("href", "javascript:" + items.Current.OnClientClick);
                }
                else if (items.Current.PostBackUrl != String.Empty)
                {
                    writer.WriteAttribute("href", items.Current.PostBackUrl);
                }

                writer.WriteAttribute("title", items.Current.Text);
                writer.Write(">");
                writer.Write(items.Current.Text);
                writer.WriteEndTag("a");
                writer.WriteEndTag("li");
            }

            writer.WriteEndTag("ul");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            HtmlGenericControl csslink = new HtmlGenericControl("link");
            csslink.ID = "MBMENUCSS";
            csslink.Attributes.Add("href", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Megabyte.Web.Controls.CSS.MBMenuStyle.css"));
            csslink.Attributes.Add("type", "text/css");
            csslink.Attributes.Add("rel", "stylesheet");
            Page.Header.Controls.Add(csslink);

            StringBuilder sb = new StringBuilder();
            sb.Append(".mbmenu li.selected a{ text-decoration: none;background: url(" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "Megabyte.Web.Controls.Images.bg-header-hover.png") + ") repeat-x top left; }");
            sb.AppendLine(".mbmenu li a:hover{ text-decoration: none; background: url(" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "Megabyte.Web.Controls.Images.bg-header-hover.png") + ") repeat-x top left; }");

            HtmlGenericControl csslink2 = new HtmlGenericControl("style");
            csslink2.ID = "MBMENUCSS2";
            csslink2.Attributes.Add("type", "text/css");
            csslink2.InnerHtml = sb.ToString();
            Page.Header.Controls.Add(csslink2);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string ctrlname = this.Page.Request.Form["__EVENTTARGET"] ?? String.Empty;
            Control c = this.Page.FindControl(ctrlname);

            if (c != null && c.ID == this.ID)
            {
                string id = this.Page.Request.Form["__EVENTARGUMENT"];
                try
                {
                    this.SelectedItem = this.MainItems.GetItemById(id);
                }
                catch
                {
                    this.SelectedItem = this.SecondItems.GetItemById(id);
                }

                if (this.OnMenuClick != null)
                    OnMenuClick(this, new MenuClickEventArgs(this.SelectedItem));
            }
        }

        private string _selectedItemKey;
    }

    public delegate void MenuClickEventHandler(object sender, MenuClickEventArgs e);

    public class MenuClickEventArgs : EventArgs
    {
        public MenuItem SelectedMenu { get; set; }

        public MenuClickEventArgs(MenuItem menuitem)
        {
            this.SelectedMenu = menuitem;
        }
    }
}
