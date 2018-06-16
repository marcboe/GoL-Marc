using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life_Marc
{
    class Cell
    {
        bool state;
        int environment;
        public Cell()
        {
            SetEnv(0);
            SetState(false);
        }

        public void SetState(bool State) //Status einlesen
        {
            state = State;
        }

        public bool GetState() // Status ausgeben
        {
            return state;
        }

        public void SetEnv (int Number) // Nachbarn einlesen
        {
            environment = Number;
        }

        public int GetEnv() // Nachbarn ausgeben
        {
            return environment;
        }

        internal void SetEnv(object count)
        {
            throw new NotImplementedException();
        }
    }
}
