using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;

namespace Dali.Drg.GCD;

public class Drg_AOE : ISlotResolver
{
    private static uint 上个连击 => Core.Resolve<MemApiSpell>().GetLastComboSpellId();
    public int Check()
    {
        // -1不满足 -5 QT没开 -9打不到
        if (Core.Me.Level < 40) return -1;
        if (Core.Resolve<MemApiSpell>().GetActionInRangeOrLoS(GetSpells()) == 566) return -9;
        if (GetSpells() == 0) return -88;
        if (!DrgIRotationEntry.QT.GetQt("AOE"))
        {
            return -5;
        }
        else
        {
            if (TargetHelper.GetEnemyCountInsideRect(Core.Me, Core.Me.GetCurrTarget(), 10, 4) >= 3)//技能范围10x4米
                return 0;
            else
                return -1;
        }

    }
    private static uint GetSpells()
    {
        if (Core.Me.Level >= 82 && Core.Me.HasAura(Data.DrgBuffs.龙眼预备))
            return Data.DrgSkill.龙眼苍穹;
        if ((上个连击 == Data.DrgSkill.死天枪 || 上个连击 == Data.DrgSkill.龙眼苍穹) && Core.Me.Level >= 62)
            return Data.DrgSkill.音速刺;
        if (上个连击 == Data.DrgSkill.音速刺 && Core.Me.Level >= 72)
            return Data.DrgSkill.山境酷刑;
        if (上个连击 == Data.DrgSkill.山境酷刑 && Core.Me.Level >= 82)
            return Data.DrgSkill.龙眼苍穹;
        return Data.DrgSkill.死天枪;
    }
    public void Build(Slot slot)
    {
        var spell = GetSpells().GetSpell();
        if (spell == null)
            return;
        slot.Add(spell);
    }
}
