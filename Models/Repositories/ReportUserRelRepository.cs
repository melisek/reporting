﻿using System.Collections.Generic;
using System.Linq;
using szakdoga.Data;

namespace szakdoga.Models.Repositories
{
    public class ReportUserRelRepository : IReportUserRelRepository
    {
        private readonly AppDbContext _context;

        public ReportUserRelRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(ReportUserRel entity)
        {
            if (entity != null)
            {
                _context.ReportUserRel.Add(entity);
                _context.SaveChanges();
            }
        }

        public ReportUserRel Get(int id)
        {
            return _context.ReportUserRel.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<ReportUserRel> GetAll()
        {
            return _context.ReportUserRel.ToList();
        }

        public bool Remove(int id)
        {
            var entity = Get(id);
            if (entity != null)
            {
                _context.ReportUserRel.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Update(ReportUserRel entity)
        {
            if (entity != null)
            {
                _context.ReportUserRel.Update(entity);
                _context.SaveChanges();
            }
        }
    }
}