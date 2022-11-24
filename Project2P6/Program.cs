using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
namespace Project2P6
{
    class Program
    {
        /// *********************************************************************************///
        /// Programming 2 Project, Tester: Tyler(a friend of mine), Project by: Chase Lipari ///
        /// *********************************************************************************/// 

        static void Main(string[] args)
        {
            List<Employee> employeeList = new List<Employee>();
            string employeeFile = "./employees.txt";
            string timeWorkedFile = "./employeesHours.txt";
            string reportFile = "./report.txt";
            string employeeNumberOrderFile = "./employeeNumberOrder.txt", nameOrderFile = "./nameOrder.txt", timeOrderFile = "./timeOrder.txt", payOrderFile = "./payOrder.txt";
            int employeeNumber = 2002, fakeeEmployeeNumber = 1111;
            TimeStamp timeWorked = new TimeStamp(30, 20, 10);
            if(GenerateEmployeeListFromFile(employeeList, employeeFile) && ProcessTimeWorkedFile(employeeList, timeWorkedFile))
            {
                List<Employee> employeeListID = SortEmployeeListByID(employeeList);
                List<Employee> employeeListName = SortEmployeeListByLastName(employeeList);
                List<Employee> employeeListTime = SortEmployeeListByTime(employeeList);
                List<Employee> employeeListPay = SortEmployeeListByPay(employeeList);
                if (PrintReport(employeeList, reportFile))
                {
                    WriteLine(reportFile + " written succesfully");
                }
                if (PrintReport(employeeListID, employeeNumberOrderFile))
                {
                    WriteLine(employeeNumberOrderFile + " written succesfully");
                }
                if (PrintReport(employeeListName, nameOrderFile))
                {
                    WriteLine(nameOrderFile + " written succesfully");
                }
                if (PrintReport(employeeListTime, timeOrderFile))
                {
                    WriteLine(timeOrderFile+ " written succesfully");
                }
                if (PrintReport(employeeListPay, payOrderFile))
                {
                    WriteLine(payOrderFile + " written succesfully");
                }




            }
            else
            {
                WriteLine("files could not be read");
            }

        }
        /// <summary>
        /// Populates a list of employees from the provided files. Return true if reading from file worked, false otherwise. 
        /// </summary>
        /// <param name="employeeList"></param>
        /// <param name="fileName"></param>
        public static bool GenerateEmployeeListFromFile(List<Employee> employeeList, string fileName)
        {
            if (File.Exists(fileName))
            {
                StreamReader streamReader = null;
                string line;
                int count = 0;
                try
                {
                    streamReader = new StreamReader(fileName);
                    string[] seperator; 
                    while ((line = streamReader.ReadLine()) != null)
                    {

                        seperator = line.Split(',');
                        count++;
                        employeeList.Add(new Employee { IdNumber = int.Parse(seperator[0]), FirstName = seperator[1], LastName = seperator[2], PayRate = decimal.Parse(seperator[3]) });
                        
                    }
    

                }
                catch (Exception e)
                {
                    Write("error reading file: " + e.Message);
                    
                }
                finally
                {
                    if (streamReader != null)
                    {
                        streamReader.Close();
                    }
             

                }
                return true;

            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Adds working hours stated in the provided file to the employees. Return true if reading from file worked, false otherwise.
        /// </summary>
        /// <param name="employeeList"></param>
        /// <param name="fileName"></param>
        public static bool ProcessTimeWorkedFile(List<Employee> employeeList, string fileName)
        {
            if (File.Exists(fileName))
            {
                StreamReader streamReader = null;
                string line;
                
                try
                {
                    streamReader = new StreamReader(fileName);
                    string[] seperator;
                    string[] timeseperator;
                    while ((line = streamReader.ReadLine()) != null)
                    {

                        seperator = line.Split(',');
                        for(int i = 0; i < employeeList.Count(); i++)
                        {
                            if(employeeList[i].IdNumber == int.Parse(seperator[0]))
                            {
                                timeseperator = seperator[1].Split(':');
                                employeeList[i].HoursWorked.Hours += int.Parse(timeseperator[0]);
                                employeeList[i].HoursWorked.Minutes += int.Parse(timeseperator[1]);
                                employeeList[i].Pay = (employeeList[i].HoursWorked.Hours * employeeList[i].PayRate) + (decimal)((employeeList[i].HoursWorked.Minutes / 60) * employeeList[i].PayRate);

                            }
 
                        }

                        
                    }

                }
                catch (Exception e)
                {
                    Write("error reading file: " + e.Message);

                }
                finally
                {
                    if (streamReader != null)
                    {
                        streamReader.Close();
                    }


                }
                return true;

            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Generated a text report of the provided employee list to the provided file. Return true if writing to file worked, false otherwise.
        /// </summary>
        /// <param name="employeeList"></param>
        /// <param name="fileName"></param>
        public static bool PrintReport(List<Employee> employeeList, string fileName)
        {
            if (File.Exists(fileName))
            {
                StreamWriter streamWrite = null;
                TimeStamp totalTime = new TimeStamp();
                decimal totalPay = 0;
                try
                {
                    streamWrite = new StreamWriter(fileName, false);
                    for (int i = 0; i < employeeList.Count(); i++)
                    {
                        streamWrite.WriteLine(employeeList[i]);
                        totalTime += employeeList[i].HoursWorked;
                        totalPay += employeeList[i].Pay;
                    }
                    streamWrite.WriteLine("Total time Worked: {0}",totalTime);
                    streamWrite.WriteLine("Total Pay: {0: 0.00}", totalPay);

                }
                catch (Exception e)
                {
                    WriteLine("Error writing file: " + e.Message);

                }
                finally
                {
                    if (streamWrite != null)
                        streamWrite.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
            
        }
        /// <summary>
        ///Given an employee number and hours worked, update the specific employee in the list. Return true if employeeNumber is found in the list, false otherwise. 
        /// </summary>
        /// <param name="employeeList"></param>
        /// <param name="employeeNumber"></param>
        /// <param name="timeWorked"></param>
        public static bool AddTimeWorkedToEmployee(List<Employee> employeeList, int employeeNumber, TimeStamp timeWorked)
        {
            if(employeeList.Exists(x => x.IdNumber == employeeNumber))
            {
                employeeList.Find(x => x.IdNumber == employeeNumber).HoursWorked += timeWorked;
                //adding pay through here because I couldnt work it out in the class 
                employeeList.Find(x => x.IdNumber == employeeNumber).Pay = (employeeList.Find(x => x.IdNumber == employeeNumber).HoursWorked.Hours * employeeList.Find(x => x.IdNumber == employeeNumber).PayRate) + 
                    ((employeeList.Find(x => x.IdNumber == employeeNumber).HoursWorked.Minutes / 60) * employeeList.Find(x => x.IdNumber == employeeNumber).PayRate);
                return true;
            }
            else
            {
                return false;
            }
        }

        //Sorting Methods to change the order of elements in the list.
        public static List<Employee> SortEmployeeListByLastName(List<Employee> employeeList)
        {
            var sortedEmployee = from employee in employeeList
                                          orderby employee.LastName 
                                          select employee;

            return sortedEmployee.ToList();
        }

        //Sorting Methods to change the order of elements in the list.
        public static List<Employee> SortEmployeeListByID(List<Employee> employeeList)
        {
            var sortedEmployee = from employee in employeeList
                                 orderby employee.IdNumber 
                                 select employee;

            return sortedEmployee.ToList();
        }
        //Sorting Methods to change the order of elements in the list.
        public static List<Employee> SortEmployeeListByTime(List<Employee> employeeList)
        {
            var sortedEmployee = from employee in employeeList
                                 orderby employee.HoursWorked.ConvertToSeconds() 
                                 select employee;

            return sortedEmployee.ToList();
        }
        //Sorting Methods to change the order of elements in the list.
        public static List<Employee> SortEmployeeListByPay(List<Employee> employeeList)
        {
            var sortedEmployee = from employee in employeeList
                                 orderby employee.Pay 
                                 select employee;

            return sortedEmployee.ToList();
        }


    }
}
