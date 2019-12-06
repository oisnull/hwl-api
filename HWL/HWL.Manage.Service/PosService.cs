using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HWL.Entity.Extends;
using HWL.Entity.Models;

namespace HWL.Manage.Service
{
    public class PosService : BaseService
    {
        public PosService(HWLEntities db) : base(db)
        {
        }

        /// <summary>
        /// Active position data in the last N days
        /// </summary>
        public List<GeoInfo> GetActiveGeoInfos(int lastDays = 7)
        {
            IQueryable<t_user_pos> query = db.t_user_pos;

            if (lastDays > 0)
            {
                DateTime recentDate = DateTime.Now.AddDays(-lastDays);
                query = query.Where(p => p.update_date >= recentDate);
            }

            return query.GroupBy(g => new { g.lat, g.lon })
                        .Select(g => g.OrderByDescending(o => o.update_date)
                                        .Select(o => new GeoInfo
                                        {
                                            Latitude = o.lat,
                                            Longitude = o.lon,
                                            Details = o.pos_details,
                                        }).First())
                        .ToList();

        }
    }
}
