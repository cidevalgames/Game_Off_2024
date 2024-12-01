using System;

public class VillageRack : Rack
{
    private const EScrollDestination Destination = EScrollDestination.Village;

    protected override void Start()
    {
        GameState().villageRack = this;
    }
}
