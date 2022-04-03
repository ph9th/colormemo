public class PlayerObject
{
    public string name;
    public int MaxLevel;

    public float[] green = new float[3]; //index 0: how many times task color was green, index 1: how many times wrong color input, 2: Error rate
    public float[] orange = new float[3];
    public float[] purple = new float[3];

    public int StolenObjId;


    public PlayerObject(string name)
    {
        this.name = name;
        this.MaxLevel = 0;
        this.StolenObjId = 0;

        for (int i = 0; i < 3; i++)
        {
            this.green[i] = 0;
            this.orange[i] = 0;
            this.purple[i] = 0;
        }
    }


}
