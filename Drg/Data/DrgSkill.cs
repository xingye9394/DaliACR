using AEAssist;

namespace Dali.Drg.Data
{
	public static class DrgSkill
	{
        public const uint
            冲刺 = 3,
            精准刺 = 75,
            贯穿尖 = 90,
            龙眼雷电 = 16479,
            龙眼苍穹 = 25770,
            //通用
            开膛枪 = 87,
            螺旋击 = 36955,
            //dot2
            樱花怒放 = 88,
            樱花缭乱 = 25772,
            //Dot3
            贯通刺 = 78,
            前冲刺 = 36954,
            //zhici2
            直刺 = 84,
            苍穹刺 = 25771,
            //zhichi3
            龙牙龙爪 = 3554,
            龙尾大回旋 = 3556,
            云蒸龙变 = 36952,
            //身位
            死天枪 = 86,
            音速刺 = 7397,
            山境酷刑 = 16477,
            //aoe
            龙剑 = 83,
            真北 = 7546,

            猛枪 = 85,
            连祷 = 3557,

            跳跃 = 92,
            高跳 = 16478,
            幻象冲 = 7399,
            武神枪 = 3555,
            死者之岸 = 7400,

            龙炎冲 = 96,
            龙炎升 = 36953,
            坠星冲 = 16480,
            渡星冲 = 36956,
            天龙点睛 = 25773;
        public static uint ktqAuto =>
            Core.Me.Level >= 96 ? 螺旋击 : 开膛枪;
        public static uint sakuraAuto =>
            Core.Me.Level >= 86 ? 樱花缭乱 : 樱花怒放;
        public static uint qjcAuto =>
            Core.Me.Level >= 86 ? 前冲刺 : 贯通刺;
        public static uint zcAuto =>
            Core.Me.Level >= 86 ? 苍穹刺 : 直刺;


    }
}
