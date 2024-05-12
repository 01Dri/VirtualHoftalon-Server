using VirtualHoftalon_Server.Enums;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Security.interfaces;

namespace VirtualHoftalon_Server.Pattern;

public class RoleStrategyValidator : IRoleStrategyValidator
{
    
    private readonly Dictionary<Roles, Func<IRoleStrategy>> _strategyRegistry;

    public RoleStrategyValidator (
        
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository,
        ILoginRepository loginRepository,
        IPasswordEncrypter passwordEncrypter)
    {
        _strategyRegistry = new Dictionary<Roles, Func<IRoleStrategy>>
        {
            { Roles.PATIENT, () => new RolePatientStrategy(patientRepository, loginRepository, passwordEncrypter) },
            { Roles.DOCTOR, () => new RoleDoctorStrategy(doctorRepository, passwordEncrypter, loginRepository) }
        };
    }

    public Login SaveLoginByRole(Roles role, RegisterLoginDTO login)
    {
        if (_strategyRegistry.TryGetValue(role, out var strategy))
        {
            return strategy().Save(role, login);
        }

        throw new NotSupportedException($"Role {role} not supported");
    }

}
