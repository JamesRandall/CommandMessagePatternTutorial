using System;

namespace Core.Model
{
    public interface IUserContextCommand
    {
        Guid AuthenticatedUserId { get; set; }
    }
}
