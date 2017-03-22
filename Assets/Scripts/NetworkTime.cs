using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(sendInterval=60)]
public class NetworkTime : NetworkBehaviour { 

    public static float offset;

    public static float time {
        get { return Time.time + offset; }
    }

    public override bool OnSerialize(NetworkWriter writer, bool initialState) {
        writer.Write(Time.time);
        return true;
    }

    public override void OnDeserialize(NetworkReader reader, bool initialState) {
        offset = reader.ReadSingle() - Time.time;
    }
}

