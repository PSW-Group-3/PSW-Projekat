using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Core.Repository.Notification
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly HospitalDbContext _context;
        public NotificationRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(Model.Notification entity)
        {
            _context.Notifications.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Model.Notification entity)
        {
            _context.Notifications.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Model.Notification> GetAll()
        {
            return _context.Notifications.ToList();
        }

        public Model.Notification GetById(int id)
        {
            return _context.Notifications.Find(id);
        }

        public void Update(Model.Notification entity)
        {
            {
                _context.Entry(entity).State = EntityState.Modified;

                try
                {
                    _context.Update(entity);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
        }
    }
}
