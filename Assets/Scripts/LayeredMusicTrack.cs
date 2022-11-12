using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public class LayeredMusicTrack
{
    public string[] layers;
    public LayeredMusicSet[] layerSets;
}

[System.Serializable]
public class LayeredMusicSet
{
    public string name;
    public string[] activeLayers;
}
