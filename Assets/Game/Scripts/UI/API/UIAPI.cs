using Atomic.UI;
using UnityEngine.UI;

//Codegen!
namespace SampleGame.UI
{
    public static class UIAPI
    {
        public const int StartButton = 1;
        public const int ExitButton = 2;
        public const int ResumeButton = 3;
        public const int FinishScreen = 4;
        public const int StartScreen = 5;
        public const int FinishButton = 6;
        public const int PauseScreen = 7;

        public static Button GetStartButton(this IView view)
        {
            return view.GetValue<Button>(StartButton);
        }
        
        public static Button GetExitButton(this IView view)
        {
            return view.GetValue<Button>(ExitButton);
        }
        
        public static Button GetResumeButton(this IView view)
        {
            return view.GetValue<Button>(ResumeButton);
        }
        
        public static Button GetFinishButton(this IView view)
        {
            return view.GetValue<Button>(FinishButton);
        }

        public static IView GetFinishPopup(this IView view)
        {
            return view.GetValue<IView>(FinishScreen);
        }
        
        public static IView GetPausePopup(this IView view)
        {
            return view.GetValue<IView>(PauseScreen);
        }
        
        public static IView GetStartPopup(this IView view)
        {
            return view.GetValue<IView>(StartScreen);
        }
    }
}