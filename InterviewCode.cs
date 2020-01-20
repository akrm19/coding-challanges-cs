using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CrackingTheCodingInterviewProblems
{
    public class InterviewCode
    {
        public InterviewCode()
        {
        }
    }

    public class HouseList
    {
        private List<House> _houses = new List<House>();

        public HouseList(int[] houseStates)
        {
            _houses.AddRange(houseStates.Select(houseState => new House(houseState)));
        }

        public void UpdateStatesForNewDay()
        {
            var updatedHouseStates = new List<House>();

            for (int i = 0; i < _houses.Count; i++)
            {
                updatedHouseStates.Add(new House(GetNextDayStateForHouse(i)));
            }

            _houses = updatedHouseStates;
        }

        private int GetNextDayStateForHouse(int houseIndex)
        {
            var prevNeightboorState = houseIndex < 1
                ? 0
                : _houses[houseIndex - 1].State;

            var nextNeighboorState = houseIndex == _houses.Count - 1
                ? 0
                : _houses[houseIndex + 1].State;

            return nextNeighboorState == prevNeightboorState
                ? 0
                : 1;
        }
    }

    public class House
    {
        public House(int state)
        {
            State = state;
        }

        public bool IsActive()
        {
            return State == 1;
        }

        public int State
        {
            get; set;
        }
    }
}
