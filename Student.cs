using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericComparator
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int[] Marks { get; set; }

        public List<String> Subjects { get; set; }

        public Dictionary<String,String> Details { get; set; }

        public List<Dictionary<String, String>> KeyValuePairs { get; set; }

    }
}
