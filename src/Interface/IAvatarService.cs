namespace Alfasoft.Interface
{
    public interface IAvatarService
    {
        Task<string> GetRandomAvatarUrl();
    }
}
