using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ControleDePresencaTest
{
    [TestClass]
    public class TestaCruds
    {
        [TestMethod]
        public void TestMethod1()
        {
            int[] x = new int[] { 1,1,0,1,0,0 };
            //solution(x,3) ;
            int ret = solution(x);


        }




        int solution(int[] A)
        {
            int n = A.Length;
            int result = 0;
            for (int i = 0; i < n - 1; i++)
            {
                if (A[i] != A[i + 1])
                    result = result + 1;
            }
            int r = 0;
            for (int i = 0; i < n; i++)
            {
                int count = 0;
                if (i > 0)
                {
                    if (A[i - 1] != A[i])
                        count = count + 1;
                    else
                        count = count - 1;
                }
                if (i < n - 1)
                {
                    if (A[i + 1] == A[i])
                        count = count + 1;
                    else
                        count = count - 1;
                }
                r = Math.Max(r, count);
            }
            return result + r;
        }






        public int  binaryGap(int n)
        {
            int maior = 0, count = 0, aux = 0;
            string bin = "";

            while(n > 0)
            {
                bin = n%2 + bin ;

                if (n % 2 == 0)
                {
                    count++;
                    maior = count;
                }
                else
                {
                    if (maior < aux)
                    {
                        maior = aux;
                    }
                    aux = maior;
                    count = 0;
                }

                n = n / 2;
            }

            return maior;
        }

        public int OddOccurrencesInArray(int[] n)
        {
            int number = 0;


            Array.Sort(n);


            for (int i = 0; i < n.Length; i++)
            {
                number ^= n[i];
            }

            return number;
        }


        public int[] solution(int[] n, int k)
        {
            int aux=0,ant =0;
            //3,9,8,7,6
            while (k!=0)
            {

                for (int i = 1; i < n.Length; i++)
                    if (n[i] == 0 && n[i - 1] != 0)
                    {
                        int tmp = n[i - 1];
                        n[i - 1] = n[i];
                        n[i] = tmp;
                    }

                k--;
            }


            return n;
        }




    }
}
