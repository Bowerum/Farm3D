using System.Collections.Generic;
class InteractingType
{
    public static Dictionary<string, List<int>> InteractingValues = new Dictionary<string, List<int>>
    {
        ["dig"] = new List<int> { 1, 3 },
        ["plant"] = new List<int> { 2, 3 },
        ["pull"] = new List<int> { 3, 6 },
        ["watering"] = new List<int> { 4, 2 }
    };
}
