using STAREvents.Data.Models;

namespace STAREvents.Web.ViewModels.Events
{
    public class EventsViewModel
    {
        public IEnumerable<EventViewModel> Events { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string SearchTerm { get; set; }
        public Guid? SelectedCategory { get; set; }
        public string SortOption { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
