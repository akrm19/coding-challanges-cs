using System;
using System.Collections;
using System.Collections.Generic;

namespace CrackingTheCodingInterviewProblems.SystemDesign
{
    public class ParkingLot
    {
        private Stack<ParkingSpace> motoSpaces;
        private Stack<ParkingSpace> carSpaces;
        private Stack<ParkingSpace> adaSpaces;

        private Dictionary<string, ParkingSpace> parkedVehicle;

        public ParkingLot(int numOfMotoSpaces, int numOfCarSpaces, int numOfAdaSpaces)
        {
            var curSpaceNum = InitSpace(motoSpaces, 0, numOfMotoSpaces);
            curSpaceNum += InitSpace(carSpaces, curSpaceNum, numOfCarSpaces);
            InitSpace(adaSpaces, curSpaceNum, numOfAdaSpaces);

            parkedVehicle = new Dictionary<string, ParkingSpace>();
        }

        private int InitSpace(Stack<ParkingSpace> space, int currentSpaceId, int numberOfSpaces)
        {
            space = new Stack<ParkingSpace>();
            for (int i = 0; i < numberOfSpaces; i++)
                space.Push(new ParkingSpace(currentSpaceId++, ParkingSpaceType.MotorCycle));
            return currentSpaceId;
        }

        public ParkingSpace GetVehiclesParkedSpace(string vehicleLicensePlate)
        {
            return parkedVehicle.ContainsKey(vehicleLicensePlate)
                ? parkedVehicle[vehicleLicensePlate]
                : null;
        }

        public int ParkVehicle(Vehicle vehicle, bool isAda  = false)
        {
            var spaceLot = GetParkingSpace(vehicle, isAda);

            if (spaceLot.Count < 1)
                throw new Exception("some exception here");

            var space = spaceLot.Pop();

            parkedVehicle.Add(vehicle.LicensePlate, space);

            return spaceLot.Count;
        }

        private Stack<ParkingSpace> GetParkingSpace(Vehicle vehicle, bool isAda = false)
        {
            if (vehicle is Car)
            {
                if (isAda && adaSpaces.Count > 0)
                    return adaSpaces;

                return carSpaces;
            }
            
            if (vehicle is Moto)
                return motoSpaces;

            return null;
        }

        public ParkingSpace GetVehicle(string licensePlate)
        {
            return null;
        }
    }

    public class ParkingSpace
    {
        public readonly int SpaceNumber;
        public readonly ParkingSpaceType SpaceType;

        public ParkingSpace(int spaceNum, ParkingSpaceType spaceType)
        {
            SpaceNumber = spaceNum;
            SpaceType = spaceType;
        }
    }

    public enum ParkingSpaceType
    {
        MotorCycle = 0,
        Car = 1,
        AdaCar = 2
    }

    public abstract class Vehicle
    {
        public string LicensePlate;
        public readonly int SizeClass;

        public Vehicle(string licensePLate, int size)
        {
            LicensePlate = licensePLate;
            SizeClass = size;
        }
    }

    class Car : Vehicle
    {
        public Car(string licensePlate) : base(licensePlate, 1){}
    }

    class Moto : Vehicle
    {
        public Moto(string licensePlate) : base(licensePlate, 0) { }
    }
}
