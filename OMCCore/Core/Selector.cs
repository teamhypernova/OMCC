using OMCCore.Model.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.Primitives;

namespace OMCCore.Core
{
    public abstract class Selector<T> : Registry<T> where T : IId
    {
        public T? Selected { get; private set; }
        public override void Init()
        {
            base.Init();
            LoadSelected();
        }
        void LoadSelected()
        {
            var config = Configs.GetConfig<SelectorConfig>();
            var id = config.Selections.GetValueOrDefault(this.GetType().ToString(), "");
            foreach (var reg in Registered)
            {
                if (reg.Id == id)
                {
                    Selected = reg;
                }
            }
            if (Selected == null)
            {
                Selected = Registered.FirstOrDefault(defaultValue: default(T));
            }
        }
        public void SetSelected(T value)
        {
            if (this.Registered.Contains(value))
            {
                var config = Configs.GetConfig<SelectorConfig>();
                config.Selections[this.GetType().ToString()] = value.Id;
                Configs.SaveAll();
                Selected = value;
            }
        }
    }
}
