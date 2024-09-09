using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericComparator
{
    public class SampleObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Dictionary<string, int> DataDictionary { get; set; }

        public SampleObject(int Id1) {
        Id = Id1;
        Name = "Test";
        Description = "Testing";
        DataDictionary = new Dictionary<string, int>();

        DataDictionary.Add("t1", 1);
        DataDictionary.Add("t2", 1);
        }


    }
}
