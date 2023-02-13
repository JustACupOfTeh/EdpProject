using eCO2Tracker.Models;

namespace eCO2Tracker.Services
{
	public interface IActivityService
	{
		Task AddActivity(Activity activity);
		Task DeleteActivity(int? id);
		Task EditActivity(Activity activity);
		Task<List<Activity>> GetActivitiesAsync();
		Task<string> GetActivitiesSummery();
		Task<Activity> GetActivityDetails(int? id);
		Task UpdatePerformedStatus(int id);
		Task UpdateCount(int? id);
		bool CountTest(int? id, bool tester);
	}
}