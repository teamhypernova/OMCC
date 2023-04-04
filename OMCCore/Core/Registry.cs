using EDGW.Logging;
using OMCCore.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OMCCore.Core
{
    public abstract class Registry<T> where T : IId
    {
        public readonly Dictionary<string, T> ValueDic = new();
        public IReadOnlyList<T> Registered => ValueDic.Values.ToList();
        bool closed = false;
        protected abstract Logger Logger { get; }
        public virtual void Register(T value)
        {
            if (closed) throw RegistryException.REGISTRY_CLOSED;
            ValueDic[value.Id] = value;
            Logger.info($"Registered {value.GetType().Name}({value.Id})");
        }
        public virtual void Init()
        {
            closed = true;
        }
    }
}
