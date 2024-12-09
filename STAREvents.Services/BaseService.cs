using STAREvents.Services.Data.Interfaces;

namespace STAREvents.Services
{
    public class BaseService : IBaseService
    {
        public bool IsGuidValid(string? id, ref Guid parsedGuid)
        {
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out parsedGuid))
            {
                return false;
            }
            return true;
        }
    }
}
