# Course work
***
## Subject
- Data structures and algorithms
## The task
- Data structure: Static stack of ordered dynamic lists
- Data: Retail chain (stack) - composition of stores, Store (list) - composition of departments
- Loading and saving data: Xml format
***
## Data type
### Commercial network

    class CommecrialNetwork 
    {
        string Name;
        Shop[] Shops;
    }
    
### Shop

    class Shop 
    {
        string Name;
        Department Head; 
    }
    
### Department

    class Department
    {
         int Number;
         string Profile;
    }

***
The program implements a standard set of methods for the stack structure and list structure.
Implemented saving the structure in xml form and loading the structure from an xml file.
