﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Enigma
{
    static class RotorSettings
    {
        // Rotor 0
        // S, L, C, Y, G, W, J, N, H, I, O, U, Q, R, M, E, Z, X, D, A, F, K, T, P, B, V
        // 18, 10, 0, 21, 2, 17, 3, 6, -1, -1, 4, 9, 4, 4, -2, -11, 9, 6, -15, -19, -15, -11, -3, -8, -23, -4
        // 19, 23, 0, 15, 11, 15, -2, 1, 1, -3, 11, -10, 2, -6, -4, 8, -4, -4, -18, 3, -9, 4, -17, -6, -21, -9

        // Rotor 1
        // R, U, B, M, D, X, A, K, Q, S, J, T, Y, W, C, P, F, V, N, O, L, I, E, G, H, Z
        // 17, 19, -1, 9, -1, 18, -6, 3, 8, 9, -1, 8, 12, 9, -12, 0, -11, 4, -5, -5, -9, -13, -18, -17, -17, 0
        // 6, 1, 12, 1, 18, 11, 17, 17, 13, 1, -3, 9, -9, 5, 5, 0, -8, -17, -9, -8, -19, -4, -9, -18, -12, 0

        // Rotor 2
        // K, T, N, C, D, W, L, B, J, V, A, P, H, Z, E, Q, U, G, X, Y, I, M, S, O, R, F
        // 10, 18, 11, -1, -1, 17, 5, -6, 1, 12, -10, 4, -5, 12, -10, 1, 4, -11, 5, 5, -12, -9, -4, -9, -7, -20
        // 10, 6, 1, 1, 10, 20, 11, 5, 12, -1, -10, -5, 9, -11, 9, -4, -1, 7, 4, -18, -4, -12, -17, -5, -5, -12

        // Rotor 3
        // E, N, S, I, X, M, Y, J, L, K, T, Z, Q, U, B, P, O, A, D, C, G, F, V, W, H, R
        // 4, 12, 16, 5, 19, 7, 18, 2, 3, 1, 9, 14, 4, 7, -13, 0, -2, -17, -15, -17, -14, -16, -1, -1, -17, -8
        // 17, 13, 17, 15, -4, 16, 14, 17, -5, -2, -1, -3, -7, -12, 2, 0, -4, 8, -16, -9, -7, 1, 1, -19, -18, -14

        // Rotor 4
        // L, U, D, R, P, J, X, O, I, M, H, Y, T, B, N, V, C, W, S, G, Q, Z, A, E, K, F
        // 11, 19, 1, 14, 11, 4, 17, 7, 0, 3, -3, 13, 7, -12, -1, 6, -14, 5, 0, -13, -4, 4, -22, -19, -14, -20
        // 22, 12, 14, -1, 19, 20, 13, 3, 0, -4, 14, -11, -3, 1, -7, -11, 4, -14, 0, -7, -19, -6, -5, -17, -13, -4

        static int[][] rotorConnections = new int[][]
        {
            /* 0 */ new int [] { 18, 10, 0, 21, 2, 17, 3, 6, -1, -1, 4, 9, 4, 4, -2, -11, 9, 6, -15, -19, -15, -11, -3, -8, -23, -4 },
            /* 1 */ new int [] { 17, 19, -1, 9, -1, 18, -6, 3, 8, 9, -1, 8, 12, 9, -12, 0, -11, 4, -5, -5, -9, -13, -18, -17, -17, 0 },
            /* 2 */ new int [] { 10, 18, 11, -1, -1, 17, 5, -6, 1, 12, -10, 4, -5, 12, -10, 1, 4, -11, 5, 5, -12, -9, -4, -9, -7, -20 },
            /* 3 */ new int [] { 4, 12, 16, 5, 19, 7, 18, 2, 3, 1, 9, 14, 4, 7, -13, 0, -2, -17, -15, -17, -14, -16, -1, -1, -17, -8 },
            /* 4 */ new int [] { 11, 19, 1, 14, 11, 4, 17, 7, 0, 3, -3, 13, 7, -12, -1, 6, -14, 5, 0, -13, -4, 4, -22, -19, -14, -20 },
        };

        static int[][] rotorConnectionsReversed = new int[][]
        {
            /* 0 */ new int [] { 19, 23, 0, 15, 11, 15, -2, 1, 1, -3, 11, -10, 2, -6, -4, 8, -4, -4, -18, 3, -9, 4, -17, -6, -21, -9 },
            /* 1 */ new int [] { 6, 1, 12, 1, 18, 11, 17, 17, 13, 1, -3, 9, -9, 5, 5, 0, -8, -17, -9, -8, -19, -4, -9, -18, -12, 0 },
            /* 2 */ new int [] { 10, 6, 1, 1, 10, 20, 11, 5, 12, -1, -10, -5, 9, -11, 9, -4, -1, 7, 4, -18, -4, -12, -17, -5, -5, -12 },
            /* 3 */ new int [] { 17, 13, 17, 15, -4, 16, 14, 17, -5, -2, -1, -3, -7, -12, 2, 0, -4, 8, -16, -9, -7, 1, 1, -19, -18, -14 },
            /* 4 */ new int [] { 22, 12, 14, -1, 19, 20, 13, 3, 0, -4, 14, -11, -3, 1, -7, -11, 4, -14, 0, -7, -19, -6, -5, -17, -13, -4 },
        };

        static char[,] rotorChars =
        {
          /* 0 */  { 'S', 'L', 'C', 'Y', 'G', 'W', 'J', 'N', 'H', 'I', 'O', 'U', 'Q', 'R', 'M', 'E', 'Z', 'X', 'D', 'A', 'F', 'K', 'T', 'P', 'B', 'V' },
          /* 1 */  { 'R', 'U', 'B', 'M', 'D', 'X', 'A', 'K', 'Q', 'S', 'J', 'T', 'Y', 'W', 'C', 'P', 'F', 'V', 'N', 'O', 'L', 'I', 'E', 'G', 'H', 'Z' },
          /* 2 */  { 'K', 'T', 'N', 'C', 'D', 'W', 'L', 'B', 'J', 'V', 'A', 'P', 'H', 'Z', 'E', 'Q', 'U', 'G', 'X', 'Y', 'I', 'M', 'S', 'O', 'R', 'F' },
          /* 3 */  { 'E', 'N', 'S', 'I', 'X', 'M', 'Y', 'J', 'L', 'K', 'T', 'Z', 'Q', 'U', 'B', 'P', 'O', 'A', 'D', 'C', 'G', 'F', 'V', 'W', 'H', 'R' },
          /* 4 */  { 'L', 'U', 'D', 'R', 'P', 'J', 'X', 'O', 'I', 'M', 'H', 'Y', 'T', 'B', 'N', 'V', 'C', 'W', 'S', 'G', 'Q', 'Z', 'A', 'E', 'K', 'F' }
        };

        public static int[] GetRotorConnections(int rotor)
        {
            return rotorConnections[rotor];
        }

        public static int[] GetReverseConnections(int rotor)
        {
            return rotorConnectionsReversed[rotor];
        }

        public static int[] GetRotorChars(int rotor)
        {
            int[] retVal = new int[26];
            for(int i =0; i < 25; i++ )
            {
                retVal[i] = Helper.letterToNumber(rotorChars[rotor, i]);
            }

            // Debug.WriteLine($"Aantal elmnt in array: {retVal.Length}");
            return retVal;           
        }
    }
}