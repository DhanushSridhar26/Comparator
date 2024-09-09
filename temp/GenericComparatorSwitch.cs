using System.Collections;
using System.Globalization;
using System.Reflection;

namespace GenericComparator
{
    public class GenericComparatorSwitchClass
    {

        public bool CompareObject<T>(T obj1, T obj2)
        {

            return (obj1, obj2) switch
            {
                (null,null) => true,
                (_ ,null) => false,
                (null,_) => false,

                _ when obj1.GetType() != obj2.GetType() => false,

                (string a,string b) => a.Equals(b),
                _ when obj1.GetType().IsValueType => obj1.Equals(obj2),

                _ when obj1 is Array a1 && obj2 is Array a2 => CompareArrays(a1,a2),
                _ when obj1 is IEnumerable e1 && obj2 is IEnumerable e2 => CompareCollections(e1,e2),

                _ => CompareObjectFieldsAndProps(obj1,obj2)
            };

        }

        bool CompareArrays(Array array1, Array array2)
        {
            if (array1.Length != array2.Length)

                return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (!CompareObject(array1.GetValue(i), array2.GetValue(i)))
                    return false;
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

            //set
            if (collec1 is ISet<object> && collec2 is ISet<object>)
            {
                return CompareSets((ISet<object>)collec1, (ISet<object>)collec2);
            }

            //other collections
            return CompareEnumerables(collec1, collec2);
        }

        bool CompareDictionaries(IDictionary dict1, IDictionary dict2)
        {
            if (dict1.Count != dict2.Count)
            {
                return false;
            }

            foreach (DictionaryEntry entry in dict1)
            {
                if (!dict2.Contains(entry.Key))
                {
                    return false; 
                }

                var value1 = entry.Value;
                var value2 = dict2[entry.Key];

                if (!CompareObject(value1, value2))
                {
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
            var enumerator1 = list1.GetEnumerator();
            var enumerator2 = list2.GetEnumerator();

            while (enumerator1.MoveNext() && enumerator2.MoveNext())
            {
                var value1 = enumerator1.Current;
                var value2 = enumerator2.Current;

                if (!CompareObject(value1, value2))
                {
                    return false;
                }
            }

            return !(enumerator1.MoveNext() || enumerator2.MoveNext());
        }

        bool CompareObjectFieldsAndProps(object obj1, object obj2)
        {
            foreach (var field in obj1.GetType().GetFields( BindingFlags.Public| BindingFlags.NonPublic | BindingFlags.Instance))
            {
                Console.WriteLine("Field" + field);
                var value1 = field.GetValue(obj1);
                var value2 = field.GetValue(obj2);

                    if (!CompareObject(value1, value2))
                    {
                        return false;
                    }
                
            }

            foreach (var prop in obj1.GetType().GetProperties(BindingFlags.Public| BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (prop.CanRead && prop.GetIndexParameters().Length == 0)
                {
                    Console.WriteLine("Property : " + prop);
                    try
                    {
                        var value1 = prop.GetValue(obj1);
                        var value2 = prop.GetValue(obj2);

                        if (!CompareObject(value1, value2))
                        {
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
