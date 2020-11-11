using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp3
{
    class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public User () : this("default", 0)
        {            
        }
        public User (string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }


    }

   
}
