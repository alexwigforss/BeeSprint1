public interface ISelectiveMemory
{
    void AddSpecie(int value);
    void RemoveSpecie(int value);
    bool ContainsSpecie(int value);
    int[] GetSpecies();
    void PrintSpecies();
}
