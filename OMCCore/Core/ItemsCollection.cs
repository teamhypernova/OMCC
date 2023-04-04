using OMCCore.Model.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OMCCore.Core
{
    public abstract class ItemsCollection<T> : IList<T>
        where T:ISerializable ,new()
    {
        public ItemsCollection()
        {
            foreach(var f in Config.Values.GetValueOrDefault(this.GetType().ToString(),new List<string>()))
            {
                var item = new T();
                item.Deserialize(f);
                ((List<T>)Values).Add(item);
            }
        }

        public T this[int index] { get => ((List<T>)Values)[index];set => ((List<T>)Values)[index] = value; }

        public ItemsCollectionConfig Config => Configs.GetConfig<ItemsCollectionConfig>();
        public IReadOnlyList<T> Values { get; } = new List<T>();

        public int Count => ((List<T>)Values).Count();

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            var s = item.Serialize();
            Config.AddValue(this.GetType().ToString(), s);
            ((List<T>)Values).Add(item);
            Configs.SaveAll();
        }

        public void Clear()
        {
            Config.Clear(this.GetType().ToString());
            ((List<T>)Values).Clear();
            Configs.SaveAll();
        }

        public bool Contains(T item) => ((List<T>)Values).Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => ((List<T>)Values).CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => ((List<T>)Values).GetEnumerator();

        public int IndexOf(T item) => ((List<T>)Values).IndexOf(item);

        public void Insert(int index, T item)
        {
            var s = item.Serialize();
            Config.InsertValue(this.GetType().ToString(), index, s);
            ((List<T>)Values).Insert(index, item);
            Configs.SaveAll();
        }

        public void Remove(T item)
        {
            var s = item.Serialize();
            Config.RemoveValue(this.GetType().ToString(), s);
            ((List<T>)Values).Remove(item);
            Configs.SaveAll();
        }

        public void RemoveAt(int index)
        {
            Config.RemoveValueAt(this.GetType().ToString(), index);
            ((List<T>)Values).RemoveAt(index);
            Configs.SaveAll();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        bool ICollection<T>.Remove(T item)
        {
            var s = item.Serialize();
            var res = Config.RemoveValue(this.GetType().ToString(), s);
            ((List<T>)Values).Remove(item);
            Configs.SaveAll();
            return res;
        }
    }
}
