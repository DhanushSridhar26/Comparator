using System.Collections;
using System.Globalization;
using System.Reflection;

namespace GenericComparator
{
    public class GenericComparatorOldClass
    {
        public bool Compare<T>( T obj1, T obj2)
        {
            Console.WriteLine($"Comparing Objects : {obj1} and {obj2}\n");
            bool res = CompareObject(obj1, obj2);
            Console.WriteLine($"\nResult: {res}\n\n");
            return res;
        }

        bool CompareObject<T>(T obj1, T obj2)
        {

            //Null CHeck
            if(obj1==null && obj2 == null)
            {
                return true;
            }

            if ((obj1 == null && obj2 != null) || (obj1 !=null && obj2==null))
            {
                Console.WriteLine("One of the Object is null.Hence they dont match");
                return false;
            }


            //Check Types
            if (obj1.GetType() != obj2.GetType())
            {
                Console.WriteLine("Object Types dont match");
                return false;
            }
            
            //basic value type comparison
            if (obj1 is string ||obj1 is Int32 || obj1.GetType().IsValueType)
            {
                return obj1.Equals(obj2);
            }

            //Array Comparison
            if(obj1 is Array array1 && obj2 is Array array2)
            {
                return CompareArrays( array1, array2);
            }

            //Comapre Collections
            if (obj1 is IEnumerable en1 && obj2 is IEnumerable en2)
            {
                return CompareCollections(en1, en2);
            }

            //else comparee objects with fields and propeties 
            return CompareObjectFieldsAndProps(obj1, obj2);
        }

        bool CompareArrays(Array array1, Array array2)
        {
            if (array1.Length != array2.Length)
                return false;

            int[] visited = new int[array1.Length];

            // Check if all elements match, ignoring order
            for (int i = 0; i < array1.Length; i++)
            {
                visited.SetValue(0, i);
            }

            for (int i = 0; i < array2.Length; i++)
            {
                bool found = false;
                for(int j = 0; j < array1.Length; j++)
                {
                    if (visited[j]==0 && CompareObject(array1.GetValue(j), array2.GetValue(i))){
                        found = true;
                        visited[j] = 1;
                        break;
                    }
                }

                if (!found)
                {
                    return false;
                }
                
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (visited[i] == 0)
                {
                    return false;
                }
            }

            return true;
        }

        bool CompareCollections(IEnumerable collec1, IEnumerable collec2)
        {
            // dictionary
            if (collec1 is IDictionary && collec2 is IDictionary)
            {
                return CompareDictionaries((IDictionary)collec1, (IDictionary)collec2);
            }

            //other collections
            return CompareEnumerables(collec1, collec2);
        }

        bool CompareDictionaries(IDictionary dict1, IDictionary dict2)
        {
            if (dict1.Count != dict2.Count)
            {
                Console.WriteLine($"Number of elements in Dictionaries are not same.");
                return false;
            }

            foreach (DictionaryEntry entry in dict1)
            {
                if (!dict2.Contains(entry.Key))
                {
                    Console.WriteLine($"{entry.Key} is missing in Dictionary 2");
                    return false; 
                }

                var value1 = entry.Value;
                var value2 = dict2[entry.Key];

                if (!CompareObject(value1, value2))
                {
                    Console.WriteLine($"Object {value1} and {value2} doesnt match in the Dictionary for key {entry.Key}");
                    return false;
                }
            }

            return true;
        }

        bool CompareSets(ISet<object> set1, ISet<object> set2)
        {
            return set1.SetEquals(set2); 
        }

        bool CompareEnumerables(IEnumerable list1, IEnumerable list2)
        {

            var list1Elements = list1.Cast<object>().ToList();
            var list2Elements = list2.Cast<object>().ToList();

            // If counts don't match, return false
            if (list1Elements.Count != list2Elements.Count)
            {
                return false;
            }


            // Check if all elements match, ignoring order
            foreach (var item in list2Elements)
            {
                if (!list1Elements.Any())
                {
                    return false;
                }

                var fval = list1Elements.FirstOrDefault(e => CompareObject(e, item), null);
                if (fval == null)
                {
                    Console.WriteLine($"Element {item} is missing in Second Collction");
                    return false;
                }
                list1Elements.Remove(fval);

                //if (!list2Elements.Any(e => { return CompareObject(e, item) && s1.Add(e); }))
                //{
                //    Console.WriteLine($"Element {item} is missing in Second Collction");
                //    return false;
                //}
            }

            if (list1Elements.Any()) { return false; }

            return true;

            /*//var enumerator1 = list1.GetEnumerator();
            //var enumerator2 = list2.GetEnumerator();

            //while (enumerator1.MoveNext() && enumerator2.MoveNext())
            //{
            //    var value1 = enumerator1.Current;
            //    var value2 = enumerator2.Current;

            //    if (!CompareObject(value1, value2))
            //    {
            //        Console.WriteLine($"Object {value1} and {value2} doesnt match in the Collection");
            //        return false;
            //    }
            //}

            //return !(enumerator1.MoveNext() || enumerator2.MoveNext());*/

        }

        bool CompareObjectFieldsAndProps(object obj1, object obj2)
        {
            foreach (var field in obj1.GetType().GetFields( BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var value1 = field.GetValue(obj1);
                var value2 = field.GetValue(obj2);

                    if (!CompareObject(value1, value2))
                    {
                    Console.WriteLine($"Object {value1} and {value2} doesnt match in the Given Objects in field {field.Name}");
                    return false;
                    }
                
            }

            foreach (var prop in obj1.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (prop.CanRead && prop.GetIndexParameters().Length == 0)
                {
                    try
                    {
                        var value1 = prop.GetValue(obj1);
                        var value2 = prop.GetValue(obj2);

                        if (!CompareObject(value1, value2))
                        {
                            Console.WriteLine($"Object {value1} and {value2} doesnt match in the given Objecs in property {prop.Name}");

                            return false;
                        }
                    }
                    catch (TargetException ex)
                    {
                        Console.WriteLine($"Error accessing property '{prop.Name}': {ex.Message}");
                    }
                }
            }

            return true;
        }

    }
}
