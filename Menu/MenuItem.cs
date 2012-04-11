// -----------------------------------------------------------------------
// <copyright file="MenuItem.cs" company="HP">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Megabyte.Web.Controls.Menu
{
    using System;


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MenuItem
    {
        public string Text { get; set; }
        public string PostBackUrl { get; set; }
        public string OnClientClick { get; set; }
        public string Id { get; set; }
        public int Position { get; set; }
        /// <summary>
        /// Ex: .mbmenu li a.macssclass { ... }
        /// </summary>
        public string CssClass { get; set; }

        public MenuItem(string id, string text, string postbackurl, string onclientclick, int position)
        {
            this.Id = id;
            this.Text = text;
            this.PostBackUrl = postbackurl;
            this.OnClientClick = onclientclick;
            this.Position = position;
        }

        public MenuItem(string id, string text, string postbackurl, string onclientclick, int position, string cssclass)
        {
            this.Id= id;
            this.Text = text;
            this.PostBackUrl = postbackurl;
            this.OnClientClick = onclientclick;
            this.Position = position;
            this.CssClass = cssclass;
        }
    }
}
