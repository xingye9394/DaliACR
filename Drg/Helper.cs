using AEAssist;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.FFXIV.Client.UI.Misc;

namespace Dali.Drg
{
    public static class DH
    {
        private static uint 上个连击 => Core.Resolve<MemApiSpell>().GetLastComboSpellId();
        /// <summary>
        /// 判断技能是否可用
        /// </summary>
        public static bool Unlock(this uint spell)
        {
            return spell.GetSpell().IsReadyWithCanCast();

        }
        public static uint 上一个连击技能()
        {
            return Core.Resolve<MemApiSpell>().GetLastComboSpellId();
        }
        public static uint 上一个gcd技能()
        {
            return Core.Resolve<MemApiSpellCastSuccess>().LastGcd;
        }

        public static uint 上一个能力技能()
        {
            return Core.Resolve<MemApiSpellCastSuccess>().LastAbility;
        }
        
        
    }
}

