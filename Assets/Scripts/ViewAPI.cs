using Atomic.UI;
using UnityEngine;
using UnityEngine.UI;

public static class ViewAPI
{
    public const int Button = 0;
    public const int Sprite = 1;
    
    public static Button GetButton(this IView view)
    {
        return view.GetValue<Button>(Button);
    }
    
    public static Sprite GetSprite(this IView view)
    {
        return view.GetValue<Sprite>(Sprite);
    }
}