namespace BeardChamp.Data
{
    /// <summary>
    /// Repository for Competiotions
    /// </summary>
    public interface ICompetitionRepository
    {
        Task<List<Competition>> GetAll();
        Task<Competition> Get(int id);
        Task<Competition> Update(Competition competition);
        Task Delete(int id);
    }
}
