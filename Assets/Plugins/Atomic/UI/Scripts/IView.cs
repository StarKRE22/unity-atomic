namespace Atomic.UI
{
    public interface IView
    {
        void Show();
        void Hide();
        
        T GetData<T>(int key) where T : class; //100% Need!
    }
}