using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public  class Customer
{
    public Customer(string customerID, string companyName)
    {
        CustomerID = customerID;
        CompanyName = companyName;
        Orders = new Order[10];
    }

    public Customer() { }

    public string CustomerID { get; set; }
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public Order[] Orders { get; set; }


    public override string ToString()
    {
        return $"{CustomerID},{CompanyName},{Address}, {City} , {Region}, {PostalCode} , {Country} , {Phone} , {Fax}";
    }
}

   

