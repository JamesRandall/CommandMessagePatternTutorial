using System;

namespace Core.Model
{
    public interface IUserContextCommand
    {
        Guid UserId { get; set; }
    }
}
