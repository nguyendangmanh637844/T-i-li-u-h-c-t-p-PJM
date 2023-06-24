using SendEmail.Models;

namespace SendEmail.Service
{
    public class SendMailService
    {
        private readonly Context.Context _context;
        public SendMailService(Context.Context context)
        {
            _context = context;
        }
        public List<Subject> getAll()
        {
            return _context.Subjects.ToList();
        }
    }
}
