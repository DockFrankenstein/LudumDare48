using System.Collections;
using qASIC.Options;
using UnityEngine;

namespace Menu.Options
{
    public class OptionsFramelimitSwapper : OptionsMenuSwapper
    {
        public int[] Framerates;

        public override void Assign()
        {
            properties.Clear();
            for (int i = 0; i < Framerates.Length; i++) properties.Add(Framerates[i]);
        }

        public override string GetValueName(object property)
        {
            if (!(property is int)) return base.GetValueName(property);
            int value = (int)property;
            if (value == -1) return "Off";
            return value.ToString();
        }

        public override void SetCurrentIndex()
        {
            int index = properties.IndexOf(Application.targetFrameRate);
            if (index < 0) return;
            SetValue(index, false);
        }

        public override void LoadOption()
        {
            if (!OptionsController.TryGetUserSetting(OptionName, out string optionValue) ||
                !int.TryParse(optionValue, out int result)) return;

            int index = properties.IndexOf(result);
            if (index < 0) return;
            SetValue(index, false);
        }
    }
}