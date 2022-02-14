using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA_V3
{
     class BigInteger
     {
          private List<int> digits;

          public BigInteger()
          {
               digits = new List<int>();
          }

          public BigInteger(int cap)
          {
               digits = new List<int>(cap);
          }

          public BigInteger(char char1)
          {
               digits = new List<int>(1)
               {
                    char1 - '0'
               };
          }

          public BigInteger(string str1)
          {
               digits = new List<int>(str1.Length);

               for (int i = 0; i < str1.Length; i++)
                    digits.Add(str1[i] - '0');
          }

          /// <summary>
          /// find the Addition of Two numbers, from [ BigInteger(custome_Class)] datatype
          /// </summary>
          /// <param name="num1">The First number</param>
          /// <param name="num2">The Second number</param>
          /// <returns>Addition Result</returns>
          public static BigInteger Addition(BigInteger num1, BigInteger num2)
          {
               /* Make the two numbers EQUAL */
               if (num1.digits.Count > num2.digits.Count)
                    num2 = MakeLengthEquals(num1, num2);
               else if (num1.digits.Count < num2.digits.Count)
                    num1 = MakeLengthEquals(num2, num1);

               int Length = num1.digits.Count;   // num1 & num2 have the same length

              
               BigInteger ResultList = new BigInteger(Length + 1);

               for (int i = 0; i < Length + 1; i++)  // intially fill resultList with zeros, (n+1) 1 for carry
                    ResultList.digits.Add(0);

               int First_num;
               int Second_num;
               bool Carry = false;
               int Result;
               for (int i = Length - 1; i >= 0; i--)   // start calculate from END of number
               {
                    First_num = num1.digits[i];
                    Second_num = num2.digits[i];

                    if (Carry)
                         First_num++;

                    Result = First_num + Second_num;
                    ResultList.digits[i + 1] = Result % 10;

                    if (Result > 9)
                         Carry = true;

                    else
                         Carry = false;
               }

               if (Carry)   // if have carry in the last
                    ResultList.digits[0] = 1;

               string Result_str = RemovePadding(ResultList);   // To remove extra 0's from padding
               return new BigInteger(Result_str);
          }

          /// <summary>
          /// find the Subtraction of Two numbers, from [ BigInteger(custome_Class)] datatype
          /// </summary>
          /// <param name="num1">The First number</param>
          /// <param name="num2">The Second number</param>
          /// <returns>Subtraction Result</returns>
          public static BigInteger Subtraction(BigInteger num1, BigInteger num2)
          {
               /* Make the two numbers EQUAL */
               if (num1.digits.Count > num2.digits.Count)
                    num2 = MakeLengthEquals(num1, num2);

               int Length = num1.digits.Count; // num1 & num2 have the same length

               BigInteger ResultList = new BigInteger(Length);

               for (int i = 0; i < Length; i++) //intially fill resultList with zeros
                    ResultList.digits.Add(0);

               int First_num;
               int Second_num;
               bool Borrow = false;
               int Result;
               for (int i = Length - 1; i >= 0; i--)
               {
                    First_num = num1.digits[i];
                    Second_num = num2.digits[i];

                    if (Borrow)
                         First_num--;

                    if (First_num < Second_num)
                    {
                         Result = (First_num + 10) - Second_num;
                         Borrow = true;
                    }
                    else
                    {
                         Result = First_num - Second_num;
                         Borrow = false;
                    }
                    ResultList.digits[i] = Result % 10;
               }


               string Result_str = RemovePadding(ResultList);   //To remove extra 0's from padding
               return new BigInteger(Result_str);
          }

          /// <summary>
          /// find the Multiplication of Two numbers, from [ BigInteger(custome_Class)] datatype, 
          /// Using Karatsuba algorithm
          /// </summary>
          /// <param name="num1">The First number</param>
          /// <param name="num2">The Second number</param>
          /// <returns>Multiplication Result</returns>
          public static BigInteger Multiplication(BigInteger num1, BigInteger num2)
          {
               /* Make the two numbers EQUAL */
               if (num1.digits.Count > num2.digits.Count)
                    num2 = MakeLengthEquals(num1, num2);
               else if (num1.digits.Count < num2.digits.Count)
                    num1 = MakeLengthEquals(num2, num1);


               int Length = num1.digits.Count; //Both same size

               if (Length == 1)
               {
                    BigInteger Temp = new BigInteger();
                    if ((num1.digits[0] * num2.digits[0]) >= 10)
                    {
                         Temp.digits.Add((num1.digits[0] * num2.digits[0]) / 10);
                         Temp.digits.Add((num1.digits[0] * num2.digits[0]) % 10);

                    }
                    else
                         Temp.digits.Add(num1.digits[0] * num2.digits[0]);

                    return Temp;
               }

               int mid_ceil = (Length / 2) + (Length % 2);  //Math.Ceiling( Length / 2 )
               int mid_floor = Length / 2;  // Length - mid_ceil

               /* Split First number to [ a (Fisrt Part) & b (Second Part)],
                *      Second number to [ c (Fisrt Part) & d (Second Part)]*/
               BigInteger a = new BigInteger
               { digits = num1.digits.GetRange(0, mid_ceil) };

               BigInteger b = new BigInteger
               { digits = num1.digits.GetRange(mid_ceil, mid_floor) }; // mid_floor = Length - mid_ceil

               BigInteger c = new BigInteger
               { digits = num2.digits.GetRange(0, mid_ceil) };

               BigInteger d = new BigInteger
               { digits = num2.digits.GetRange(mid_ceil, mid_floor) };


               BigInteger m1 = Multiplication(b, d);   // m1 = b*d
               BigInteger m2 = Multiplication(a, c);   // m2 = a*c 
               BigInteger z = Multiplication(Addition(a, b), Addition(c, d));   // z = (a+b)(c+d)

               BigInteger Z3 = Subtraction(z, m1);     // Z3 = ( z - m1 )
               BigInteger Z1 = Subtraction(Z3, m2);    // Z1 = ( Z3 - m2 ) = ( z - m1 - m2 )

               // ( 10^(2*mid_floor) ) * m2 
               for (int i = 0; i < 2 * mid_floor; i++)
                    m2.digits.Add(0);

               // (10^mid_floor) * Z1
               for (int i = 0; i < mid_floor; i++)
                    Z1.digits.Add(0);

               BigInteger Z2 = Addition(Addition(m2, Z1), m1);  // Z2 = ( m2 + Z1 + m1 )

               return Z2;

          }


          /// <summary>
          /// find the Division of Two numbers, from [ BigInteger(custome_Class)] datatype
          /// </summary>
          /// <param name="num1">The First number</param>
          /// <param name="num2">The Second number</param>
          /// <returns>Divion Result As Pair (Quotient, Reminder)</returns>
          public static KeyValuePair<BigInteger, BigInteger> Division(BigInteger num1, BigInteger num2)  // O(N)
          {
               if (IsFirstLarger(num2, num1))   //O(N)
                    return (new KeyValuePair<BigInteger, BigInteger>(new BigInteger('0'), num1));   // O(1)

               KeyValuePair<BigInteger, BigInteger> Result = Division(num1, Addition(num2, num2));  // T(N/2) // Addition O(N)

               BigInteger q = Result.Key;     //O(1)
               BigInteger r = Result.Value;   //O(1)

               q = Addition(q, q);       //O(N)     

               if (IsFirstLarger(num2, r))    //O(N)
                    return (new KeyValuePair<BigInteger, BigInteger>(q, r));   // O(1)

               else
                    return (new KeyValuePair<BigInteger, BigInteger>(Addition(q, new BigInteger('1')), Subtraction(r, num2)));  // Addition O(N) // Subtraction O(N)  -> //O(N)


               // Recursion Order: T(N).
                   // T(N) = T(N/2) + θ(N)

               //Total Divison Function order 
                      // T(N)= O(N)


          }

          // <summary>
          /// find the Mod Of Power of Two numbers, from [ BigInteger(custome_Class)] datatype
          /// </summary>
          /// <param name="B">The Base</param>
          /// <param name="P">The Power</param>
          /// <param name="M">The Modulus</param>
          /// <returns> B^P mod M </returns>
          public static BigInteger ModOfPower(BigInteger B, BigInteger P, BigInteger M)  // θ(N^1.58)
          {
               if (P.digits[0] == 0 && P.digits.Count == 1)    //Θ(1)
                    return new BigInteger('1');  //Θ(1)
               else if (P.digits[0] == 1 && P.digits.Count == 1)    //Θ(1)
                    return B;  //Θ(1)
               if (B.digits[0] == 0 && B.digits.Count == 1)    //Θ(1)
                    return new BigInteger('0');  //Θ(1)

               BigInteger Res = ModOfPower(B, Division(P, new BigInteger('2')).Key, M); // [ B^(P/2) mod M]
               Res = Division(Multiplication(Res, Res), M).Value;   // // [ (B^(P/2) mod M) * (B^(P/2) mod M)] mod M

               if (!IsEven(P))
               {
                    BigInteger a = Division(B, M).Value;  // [B mod M]
                    Res = Division(Multiplication(a, Res), M).Value; // [(B mod M)*(B ^ (P / 2) mod M) * (B^(P/2) mod M)] mod M

               }


               return Res;
          }

          

          /// <summary>
          /// Set Shorter number Equals to Longer one by 0's padding
          /// </summary>
          /// <param name="longer">The Long number</param>
          /// <param name="shorter">The Short number</param>
          /// <returns>Short number Longer</returns>
          private static BigInteger MakeLengthEquals(BigInteger longer, BigInteger shorter)  //O(N)
          {
               int Length_longer = longer.digits.Count;    // Θ(1)
               int Length_shorter = shorter.digits.Count;  // Θ(1)

               BigInteger temp = new BigInteger(Length_longer);  // Θ(1)
               for (int i = 0; i < Length_longer; i++)  //intially fill temp with zeros // O(N)
                    temp.digits.Add(0);       //Θ(1)
               //Total loop order //O(N)

               int j = Length_shorter - 1;  // Θ(1)
               for (int i = Length_longer - 1; i >= Length_longer - Length_shorter; i--)  // O(N)
               {
                    temp.digits[i] = shorter.digits[j];  // Θ(1)
                    j--;  //Θ(1)
               }
               //Total loop order //O(N)
               return temp; // Θ(1)
          }

          /// <summary>
          /// Remove extra 0's from padding, USE [TrimStart()], and Return the string
          /// </summary>
          /// <param name="Result">The number to remove 0's from</param>
          /// <returns>The number As String</returns>
          private static String RemovePadding(BigInteger Result)  // O(N)
          {
               string Result_str = Result.ToString();   // O(N)
               Result_str = Result_str.TrimStart('0');   // O(N)
               if (Result_str.Length == 0)   // Θ(1)
                    Result_str = "0";   // Θ(1)
               return Result_str;  // Θ(1)
          }

          /// <summary>
          /// Check if the First number is Larger
          /// </summary>
          /// <param name="num1">The first number</param>
          /// <param name="num2">The second number</param>
          /// <returns>True if the First number Larger</returns>
          private static bool IsFirstLarger(BigInteger num1, BigInteger num2)   // O(N)
          {
               /* check if number1's length larger or not*/
               if (num1.digits.Count > num2.digits.Count)      // Θ(1)
                   return true;
               else if (num1.digits.Count < num2.digits.Count)   //Θ(1)
                    return false;
               /* if the two numbers are equal in length, then compare digits */
               int Length = num1.digits.Count;         // Θ(1)
               for (int i = 0; i < Length; i++)       // O(N * Θ(1)) = O(N)
               {
                    if (num1.digits[i] > num2.digits[i])    // Θ(1)
                         return true;

                    else if (num1.digits[i] < num2.digits[i])    // Θ(1)
                         return false;
               }
               // Total Loop order // O(N)
               return false;
          }

          /// <summary>
          /// Check if the number is Even or not
          /// </summary>
          /// <param name="num">The number to check</param>
          /// <returns>True if the number Even</returns>
          public static bool IsEven(BigInteger num)    // Θ(1)
          {
               int Length = num.digits.Count;           // Θ(1)
               if (num.digits[Length - 1] % 2 == 0)     // Θ(1)
                    return true;
               //if (num.digits.Last() % 2 == 0)
               //     return true;
               else                                   // Θ(1)
                    return false;
               //Total if order Θ(1)
          }

          public override string ToString()   // O(N)
          {
               return string.Join("", ((IEnumerable<int>)digits));
          }

     }
}
