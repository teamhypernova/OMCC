using System;
using System.Collections.Generic;
using System.Reflection;

namespace OMCCore.Plugins
{
    public abstract class Plugin
    {
        /// <summary>
        /// 0 On Registering LanPack
        /// </summary>
        public virtual void OnRegisteringLanguagePack()
        {

        }
        /// <summary>
        /// 1 On Registering
        /// </summary>
        public virtual void OnRegistering()
        {

        }
        
    }
}
