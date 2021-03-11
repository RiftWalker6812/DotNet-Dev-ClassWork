using System.Windows.Forms;

namespace PartV.EhLibs
{
    namespace CarLibrary
    {
        // Represents the state of the engine.
        public enum EngineState
        { engineAlive, engineDead }

        // The abstract base class in the hierarchy.
        public abstract class Car
        {
            public string PetName { get; set; }
            public int CurrentSpeed { get; set; }
            public int MaxSpeed { get; set; }
            protected EngineState egnState = EngineState.engineAlive;

            public EngineState EngineState
            {
                get { return egnState; }
            }

            public abstract void TurboBoost();

            public Car()
            {
            }

            public Car(string name, int maxSp, int currSp)
            {
                PetName = name; MaxSpeed = maxSp; CurrentSpeed = currSp;
            }
        }

        public class SportsCar : Car
        {
            public SportsCar()
            {
            }

            public SportsCar(string name, int maxSp, int currSp)
            : base(name, maxSp, currSp) { }

            public override void TurboBoost()
            {
                MessageBox.Show("Ramming speed!", "Faster is better...");
            }
        }

        public class MiniVan : Car
        {
            public MiniVan()
            {
            }

            public MiniVan(string name, int maxSp, int currSp)
            : base(name, maxSp, currSp) { }

            public override void TurboBoost()
            {
                // Minivans have poor turbo capabilities!
                egnState = EngineState.engineDead;
                MessageBox.Show("Eek!", "Your engine block exploded!");
            }
        }
    }
}