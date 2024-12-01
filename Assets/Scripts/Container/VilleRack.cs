using System;

public class VilleRack : Rack
{
    private const EScrollDestination Destination = EScrollDestination.Ville;

    protected override void Start()
    {
        GameState().villeRack = this;
    }
}
