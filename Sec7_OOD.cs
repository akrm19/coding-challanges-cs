using System;
using System.Collections.Generic;
using System.Linq;

namespace CrackingTheCodingInterviewProblems
{
    public static class Sec7_OOD
    {
        /// <summary>
        /// Probs the 7 2 call center.
        /// Call Center: Imagine you have a call center with three levels of employees: 
        /// respondent, manager, and director. 
        /// An incoming telephone call must be first allocated to a respondent who is free. 
        /// If the respondent can't handle the call, he or she must escalate the call to 
        /// a manager. If the manager is not free or not able to handle it, then the call 
        /// should be escalated to a director. Design the classes and data structures for 
        /// this problem. Implement a method dispatchCall() which assigns a call to the 
        /// first available employee.
        /// 
        /// Obect defined:
        /// -Employee, Respondent, Manager, Director: IsBusy(), GetType()
        /// -Call: Handled, Responder (interface?), Escalate()
        /// -Call Center: List<Responder> responders, dispatchCall(), director, manager
        /// </summary>
        public static void Prob_7_2_CallCenter()
        {
            //See CallCenter example
        }
    }

    public class CallCenterProb
    {
        public class CallCenter
        {
            private Director director;
            private Manager manager;
            private List<Respondent> respondents;


            public CallCenter()
            {
                respondents = new List<Respondent>();
            }

            public void DispatchCall(Caller caller)
            {
                var call = new Call(caller);
                DispatchCall(call);
            }

            public void DispatchCall(Call call)
            {
                var responder = GetAvailableRespondent();

                if (responder == null)
                {
                    //Handle case where no one is available
                    //PutOnHold
                }
                else
                {
                    call.Handler = responder;
                    responder.CurrentCall = call;
                }
            }

            public Employee GetAvailableRespondent()
            {
                Employee responder = respondents.FirstOrDefault(r => !r.IsBusy);

                if (responder == null)
                {
                    if (!manager.IsBusy)
                        responder = manager;
                    else if (!director.IsBusy)
                        responder = director;
                }

                return responder;
            }
        }

        public class Call
        {
            public Employee Handler;
            public Person Caller;

            public Call(Person caller)
            {
                Caller = caller;
            }

            public bool Taken
            {
                get
                {
                    return Handler != null;
                }
            }
        }
        public class Director : Employee
        {
            public Director()
            {
                EmployeeLevel = EmployeeType.Director;
            }
        }
        public class Manager : Employee
        {
            public Manager()
            {
                EmployeeLevel = EmployeeType.Manager;
            }
        }
        public class Respondent : Employee
        {
            public Respondent()
            {
                EmployeeLevel = EmployeeType.Responder;
            }
        }

        public abstract class Employee : Person
        {
            public bool IsBusy;
            public EmployeeType EmployeeLevel;
            public Call CurrentCall;
        }

        public enum EmployeeType
        {
            Responder,
            Manager,
            Director
        }

        public class Caller: Person
        {
        
        }

        public abstract class Person
        {
            string FirstName;
            string LastName;
        }
    }
}
