using RestApiMantenimientoEF.Modelos;
namespace RestApiMantenimientoEF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MantenimientoContext _context;

        public UserRepository(MantenimientoContext context)
        {
            _context = context;
        }

        public async Task<int> GetIdAreaSByIdNoColaborador(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user?.IdAreaS ?? 0;
        }
    }
}