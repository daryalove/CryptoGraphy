using SecretSharingDotNet.Cryptography;
using SecretSharingDotNet.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace CryptoGraphy.Services
{
    public static class SecretSharingShamir
    {
        public static string Run(string message)
        {
            var gcd = new ExtendedEuclideanAlgorithm<BigInteger>();

            //// Create Shamir's Secret Sharing instance with BigInteger
            var split = new ShamirsSecretSharing<BigInteger>(gcd);

            string password = "J";
            //// Minimum number of shared secrets for reconstruction: 3
            //// Maximum number of shared secrets: 7
            //// Attention: The password length changes the security level set by the ctor
            var x = split.MakeShares(3, 7, password);

            //// Item1 represents the password (original secret)
            var secret = x.Item1;

            //// Item 2 contains the shared secrets
            var combine = new ShamirsSecretSharing<BigInteger>(gcd);
            var subSet1 = x.Item2.Where(p => p.X.IsEven).ToList();
            var recoveredSecret1 = combine.Reconstruction(subSet1.ToArray());
            var subSet2 = x.Item2.Where(p => !p.X.IsEven).ToList();
            var recoveredSecret2 = combine.Reconstruction(subSet2.ToArray());
            return "OK";
        }
    }
}
