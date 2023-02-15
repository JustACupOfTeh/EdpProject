using eCO2Tracker.Models;

namespace eCO2Tracker.Services
{
	public class TestService
	{
        private readonly MyDbContext _context;

        public TestService(MyDbContext context)
        {
            _context = context;
        }

        public List<TestQuestions> GetAll()
        {
            return _context.TestQuestions.OrderBy(m => m.QuestionType).ToList();
        }

        public TestQuestions? GetQuestionById(string id)
        {
            TestQuestions? question = _context.TestQuestions.FirstOrDefault(x => x.QuestionID.Equals(id));
            return question;
        }

        public void AddQuestion(TestQuestions question)
        {
            _context.TestQuestions.Add(question);
            _context.SaveChanges();
        }

        public void UpdateQuestion(TestQuestions question)
        {
            _context.TestQuestions.Update(question);
            _context.SaveChanges();
        }

        public void RemoveQuestion(TestQuestions question)
        {
            _context.TestQuestions.Remove(question);
            _context.SaveChanges();
        }

        public string generateUuid()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }
        public enum QuestionTypeList
        {
            TrueOrFalse,
            Rating
        }
        public enum QuestionSectionList
        {
            Household,
            Transport
        }
    }
}
