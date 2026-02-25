#ifndef FLIP_A_IF_B_INCLUDED
#define FLIP_A_IF_B_INCLUDED

void Checkers_float(
    float3 position,
    float3 colorA,
    float3 colorB,
    float squareSize,
    out float3 colorOut
)
{
    float result = 0.0; // 0.0 is color A, 1.0 is color B
    float x = position.x;
    float z = position.z;

    if (x < 0)  {
        x = abs(x - 1.0);
    }
    if (z < 0)  {
        z = abs(z - 1.0);
    }
    if (z % (squareSize * 2.0) > squareSize)    {
        result = abs(result - 1); // this logic just flips the result
    }

    if (x % (squareSize * 2.0) > squareSize)    {
        result = abs(result - 1); // this logic just flips the result
    }
    if (result > 0.5)   {
        colorOut = colorA;
    } else {
        colorOut = colorB;
    }
}

#endif