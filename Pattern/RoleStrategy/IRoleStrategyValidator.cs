using VirtualHoftalon_Server.Enums;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto;

namespace VirtualHoftalon_Server.Pattern;

public interface IRoleStrategyValidator
{
    Login SaveLoginByRole(Roles role, RegisterLoginDTO login);
}