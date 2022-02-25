public class Cell
{
    public Cell(int position, int[] walls)
    {
        Walls = walls;
        Pos = position;
        isVisited = false;
    }
    public int[] Walls;
    public int Pos;
    public int DistanceFromOrigin = -1;
    public int SolutionDirection;
    public bool isVisited;
}
