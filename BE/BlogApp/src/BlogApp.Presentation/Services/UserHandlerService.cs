using BlogApp.Core.Services;

namespace BlogApp.Presentation.Services;

public class UserHandlerService : IUserHandlerService
{
    public Guid GetUserId()
    {
        return Guid.Parse("ba4e7e56-65ee-4fd5-abdf-ef1b02d1261f");
    }
}   