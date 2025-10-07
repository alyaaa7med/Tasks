using System;
using System.Collections.Generic;

namespace lab3_advanced_C_
{

    //notes :- 
    /*
  
    1- internal interface IExample < in  t1 , out t2 > can be implemented many times for different data types 
    t1 : can be any data type  but only for input 
    t2 : ......................but only for output 
    also , i can implement it many times for the same class :D

    2- You define type parameters when declaring the interface.
    You pass actual types either when implementing the interface in a class or when declaring/instantiating
    a variable/object of that interface.

    3- A delegate is like a type-safe pointer to a method.Once you define a delegate with a specific signature 
    (parameters + return type), you can assign any method that matches that signature to it.

    4- sending some of code to a function can be achieved by using delegates ==> which now called callback function
       ex :- 
       static list<int> Find(List<int> l1 , Func<int ,bool> cond) {
        var res = new List <int> ();
        foreach(i in l1){
            if(cond.Invoke(i)) 
                    res.Add(i);
            }
        return  res ;
        }

      //in main 
      
      static void Main(){
      List<int> l1 = new List<int> { 1, 2, 3, 4, 5, 5, 6, 6, 7 };
      var res = Find(l,a=>a%2!=0);  // a=>a%3 !=0 // a=>a%5 != 0 // delegate (int r ) { return r%5 == 0;} 
      foreach (int i in res ){
      conosle.writeLine(i) ;}

    }

    5- sealed class , struct => no inheritance 
    6- extension method :-
    - static function defined in static class , called as if they belong to the Type (built-in or user-defined ,
    struct/class/interface ) with dot syntax 
    - e.g : string name = "alyaa"
          name.myExtensionMethod( )  
    what happened : The compiler gives you the illusion that the method is part of string 
    (so you can call it with dot notation) 
    But in reality, The compiler just rewrites the call behind the scenes :           
        name.myExtensionMethod( ) => will be:  ExtensionClass.myExtensionMethod(name) 
    & the string class implementation does not change 
    - benefits :- 
      1- Cleaner in readablity (linq)
      2- Add functionality to types we  don’t control
      
    - how it is implemented : 
    public static class StringExtensions {
    public static bool IsNullOrEmpty(this string value) //this string value tells the compiler:
                                                        //“whatever object is before the dot, pass it into 
                                                        //this parameter (value)
    {
        return string.IsNullOrEmpty(value);
    }
    }
    string name = null;
    bool check = name.IsNullOrEmpty();

    Big difference:
    In class methods: this = “the current object instance.”
    In extension methods: this = “the object before the dot gets passed here.”
       
    */
    /* equals in struct => checks the data values inside it 
       equals in class => checks the reference only 
    */
    /*
     == operator :  defined  in class and check the equality 
                    but does not in struct so need to be overloaded there
     */
    /* 
     A) object.ReferenceEquals(e1,e2): 
     1- Static method & Cannot be overridden.
     2- for class : Always checks if two references point to the exact same object in memory.

     B) e1.Equals(e2) 
    1- instance method can be overriden 
    2- for class  : if the class does not override the Equals , it will call 
                    object.Equals(s1, s2)
       for sturct : if the struct does not override Equals ,it will call default one 
                    which is for value equality (field by field)

     c) object.Equals(e1,e2) :D
    it is valid for both class & struct 
    object o = mystruct_var // boxinggggg :)

    public static bool Equals(object objA, object objB) {
    if (objA == objB) return true;        // both null or same reference
    if (objA == null || objB == null) return false;
    return objA.Equals(objB);             // call instance Equals()
    }

     */
    /* important :))) 
     * when we override the equal :-
     public override bool Equals (object? obj) {
        if(obj == null ) return false;
        else if( obj.GetType() != GetType() ) return false ;
        Emp e = (Emp) obj; 
        return Id==e.Id && Name==e.name
     }
     */



    //task 1 

    //i extend the generic built-in List
    static class ExtensionClass
    {

        public static List<string> myFind(this List<string> list, Func<string, bool> cond)
        {
            List<string> result = new List<string>();

            foreach (string i in list)
            {
                if (cond(i))
                {
                    result.Add(i);
                }
            }

            return result;
        }


    }


    //task 2
    class Emp  //publisher 
    {
        public int Id { set; get; }
        public string Name { set; get; }
        int _countAbsDays;

        //delegates 
        public event Action<Emp> remove_emp_from_Dep; //return void
        public event Action<Emp> remove_emp_from_Club; //return void 


        public int CountAbsDays
        {

            get
            {
                return _countAbsDays;
            }
            set
            {
                if (value > 3)
                {
                    remove_emp_from_Dep?.Invoke(this); //fire remove 
                    remove_emp_from_Club?.Invoke(this);
                }
                _countAbsDays = value;


            }

        }

        public override string ToString()
        {
            return $"Employee name is {Name} , with Id {Id} ";
        }
        public override bool Equals(object? obj) // override :checking the inside data 
        {
            if (obj == null) return false; // compare with null 
            else if (obj.GetType() != GetType()) return false; // compare with different type

            return Id == ((Emp)obj).Id && Name == ((Emp)obj).Name;
        }
    }
    class Department //subscriber1
    {
        public int Id { set; get; }
        public string Name { set; get; }
        List<Emp> employees = new List<Emp>();

        public void Add(Emp e)
        {
            employees.Add(e);
            e.remove_emp_from_Dep += Remove; //add Remove to the invocation list  
        }
        public void Remove(Emp e) //event handler 
        {
            employees.Remove(e); //will call the equals overriden here 
            Console.WriteLine("removed successfully from Departments");
        }
        public override string ToString()
        {
            return $"Department name is {Name} , with Id {Id} ";
        }

        public override bool Equals(object? obj) // override :checking the inside data 
        {
            if (obj == null || obj.GetType() != GetType()) return false;

            if (Id != ((Department)obj).Id || Name != ((Department)obj).Name) return false; //not of the same name,id
            if (employees.Count != ((Department)obj).employees.Count) return false;//not same length

            List<Emp> otherEmplist = ((Department)obj).employees;

            for (int i = 0; i < otherEmplist.Count; i++)
            {
                if (otherEmplist[i].Id != employees[i].Id || otherEmplist[i].Name != employees[i].Name)
                {
                    return false;
                }
            }
            return true;

        }
    }
    class Club //subscriber2
    {

        public int Id { set; get; }
        public string Name { set; get; }
        List<Emp> employees = new List<Emp>();

        public void Add(Emp e)
        {
            employees.Add(e);
            e.remove_emp_from_Club += Remove; //add Remove to the invokation list 

        }
        public void Remove(Emp e) //event handler 
        {
             employees.Remove(e); //will call the equals overriden here 
            Console.WriteLine("removed successfully from club ");
        }

        public override string ToString()
        {
            return $"Club name is {Name} , with Id {Id} ";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;

            if (Id != ((Club)obj).Id || Name != ((Club)obj).Name) return false; //not of the same name,id
            if (employees.Count != ((Club)obj).employees.Count) return false;//not same length

            List<Emp> otherEmplist = ((Club)obj).employees;

            for (int i = 0; i < otherEmplist.Count; i++)
            {
                if (otherEmplist[i].Id != employees[i].Id || otherEmplist[i].Name != employees[i].Name)
                {
                    return false;
                }
            }
            return true;

        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //task 1 

            
            List<string> names = new List<string>() { "alyaa", "hoda", "ahmed", "amr", "name" };
            List<string> result1 = names.myFind(str_value => str_value.Length > 3);
            for(int i  = 0; i < result1.Count; i++)
            {
                Console.Write(result1[i]+"  ");
            }
            Console.WriteLine("\n===========================================");
            List<string> result2 = names.myFind(str_value => str_value[0] == 'a');
            for (int i = 0; i < result2.Count; i++)
            {
                Console.Write(result2[i] + "  ");
            }
            Console.WriteLine("\n===========================================");
            List<string> result3 = names.myFind(str_value => str_value[str_value.Length-1] == 'e');
            for (int i = 0; i < result3.Count; i++)
            {
                Console.Write(result3[i] + "  ");
            }
            Console.WriteLine("\n===========================================");
            List<string> result4 = names.myFind(str_value => str_value.Contains('a'));
            for (int i = 0; i < result4.Count; i++)
            {
                Console.Write(result4[i] + "  ");
            }
            Console.WriteLine("\n===========================================");
            

            //task 2 

            //create 6 Employees 

            Emp e1 = new Emp() { Id = 1, Name = "alyaa" };
            Emp e2 = new Emp() { Id = 2, Name = "nayra" };
            Emp e3 = new Emp() { Id = 3, Name = "nada" };
            Emp e4 = new Emp() { Id = 4, Name = "eman" };
            Emp e5 = new Emp() { Id = 5, Name = "basma" };
            Emp e6 = new Emp() { Id = 6, Name = "hoda" };

            //create 1 club , 1 dept 
            Club c1 = new Club() { Id = 1, Name = "sports" };
            c1.Add(e1); c1.Add(e2); c1.Add(e3);
            Department d1 = new Department() { Id = 1, Name = "HR" };
            d1.Add(e4); d1.Add(e5); d1.Add(e6);

            e1.CountAbsDays = 4;
            e2.CountAbsDays = 4;
            e4.CountAbsDays = 4;



            Console.ReadKey();


        }
    }
}
