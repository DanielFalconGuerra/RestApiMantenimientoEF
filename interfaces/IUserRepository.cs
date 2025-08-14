
namespace RestApiMantenimientoEF.Repositories
{
    public interface IUserRepository
    {
        Task<int> GetIdAreaSByIdNoColaborador(int id);
    }
}