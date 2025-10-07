namespace lab5_advanced_C_
{
    //task 2 

    class stringList : List <string>  , IDisposable
    {
        StreamWriter st;
        public stringList()
        {
            //create + update
            st = new StreamWriter(@"D:\ITI 3 months\c#_advanced\lab5_advanced_C#\test.txt", true);
            //after opened and writing in it , i should close it 
            //first : think in destructor , but it is not always work because of
            //        thread , the main thread if finished , its thread will not
            //        executed .
            //so , i will implement the idisponsable interface then implement the
            // i dispone function , but it must called exciplictly in the main
            // which  may be forgotten , so i will 
        }
        public  void Add(string  s)
        {
            st.WriteLine(s);
        }
        public void AddRange(List<string> s)
        {
            foreach (string i in s)
            {
                st.WriteLine(i);

            }
        }
        bool is_destructor_called = false;
        public void Dispose() {
            st.Dispose();
            if (is_destructor_called == false)
            {
                GC.SuppressFinalize(this);//if i have a destructor , deallocate it
                                          //from the finailization queue
            }
        }
        ~stringList() {
            is_destructor_called = true;
            Dispose(); //if i forget to call it , i will use the destructor 
        }
    }
    internal class Program
    {
        //task 1 
        public static void test_fun(byte x , int y , out int z , out int r)
        {
            try
            {
                checked {
                    x++; //if x > 255 , it throws overflow exception 
                    z = x / y;//throw exception because of try 
                }
                unchecked
                {
                    r = x + y; //it ignores overflow , it does not matter 
                }
            }
            catch ( DivideByZeroException ex)
            {
                //may be: throw , throw ex , new Exception("error")
                throw new Exception ("new divide by zero exception : " + ex.Message);
            }
            catch (Exception ex)
            {
                //works for all types of errors 
                throw new Exception( "new general exception : " + ex.Message);
            }
            finally
            {
                //always work try/catch
                Console.WriteLine("hi from the final block");
            }


        }



        static void Main(string[] args)
        {
            //task 1

            //Convert :-
            //on invalid input throws format exception
            //on null return 0
            //---------------------------------------------//
            //TryParse :-
            //on invalid input returns false
            //on null returns false, out = 0

            //notes :- clr who creates the exception object 
            //any function has stack frame and the whole program of frames is stack trace 

            bool ok1  = byte.TryParse(Console.ReadLine(),out byte x);
            bool ok2  = int.TryParse(Console.ReadLine(),out int y);
            int z, r;
            if(ok1 == true  && ok2 == true)
            {
                test_fun(x, y, out z, out r);

                Console.WriteLine($"z = {z} , r = {r}");
            }
            else
            {
                Console.WriteLine("you entered invalid input ");
            }


            //task 2 
            using (stringList l = new stringList()){//using implicity call dispose
                string s = "alyaa";
                List<string> ls = new List<string>() { "nada", "nayra", "noran" };
                
                l.Add(s);
                l.AddRange(ls);
                //l.Dispose(); no need because of using :) 
            }


        }
    }
}
