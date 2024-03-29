# RSA_Public-Key_Cryptosystem

## Problem Definition

The goal is to implement the RSA public-key cryptosystem. The RSA (Rivest-Shamir-Adleman) cryptosystem is widely used
for secure communication in browsers, bank ATM machines, credit card machines, mobile phones, smart cards, and the
Windows operating system. It works by manipulating integers. To prevent listeners, the RSA cryptosystem must manipulate
huge integers (hundreds of digits). The built-in C type int is only capable of dealing with 16 or 32 bit integers,
providing little or no security. You will:

1. Design, implement, and analyze an extended precision arithmetic data type (big integer) that is capable of
   manipulating much larger integers.
2. Use this data type to write a client program that encrypts and decrypts messages using RSA.

**Cryptosystem** mean encoding and decoding confidential messages.

**RSA** is a public key cryptosystem algorithm. Public key cryptosystem requires two separate keys, one of which is
secret (or private) and one of which is public. Although different, the two parts of this key pair are mathematically
linked. The public key is used to encrypt plaintext; whereas the private key is used to decrypt cipher text. Only the
one having the private key can decrypt the cipher text and get the original message.

![RSA cycle](./RSA-pic.png)

## How RSA Works?

Alice wants to send message M to Bob using RSA. How will this happen? RSA involves three integer parameters d, e, and n
that satisfy certain mathematical properties. The private key (d, n) is known only by Bob, while the public key (e, n)
is published on the Internet. If Alice wants to send Bob a message (e.g., her credit card number) she encodes her
integer M that is between 0 and n-1. Then she computes:

                        E(M) = Me mod n

and sends the integer E(M) to Bob. When Bob receives the encrypted communication E(M), he decrypts it by computing:

                        M = E(M)d mod n.

## The goal

Being able to design and implement an efficient algorithm for the RSA encryption and decryption functions dealing with
large integer.
