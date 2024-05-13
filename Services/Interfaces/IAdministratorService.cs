using VirtualHoftalon_Server.Models.Dto.Administrator;

namespace VirtualHoftalon_Server.Services.Interfaces;

public interface IAdministratorService
{
    List<AdministratorResponseDTO> GetAll();
    AdministratorResponseDTO Save(AdministratorRequestDTO administrator);
    AdministratorResponseDTO GetOneById(int id);
    AdministratorResponseDTO Update(int id, AdministratorRequestDTO administrator);
    bool DeleteById(int id);
}