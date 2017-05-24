﻿namespace CreateThis.Math {
    public static class Ratio {
        public static float SolveForD(float a, float b, float c) {
            // a   c
            // - = -
            // b   d
            return c * b / a;
        }
    }
}