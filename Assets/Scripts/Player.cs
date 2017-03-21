using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NetworkName))]
public class Player : Entity {
    [Header("Health")]
    [SerializeField] int baseHpMax = 100;
    public override int hpMax {
        get {
            return baseHpMax;
        }
    }

    [SerializeField] int baseMpMax = 100;
    public override int mpMax {
        get {
            return baseHpMax;
        }
    }

    [Header("Damage")]
    [SyncVar, SerializeField] int baseDamage = 1;
    public override int damage {
        get {
            return baseDamage;
        }
    }

    [Header("Defense")]
    [SyncVar, SerializeField] int baseDefense = 1;
    public override int defense {
        get {
            return baseDefense;
        }
    }

    [Header("Toolbar")]
    public int toolbarSize = 8;
    public KeyCode[] toolbarHotkeys = new KeyCode[] {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8};
    [SyncVar] public int toolbarSelection = 0;
        


    HashSet<string> cmdEvents = new HashSet<string>();

    Vector3 navigatePos = Vector3.zero;
    float navigateStop = 0;
    [Command(channel=Channels.DefaultUnreliable)] // unimportant => unreliable
    void CmdNavigateTo(Vector3 pos, float stoppingDistance) {
        navigatePos = pos; 
        navigateStop = stoppingDistance;
        cmdEvents.Add("NavigateTo");
    }
    bool EventNavigateTo() { return cmdEvents.Remove("NavigateTo"); }


    protected override void Awake() {
        base.Awake();
    }

    void Start() {
        
    }

    public override void OnStartLocalPlayer() {
        Camera.main.GetComponent<CameraMMO>().target = transform;
     
    }

    public override void OnStartServer() {
        base.OnStartServer();
    }
        
    [Server]
    protected override string UpdateServer() {
        if (state == "IDLE")    return UpdateServer_IDLE();
        if (state == "MOVING")  return UpdateServer_MOVING();
//        if (state == "CASTING") return UpdateServer_CASTING();
//        if (state == "TRADING") return UpdateServer_TRADING();
//        if (state == "DEAD")    return UpdateServer_DEAD();
        Debug.LogError("invalid state:" + state);
        return "IDLE";
    }

    [Server]
    string UpdateServer_IDLE() {
        
        if (EventNavigateTo()) {
            // cancel casting (if any) and start moving
//            skillCur = skillNext = -1;
            // move
            agent.stoppingDistance = navigateStop;
            agent.destination = navigatePos;
            return "MOVING";
        }

        return "IDLE"; 
    }

    [Server]
    string UpdateServer_MOVING() {

        if (EventNavigateTo()) {
            // cancel casting (if any) and start moving
            //            skillCur = skillNext = -1;
            // move
            agent.stoppingDistance = navigateStop;
            agent.destination = navigatePos;
            return "MOVING";
        }

        return "MOVING"; 
    }
        
    [Client]
    protected override void UpdateClient() {
        if (isLocalPlayer) {
            LeftClickHandling();
            RightClickHandling();
            WSADHandling();
        }
    }


    [Client]
    void LeftClickHandling() {

    }
        
    [Client]
    void RightClickHandling() {
        
    }


    Vector3 lastDest = Vector3.zero;
    float lastTime = 0;
    [Client]
    void WSADHandling() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(h != 0 || v != 0) {

            var input = new Vector3(h, 0, v);
            if (input.magnitude > 1) input = input.normalized;

            var angles = Camera.main.transform.rotation.eulerAngles;
            angles.x = 0;
            var rot = Quaternion.Euler(angles);

            var dir = rot * input;

            var lastDir = lastDest - transform.position;
            bool dirChanged = dir.normalized != lastDir.normalized;
            bool almostThere = Vector3.Distance(transform.position, lastDest) < agent.speed / 5;
            if (Time.time - lastTime > 0.05f && (dirChanged || almostThere)) {
                // set destination a bit further away to not sync that soon
                // again. we multiply it by speed, because that sets it one
                // second further away
                // (because we move 'speed' meters per second)
                var dest = transform.position + dir * agent.speed;

                // navigate to the nearest walkable destination. this
                // prevents twitching when destination is accidentally in a
                // room without a door etc.
                var bestDest = agent.NearestValidDestination(dest);
                CmdNavigateTo(bestDest, 0);
                lastDest = bestDest;
                lastTime = Time.time;
            }
        }
    }

}