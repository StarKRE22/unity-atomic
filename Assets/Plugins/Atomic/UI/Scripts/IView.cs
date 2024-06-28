namespace Atomic.UI
{
    public interface IView
    {
        void Show();
        void Hide();
        
        T GetData<T>(int key) where T : class;
        bool HasData(int key);
        bool AddData(int key, object value);
        bool DelData(int key);

        bool AddHandler(IHandler handler);
        bool DelHandler(IHandler handler);
        bool AddHandler<T>() where T : IHandler;
        bool DelHandler<T>() where T : IHandler;
    }
}