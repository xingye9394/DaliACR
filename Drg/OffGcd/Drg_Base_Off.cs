
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.Helper;
using AEAssist.JobApi;
using Dali.Drg.Data;

namespace Dali.Drg.OffGcd;

public class DrgBaseOff : ISlotResolver
{
    public int Check()
    {
        if (GCDHelper.GetGCDCooldown() < 700) return -3;//GCD剩余时间不足，不打了
        if (!DrgIRotationEntry.QT.GetQt("爆发")) return -5;//QT控制
        if (GetSpells() == 0) return -88;//无技能返回
        if (!GetSpells().Unlock()) return -8;//技能冷却不足
        return 0;
    }
    public static uint GetSpells()
    {
        if (DrgIRotationEntry.QT.GetQt("武神枪") && DrgSkill.武神枪.Unlock())
            return DrgSkill.武神枪;
        if (DrgIRotationEntry.QT.GetQt("高跳") && DrgSkill.高跳.Unlock())
            return DrgSkill.高跳;
        if (DrgIRotationEntry.QT.GetQt("龙剑") && DrgSkill.龙剑.Unlock() && DH.上一个能力技能() != DrgSkill.龙剑 && (DH.上一个连击技能() == DrgSkill.龙尾大回旋 || DH.上一个连击技能() == DrgSkill.龙牙龙爪 || DH.上一个连击技能() == DrgSkill.qjcAuto))
            return DrgSkill.龙剑;
        if (DrgIRotationEntry.QT.GetQt("龙炎冲") && DrgSkill.龙炎冲.Unlock())
            return DrgSkill.龙炎冲;
        if (DrgIRotationEntry.QT.GetQt("红龙炮") && DrgSkill.死者之岸.Unlock() && Core.Resolve<JobApi_Dragoon>().IsLOTDActive)
            //Core.Resolve<JobApi_Dragoon>().IsLOTDActive 查询龙血
            return DrgSkill.死者之岸;
        if (DrgIRotationEntry.QT.GetQt("坠星冲") && DrgSkill.坠星冲.Unlock() && Core.Resolve<JobApi_Dragoon>().IsLOTDActive)
            return DrgSkill.坠星冲;
        if (DrgIRotationEntry.QT.GetQt("渡星冲") && DrgSkill.渡星冲.Unlock() && Core.Resolve<JobApi_Dragoon>().IsLOTDActive)
            return DrgSkill.渡星冲;
        if (DrgIRotationEntry.QT.GetQt("龙炎升") && DrgSkill.龙炎升.Unlock())
            return DrgSkill.龙炎升;
        if (DrgIRotationEntry.QT.GetQt("幻象冲") && DrgSkill.幻象冲.Unlock())
            return DrgSkill.幻象冲;
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