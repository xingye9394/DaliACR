
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.Helper;

namespace Dali.Drg.OffGcd;

public class Drg_爆发_Off : ISlotResolver
{
    public int Check()
    {
        if (GCDHelper.GetGCDCooldown() < 700) return -3;//GCD剩余时间不足，不打了
        if (!DrgIRotationEntry.QT.GetQt("爆发")) return -5;//QT控制
        if (GetSpells() == 0) return -88;//无技能返回
        if (!DH.Unlock(GetSpells())) return -8;//技能冷却不足
        return 0;
    }
    public static uint GetSpells()
    {
        if (DrgIRotationEntry.QT.GetQt("猛枪") && DH.Unlock(Data.DrgSkill.猛枪))
            return Data.DrgSkill.猛枪;
        if (DrgIRotationEntry.QT.GetQt("连祷") && DH.Unlock(Data.DrgSkill.连祷))
            return Data.DrgSkill.连祷;
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