using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;

namespace Dali.Drg.GCD;

public class Drg_Base : ISlotResolver
{
    private static uint 上个连击 => Core.Resolve<MemApiSpell>().GetLastComboSpellId();

    public int Check()
    {
        if (Core.Me.Level < 50) return -1;
        if (Core.Resolve<MemApiSpell>().GetActionInRangeOrLoS(GetSpells()) == 566) return -9;
        if (!DrgIRotationEntry.QT.GetQt("测试")) return -5;
        if (GetSpells() == 0) return -88;
        if (TargetHelper.GetEnemyCountInsideRect(Core.Me, Core.Me.GetCurrTarget(), 10, 4) >= 3)//技能范围10x4米
            return -1;
        return 0;
    }
    public uint Zhichi()
    {
        if (上个连击 == Data.DrgSkill.龙眼雷电)
            return Data.DrgSkill.qjcAuto;
        if (上个连击 == Data.DrgSkill.ktqAuto)
            return Data.DrgSkill.sakuraAuto;
        switch (上个连击)
        {
            case Data.DrgSkill.精准刺:
                return Data.DrgSkill.qjcAuto;
            case Data.DrgSkill.贯通刺:
                return Data.DrgSkill.zcAuto;
            case Data.DrgSkill.前冲刺:
                return Data.DrgSkill.zcAuto;
            default:
                if (Core.Me.HasAura(Data.DrgBuffs.龙眼预备))
                    return Data.DrgSkill.龙眼雷电;
                else
                    return Data.DrgSkill.精准刺;
        }
    }
    public uint Sakura()
    {
        if (上个连击 == Data.DrgSkill.龙眼雷电)
            return Data.DrgSkill.ktqAuto;
        if (上个连击 == Data.DrgSkill.qjcAuto)
            return Data.DrgSkill.zcAuto;
        switch (上个连击)
        {
            case Data.DrgSkill.精准刺:
                return Data.DrgSkill.ktqAuto;
            case Data.DrgSkill.开膛枪:
                return Data.DrgSkill.sakuraAuto;
            case Data.DrgSkill.螺旋击:
                return Data.DrgSkill.sakuraAuto;
            default:
                if (Core.Me.HasAura(Data.DrgBuffs.龙眼预备))
                    return Data.DrgSkill.龙眼雷电;
                else
                    return Data.DrgSkill.精准刺;
        }
    }
    private uint Longwei()
    {
        if (上个连击 == Data.DrgSkill.sakuraAuto && Core.Me.Level >= 58)
            return Data.DrgSkill.龙尾大回旋;
        if (上个连击 == Data.DrgSkill.zcAuto && Core.Me.Level >= 56)
            return Data.DrgSkill.龙牙龙爪;
        if (上个连击 == Data.DrgSkill.龙尾大回旋 || 上个连击 == Data.DrgSkill.龙牙龙爪 && Core.Me.Level >= 64)
            return Data.DrgSkill.云蒸龙变;
        if (上个连击 == Data.DrgSkill.云蒸龙变 && Core.Me.Level >= 76)
            return Data.DrgSkill.龙眼雷电;
        return Data.DrgSkill.精准刺;
    }
    private uint GetSpells()
    {
        uint lw = Longwei();
        if (lw != Data.DrgSkill.精准刺) // 默认返回精准刺时说明不处于龙尾流程
            return lw;
        if (!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(Data.DrgBuffs.SakuraDot, 5000))//目标buff不大于5秒
            return Sakura();
        return Zhichi();

    }
    public void Build(Slot slot)
    {
        var spell = GetSpells().GetSpell();
        if (spell == null)
            return;
        slot.Add(spell);
    }
}
