using System.Collections.Generic;
using qASIC.Options.Menu;
using qASIC.Options;

namespace Menu.Options
{
    public abstract class OptionsMenuSwapper : MenuOption
    {
        public List<object> properties = new List<object>();
        public int currentIndex;

        public override void Start()
        {
            Assign();
            SetCurrentIndex();
            LoadOption();
        }

        public abstract void Assign();

        public void SetIndexSilent(int index)
        {
            currentIndex = index;
        }

        public void SetValue(int value, bool log) => SetValue(properties[value], log);
        public virtual void SetValue(object property) => SetValue(property, true);

        public abstract void SetCurrentIndex();
        public virtual string GetValueName(object propertie) => propertie.ToString();
        public override string GetLabel()
        {
            return $"{OptionLabelName}{GetValueName(properties[currentIndex])}";
        }

        public override void LoadOption()
        {
            if (!OptionsController.TryGetUserSetting(OptionName, out string optionValue) ||
                !int.TryParse(optionValue, out int value)) return;
            SetValue(value, false);
        }

        public void OnClick()
        {
            currentIndex++;
            if (currentIndex >= properties.Count) currentIndex = 0;
            SetValue(properties[currentIndex]);
        }    
    }
}