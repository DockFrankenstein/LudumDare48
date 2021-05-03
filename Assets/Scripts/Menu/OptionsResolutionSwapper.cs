using UnityEngine;
using System.Collections.Generic;
using qASIC;
using qASIC.Options;

namespace Menu.Options
{
    public class OptionsResolutionSwapper : OptionsMenuSwapper
    {
        object newValue;

        public override void Assign()
        {
            Resolution[] resolutions = Screen.resolutions;
            List<object> resolutionList = new List<object>();
            for (int i = 0; i < resolutions.Length; i++)
            {
                Vector2Int res = new Vector2Int(resolutions[i].width, resolutions[i].height);
                if (resolutionList.Contains(res)) continue;
                resolutionList.Add(res);
            }

            properties = resolutionList;
        }

        public override void SetValue(object propertie) =>
            newValue = VectorStringConvertion.Vector2IntToString((Vector2Int)propertie);

        public override void SetCurrentIndex()
        {
            int index = properties.IndexOf(new Vector2Int(Screen.width, Screen.height));
            if (index < 0) return;
            SetIndexSilent(index);
        }

        public override string GetValueName(object property)
        {
            if (!(property is Vector2Int)) return string.Empty;
            Vector2Int res = (Vector2Int)property;
            return VectorStringConvertion.Vector2IntToString(res);
        }

        public override void LoadOption()
        {
            if (!OptionsController.TryGetUserSetting(OptionName, out string optionValue) ||
                !VectorStringConvertion.TryStringToVector2Int(optionValue, out Vector2Int result)) return;
            int index = properties.IndexOf(result);
            if (index < 0) return;
            SetIndexSilent(index);
        }

        public void Apply() => base.SetValue(newValue);
    }
}