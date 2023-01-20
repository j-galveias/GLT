using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeController : MonoBehaviour
{
    public List<Transform> cubesPrefabs;
    public int numCubesToSpawn;
    public List<(Transform cube, int color)> unsortedCubes;
    public List<(Transform cube, int color)> sortedCubes;
    public int nextPosition = 0;
    public Vector3 redPos;
    public Vector3 greenPos;
    public Vector3 bluePos;
    public float moveDuration;

    // Start is called before the first frame update
    void Start()
    {
        unsortedCubes = new List<(Transform cube, int color)>();
        sortedCubes = new List<(Transform cube, int color)>();

        for (int i = 0; i < numCubesToSpawn; i++)
        {
            int randomColor = Random.Range(0, cubesPrefabs.Count); // Red = 0; Green = 1; Blue = 2;
            var newUnsortedCube = Instantiate(cubesPrefabs[randomColor], new Vector3(0, 0.5f, nextPosition), Quaternion.identity);
            unsortedCubes.Add((newUnsortedCube, randomColor));
            var newSortedCube = Instantiate(cubesPrefabs[randomColor], new Vector3(4, 0.5f, nextPosition), Quaternion.identity);
            sortedCubes.Add((newSortedCube, randomColor));
            nextPosition += 2;
        }
    }

    public void SortButtonClick()
    {
        if (unsortedCubes.Count == 0 || sortedCubes.Count == 0)
        {
            Transform error = GameObject.Find("ErrorMessage").transform.GetChild(0);
            Text errorText = error.GetComponentInChildren<Text>();
            errorText.text = "No unsorted cubes to sort";
            error.gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(SortingCoroutines());
        }
    }

    IEnumerator SortingCoroutines()
    {
        yield return StartCoroutine(OrderUnsorted());
        StartCoroutine(OrderSorted());
    }

    IEnumerator OrderUnsorted()
    {
        foreach(var cube in unsortedCubes)
        {
            if(cube.color == 0)
            {
                yield return StartCoroutine(MoveCube(cube.cube, redPos));
                redPos.z += 2;
            }
            else if(cube.color == 1)
            {
                yield return StartCoroutine(MoveCube(cube.cube, greenPos));
                greenPos.z += 2;
            }
            else
            {
                yield return StartCoroutine(MoveCube(cube.cube, bluePos));
                bluePos.z += 2;
            }

            yield return new WaitForSeconds(0.5f);
        }

        unsortedCubes.Clear();
    }

    IEnumerator MoveCube(Transform cubeTransform, Vector3 destination)
    {
        Vector3 startPosition = cubeTransform.position;
        float startTime = Time.time;
        while (Time.time - startTime < moveDuration)
        {
            float timePercentage = (Time.time - startTime) / moveDuration;
            cubeTransform.position = Vector3.Lerp(startPosition, destination, timePercentage);
            yield return null;
        }
        cubeTransform.position = destination;
    }

    IEnumerator OrderSorted()
    {
        int red = 0;
        int blue = sortedCubes.Count - 1;
        int index = 0;

        while(index <= blue)
        {
            if(sortedCubes[index].color == 0)
            {
                Swap(sortedCubes[index].cube, sortedCubes[red].cube);
                var temp = sortedCubes[index];
                sortedCubes[index] = sortedCubes[red];
                sortedCubes[red] = temp;
                index++;
                red++;
            }
            else if(sortedCubes[index].color == 2)
            {
                Swap(sortedCubes[index].cube, sortedCubes[blue].cube);
                var temp = sortedCubes[index];
                sortedCubes[index] = sortedCubes[blue];
                sortedCubes[blue] = temp;
                blue--;
            }
            else
            {
                index++;
            }

            yield return new WaitForSeconds(0.5f);
        }

        sortedCubes.Clear();
    }

    void Swap(Transform cubeT1, Transform cubeT2)
    {
        StartCoroutine(MoveCube(cubeT1, cubeT2.position));
        StartCoroutine(MoveCube(cubeT2, cubeT1.position));
    }
}
