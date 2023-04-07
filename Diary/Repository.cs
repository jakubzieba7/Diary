using Diary.Models.Domains;
using Diary.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Diary.Models.Converters;

namespace Diary
{
    public class Repository
    {
        public List<Group> GetGroups()
        {
            using (var context = new ApplicationDBContext())
            {
                return context.Groups.ToList();
            }
        }

        public List<StudentWrapper> GetStudents(int groupId)
        {
            using (var context = new ApplicationDBContext())
            {
                var students = context.Students.Include(x => x.Group).Include(x => x.Ratings).AsQueryable();

                if (groupId != 0)
                    students = students.Where(x => x.GroupId == groupId);

                return students.ToList().Select(x => x.ToWrapper()).ToList();

            }
        }
    }
}
