using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car = PartV.Chapter10.EvMain.ESpace.Car;

namespace PartV.Chapter10
{
    public class CarCollection : IEnumerable
    {
        private ArrayList arCars = new ArrayList();
        public Car GetCar(int pos) => (Car) arCars[pos];
        public void AddCar(Car C) => arCars.Add(C);
        public void ClearCars() => arCars.Clear();
        public int Count => arCars.Count;
        IEnumerator IEnumerable.GetEnumerator() => arCars.GetEnumerator();
        public IEnumerator GetEnumerator()
        {
            return arCars.GetEnumerator();
        }
    }
}
