#ifndef FLIP_A_IF_B_INCLUDED
#define FLIP_A_IF_B_INCLUDED

// A and B should be either 0.0 or 1.0
void FlipAIfB_float(
    float A,
    float B,
    out float Out
)
{
    Out = abs(A - B);
}

#endif