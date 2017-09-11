using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCG.Library.Collections;

namespace PCG.Library.Intelligence
{
    public class WallFollower : IPathFinder
    {
        private IWallFollowerStrategy strategy;

        public WallFollower(IWallFollowerStrategy strategy)
        {
            this.strategy = strategy;
        }

        public static readonly IPathFinder LeftHandRule = new WallFollower(new WallFollowerLeftHandStrategy());
        public static readonly IPathFinder RightHandRule = new WallFollower(new WallFollowerRightHandStrategy());

        public List<Direction> FindRoute<T>(CellMap<T> map, Point3 start, Point3 goal)
        {
            var path = new List<Direction>();
            var facing = Direction.North;
            
            Cell<T> cc = map[start];
            Point3 cp = start;
            Point3 np;

            //Find the right-hand wall, keep turning until you find one, or take a step if there are no walls
            var facingCounter = 0;
            while (true)
            {
                if (cc.Edges.Includes(strategy.Turn(facing)))
                {
                    break;
                }
                else
                {
                    facing = strategy.Turn(facing);
                    facingCounter++;
                }
                if (facingCounter >= 4)
                {
                    facingCounter = 0;
                    cp = start.Move(facing);
                    cc = map[cp];
                    path.Add(facing);
                }
            }
            facingCounter = 0;

            while (cp != goal)
            {
                np = cp.Move(facing);

                if (cc.Edges.Includes(strategy.Turn(facing)))
                {
                    facing = strategy.Turn(facing);
                    cp = cp.Move(facing);
                    cc = map[cp];
                    path.Add(facing);
                }
                else
                {
                    if (cc.Edges.Includes(facing) && map.IsInBounds(np))
                    {
                        cp = np;
                        cc = map[np];
                        path.Add(facing);
                    }
                    else
                    {
                        facing = strategy.AntiTurn(facing);
                    }
                }
            }

            return path;
        }

        public interface IWallFollowerStrategy
        {
            Direction Turn(Direction dir);
            Direction AntiTurn(Direction dir);
        }

        private class WallFollowerRightHandStrategy : IWallFollowerStrategy
        {
            public Direction Turn(Direction dir) { return dir.CycleXyCw; }
            public Direction AntiTurn(Direction dir) { return dir.CycleXyCcw; }
        }

        private class WallFollowerLeftHandStrategy : IWallFollowerStrategy
        {
            public Direction Turn(Direction dir) { return dir.CycleXyCcw; }
            public Direction AntiTurn(Direction dir) { return dir.CycleXyCw; }
        }
    }
}
