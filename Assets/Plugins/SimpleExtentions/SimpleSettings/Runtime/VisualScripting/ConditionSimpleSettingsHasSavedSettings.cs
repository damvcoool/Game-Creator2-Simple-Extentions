using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

[Serializable]
public class ConditionSimpleSettingsHasSavedSettings : Condition
{
    protected override bool Run(Args args)
    {
        return true;
    }
}
