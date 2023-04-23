using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;

public class UnitSpawner : NetworkBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject unitPrefab = null;
    [SerializeField] private Transform UnitSpawnPoint = null;
    #region Server
    [Command]
    private void CmdSpawnUnit()
    {
        GameObject unitInstance = Instantiate(unitPrefab, UnitSpawnPoint.position, UnitSpawnPoint.rotation);
        NetworkServer.Spawn(unitInstance, connectionToClient);
    }
    #endregion

    #region Client
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) { return; }
        if(!hasAuthority) {return;}
        CmdSpawnUnit();
    }
    #endregion
}
