using AEAssist.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dali.Helper
{
    public static class DH
    {
        /// <summary>
        /// 判断技能是否可用
        /// </summary>
        public static bool Unlock(uint spell)
        {
            return spell.GetSpell().IsReadyWithCanCast();
        }
    }
}

