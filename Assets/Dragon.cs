
public class Dragon : Entity
{
    public override void Critical()
    {
        opponent.health = 0;
        opponent.isAlive = false;
        print("The dragon has Landed a critical hit");
        print("The Knight has been slain");
        wins++;
    }
}
