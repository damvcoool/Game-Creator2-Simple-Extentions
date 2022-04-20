using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace DM_Customization
{
    [Title("Pick Previous Element")]
    [Category("Pick Previous Element")]
    
    [Description("Selects the element that appears previous on the list")]
    [Image(typeof(IconSkipPrevious), ColorTheme.Type.Yellow)]

    [Serializable]
    public class GetPickPrevious : TListGetPick
    {
        int current = 0;
        
        public override int GetIndex(int count)
        {
            current--;
            if (current < 0)
            {
                current = (count - 1);
            }
            return current;
        }  
        
        public override string ToString() => "Previous";
    }
}