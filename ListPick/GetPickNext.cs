using System;
using GameCreator.Runtime.Common;

namespace GameCreator.Runtime.Variables
{
    [Title("Pick Next Element")]
    [Category("Pick Next Element")]
    
    [Description("Selects the element that appears next on the list")]
    [Image(typeof(IconSkipNext), ColorTheme.Type.Yellow)]

    [Serializable]
    public class GetPickNext : TListGetPick
    {
        int current = 0;
        
        public override int GetIndex(int count)
        {
            
            if (count <= current)
            {
                current = 0;
            }
            return current++;
        }  
        
        public override string ToString() => "Next";
    }
}