using System;
using GenericComparator;

class Program
{
    static void Main(string[] args) {
        
        Example1();
        Console.WriteLine("==================================================\n");

        Example2_Objects();
        Console.WriteLine("==================================================\n");

        Example1_Student();

    }

    static void Example1()
    {
        GenericComparatorClass comparator = new GenericComparatorClass();

        var dict1 = new Dictionary<String, List<string>> { };
        dict1.Add("hello1", new List<string> { "1", "2" });
        dict1.Add("hello2", new List<string> { "2", "3" });
        var dict2 = new Dictionary<String, List<string>> { };
        dict2.Add("hello1", new List<string> { "1", "2" });
        dict2.Add("hello2", new List<string> { "3", "2" });


      

        var res1 = comparator.Compare("test1", "test1");
        Console.WriteLine(res1);
        Console.WriteLine("--------------------------------------------\n");

        res1 = comparator.Compare("test1", "test2");
        Console.WriteLine(res1);
        Console.WriteLine("--------------------------------------------\n");

        var res2 = comparator.Compare(new List<long>() { 1, 2, 3 }, new List<long>() { 1, 3, 2 });
        Console.WriteLine("--------------------------------------------\n");

        var res3 = comparator.Compare(dict1, dict2);
        Console.WriteLine("--------------------------------------------\n");


        Console.WriteLine(res1);
        Console.WriteLine(res2);
        Console.WriteLine(res3);


    }

    static void Example2_Objects()
    {
        GenericComparatorClass comparator = new GenericComparatorClass();

        SampleObject obj1 = new SampleObject(1);
        SampleObject obj2 = new SampleObject(1);
        SampleObject obj3 = new SampleObject(2);

        var res4 = comparator.Compare(obj1, obj2);
        Console.WriteLine("--------------------------------------------\n");

        var res5 = comparator.Compare(obj1, obj3);
        Console.WriteLine("--------------------------------------------\n");

        Console.WriteLine(res4);
        Console.WriteLine(res5);
    }

    static void Example1_Student()
    {
        GenericComparatorClass comparator = new GenericComparatorClass();
        var dict1 = new Dictionary<string,string> { };
        dict1.Add("key1","value1" );
        dict1.Add("key2", "value2");

        var dict2 = new Dictionary<string, string> { };
        dict2.Add("key3", "value3");
        dict2.Add("key4", "value4");

        var dict3 = new Dictionary<string, string> { };
        dict2.Add("key5", "value5");
        dict2.Add("key6", "value6");

        Student obj1 = new Student()
        {
            Id = 1,
            Name = "test1",
            Subjects = new List<string> { "test1", "test2" },
            Marks = new int[] { 100, 200, 300 },
            KeyValuePairs = new List<Dictionary<string, string>> { dict1, dict2 }
        };

        Student obj2 = new Student()
        {
            Id = 1,
            Name = "test1",
            Subjects = new List<string> { "test2", "test1" },
            Marks = new int[] { 100, 200, 300 },
            KeyValuePairs = new List<Dictionary<string, string>> { dict1, dict2 }
        };
        Student obj3 = new Student()
        {
            Id = 1,
            Name = "test1",
            Subjects = new List<string> { "test1", "test2" },
            Marks = new int[] { 100, 300, 200 },
            KeyValuePairs = new List<Dictionary<string, string>> { dict2, dict1 }
        };

        Student obj4 = new Student()
        {
            Id = 1,
            Name = "test1",
            Subjects = new List<string> { "test1", "test2" },
            Marks = new int[] { 100, 200, 300 },
            KeyValuePairs = new List<Dictionary<string, string>> { dict2, dict2 }
        };

        Student obj5 = new Student()
        {
            Id = 1,
            Name = "test1",
            Subjects = new List<string> { "test1", "test2" },
            Marks = new int[] { 100, 200, 300 },
            KeyValuePairs = new List<Dictionary<string, string>> { dict2, dict3 }
        };

        var res4 = comparator.Compare(obj1, obj2);
        Console.WriteLine("--------------------------------------------\n");

        var res5 = comparator.Compare(obj1, obj3);
        Console.WriteLine("--------------------------------------------\n");

        var res6 = comparator.Compare(obj1, obj4);
        Console.WriteLine("--------------------------------------------\n");
        var res7 = comparator.Compare(obj4, obj5);
        Console.WriteLine("--------------------------------------------\n");

        Console.WriteLine(res4);
        Console.WriteLine(res5);
        Console.WriteLine(res6);
        Console.WriteLine(res7);



    }
}




