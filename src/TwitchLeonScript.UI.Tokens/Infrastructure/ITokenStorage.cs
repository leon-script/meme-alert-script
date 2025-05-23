namespace TwitchLeonScript.UI.Tokens.Infrastructure
{
    public interface ITokenStorage<T> where T : class
    {
        void Save(T tokens);
        T? Load();
        void Clear();
    }
}
