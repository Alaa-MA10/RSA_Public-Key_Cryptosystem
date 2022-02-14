using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA_V3
{
     class RSA
     {
          public static BigInteger Encrypt(string M, string e, string n)
          {
               BigInteger Message = new BigInteger(M);
               BigInteger power = new BigInteger(e);
               BigInteger mod = new BigInteger(n);

               Message = BigInteger.ModOfPower(Message, power, mod);

               return Message;
          }

          public static BigInteger Decrypt(string M, string d, string n)
          {
               BigInteger Encrypt_message = new BigInteger(M);
               BigInteger power = new BigInteger(d);
               BigInteger mod = new BigInteger(n);

               Encrypt_message = BigInteger.ModOfPower(Encrypt_message, power, mod);

               return Encrypt_message;

          }
     }
}
