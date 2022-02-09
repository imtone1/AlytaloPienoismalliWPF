using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlytaloWPF
{
    class Sauna
    {
        
        public Boolean Switched { get; set; }
        public int Lampotila { get; set; }
        private int tavoiteLampotila;

        public int TavoiteLampotila
        {
            get
            {
                return tavoiteLampotila;
            }
            set
            {
                if (Regex.IsMatch(value.ToString(), "^([0-9]{1,2})$"))
                {
                    tavoiteLampotila = value;
                }
                else
                { if (value==0)
                    {

                    }else
                    tavoiteLampotila = 0;
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void AsetaSaunaMaxArvo(int annettuLampotila)
        {
            if ((annettuLampotila >= 0) && (annettuLampotila <= 100))
                tavoiteLampotila = annettuLampotila;
            else
            {
                tavoiteLampotila = 0;
                throw new ArgumentOutOfRangeException();
            }
        }
        public int OtaAsetettuMaxArvo()
        {
            return tavoiteLampotila;
        }
        public void SaunaPaalle()
        {
            Switched = true;
            Lampotila ++;
        }
        public void SaunaPois()
        {
            Switched = false;
            if (Lampotila>=0) Lampotila -= 1;
        }

    }
}
