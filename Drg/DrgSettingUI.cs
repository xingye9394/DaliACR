using ImGuiNET;
using Dali.Drg;


namespace Dali.Drg.Settings
{
    public class DrgSettingsUI
    {
        public static DrgSettingsUI Instance = new();
        public DrgSettings DrgSettingss => DrgSettings.Instance;

        public void Draw()
        {
            //这里设置ui
            //ui类型请查询ImGui
            //ImGui.InputInt("目标剩余多少百分比血量时禁用读条画画", ref DrgSettingss.画画百分比);
            // ImGui.Checkbox("音效", ref PCTSettings.音效);
            // ImGui.Checkbox("智能aoe目标", ref PCTSettings.智能aoe目标);

            if (ImGui.Button("Save"))//保存按钮，不用动
            {
                DrgSettings.Instance.Save();
            }
        }
    }
}