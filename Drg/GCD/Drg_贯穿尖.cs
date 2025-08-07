using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;

namespace Dali.Drg.GCD;

public class Drg_贯穿尖 : ISlotResolver
{
    public int Check()
    {
        if (Core.Me.Level < 15) return -1;
        if (Core.Resolve<MemApiSpell>().GetActionInRangeOrLoS(GetSpells()) == 566) return -9;//打不中目标
        if (!DrgIRotationEntry.QT.GetQt("贯穿尖")) return -5;//QT开关
        if (GetSpells() == 0) return -88;
        if (TargetHelper.GetEnemyCountInsideRect(Core.Me, Core.Me.GetCurrTarget(), 10, 4) >= 3)//技能范围10x4米
            return -1;//AOE停手
        return 0;
    }
    private uint GetSpells()
    {
        if (DrgIRotationEntry.QT.GetQt("贯穿尖"))
            return Data.DrgSkill.贯穿尖;
        return 0;
    }
    public void Build(Slot slot)
    {
        var spell = GetSpells().GetSpell();
        if (spell == null)
            return;
            slot.Add(spell);
    }
}
