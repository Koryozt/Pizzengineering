using Pizzengineering.Domain.Entities;

namespace Pizzengineering.Application.Abstractions;

public interface IJwtProvider
{
    string Generate(User member);
}
