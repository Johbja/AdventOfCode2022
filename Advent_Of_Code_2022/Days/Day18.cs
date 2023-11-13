using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days;
public class Day18 : Solution
{
    public Day18(string path, Type instanceType, bool render) 
        : base(path, instanceType, render)
    {

        var cubes = input.Select(x => new Cube(x.Split(",").Select(n => int.Parse(n)).ToArray()));


        int totalExposedSurfaces = CountExposedSurfaces(cubes);
        var t = 0;
    }

    private int CountExposedSurfaces(IEnumerable<Cube> cubes)
    {
        int exposedSurfaces = 0;

        foreach (Cube cube in cubes)
        {
            int x = cube.X;
            int y = cube.Y;
            int z = cube.Z;

            // Check each face of the cube
            foreach ((int dx, int dy, int dz) in new (int, int, int)[] { (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1) })
            {
                if (!cubes.Contains(new Cube(x + dx, y + dy, z + dz)))
                {
                    exposedSurfaces++;
                }
            }
        }

        return exposedSurfaces;
    }

    class Cube
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Cube(int[] cords)
        {
            X = cords[0];
            Y = cords[1];
            Z = cords[2];
        }

        public Cube(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object obj)
        {
            if (obj is Cube otherCube)
            {
                return X == otherCube.X && Y == otherCube.Y && Z == otherCube.Z;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
    }
}
