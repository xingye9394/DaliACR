using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Media;
using System.Numerics;
using System.Threading.Tasks;

namespace Dali.Drg
{
    public class DrgRotationEventHandler : IRotationEventHandler
    {




        public void OnResetBattle()//重置战斗
        {
            // 重置战斗中缓存的数据
            // QT的设置重置为默认值
            DrgBattleData.Instance = new();
        }

        public async Task OnNoTarget()//进战且无目标时
        {

            await Task.CompletedTask;
        }

        public void OnSpellCastSuccess(Slot slot, Spell spell)//施法成功判定可以滑步时
        {
        
        }

        public async Task OnPreCombat()//脱战时
        {
            
            await Task.CompletedTask;
        }
        
    

        public void AfterSpell(Slot slot, Spell spell)
        //某个技能使用之后的处理,比如诗人在刷Dot之后记录这次是否是强化buff的Dot 如果是读条技能，则是服务器判定它释放成功的时间点，比上面的要慢一点
        {

             

        }

        //public void OnBattleUpdate(int currTime)//战斗中逐帧检测
        //{
        //    var 上个连击 = Core.Resolve<MemApiSpell>().GetLastComboSpellId();
        //    var gcd剩余时间 = GCDHelper.GetGCDCooldown();
        //    if (上个连击 == Data.DrgSkill.ktqAuto)
        //    {
        //        MeleePosHelper2.DrawMeleePos(MeleePosHelper.Pos.Behind, 2500, Data.DrgSkill.sakuraAuto);
        //    }
        //    else if (上个连击 == Data.DrgSkill.sakuraAuto)
        //    {
        //        MeleePosHelper2.DrawMeleePos(MeleePosHelper.Pos.Behind, 2500, Data.DrgSkill.龙尾大回旋);
        //    }
        //    else if (上个连击 == Data.DrgSkill.qjcAuto)
        //    {
        //        MeleePosHelper2.DrawMeleePos(MeleePosHelper.Pos.Flank, 5000, Data.DrgSkill.龙牙龙爪);
        //    }
        //    else
        //        MeleePosHelper2.Clear();
        //}不知道咋用先放着了


        public void OnBattleUpdate(int currTimeInMs)
        {

            var 上个连击 = Core.Resolve<MemApiSpell>().GetLastComboSpellId();
            int gcd剩余时间 = GCDHelper.GetGCDCooldown();

            if ((上个连击 == Data.DrgSkill.精准刺 || 上个连击 == Data.DrgSkill.龙眼雷电 || 上个连击 == Data.DrgSkill.ktqAuto) && !Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(Data.DrgBuffs.SakuraDot, 5000))
            {
                MeleePosHelper.Draw(MeleePosHelper.Pos.Behind, 100);
            }
            else if ((上个连击 == Data.DrgSkill.精准刺 || 上个连击 == Data.DrgSkill.龙眼雷电 || 上个连击 == Data.DrgSkill.qjcAuto) && Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(Data.DrgBuffs.SakuraDot, 5000))
            {
                MeleePosHelper.Draw(MeleePosHelper.Pos.Flank, 100);
            }
            else if(上个连击 == Data.DrgSkill.sakuraAuto)
            {
                MeleePosHelper.Draw(MeleePosHelper.Pos.Behind, gcd剩余时间/25);
            }
            else if (上个连击 == Data.DrgSkill.zcAuto)
            {
                MeleePosHelper.Draw(MeleePosHelper.Pos.Flank, gcd剩余时间/25);

            }
            else
                MeleePosHelper.Clear();

        }
        public void OnEnterRotation()//切换到当前ACR
        {
        
        }

        public void OnExitRotation()//退出ACR
        {
      
        }

        public void OnTerritoryChanged()
        {
        
        }
        

    }
}