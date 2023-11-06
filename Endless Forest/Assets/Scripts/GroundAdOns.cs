using UnityEngine;
public class GroundAdOns : MonoBehaviour
{
    [SerializeField] private GameObject Coin, Obs;

    private void OnTriggerExit(Collider other)
    {
        // When this ground exits the trigger zone, spawn the next ground and destroy this one after 1 second.
        GroundSpawner.groundSpawner.SpawnGround(true);
        Destroy(gameObject, 1);
    }

    public void SpawnObstacle()//Honnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn
    {
        // Choose a random point on the tile to spawn the obstacle
        int obstacleSpawnIndex = Random.Range(2, 5); // Randomly select one of the tile's child positions for obstacle placement
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        // Spawn the obstacle at the chosen position within the tile
        Instantiate(Obs, spawnPoint.position, Obs.transform.rotation, transform);
    }

    public void SpawnCoins()
    {
        // Spawn a fixed number of coins within the tile
        int coinsCountToSpawn = 6;
        for (int i = 0; i < coinsCountToSpawn; i++)
        {
            GameObject temp = Instantiate(Coin, transform);

            // Randomly position the coin within the tile's collider bounds
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider)
    {
        // Generate a random point within the bounds of the collider
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
        );

        // Check if the generated point is inside the collider; if not, recursively find a new point
        if (point != collider.ClosestPoint(point))
            point = GetRandomPointInCollider(collider);


        // Adjust the height of the point to the desired level
        point.y = 1;
        return point;
    }
}