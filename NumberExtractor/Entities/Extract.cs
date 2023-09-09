using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberExtractor.Entities
{
    static class Extract
    {
        public static string[] FindNumber(string[] lines)
        {
            List<string> principalList = new List<string>();
            List<string> allFileLines = new List<string>();
            string[] vetNumbers;

            foreach (string s in lines)
            {                
                string aux = GetNumbers(s);
                if(aux != null)
                {
                    allFileLines.Add(aux);
                }
            }

            Console.Write("How many DDDs number do you want assign in your search? ");
            int[] dddVet = new int[int.Parse(Console.ReadLine())];
            for (int i = 0; i < dddVet.Length; i++)
            {
                Console.Write($"Enter the #{i + 1} DDD ex; 11 : ");
                dddVet[i] = int.Parse(Console.ReadLine());
            }

            foreach (string s in allFileLines)
            {
                List<string> phoneNumbers = RemoveSameNumber(DDITest(s), DDDTest(s, dddVet), NumberWithoutPrefix(s));
                // ---> Precisamos passar com prioridade as listas onde os números estejam mais completos, instânciando-os
                // primeiro que os com menos informações. Caso isso não ocorra, podemos pegar um número incompleto, usá-lo como parâmetro e depois 
                // remover um outro número com mais especifícações. Ex: Pagamos o número 9 5512-3487 e comparamos com o número +55 11 9 5512-3487,
                // Nesse caso, vamos acabar removendo da lista o número +55 11 9 5512-3487, e não queremos isso. Queremos manter o número mais completo
                // e remover o outro com menos informações.
                foreach (string obj in phoneNumbers)
                {
                    if (obj.Contains('+') && obj.IndexOf('+') == 0 || !obj.Contains('+'))
                    {
                        principalList.Add(obj);

                    }
                }
            }

            vetNumbers = principalList.ToArray();

            return vetNumbers;
        }

        static List<string> RemoveSameNumber(List<string> l1, List<string> l2, List<string> l3)
        {
            List<string> AllNumbers = new List<string>(l1.Concat(l2).Concat(l3).ToList());
            List<string> NewListnumber = new List<string>();

            for (int i = 0; AllNumbers.Count != i;)
            {
                string aux = new string(AllNumbers[i]);
                NewListnumber.Add(aux);
                int indice = aux.Count() - 9;
                string teste = aux.Substring(indice, 9);
                AllNumbers.RemoveAll(X => X.Contains(teste));
            }

            return NewListnumber;
        }

        static string GetNumbers(string stringLine)
        {
            string numbers = null;
            for (int i = 0; i < stringLine.Length; i++)
            {
                char aux = stringLine[i];
                if (char.IsDigit(aux) || aux == '+')
                {
                    numbers += aux;
                }
            }
            return numbers;
        }

        static List<string> DDITest(string findNumber)
        {
            List<string> numberList = new List<string>(NumberVerification(findNumber, "+55119"));                       
           
            return numberList;

        }

        static List<string> DDDTest(string findNumber, int[] ddds)
        {
            List<string> numberList = new List<string>();
            foreach (int ddd in ddds)
            {
                string s = ddd + "9";
                List<string> aux = new List<string>(NumberVerification(findNumber, s));
               
            }
            return numberList;
        }

        static List<string> NumberWithoutPrefix(string findNumber)
        {
            List<string> numberList = new List<string>(NumberVerification(findNumber, "9"));
                      
            return numberList;
        }

        static List<string> NumberVerification(string findNumber, string stringIndice)
        {
            int intNumber;
            if (stringIndice.Length == 1)
            {
                intNumber = 9;
            }
            else if (stringIndice.Length == 3)
            {
                intNumber = 11;
            }
            else
            {
                intNumber = 14;
            }

            List<string> numberList = new List<string>();
            string auxNumber = findNumber;
            int auxIndex = auxNumber.IndexOf(stringIndice);

            while (auxIndex >= 0)
            {
                if (auxNumber.Length - auxIndex >= intNumber)
                {
                    numberList.Add(auxNumber.Substring(auxIndex, intNumber));
                }

                auxNumber = auxNumber.Remove(auxIndex, stringIndice.Length);
                auxIndex = auxNumber.IndexOf(stringIndice);
            }                      

            return numberList;
        }
    }
}
