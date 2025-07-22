using AppointmentScheduler.Services.ISevices;
using System.Text.Json;
namespace AppointmentScheduler.Services;

public class DBInitializer : IDBInitializer
{
    private readonly RepositoryService _repositoryService;
    private readonly IConfiguration _configuration;
    public DBInitializer(RepositoryService repositoryService, IConfiguration configuration)
    {
        _repositoryService = repositoryService;
        _configuration = configuration;
    }


    public void InitializeDB()
    {
        _repositoryService.CreateDB();
        
    }

}
