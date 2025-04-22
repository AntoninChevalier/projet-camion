namespace projetcamion
{
    public class Lien
{
    protected int distance;
    protected Noeud villeDep;
    protected Noeud villeArr;

    public Lien(Noeud villeDep, Noeud villeArr,int distance)
    {
        this.distance = distance;
        this.villeDep = villeDep;
        this.villeArr = villeArr;
    }

    public int Distance
    {
        get { return this.distance; }
    }

    public Noeud VilleDep
    {
        get { return this.villeDep; }
    }

    public Noeud VilleArr
    {
        get { return this.villeArr; }
    }

}

}