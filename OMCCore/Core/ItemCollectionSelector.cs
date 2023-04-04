using OMCCore.Model.Data;
using System.Collections.Generic;
using System.Linq;

namespace OMCCore.Core
{
    public class ItemCollectionSelector<T> : ItemsCollection<T>
        where T : ISerializable, new()
    {
        public T? Selected { get; private set; }
        public ItemCollectionSelector()
        {
            var config = Configs.GetConfig<SelectorConfig>();
            var sel = config.Selections.GetValueOrDefault(this.GetType().ToString(), "");
            foreach (var item in this)
            {
                if (item.Serialize() == sel)
                {
                    break;
                }
            }
            if (Selected == null)
            {
                Selected = Values.FirstOrDefault(defaultValue: default(T));
            }
        }
        public void SetSelected(T value)
        {
            if (this.Values.Contains(value))
            {
                var config = Configs.GetConfig<SelectorConfig>();
                config.Selections[this.GetType().ToString()] = value.Serialize();
                Configs.SaveAll();
                Selected = value;
            }
        }
    }
}
