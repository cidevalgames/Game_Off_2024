using System;

public class VillageRack : Rack
{
    private const EScrollDestination Destination = EScrollDestination.Village;

    private void Start()
    {
        GameState().villageRack = this;
    }
}
