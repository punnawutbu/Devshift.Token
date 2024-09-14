using System;

namespace Devshift.Token.Providers
{
    public interface IDateTimeProvider
    {
        DateTime Now();
        DateTime UtcNow();
    }
}