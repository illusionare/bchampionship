using BeardChamp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeardChamp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICompetitionRepository _competitionRepository;


        public IEnumerable<Competition> Competitions { get; set; } = Enumerable.Empty<Competition>();
        public IndexModel(ILogger<IndexModel> logger, ICompetitionRepository competitionRepository )
        {
            _logger = logger;
            _competitionRepository = competitionRepository;
        }

        public async void OnGet()
        {
            Competitions = await _competitionRepository.GetAll();
        }
    }
}