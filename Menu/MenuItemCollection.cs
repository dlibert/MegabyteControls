// -----------------------------------------------------------------------
// <copyright file="MenuItemCollection.cs" company="HP">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Megabyte.Web.Controls.Menu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MenuItemCollection : IEnumerable<MenuItem>
    {
        public void Add(MenuItem item)
        {
            this._items.Add(item);
        }

        public void AddRange(List<MenuItem> items) {
            this._items.AddRange(items);
        }

        public void Remove(MenuItem item)
        {
            this._items.Remove(item);
        }

        public MenuItemCollection()
        {
            this._items = new List<MenuItem>();
        }

        public MenuItemCollection(MenuItem [] items) : base()
        {
            _items.AddRange(items);
        }

        public IEnumerator<MenuItem> GetEnumerator()
        {
            return this._items.GetEnumerator(); 
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new MenuItemCollectionEnum(_items.ToArray());
        }

        public MenuItem GetItemByIndex(int ix) 
        {
            try
            {
                return this._items[ix];
            }
            catch (IndexOutOfRangeException)
            {
                throw new MenuItemCollectionEnumException("Item is out of range.");
            }
        }

        public MenuItem GetItemById(string id)
        {
            try
            {
                return this._items.Single(s => s.Id == id);
            }
            catch (Exception)
            {
                throw new MenuItemCollectionEnumException("MenuItem [" + id + "] does not exist");
            }
        }

        private List<MenuItem> _items;
    }

    public class MenuItemCollectionEnum : IEnumerator<MenuItem>
    {
        public MenuItemCollectionEnum(MenuItem [] list)
        {
            _items = list;
        }

        public MenuItem Current
        {
            get
            {
                try
                {
                    return _items[_position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new MenuItemCollectionEnumException("List is empty.");
                }
            }
        }

        public void Dispose()
        {
            _items = null;
            _position = -1;
        }

        object System.Collections.IEnumerator.Current
        {
            get { return this.Current; }
        }

        public bool MoveNext()
        {
            _position++;
            return (_position < _items.Length);
        }

        public void Reset()
        {
            _position = -1;
        }

        private MenuItem[] _items;
        private int _position=-1;
    }

    [Serializable]
    public class MenuItemCollectionEnumException : Exception
    {
        public MenuItemCollectionEnumException() { }
        public MenuItemCollectionEnumException(string message) : base(message) { }
        public MenuItemCollectionEnumException(string message, Exception inner) : base(message, inner) { }
        protected MenuItemCollectionEnumException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
