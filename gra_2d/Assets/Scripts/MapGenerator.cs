using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] topRoom, bottomRoom, leftRoom, rightRoom, bossRooms, fullRoomList, torchList, enemyList;
    public GameObject keyObject, potionObject;
    public Transform mapHandler;
    List<GameObject> currentRoomList = new List<GameObject>();
    List<GameObject> currentEnemyList = new List<GameObject>();
    GameObject startingRoom, newRoom, bossRoom;

    public static int? minRoomsNo;
    Vector3 startPoint = new Vector3(0,0,0);
    int i, random, x;

    void Start(){
        if(minRoomsNo == null) minRoomsNo = 15;
        createMap(minRoomsNo);
        AstarPath.active.Scan();
    }

    private void createMap(int? minRoomsNo){
        i = 0;
        while(i < 1000){
            random = Random.Range(0, fullRoomList.Length);
            currentRoomList.Clear();
            currentRoomList.Add(Instantiate(fullRoomList[random], startPoint, Quaternion.identity, mapHandler) as GameObject);
            for(int j = 0; j < minRoomsNo; j++){
                try{
                    currentRoomList = createChildRooms(currentRoomList[j], currentRoomList);
                } catch{
                    //Debug.Log("cant make child rooms. Loop "+i+" room "+j);
                }
            }

            if(currentRoomList.Count() < minRoomsNo){
                //Debug.Log("current room list count: "+currentRoomList.Count());
                foreach(GameObject room in currentRoomList){
                    Destroy(room);
                }
            } 
            else if(currentRoomList.Count() >= minRoomsNo) {
                break;
            }
            i++;
        }


        currentRoomList = fixRooms(currentRoomList);
        generateTorches(currentRoomList, minRoomsNo);
        currentEnemyList = generateEnemy(currentRoomList, enemyList, minRoomsNo);
        createHealPotions(currentRoomList, minRoomsNo);
        createKey(currentRoomList);
        currentRoomList = createBossRoom(currentRoomList); 
    }

    private List<GameObject> createChildRooms(GameObject room, List<GameObject> currentRoomList){
        string[] roomName = room.name.Split("(");

        bool create = true;
        if(roomName[0].Contains("T")){
            foreach(GameObject r in currentRoomList){
                if(r.transform.position.x == room.transform.position.x && r.transform.position.y == room.transform.position.y + 14){
                    create = false;
                    break;
                } 
            }
            if(create == true){
                x = Random.Range(0, bottomRoom.Length);
                currentRoomList.Add(Instantiate(bottomRoom[x], new Vector3(room.transform.position.x, room.transform.position.y + 14, room.transform.position.z), Quaternion.identity, mapHandler));
            }
        }
        create = true;
        if(roomName[0].Contains("B")){
            foreach(GameObject r in currentRoomList){
                if(r.transform.position.x == room.transform.position.x && r.transform.position.y == room.transform.position.y - 14){
                    create = false;
                    break;
                }
            }
            if(create == true){
                x = Random.Range(0, topRoom.Length);
                currentRoomList.Add(Instantiate(topRoom[x], new Vector3(room.transform.position.x, room.transform.position.y - 14, room.transform.position.z), Quaternion.identity, mapHandler));
            }
        }
        create = true;
        if(roomName[0].Contains("R")){
            foreach(GameObject r in currentRoomList){
                if(r.transform.position.x == room.transform.position.x + 14 && r.transform.position.y == room.transform.position.y){
                    create = false;
                    break;
                } 
            }
            if(create == true){
                x = Random.Range(0, leftRoom.Length);
                currentRoomList.Add(Instantiate(leftRoom[x], new Vector3(room.transform.position.x + 14, room.transform.position.y, room.transform.position.z), Quaternion.identity, mapHandler));
            }
        } 
        create = true;
        if(roomName[0].Contains("L")){
            foreach(GameObject r in currentRoomList){
                if(r.transform.position.x == room.transform.position.x - 14 && r.transform.position.y == room.transform.position.y){
                    create = false;
                    break;
                } 
                else create = true;
            }
            if(create == true){
                x = Random.Range(0, rightRoom.Length);
                currentRoomList.Add(Instantiate(rightRoom[x], new Vector3(room.transform.position.x - 14, room.transform.position.y, room.transform.position.z), Quaternion.identity, mapHandler));
            }
        }
        return currentRoomList;
    }
    
    private List<GameObject> fixRooms(List<GameObject> currentRoomList){
        List<GameObject> newCurrentRoomList = new List<GameObject>();
        string[] roomAroundName = null, roomName = null;
        string roomToMakeName = null, name = null;

        foreach(GameObject room in currentRoomList)
        {
            roomName = null;
            name = "";
            roomName = room.name.Split("(");
            foreach(GameObject roomAround in currentRoomList)
            {
                roomAroundName = null;
                roomAroundName = roomAround.name.Split("(");

                if (roomName[0].Contains("T"))
                {
                    if(room.transform.position.x == roomAround.transform.position.x && room.transform.position.y + 14 == roomAround.transform.position.y)
                    {
                        if (roomAroundName[0].Contains("B"))
                        {
                            name += "T";
                        }
                    }
                }

                if (roomName[0].Contains("R"))
                {
                    if (room.transform.position.x + 14 == roomAround.transform.position.x && room.transform.position.y == roomAround.transform.position.y)
                    {
                        if (roomAroundName[0].Contains("L"))
                        {
                            name += "R";
                        }
                    }
                }

                if (roomName[0].Contains("B"))
                {
                    if (room.transform.position.x == roomAround.transform.position.x && room.transform.position.y - 14 == roomAround.transform.position.y)
                    {
                        if (roomAroundName[0].Contains("T"))
                        {
                            name += "B";
                        }
                    }
                }

                if (roomName[0].Contains("L"))
                {
                    if (room.transform.position.x - 14 == roomAround.transform.position.x && room.transform.position.y == roomAround.transform.position.y)
                    {
                        if (roomAroundName[0].Contains("R"))
                        {
                            name += "L";
                        }
                    }
                }
            }

            roomToMakeName = "";
            if (name.Contains("T")) roomToMakeName += "T";
            if (name.Contains("R")) roomToMakeName += "R";
            if (name.Contains("B")) roomToMakeName += "B";
            if (name.Contains("L")) roomToMakeName += "L";

            Vector3 roomPosition = room.transform.position;
            Destroy(room);
            foreach(GameObject roomToMake in fullRoomList)
            {
                if(roomToMake.name == roomToMakeName)
                {
                    newCurrentRoomList.Add(Instantiate(roomToMake, roomPosition, Quaternion.identity, mapHandler));
                }
            }
        }

        return newCurrentRoomList;
    }
    private List<GameObject> createBossRoom(List<GameObject> currentRoomList){
        int i = 0, x;
        bool breaks = false, above_1 = false, above_2 = false;
        string[] roomName = null;
        GameObject roomToMake = null;

        while(!breaks){
            above_1 = false;
            above_2 = false;
            x = Random.Range(0, currentRoomList.Count());
            roomName = currentRoomList[x].name.Split("(");
            if(!roomName[0].Contains("T"))
            {
                foreach(GameObject room1 in currentRoomList){
                    if(room1.transform.position.x == currentRoomList[x].transform.position.x && room1.transform.position.y == currentRoomList[x].transform.position.y+14){
                        above_1 = true;
                        break;
                    }
                }
                foreach(GameObject room1 in currentRoomList){
                    if(room1.transform.position.x == currentRoomList[x].transform.position.x && room1.transform.position.y == currentRoomList[x].transform.position.y+28){
                        above_2 = true; 
                        break;
                    }
                }
                if(above_1 == false && above_2 == false){
                    GameObject room = currentRoomList[x];
                    Vector3 pos = room.transform.position;
                    string name = "T"+roomName[0];
                    Destroy(currentRoomList[x]);
                    foreach(GameObject room1 in fullRoomList){
                        if(name == room1.name.ToString()){
                            roomToMake = room1;
                            break;
                        }
                    }
                    currentRoomList.Add(Instantiate(roomToMake, pos, Quaternion.identity, mapHandler));
                    currentRoomList.Add(Instantiate(bossRooms[0], new Vector3(pos.x, pos.y+14, pos.z), Quaternion.identity, mapHandler));
                    breaks = true;
                }
            }
            i++;
        }

        return currentRoomList;
    }

    void generateTorches(List<GameObject> RoomList, int? minRoomsNo){
        float numberOfTorchesOnMapf =  (float) minRoomsNo/0.4f;
        int numberOfTorchesOnMap = Mathf.RoundToInt(numberOfTorchesOnMapf);

        for(i = 0; i < numberOfTorchesOnMap; i++){
            random = Random.Range(0,RoomList.Count());
            if(!(RoomList[random].transform.position == startPoint)){
                bool isTorchThere = false;
                foreach(GameObject room in RoomList){
                    float posX = room.transform.position.x;
                    float posY = room.transform.position.y;
                    float ranPosX = RoomList[random].transform.position.x;
                    float ranPosY = RoomList[random].transform.position.y;
                    if(room.transform.position != RoomList[random].transform.position){
                        if(posX+14 == ranPosX && posY == ranPosY){
                            isTorchThere = checkRoomTorch(room);
                        } else if(posX-14 == ranPosX && posY == ranPosY){
                            isTorchThere = checkRoomTorch(room);
                        } else if(posX == ranPosX && posY == ranPosY+14){
                            isTorchThere = checkRoomTorch(room);
                        } else if(posX == ranPosX && posY == ranPosY-14){
                            isTorchThere = checkRoomTorch(room);
                        }
                    }
                }
                if(!isTorchThere) Instantiate(torchList[0], RoomList[random].transform.position, Quaternion.identity, RoomList[random].transform);
            }
            else i--;
        }
    }
    bool checkRoomTorch(GameObject room){
        foreach(Transform child in room.transform){
            Debug.Log("checking "+room.name+". Child: "+child.name);
            if(child.gameObject.name.Contains("room_torch(Clone)")){
                Debug.Log("there is a torch! "+room.transform.position);
                return true;
            }
        }
        return false;
    }

    List<GameObject> generateEnemy(List<GameObject> RoomList, GameObject[] enemyList, int? minRoomsNo){
        float roomsWithEnemyCount = (float) minRoomsNo/0.7f;

        for(i = 0; i < roomsWithEnemyCount; i++){
            random = Random.Range(0,RoomList.Count());
            if(!(RoomList[random].transform.position == startPoint)){
                int randEnemy = Random.Range(1,2);
                for(int j=0; j < randEnemy; j++){
                    float offX = Random.Range(-3f, 3f);
                    float offY = Random.Range(-3f, 3f);
                    Vector3 roomPos = RoomList[random].transform.position;
                    Instantiate(enemyList[0], new Vector3(roomPos.x + offX, roomPos.y+offY, roomPos.z), Quaternion.identity, RoomList[random].transform);
                }   
            }
            else i--;
        }

        return currentEnemyList;
    }

    void createKey(List<GameObject> RoomList){
        random = Random.Range(0,RoomList.Count());
        if(!(RoomList[random].transform.position == startPoint)){
            float offX = Random.Range(-3f, 3f);
            float offY = Random.Range(-3f, 3f);
            Vector3 roomPos = RoomList[random].transform.position;
            GameObject key = Instantiate(keyObject, new Vector3(roomPos.x + offX, roomPos.y+offY, roomPos.z), Quaternion.identity, RoomList[random].transform);
            Debug.Log("Key position: "+key.transform.position);
        } 
        else i--;
    }
    void createHealPotions(List<GameObject> RoomList, int? minRoomsNo){
        float roomsWithEnemyCount = (float) minRoomsNo/2f;
        for(i = 0; i < roomsWithEnemyCount; i++){
            random = Random.Range(0,RoomList.Count());
            if(!(RoomList[random].transform.position == startPoint)){
                float offX = Random.Range(-3f, 3f);
                float offY = Random.Range(-3f, 3f);
                Vector3 roomPos = RoomList[random].transform.position;
                Instantiate(potionObject, new Vector3(roomPos.x + offX, roomPos.y+offY, roomPos.z), Quaternion.identity, RoomList[random].transform);
            }
            else i--;
        }
    }
}

